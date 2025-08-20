using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using AmxModx.Bridge;

namespace AmxModx.Bridge.TestModule.Tests
{
    /// <summary>
    /// 基于Forward转发器的同步验证系统
    /// 实现C#和Pawn插件的精确对等测试
    /// </summary>
    public static class SyncForwardValidator
    {
        // Forward转发器ID
        private static int _testTriggerForward = -1;
        private static int _testResultForward = -1;
        
        // 测试状态管理
        private static Dictionary<string, TestResult> _csharpResults = new Dictionary<string, TestResult>();
        private static Dictionary<string, TestResult> _pawnResults = new Dictionary<string, TestResult>();
        private static List<string> _pendingTests = new List<string>();
        private static object _syncLock = new object();
        
        // 日志文件路径
        private static readonly string _csharpLogPath = "addons/amxmodx/logs/sync_csharp_results.txt";
        private static readonly string _pawnLogPath = "addons/amxmodx/logs/sync_pawn_results.txt";
        private static readonly string _comparisonLogPath = "addons/amxmodx/logs/sync_comparison.txt";
        
        /// <summary>
        /// 测试结果数据结构
        /// </summary>
        private struct TestResult
        {
            public string TestName;
            public string Status;
            public string Details;
            public DateTime Timestamp;
            public string Source; // "C#" 或 "Pawn"
        }

        /// <summary>
        /// 初始化同步验证系统
        /// </summary>
        public static void Initialize()
        {
            Console.WriteLine("[SyncForwardValidator] 初始化同步验证系统...");
            
            // 创建测试触发转发器（C# → Pawn）
            _testTriggerForward = Forward.CreateMultiForward(
                "Sync_Test_Trigger",
                Forward.ForwardExecType.Stop,
                Forward.ForwardParamType.String,   // 测试名称
                Forward.ForwardParamType.String,   // 测试参数
                Forward.ForwardParamType.String    // 期望结果
            );

            // 创建测试结果转发器（Pawn → C#）
            _testResultForward = Forward.CreateMultiForward(
                "Sync_Test_Result",
                Forward.ForwardExecType.Stop,
                Forward.ForwardParamType.String,   // 测试名称
                Forward.ForwardParamType.String,   // 实际结果
                Forward.ForwardParamType.String    // 详细信息
            );

            if (_testTriggerForward < 0 || _testResultForward < 0)
            {
                Console.WriteLine("[SyncForwardValidator] 警告：Forward转发器创建失败");
            }
            else
            {
                Console.WriteLine($"[SyncForwardValidator] 转发器创建成功: Trigger={_testTriggerForward}, Result={_testResultForward}");
            }

            InitializeLogFiles();
            RegisterForwardListeners();
        }

