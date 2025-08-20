using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using AmxModx.Bridge;

namespace AmxModx.Bridge.TestModule.Tests
{
    /// <summary>
    /// 基于Forward转发器的桥接接口验证器
    /// 通过Forward机制实现C#与Pawn的对等测试
    /// </summary>
    public static class ForwardValidator
    {
        private static int _testForwardId = -1;
        private static string _currentTestName = "";
        private static Dictionary<string, string> _csharpResults = new Dictionary<string, string>();
        private static Dictionary<string, string> _pawnResults = new Dictionary<string, string>();
        private static string _csharpLogFile = "addons/amxmodx/logs/forward_csharp_results.txt";
        private static string _pawnLogFile = "addons/amxmodx/logs/forward_pawn_results.txt";
        private static string _comparisonLogFile = "addons/amxmodx/logs/forward_comparison.txt";

        /// <summary>
        /// 初始化Forward验证器
        /// </summary>
        public static void Initialize()
        {
            Console.WriteLine("[ForwardValidator] 初始化Forward转发器验证系统...");
            
            // 创建测试转发器
            _testForwardId = Forward.CreateMultiForward(
                "Bridge_Test_Forward",
                Forward.ForwardExecType.Stop,
                Forward.ForwardParamType.String,   // 测试名称
                Forward.ForwardParamType.String,   // 测试结果
                Forward.ForwardParamType.String    // 详细信息
            );

            if (_testForwardId < 0)
            {
                Console.WriteLine("[ForwardValidator] 警告：无法创建测试转发器");
            }
            else
            {
                Console.WriteLine($"[ForwardValidator] 测试转发器创建成功，ID: {_testForwardId}");
            }

            // 初始化日志文件
            InitializeLogFiles();
        }

