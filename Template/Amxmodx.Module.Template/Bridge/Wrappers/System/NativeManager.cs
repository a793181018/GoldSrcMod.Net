using System;
using System.Runtime.InteropServices;
using System.Text;
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
        /// <param name="functionName">函数名称</param>
        /// <returns>函数指针</returns>
        public static IntPtr GetNativeFunction(string functionName)
        {
            if (string.IsNullOrEmpty(functionName))
                return IntPtr.Zero;

            return NativeBridge.AmxModx_Bridge_GetNativeFunction(functionName);
        }

        /// <summary>
        /// 调用AMX本地函数
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="args">参数数组</param>
        /// <returns>函数返回值</returns>
        public static int CallNativeFunction(string functionName, params object[] args)
        {
            if (string.IsNullOrEmpty(functionName))
                return 0;

            IntPtr funcPtr = GetNativeFunction(functionName);
            if (funcPtr == IntPtr.Zero)
                return 0;

            // 这里简化处理，实际应该使用更复杂的参数转换
            // 需要根据具体函数签名进行参数处理
            return NativeBridge.AmxModx_Bridge_CallNativeFunction(funcPtr, args);
        }

        /// <summary>
        /// 检查本地函数是否存在
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool NativeFunctionExists(string functionName)
        {
            if (string.IsNullOrEmpty(functionName))
                return false;

            return GetNativeFunction(functionName) != IntPtr.Zero;
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
            Marshal.Copy(array, 0, ptr, array.Length);
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
            Marshal.Copy(ptr, array, 0, length);
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
    }
}