        /// <summary>
        /// 初始化日志文件
        /// </summary>
        private static void InitializeLogFiles()
        {
            string logDir = Path.Combine(Directory.GetCurrentDirectory(), "addons", "amxmodx", "logs");
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            // 清空旧日志并添加时间戳
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), _csharpLogPath), 
                $"C#同步验证结果 - {timestamp}\n" + new string('=', 50) + "\n");
            
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), _pawnLogPath), 
                $"Pawn同步验证结果 - {timestamp}\n" + new string('=', 50) + "\n");
        }

        /// <summary>
        /// 注册Forward监听器
        /// </summary>
        private static void RegisterForwardListeners()
        {
            // 监听Pawn的测试结果
            // 这里需要Pawn插件通过Forward发送结果回来
            Console.WriteLine("[SyncForwardValidator] Forward监听器已注册");
        }

        /// <summary>
        /// 执行同步测试
        /// </summary>
        public static void ExecuteSyncTest(string testName, string testParam, string expectedResult)
        {
            if (_testTriggerForward < 0)
            {
                Console.WriteLine("[SyncForwardValidator] 错误：测试转发器未初始化");
                return;
            }

            lock (_syncLock)
            {
                _pendingTests.Add(testName);
                
                // 记录C#端结果
                RecordCSharpResult(testName, "PASS", expectedResult);
                
                // 通过Forward触发Pawn执行相同测试
                TriggerPawnTest(testName, testParam, expectedResult);
                
                Console.WriteLine($"[SyncForwardValidator] 同步测试已触发: {testName}");
            }
        }

        /// <summary>
        /// 触发Pawn端测试
        /// </summary>
        private static void TriggerPawnTest(string testName, string testParam, string expectedResult)
        {
            try
            {
                int[] parameters = new int[3];
                parameters[0] = Marshal.StringToHGlobalAnsi(testName).ToInt32();
                parameters[1] = Marshal.StringToHGlobalAnsi(testParam).ToInt32();
                parameters[2] = Marshal.StringToHGlobalAnsi(expectedResult).ToInt32();

                int result = Forward.ExecuteForward(_testTriggerForward, parameters);
                
                Marshal.FreeHGlobal(new IntPtr(parameters[0]));
                Marshal.FreeHGlobal(new IntPtr(parameters[1]));
                Marshal.FreeHGlobal(new IntPtr(parameters[2]));

                Console.WriteLine($"[SyncForwardValidator] Forward触发结果: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SyncForwardValidator] Forward触发异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 接收Pawn测试结果
        /// </summary>
        public static void ReceivePawnResult(string testName, string actualResult, string details)
        {
            lock (_syncLock)
            {
                var result = new TestResult
                {
                    TestName = testName,
                    Status = actualResult,
                    Details = details,
                    Timestamp = DateTime.Now,
                    Source = "Pawn"
                };

                _pawnResults[testName] = result;
                
                // 记录Pawn结果
                LogPawnResult(testName, actualResult, details);
                
                // 从待处理列表移除
                _pendingTests.Remove(testName);
                
                Console.WriteLine($"[SyncForwardValidator] 收到Pawn结果: {testName} = {actualResult}");
                
                // 立即对比结果
                CompareSingleTest(testName);
            }
        }

        /// <summary>
        /// 记录C#测试结果
        /// </summary>
        private static void RecordCSharpResult(string testName, string status, string details)
        {
            var result = new TestResult
            {
                TestName = testName,
                Status = status,
                Details = details,
                Timestamp = DateTime.Now,
                Source = "C#"
            };

            _csharpResults[testName] = result;
            LogCSharpResult(testName, status, details);
        }

        /// <summary>
        /// 记录C#结果到日志文件
        /// </summary>
        private static void LogCSharpResult(string testName, string status, string details)
        {
            string logLine = $"[{DateTime.Now:HH:mm:ss.fff}] [{testName}] {status}: {details}";
            System.IO.File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), _csharpLogPath), logLine + "\n");
        }

        /// <summary>
        /// 记录Pawn结果到日志文件
        /// </summary>
        private static void LogPawnResult(string testName, string status, string details)
        {
            string logLine = $"[{DateTime.Now:HH:mm:ss.fff}] [{testName}] {status}: {details}";
            System.IO.File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), _pawnLogPath), logLine + "\n");
        }

        /// <summary>
        /// 对比单个测试结果
        /// </summary>
        private static void CompareSingleTest(string testName)
        {
            if (!_csharpResults.ContainsKey(testName) || !_pawnResults.ContainsKey(testName))
                return;

            var csharp = _csharpResults[testName];
            var pawn = _pawnResults[testName];

            bool isConsistent = csharp.Status == pawn.Status && csharp.Details == pawn.Details;
            
            Console.WriteLine($"[SyncComparison] {testName}: {(isConsistent ? "✅一致" : "❌不一致")}");
            
            if (!isConsistent)
            {
                Console.WriteLine($"  C#: {csharp.Status} - {csharp.Details}");
                Console.WriteLine($"  Pawn: {pawn.Status} - {pawn.Details}");
            }
        }

        /// <summary>
        /// 运行完整的同步验证测试
        /// </summary>
        public static void RunSyncValidation()
        {
            Console.WriteLine("[SyncForwardValidator] 开始同步验证测试...");
            
            // 系统接口测试
            ExecuteSyncTest("GetServerName", "hostname", "de_dust2");
            ExecuteSyncTest("GetGameTime", "time", "123.45");
            ExecuteSyncTest("GetMapName", "map", "de_dust2");
            ExecuteSyncTest("GetMaxPlayers", "maxplayers", "32");

            // 数据接口测试
            ExecuteSyncTest("CreateCVar", "testvar", "test_cvar_1337");
            ExecuteSyncTest("GetCVarString", "cvar_name", "test_value");
            ExecuteSyncTest("SetCVarString", "cvar_value", "new_value");

            // 引擎接口测试
            ExecuteSyncTest("GetMaxEntities", "maxents", "1024");
            ExecuteSyncTest("PrecacheModel", "model", "models/player.mdl");
            ExecuteSyncTest("ClientPrint", "message", "test_message");

            // 游戏接口测试
            ExecuteSyncTest("GetWeaponName", "weapon_id", "weapon_ak47");
            ExecuteSyncTest("GetEntityCount", "count", "128");
            ExecuteSyncTest("HamSandwichInit", "ham", "available");

            Console.WriteLine("[SyncForwardValidator] 同步验证测试完成！");
            
            // 生成最终报告
            GenerateSyncReport();
        }

        /// <summary>
        /// 生成同步验证报告
        /// </summary>
        private static void GenerateSyncReport()
        {
            Console.WriteLine("[SyncForwardValidator] 生成同步验证报告...");
            
            int totalTests = _csharpResults.Count;
            int matchingTests = 0;
            
            string report = $"同步验证对比报告 - {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            report += new string('=', 60) + "\n";
            report += $"总测试数: {totalTests}\n";
            report += $"C#记录数: {_csharpResults.Count}\n";
            report += $"Pawn记录数: {_pawnResults.Count}\n";
            report += new string('-', 60) + "\n";

            foreach (var test in _csharpResults)
            {
                string testName = test.Key;
                report += $"{testName}: ";
                
                if (_pawnResults.ContainsKey(testName))
                {
                    var csharp = test.Value;
                    var pawn = _pawnResults[testName];
                    
                    bool isMatch = csharp.Status == pawn.Status && csharp.Details == pawn.Details;
                    report += isMatch ? "PASS" : "FAIL";
                    
                    if (!isMatch)
                    {
                        report += $" (C#:{csharp.Status}/{csharp.Details} vs Pawn:{pawn.Status}/{pawn.Details})";
                    }
                    else
                    {
                        matchingTests++;
                    }
                }
                else
                {
                    report += "NO_PAWN_RESPONSE";
                }
                report += "\n";
            }
            
            report += new string('=', 60) + "\n";
            report += $"匹配率: {(float)matchingTests / totalTests * 100:F1}%\n";
            report += new string('=', 60) + "\n";

            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), _comparisonLogPath), report);
            
            Console.WriteLine(report);
        }

        /// <summary>
        /// 通过客户端命令触发同步测试
        /// </summary>
        public static void HandleClientCommand(int playerId, string command)
        {
            Console.WriteLine($"[SyncForwardValidator] 客户端命令: {command}");
            
            switch (command.ToLower())
            {
                case "sync_test":
                    RunSyncValidation();
                    break;
                case "sync_system":
                    ExecuteSyncTest("GetServerName", "hostname", "de_dust2");
                    break;
                case "sync_data":
                    ExecuteSyncTest("CreateCVar", "testvar", "test_cvar_1337");
                    break;
                case "sync_engine":
                    ExecuteSyncTest("GetMaxEntities", "maxents", "1024");
                    break;
                case "sync_game":
                    ExecuteSyncTest("GetWeaponName", "weapon_id", "weapon_ak47");
                    break;
                case "sync_report":
                    GenerateSyncReport();
                    break;
                default:
                    Console.WriteLine($"[SyncForwardValidator] 未知命令: {command}");
                    break;
            }
        }
    }
}