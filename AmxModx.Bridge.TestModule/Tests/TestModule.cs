using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Core;
using AmxModx.Bridge.CVar;
using AmxModx.Bridge.File;
using AmxModx.Bridge.CommandSystem;
using AmxModx.Bridge.Engine;
using AmxModx.Bridge.Messages;
using AmxModx.Bridge.Event;
using AmxModx.Bridge.Fakemeta;
using AmxModx.Bridge.HamSandwich;

namespace AmxModx.Bridge.TestModule.Tests
{
    /// <summary>
    /// 测试模块主类
    /// 负责协调所有测试组件的初始化和执行
    /// </summary>
    public static class TestModule
    {
        private static bool _initialized = false;
        private static List<string> _testResults = new List<string>();
        private static string _mainLogFile = "addons/amxmodx/logs/test_module_main.txt";

        /// <summary>
        /// 初始化测试模块
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            Console.WriteLine("[TestModule] 初始化测试模块...");
            
            try
            {
                // 初始化日志系统
                InitializeLogging();
                
                // 初始化各个测试组件
                BridgeInterfaceTests.InitializeTests();
                ForwardValidator.Initialize();
                SyncForwardValidator.Initialize();
                
                _initialized = true;
                LogMessage("测试模块初始化完成");
                Console.WriteLine("[TestModule] 测试模块初始化完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestModule] 初始化失败: {ex.Message}");
                LogMessage($"初始化失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 运行所有测试
        /// </summary>
        public static void RunAllTests()
        {
            if (!_initialized)
            {
                Console.WriteLine("[TestModule] 错误：测试模块未初始化");
                return;
            }

            Console.WriteLine("[TestModule] 开始运行所有测试...");
            LogMessage("开始运行所有测试");

            try
            {
                // 运行桥接接口测试
                BridgeInterfaceTests.RunAllTests();
                LogMessage("桥接接口测试完成");

                // 运行Forward验证测试
                ForwardValidator.RunForwardValidation();
                LogMessage("Forward验证测试完成");

                // 运行同步验证测试
                SyncForwardValidator.RunSyncValidation();
                LogMessage("同步验证测试完成");

                Console.WriteLine("[TestModule] 所有测试运行完成");
                LogMessage("所有测试运行完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestModule] 测试运行失败: {ex.Message}");
                LogMessage($"测试运行失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取测试结果摘要
        /// </summary>
        public static string GetTestSummary()
        {
            return string.Join("\n", _testResults);
        }

        /// <summary>
        /// 初始化日志系统
        /// </summary>
        private static void InitializeLogging()
        {
            try
            {
                string logDir = System.IO.Path.Combine(
                    System.IO.Directory.GetCurrentDirectory(), 
                    "addons", "amxmodx", "logs");
                
                if (!System.IO.Directory.Exists(logDir))
                    System.IO.Directory.CreateDirectory(logDir);

                string logPath = System.IO.Path.Combine(
                    System.IO.Directory.GetCurrentDirectory(), 
                    _mainLogFile);

                System.IO.File.WriteAllText(logPath, 
                    $"测试模块日志 - {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                    new string('=', 50) + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestModule] 日志初始化失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 记录消息到日志
        /// </summary>
        private static void LogMessage(string message)
        {
            try
            {
                string logPath = System.IO.Path.Combine(
                    System.IO.Directory.GetCurrentDirectory(), 
                    _mainLogFile);

                string logLine = $"[{DateTime.Now:HH:mm:ss.fff}] {message}";
                System.IO.File.AppendAllText(logPath, logLine + "\n");
                _testResults.Add(logLine);
            }
            catch
            {
                // 忽略日志记录错误
            }
        }
    }
}