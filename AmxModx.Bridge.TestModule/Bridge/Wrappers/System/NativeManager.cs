using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AmxModx.Bridge;
using AmxModx.Bridge.Natives;

namespace AmxModx.Wrappers.System
{
    /// <summary>
    /// 本地函数高级封装管理器
    /// 提供对AMX Mod X本地函数的高级封装
    /// </summary>
    public static class NativeManager
    {
        /// <summary>
        /// 初始化本地管理器
        /// </summary>
        public static void Initialize()
        {
            // 初始化本地函数系统
        }
        /// <summary>
        /// 将指针转换为字符串
        /// </summary>
        /// <param name="ptr">字符串指针</param>
        /// <returns>转换后的字符串</returns>
        public static string PtrToString(int ptr)
        {
            if (ptr == 0)
                return string.Empty;

            IntPtr nativePtr = new IntPtr(ptr);
            return Marshal.PtrToStringAnsi(nativePtr) ?? string.Empty;
        }

        /// <summary>
        /// 将字符串转换为指针
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串指针</returns>
        public static int StringToPtr(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            IntPtr ptr = Marshal.StringToHGlobalAnsi(str);
            return ptr.ToInt32();
        }

        /// <summary>
        /// 释放字符串指针
        /// </summary>
        /// <param name="ptr">字符串指针</param>
        public static void FreeStringPtr(int ptr)
        {
            if (ptr != 0)
            {
                IntPtr nativePtr = new IntPtr(ptr);
                Marshal.FreeHGlobal(nativePtr);
            }
        }

        /// <summary>
        /// 获取AMX本地函数地址
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="functionName">函数名称</param>
        /// <returns>函数指针</returns>
        public static IntPtr GetNativeFunction(IntPtr amx, string functionName)
        {
            if (string.IsNullOrEmpty(functionName) || amx == IntPtr.Zero)
                return IntPtr.Zero;

            return NativeBridge.AmxModx_Bridge_GetNativeFunction(amx, functionName);
        }

        /// <summary>
        /// 调用AMX本地函数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="functionName">函数名称</param>
        /// <param name="args">参数数组</param>
        /// <returns>函数返回值</returns>
        public static int CallNativeFunction(IntPtr amx, string functionName, params object[] args)
        {
            if (string.IsNullOrEmpty(functionName) || amx == IntPtr.Zero)
                return 0;

            IntPtr funcPtr = GetNativeFunction(amx, functionName);
            if (funcPtr == IntPtr.Zero)
                return 0;

            // 将object[]转换为int[]
            int[] intArgs = new int[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is int intValue)
                {
                    intArgs[i] = intValue;
                }
                else if (args[i] is float floatValue)
                {
                    // 将float转换为int的位表示
                    intArgs[i] = BitConverter.ToInt32(BitConverter.GetBytes(floatValue), 0);
                }
                else if (args[i] is bool boolValue)
                {
                    intArgs[i] = boolValue ? 1 : 0;
                }
                else
                {
                    intArgs[i] = 0; // 默认处理
                }
            }

            return NativeBridge.AmxModx_Bridge_CallNativeFunction(funcPtr, intArgs);
        }

        /// <summary>
        /// 检查本地函数是否存在
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="functionName">函数名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool NativeFunctionExists(IntPtr amx, string functionName)
        {
            if (string.IsNullOrEmpty(functionName) || amx == IntPtr.Zero)
                return false;

            return GetNativeFunction(amx, functionName) != IntPtr.Zero;
        }

