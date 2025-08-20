using System;
using AmxModx.Bridge.CommandSystem;
using AmxModx.Bridge.TestModule.Tests;

namespace AmxModx.Bridge.TestModule
{
    /// <summary>
    /// 测试插件主类
    /// </summary>
    public class TestPlugin
    {
        private static bool _isInitialized = false;

        /// <summary>
        /// 插件加载时调用
        /// </summary>
        public static void OnPluginLoad()
        {
            Console.WriteLine("[TestPlugin] 测试插件加载成功！");
            
            try
            {
                // 注册测试命令
                RegisterTestCommands();
                
                // 初始化测试环境
                BridgeInterfaceTests.InitializeTests();
                
                _isInitialized = true;
                Console.WriteLine("[TestPlugin] 测试环境初始化完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestPlugin] 初始化失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 插件卸载时调用
        /// </summary>
        public static void OnPluginUnload()
        {
            Console.WriteLine("[TestPlugin] 测试插件卸载");
            _isInitialized = false;
        }

        /// <summary>
        /// 注册测试命令
        /// </summary>
        private static void RegisterTestCommands()
        {
            try
            {
                // 注册测试命令
                CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                    9999, 
                    1001, 
                    "bridge_test", 
                    "测试桥接接口功能", 
                    0, 
                    true);

                CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                    9999, 
                    1002, 
                    "bridge_test_all", 
                    "运行所有桥接接口测试", 
                    0, 
                    true);

                CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                    9999, 
                    1003, 
                    "bridge_test_system", 
                    "测试系统接口", 
                    0, 
                    true);

                CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                    9999, 
                    1004, 
                    "bridge_test_data", 
                    "测试数据接口", 
                    0, 
                    true);

                CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                    9999, 
                    1005, 
                    "bridge_test_engine", 
                    "测试引擎接口", 
                    0, 
                    true);

                CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                    9999, 
                    1006, 
                    "bridge_test_game", 
                    "测试游戏接口", 
                    0, 
                    true);

                Console.WriteLine("[TestPlugin] 测试命令注册完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestPlugin] 注册测试命令失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理测试命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <param name="args">命令参数</param>
        public static void HandleTestCommand(string command, string[] args)
        {
            if (!_isInitialized)
            {
                Console.WriteLine("[TestPlugin] 测试环境未初始化");
                return;
            }

            try
            {
                switch (command.ToLower())
                {
                    case "bridge_test":
                        Console.WriteLine("[TestPlugin] 运行基础桥接测试...");
                        BridgeInterfaceTests.TestCoreSystemInterfaces();
                        break;

                    case "bridge_test_all":
                        Console.WriteLine("[TestPlugin] 运行所有桥接接口测试...");
                        BridgeInterfaceTests.RunAllTests();
                        break;

                    case "bridge_test_system":
                        Console.WriteLine("[TestPlugin] 运行系统接口测试...");
                        BridgeInterfaceTests.TestCoreSystemInterfaces();
                        BridgeInterfaceTests.TestCommandSystemInterfaces();
                        break;

                    case "bridge_test_data":
                        Console.WriteLine("[TestPlugin] 运行数据接口测试...");
                        BridgeInterfaceTests.TestCVarInterfaces();
                        BridgeInterfaceTests.TestFileSystemInterfaces();
                        break;

                    case "bridge_test_engine":
                        Console.WriteLine("[TestPlugin] 运行引擎接口测试...");
                        BridgeInterfaceTests.TestEngineInterfaces();
                        break;

                    case "bridge_test_game":
                        Console.WriteLine("[TestPlugin] 运行游戏接口测试...");
                        BridgeInterfaceTests.TestGameSpecificInterfaces();
                        BridgeInterfaceTests.TestFakeMetaInterfaces();
                        BridgeInterfaceTests.TestHamSandwichInterfaces();
                        break;

                    case "bridge_analyze":
                        Console.WriteLine("[TestPlugin] 对比分析测试结果...");
                        ComparisonAnalyzer.AnalyzeResults();
                        break;

                    case "bridge_compare":
                        Console.WriteLine("[TestPlugin] 显示对比结果...");
                        ComparisonAnalyzer.PrintResults();
                        break;

                    // Forward验证器命令
                case "forward_test":
                    Console.WriteLine("[TestPlugin] 开始Forward验证测试...");
                    ForwardValidator.RunForwardValidation();
                    break;
                    
                case "forward_system":
                    Console.WriteLine("[TestPlugin] 执行Forward系统接口测试...");
                    ForwardValidator.TriggerTestByClientCommand(0, "forward_system");
                    break;
                    
                case "forward_data":
                    Console.WriteLine("[TestPlugin] 执行Forward数据接口测试...");
                    ForwardValidator.TriggerTestByClientCommand(0, "forward_data");
                    break;
                    
                case "forward_engine":
                    Console.WriteLine("[TestPlugin] 执行Forward引擎接口测试...");
                    ForwardValidator.TriggerTestByClientCommand(0, "forward_engine");
                    break;
                    
                case "forward_game":
                    Console.WriteLine("[TestPlugin] 执行Forward游戏接口测试...");
                    ForwardValidator.TriggerTestByClientCommand(0, "forward_game");
                    break;
                    
                // 同步验证器命令
                case "sync_test":
                    Console.WriteLine("[TestPlugin] 开始同步验证测试...");
                    SyncForwardValidator.RunSyncValidation();
                    break;
                    
                case "sync_system":
                    Console.WriteLine("[TestPlugin] 执行同步系统接口测试...");
                    SyncForwardValidator.HandleClientCommand(0, "sync_system");
                    break;
                    
                case "sync_data":
                    Console.WriteLine("[TestPlugin] 执行同步数据接口测试...");
                    SyncForwardValidator.HandleClientCommand(0, "sync_data");
                    break;
                    
                case "sync_engine":
                    Console.WriteLine("[TestPlugin] 执行同步引擎接口测试...");
                    SyncForwardValidator.HandleClientCommand(0, "sync_engine");
                    break;
                    
                case "sync_game":
                    Console.WriteLine("[TestPlugin] 执行同步游戏接口测试...");
                    SyncForwardValidator.HandleClientCommand(0, "sync_game");
                    break;
                    
                case "sync_report":
                    Console.WriteLine("[TestPlugin] 生成同步验证报告...");
                    SyncForwardValidator.HandleClientCommand(0, "sync_report");
                    break;

                    default:
                        Console.WriteLine($"[TestPlugin] 未知测试命令: {command}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestPlugin] 执行测试命令失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取测试状态
        /// </summary>
        public static string GetTestStatus()
        {
            return _isInitialized ? "测试环境已初始化" : "测试环境未初始化";
        }
    }
}