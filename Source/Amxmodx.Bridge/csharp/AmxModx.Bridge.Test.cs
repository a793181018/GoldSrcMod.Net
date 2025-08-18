// vim: set ts=4 sw=4 tw=99 noet:
using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Natives;

namespace AmxModx.Bridge.Test
{
    /// <summary>
    /// 测试模块，演示如何使用原生函数桥接
    /// </summary>
    public static class TestModule
    {
        /// <summary>
        /// 示例原生函数
        /// </summary>
        private static int TestNative(IntPtr amx, IntPtr params)
        {
            // 获取参数数量
            int paramCount = Marshal.ReadInt32(params) / 4;
            
            if (paramCount < 1)
            {
                NativeBridge.AmxModx_Bridge_LogError(amx, 1, "TestNative: 需要至少1个参数");
                return 0;
            }

            // 获取第一个参数
            int param1 = NativeBridge.AmxModx_Bridge_GetParam(amx, 1);
            
            // 记录日志
            Console.WriteLine($"TestNative called with param: {param1}");
            
            return param1 * 2; // 返回参数的两倍
        }

        /// <summary>
        /// 字符串处理原生函数
        /// </summary>
        private static int TestStringNative(IntPtr amx, IntPtr params)
        {
            try
            {
                // 获取字符串参数
                string str = NativeBridge.GetStringSafe(amx, 1);
                
                // 处理字符串
                string result = str.ToUpper();
                
                // 设置返回字符串
                NativeBridge.AmxModx_Bridge_SetString(amx, 2, result, result.Length);
                
                return 1; // 成功
            }
            catch (Exception ex)
            {
                NativeBridge.AmxModx_Bridge_LogError(amx, 2, $"TestStringNative error: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 数组处理原生函数
        /// </summary>
        private static int TestArrayNative(IntPtr amx, IntPtr params)
        {
            try
            {
                // 获取数组参数
                int[] array = new int[5];
                NativeBridge.AmxModx_Bridge_GetArray(amx, 1, array, array.Length);
                
                // 处理数组
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] *= 2;
                }
                
                // 设置返回数组
                NativeBridge.AmxModx_Bridge_SetArray(amx, 2, array, array.Length);
                
                return 1; // 成功
            }
            catch (Exception ex)
            {
                NativeBridge.AmxModx_Bridge_LogError(amx, 2, $"TestArrayNative error: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 注册测试原生函数
        /// </summary>
        public static void RegisterTestNatives(IntPtr amx)
        {
            // 注册测试函数
            NativeBridge.AmxModx_Bridge_RegisterNative(amx, "test_native", TestNative);
            NativeBridge.AmxModx_Bridge_RegisterNative(amx, "test_string", TestStringNative);
            NativeBridge.AmxModx_Bridge_RegisterNative(amx, "test_array", TestArrayNative);
            
            // 注册测试库
            NativeBridge.AmxModx_Bridge_RegisterLibrary(amx, "test_module");
            
            Console.WriteLine("测试模块已注册");
        }

        /// <summary>
        /// 注销测试原生函数
        /// </summary>
        public static void UnregisterTestNatives(IntPtr amx)
        {
            // 注销测试函数
            NativeBridge.AmxModx_Bridge_UnregisterNative(amx, "test_native");
            NativeBridge.AmxModx_Bridge_UnregisterNative(amx, "test_string");
            NativeBridge.AmxModx_Bridge_UnregisterNative(amx, "test_array");
            
            Console.WriteLine("测试模块已注销");
        }
    }
}