        /// <summary>
        /// 获取本地函数列表
        /// </summary>
        /// <returns>函数名称数组</returns>
        public static string[] GetNativeFunctionList()
        {
            // 这里简化处理，实际应该从AMX Mod X获取完整列表
            return new string[]
            {
                "register_plugin",
                "register_event",
                "client_cmd",
                "client_print",
                "get_user_msgid",
                "engfunc",
                "dllfunc",
                "rg_get_entity_class",
                "rg_set_entity_class"
            };
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <param name="args">参数数组</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatString(string format, params object[] args)
        {
            if (string.IsNullOrEmpty(format))
                return string.Empty;

            try
            {
                return string.Format(format, args);
            }
            catch
            {
                return format;
            }
        }

        /// <summary>
        /// 安全地获取字符串
        /// </summary>
        /// <param name="ptr">字符串指针</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>安全字符串</returns>
        public static string GetStringSafe(int ptr, int maxLength = 1024)
        {
            if (ptr == 0)
                return string.Empty;

            try
            {
                IntPtr nativePtr = new IntPtr(ptr);
                string result = Marshal.PtrToStringAnsi(nativePtr);
                
                if (string.IsNullOrEmpty(result))
                    return string.Empty;

                return result.Length > maxLength ? result.Substring(0, maxLength) : result;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将数组转换为指针
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>数组指针</returns>
        public static IntPtr ArrayToPtr<T>(T[] array) where T : struct
        {
            if (array == null || array.Length == 0)
                return IntPtr.Zero;

            int size = Marshal.SizeOf(typeof(T)) * array.Length;
            IntPtr ptr = Marshal.AllocHGlobal(size);
            
            if (typeof(T) == typeof(byte))
            {
                Marshal.Copy(array as byte[] ?? Array.Empty<byte>(), 0, ptr, array.Length);
            }
            else if (typeof(T) == typeof(int))
            {
                Marshal.Copy(array as int[] ?? Array.Empty<int>(), 0, ptr, array.Length);
            }
            else if (typeof(T) == typeof(float))
            {
                Marshal.Copy(array as float[] ?? Array.Empty<float>(), 0, ptr, array.Length);
            }
            else
            {
                // 对于其他类型，使用通用方法
                unsafe
                {
                    byte* bytePtr = (byte*)ptr.ToPointer();
                    for (int i = 0; i < array.Length; i++)
                    {
                        Marshal.StructureToPtr(array[i], new IntPtr(bytePtr + i * Marshal.SizeOf(typeof(T))), false);
                    }
                }
            }
            
            return ptr;
        }

        /// <summary>
        /// 从指针转换数组
        /// </summary>
        /// <param name="ptr">指针</param>
        /// <param name="length">数组长度</param>
        /// <returns>数组</returns>
        public static T[] PtrToArray<T>(IntPtr ptr, int length) where T : struct
        {
            if (ptr == IntPtr.Zero || length <= 0)
                return new T[0];

            T[] array = new T[length];
            
            if (typeof(T) == typeof(byte))
            {
                byte[] byteArray = new byte[length];
                Marshal.Copy(ptr, byteArray, 0, length);
                array = (T[])(object)byteArray;
            }
            else if (typeof(T) == typeof(int))
            {
                int[] intArray = new int[length];
                Marshal.Copy(ptr, intArray, 0, length);
                array = (T[])(object)intArray;
            }
            else if (typeof(T) == typeof(float))
            {
                float[] floatArray = new float[length];
                Marshal.Copy(ptr, floatArray, 0, length);
                array = (T[])(object)floatArray;
            }
            else
            {
                // 对于其他类型，使用通用方法
                unsafe
                {
                    byte* bytePtr = (byte*)ptr.ToPointer();
                    for (int i = 0; i < length; i++)
                    {
                        object? structure = Marshal.PtrToStructure(new IntPtr(bytePtr + i * Marshal.SizeOf(typeof(T))), typeof(T));
                        if (structure != null)
                        {
                            array[i] = (T)structure;
                        }
                    }
                }
            }
            
            return array;
        }

        /// <summary>
        /// 释放数组指针
        /// </summary>
        /// <param name="ptr">数组指针</param>
        public static void FreeArrayPtr(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
                Marshal.FreeHGlobal(ptr);
        }
        /// <summary>
        /// 清理本地管理器资源
        /// </summary>
        public static void Cleanup()
        {
            // 本地函数管理无需要清理的特定资源
        }
    }
}