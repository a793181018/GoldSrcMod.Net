using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Core;

namespace AmxModx.Bridge.Examples
{
    /// <summary>
    /// 核心桥接功能测试类
    /// </summary>
    public class CoreBridgeTest
    {
        /// <summary>
        /// 运行所有核心桥接测试
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("=== AMX Mod X 核心桥接功能测试 ===");
            
            // 测试字节交换功能
            TestSwapChars();
            
            // 测试函数索引查找
            TestFunctionIndexLookup();
            
            // 测试属性系统
            TestPropertySystem();
            
            Console.WriteLine("=== 测试完成 ===");
        }
        
        /// <summary>
        /// 测试字节交换功能
        /// </summary>
        private static void TestSwapChars()
        {
            Console.WriteLine("\n--- 测试字节交换功能 ---");
            
            int originalValue = 0x12345678;
            int swappedValue = CoreBridge.SwapChars(originalValue);
            
            Console.WriteLine($"原始值: 0x{originalValue:X8}");
            Console.WriteLine($"交换后: 0x{swappedValue:X8}");
            
            // 验证交换是否正确
            int expectedSwapped = 0x78563412;
            Console.WriteLine($"期望值: 0x{expectedSwapped:X8}");
            Console.WriteLine($"测试结果: {(swappedValue == expectedSwapped ? "通过" : "失败")}");
        }
        
        /// <summary>
        /// 测试函数索引查找
        /// </summary>
        private static void TestFunctionIndexLookup()
        {
            Console.WriteLine("\n--- 测试函数索引查找 ---");
            
            // 注意：这些测试需要在实际的AMX环境中运行
            // 这里只是演示API调用
            try
            {
                int index = CoreBridge.GetFunctionIndex("test_function");
                Console.WriteLine($"函数 'test_function' 索引: {index}");
                
                index = CoreBridge.GetFunctionIndex("nonexistent_function");
                Console.WriteLine($"函数 'nonexistent_function' 索引: {index}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"函数查找测试: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 测试属性系统
        /// </summary>
        private static void TestPropertySystem()
        {
            Console.WriteLine("\n--- 测试属性系统 ---");
            
            try
            {
                // 测试属性存在性检查
                bool exists = CoreBridge.PropertyExists(0, "test_property", 0);
                Console.WriteLine($"属性 'test_property' 存在: {exists}");
                
                // 测试获取属性（需要实际AMX环境）
                string propertyName = "test_property";
                System.Text.StringBuilder buffer = new System.Text.StringBuilder(256);
                int result = CoreBridge.GetProperty(0, propertyName, 0, buffer, buffer.Capacity);
                Console.WriteLine($"获取属性结果: {result}");
                
                if (result > 0)
                {
                    Console.WriteLine($"属性值: {buffer}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"属性系统测试: {ex.Message}");
            }
        }
    }
}