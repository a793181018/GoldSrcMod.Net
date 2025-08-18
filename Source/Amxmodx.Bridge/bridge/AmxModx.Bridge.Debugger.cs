using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.Debugger
{
    /// <summary>
    /// 调试器桥接接口，提供调试相关功能
    /// </summary>
    public static class DebuggerBridge
    {
        private const string NativeLibrary = "amxmodx_mm";

        #region 调试器操作

        /// <summary>
        /// 设置错误过滤器
        /// </summary>
        /// <param name="filter">过滤器类型 (0=全部, 1=警告, 2=错误)</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetErrorFilter(int filter);

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message">调试信息</param>
        /// <param name="level">日志级别 (0=调试, 1=信息, 2=警告, 3=错误)</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_LogDebug([MarshalAs(UnmanagedType.LPStr)] string message, int level);

        /// <summary>
        /// 获取最后错误信息
        /// </summary>
        /// <returns>错误信息字符串</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_GetLastError();

        /// <summary>
        /// 清除最后错误信息
        /// </summary>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_ClearLastError();

        /// <summary>
        /// 启用/禁用调试模式
        /// </summary>
        /// <param name="enabled">是否启用</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetDebugMode([MarshalAs(UnmanagedType.I1)] bool enabled);

        /// <summary>
        /// 检查是否处于调试模式
        /// </summary>
        /// <returns>是否处于调试模式</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_IsDebugMode();

        /// <summary>
        /// 设置断点
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="line">行号</param>
        /// <returns>设置是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_SetBreakpoint([MarshalAs(UnmanagedType.LPStr)] string file, int line);

        /// <summary>
        /// 移除断点
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="line">行号</param>
        /// <returns>移除是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_RemoveBreakpoint([MarshalAs(UnmanagedType.LPStr)] string file, int line);

        /// <summary>
        /// 获取当前调用栈信息
        /// </summary>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>调用栈信息长度</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetCallStack([Out, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int bufferSize);

        #endregion

        #region 便捷方法

        /// <summary>
        /// 获取最后错误信息的安全封装
        /// </summary>
        /// <returns>错误信息字符串</returns>
        public static string GetLastErrorSafe()
        {
            IntPtr ptr = AmxModx_Bridge_GetLastError();
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : string.Empty;
        }

        /// <summary>
        /// 获取当前调用栈信息的安全封装
        /// </summary>
        /// <returns>调用栈信息字符串</returns>
        public static string GetCallStackSafe()
        {
            byte[] buffer = new byte[1024];
            int length = AmxModx_Bridge_GetCallStack(buffer, buffer.Length);
            return length > 0 ? Encoding.ASCII.GetString(buffer, 0, length) : string.Empty;
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message">调试信息</param>
        /// <param name="level">日志级别</param>
        public static void LogDebugSafe(string message, LogLevel level = LogLevel.Info)
        {
            if (!string.IsNullOrEmpty(message))
            {
                AmxModx_Bridge_LogDebug(message, (int)level);
            }
        }

        #endregion
    }

    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3
    }
}