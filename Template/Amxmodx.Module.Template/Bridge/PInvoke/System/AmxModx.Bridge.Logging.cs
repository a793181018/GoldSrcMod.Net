using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.System
{
    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum LogLevel
    {
        /// <summary>调试日志</summary>
        Debug = 0,
        /// <summary>信息日志</summary>
        Info = 1,
        /// <summary>警告日志</summary>
        Warning = 2,
        /// <summary>错误日志</summary>
        Error = 3,
        /// <summary>致命错误</summary>
        Fatal = 4
    }

    /// <summary>
    /// 日志目标枚举
    /// </summary>
    public enum LogTarget
    {
        /// <summary>控制台</summary>
        Console = 0,
        /// <summary>日志文件</summary>
        File = 1,
        /// <summary>游戏聊天</summary>
        Chat = 2,
        /// <summary>所有目标</summary>
        All = 3
    }

    /// <summary>
    /// 日志系统桥接接口
    /// 提供AMX Mod X日志系统的C#访问
    /// </summary>
    public static class LoggingBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 记录日志消息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_LogMessage(
            LogLevel level,
            [MarshalAs(UnmanagedType.LPStr)] string message);

        /// <summary>
        /// 记录格式化日志消息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="format">格式化字符串</param>
        /// <param name="args">参数数组</param>
        /// <returns>是否成功记录</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_LogFormat(
            LogLevel level,
            [MarshalAs(UnmanagedType.LPStr)] string format,
            [MarshalAs(UnmanagedType.LPArray)] object[] args,
            int argCount);

        /// <summary>
        /// 记录日志到指定目标
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="target">日志目标</param>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_LogToTarget(
            LogLevel level,
            LogTarget target,
            [MarshalAs(UnmanagedType.LPStr)] string message);

        /// <summary>
        /// 设置日志级别
        /// </summary>
        /// <param name="level">日志级别</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetLogLevel(LogLevel level);

        /// <summary>
        /// 获取当前日志级别
        /// </summary>
        /// <returns>当前日志级别</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern LogLevel AmxModx_Bridge_GetLogLevel();

        /// <summary>
        /// 启用/禁用日志目标
        /// </summary>
        /// <param name="target">日志目标</param>
        /// <param name="enabled">是否启用</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetLogTargetEnabled(
            LogTarget target,
            [MarshalAs(UnmanagedType.Bool)] bool enabled);

        /// <summary>
        /// 检查日志目标是否启用
        /// </summary>
        /// <param name="target">日志目标</param>
        /// <returns>是否启用</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsLogTargetEnabled(LogTarget target);

        /// <summary>
        /// 设置日志文件路径
        /// </summary>
        /// <param name="filePath">日志文件路径</param>
        /// <returns>是否成功设置</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetLogFilePath([MarshalAs(UnmanagedType.LPStr)] string filePath);

        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <param name="buffer">路径缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>实际路径长度</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetLogFilePath(
            [Out] StringBuilder buffer,
            int bufferSize);

        /// <summary>
        /// 清空日志文件
        /// </summary>
        /// <returns>是否成功清空</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ClearLogFile();

        /// <summary>
        /// 获取日志文件大小
        /// </summary>
        /// <returns>文件大小（字节）</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long AmxModx_Bridge_GetLogFileSize();

        /// <summary>
        /// 安全的记录日志消息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        public static bool LogMessageSafe(LogLevel level, string message)
        {
            if (string.IsNullOrEmpty(message))
                return false;

            return AmxModx_Bridge_LogMessage(level, message);
        }

        /// <summary>
        /// 安全的记录格式化日志消息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="format">格式化字符串</param>
        /// <param name="args">参数数组</param>
        /// <returns>是否成功记录</returns>
        public static bool LogFormatSafe(LogLevel level, string format, params object[] args)
        {
            if (string.IsNullOrEmpty(format) || args == null)
                return false;

            return AmxModx_Bridge_LogFormat(level, format, args, args.Length);
        }

        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <returns>日志文件路径</returns>
        public static string GetLogFilePathSafe()
        {
            StringBuilder pathBuilder = new StringBuilder(512);
            int length = AmxModx_Bridge_GetLogFilePath(pathBuilder, pathBuilder.Capacity);
            
            if (length > 0)
            {
                return pathBuilder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        public static bool LogDebug(string message)
        {
            return LogMessageSafe(LogLevel.Debug, message);
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        public static bool LogInfo(string message)
        {
            return LogMessageSafe(LogLevel.Info, message);
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        public static bool LogWarning(string message)
        {
            return LogMessageSafe(LogLevel.Warning, message);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        public static bool LogError(string message)
        {
            return LogMessageSafe(LogLevel.Error, message);
        }

        /// <summary>
        /// 记录致命错误日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <returns>是否成功记录</returns>
        public static bool LogFatal(string message)
        {
            return LogMessageSafe(LogLevel.Fatal, message);
        }
    }
}