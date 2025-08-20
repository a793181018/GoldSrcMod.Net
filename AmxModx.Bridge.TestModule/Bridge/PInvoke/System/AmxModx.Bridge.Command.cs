
using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Command
{
    /// <summary>
    /// 命令类型枚举
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 控制台命令
        /// </summary>
        Console = 0,
        
        /// <summary>
        /// 客户端命令
        /// </summary>
        Client = 1,
        
        /// <summary>
        /// 服务器命令
        /// </summary>
        Server = 2
    }

    /// <summary>
    /// 命令标志位
    /// </summary>
    [Flags]
    public enum CommandFlags
    {
        /// <summary>
        /// 管理员命令
        /// </summary>
        Admin = 1,
        
        /// <summary>
        /// RCON命令
        /// </summary>
        Rcon = 2,
        
        /// <summary>
        /// 公共命令
        /// </summary>
        Public = 4,
        
        /// <summary>
        /// 隐藏命令
        /// </summary>
        Hidden = 8
    }

    /// <summary>
    /// 命令信息结构
    /// </summary>
    public struct CommandInfo
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string Command;
        
        /// <summary>
        /// 命令描述
        /// </summary>
        public string Info;
        
        /// <summary>
        /// 命令标志位
        /// </summary>
        public CommandFlags Flags;
    }

    /// <summary>
    /// 命令系统P/Invoke接口
    /// </summary>
    public static class CommandBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 注册控制台命令
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="funcId">函数ID</param>
        /// <param name="cmd">命令名称</param>
        /// <param name="info">命令描述</param>
        /// <param name="flags">命令标志位</param>
        /// <param name="listable">是否可列出</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RegisterConsoleCommand(
            int pluginId,
            int funcId,
            [MarshalAs(UnmanagedType.LPStr)] string cmd,
            [MarshalAs(UnmanagedType.LPStr)] string info,
            int flags,
            [MarshalAs(UnmanagedType.Bool)] bool listable);

        /// <summary>
        /// 注册客户端命令
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="funcId">函数ID</param>
        /// <param name="cmd">命令名称</param>
        /// <param name="info">命令描述</param>
        /// <param name="flags">命令标志位</param>
        /// <param name="listable">是否可列出</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RegisterClientCommand(
            int pluginId,
            int funcId,
            [MarshalAs(UnmanagedType.LPStr)] string cmd,
            [MarshalAs(UnmanagedType.LPStr)] string info,
            int flags,
            [MarshalAs(UnmanagedType.Bool)] bool listable);

        /// <summary>
        /// 注册服务器命令
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="funcId">函数ID</param>
        /// <param name="cmd">命令名称</param>
        /// <param name="info">命令描述</param>
        /// <param name="flags">命令标志位</param>
        /// <param name="listable">是否可列出</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RegisterServerCommand(
            int pluginId,
            int funcId,
            [MarshalAs(UnmanagedType.LPStr)] string cmd,
            [MarshalAs(UnmanagedType.LPStr)] string info,
            int flags,
            [MarshalAs(UnmanagedType.Bool)] bool listable);

        /// <summary>
        /// 获取命令数量
        /// </summary>
        /// <param name="type">命令类型</param>
        /// <param name="access">访问级别</param>
        /// <returns>命令数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetCommandCount(int type, int access);

        /// <summary>
        /// 执行控制台命令
        /// </summary>
        /// <param name="cmd">命令字符串</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ExecuteConsoleCommand([MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 执行客户端命令
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="cmd">命令字符串</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ExecuteClientCommand(int playerId, [MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 执行服务器命令
        /// </summary>
        /// <param name="cmd">命令字符串</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ExecuteServerCommand([MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 检查命令是否存在
        /// </summary>
        /// <param name="type">命令类型</param>
        /// <param name="cmd">命令名称</param>
        /// <returns>存在返回true，否则返回false</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_CommandExists(int type, [MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 移除命令
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="cmd">命令名称</param>
        /// <returns>移除的命令数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RemoveCommands(int pluginId, [MarshalAs(UnmanagedType.LPStr)] string cmd);
    }
}