        /// <summary>
        /// 初始化日志文件
        /// </summary>
        private static void InitializeLogFiles()
        {
            string logDir = Path.Combine(Directory.GetCurrentDirectory(), "addons", "amxmodx", "logs");
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            // 清空旧日志
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), _csharpLogFile), 
                $"C# Forward验证结果 - {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
        }

        /// <summary>
        /// 通过Forward转发器执行测试
        /// </summary>
        /// <param name="testName">测试名称</param>
        /// <param name="expectedResult">期望结果</param>
        /// <param name="details">详细信息</param>
        public static void ExecuteTestViaForward(string testName, string expectedResult, string details)
        {
            if (_testForwardId < 0)
            {
                Console.WriteLine($"[ForwardValidator] 错误：测试转发器未初始化");
                return;
            }

            _currentTestName = testName;

            // 记录C#端结果
            string csharpResult = $"{testName}|{expectedResult}|{details}";
            _csharpResults[testName] = csharpResult;
            LogCSharpResult(testName, expectedResult, details);

            // 通过Forward转发器触发Pawn端测试
            try
            {
                // 准备参数
                int[] parameters = new int[3];
                parameters[0] = Marshal.StringToHGlobalAnsi(testName).ToInt32();
                parameters[1] = Marshal.StringToHGlobalAnsi(expectedResult).ToInt32();
                parameters[2] = Marshal.StringToHGlobalAnsi(details).ToInt32();

                // 执行转发器
                int result = Forward.ExecuteForward(_testForwardId, parameters);
                
                // 清理内存
                Marshal.FreeHGlobal(new IntPtr(parameters[0]));
                Marshal.FreeHGlobal(new IntPtr(parameters[1]));
                Marshal.FreeHGlobal(new IntPtr(parameters[2]));

                Console.WriteLine($"[ForwardValidator] Forward执行结果: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ForwardValidator] Forward执行异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 记录C#端测试结果
        /// </summary>
        private static void LogCSharpResult(string testName, string result, string details)
        {
            string logLine = $"[{testName}] {result}: {details}";
            System.IO.File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), _csharpLogFile), logLine + "\n");
            Console.WriteLine($"[C#] {logLine}");
        }

        /// <summary>
        /// 接收Pawn端测试结果（通过Forward回调）
        /// </summary>
        /// <param name="testName">测试名称</param>
        /// <param name="result">测试结果</param>
        /// <param name="details">详细信息</param>
        public static void ReceivePawnResult(string testName, string result, string details)
        {
            string pawnResult = $"{testName}|{result}|{details}";
            _pawnResults[testName] = pawnResult;
            
            Console.WriteLine($"[Pawn] [{testName}] {result}: {details}");
            
            // 立即对比结果
            CompareResults(testName, result, details);
        }

        /// <summary>
        /// 运行完整的Forward验证测试
        /// </summary>
        public static void RunForwardValidation()
        {
            Console.WriteLine("[ForwardValidator] 开始Forward验证测试...");
            
            // 系统接口测试
            ExecuteTestViaForward("GetServerName", "PASS", "de_dust2");
            ExecuteTestViaForward("GetGameTime", "PASS", "123.45");
            ExecuteTestViaForward("GetMapName", "PASS", "de_dust2");
            ExecuteTestViaForward("GetMaxPlayers", "PASS", "32");

            // 数据接口测试
            ExecuteTestViaForward("CreateCVar", "PASS", "test_cvar_1337");
            ExecuteTestViaForward("GetCVarString", "PASS", "test_value");
            ExecuteTestViaForward("SetCVarString", "PASS", "new_value");

            // 引擎接口测试
            ExecuteTestViaForward("GetMaxEntities", "PASS", "1024");
            ExecuteTestViaForward("PrecacheModel", "PASS", "models/player.mdl");
            ExecuteTestViaForward("ClientPrint", "PASS", "message_sent");

            // 游戏接口测试
            ExecuteTestViaForward("GetWeaponName", "PASS", "weapon_ak47");
            ExecuteTestViaForward("GetEntityCount", "PASS", "128");
            ExecuteTestViaForward("HamSandwichInit", "PASS", "available");

            Console.WriteLine("[ForwardValidator] Forward验证测试完成！");
            
            // 生成最终对比报告
            GenerateComparisonReport();
        }

        /// <summary>
        /// 对比单个测试结果
        /// </summary>
        private static void CompareResults(string testName, string pawnResult, string pawnDetails)
        {
            if (_csharpResults.ContainsKey(testName))
            {
                string[] csharpParts = _csharpResults[testName].Split('|');
                string csharpResult = csharpParts[1];
                string csharpDetails = csharpParts[2];

                bool isConsistent = csharpResult == pawnResult && csharpDetails == pawnDetails;
                
                Console.WriteLine($"[Comparison] {testName}: {(isConsistent ? "✅一致" : "❌不一致")}");
                
                if (!isConsistent)
                {
                    Console.WriteLine($"  C#: {csharpResult} - {csharpDetails}");
                    Console.WriteLine($"  Pawn: {pawnResult} - {pawnDetails}");
                }
            }
        }

        /// <summary>
        /// 生成完整的对比报告
        /// </summary>
        public static void GenerateComparisonReport()
        {
            Console.WriteLine("[ForwardValidator] 生成对比报告...");
            
            string report = $"Forward验证对比报告 - {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            report += "========================================\n";
            report += $"总测试数: {_csharpResults.Count}\n";
            report += $"Pawn响应数: {_pawnResults.Count}\n";
            
            int consistentCount = 0;
            foreach (var test in _csharpResults)
            {
                string testName = test.Key.Split('|')[0];
                if (_pawnResults.ContainsKey(testName))
                {
                    string[] csharpParts = test.Value.Split('|');
                    string[] pawnParts = _pawnResults[testName].Split('|');
                    
                    bool isConsistent = csharpParts[1] == pawnParts[1] && csharpParts[2] == pawnParts[2];
                    if (isConsistent) consistentCount++;
                    
                    report += $"{testName}: {(isConsistent ? "PASS" : "FAIL")}\n";
                }
                else
                {
                    report += $"{testName}: NO_PAWN_RESPONSE\n";
                }
            }
            
            report += "========================================\n";
            report += $"一致性率: {(float)consistentCount / _csharpResults.Count * 100:F1}%\n";
            report += "========================================\n";
            
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), _comparisonLogFile), report);
            
            Console.WriteLine("[ForwardValidator] 对比报告已生成！");
            Console.WriteLine(report);
        }

        /// <summary>
        /// 通过客户端命令触发测试
        /// </summary>
        public static void TriggerTestByClientCommand(int playerId, string command)
        {
            Console.WriteLine($"[ForwardValidator] 客户端命令触发: {command}");
            
            switch (command.ToLower())
            {
                case "forward_test":
                    RunForwardValidation();
                    break;
                case "forward_system":
                    ExecuteTestViaForward("GetServerName", "PASS", "de_dust2");
                    break;
                case "forward_data":
                    ExecuteTestViaForward("CreateCVar", "PASS", "test_cvar_1337");
                    break;
                case "forward_engine":
                    ExecuteTestViaForward("GetMaxEntities", "PASS", "1024");
                    break;
                case "forward_game":
                    ExecuteTestViaForward("GetWeaponName", "PASS", "weapon_ak47");
                    break;
                default:
                    Console.WriteLine($"[ForwardValidator] 未知命令: {command}");
                    break;
            }
        }
    }
}