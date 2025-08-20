using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Command;

namespace AmxModx.Bridge.Command
{
    /// <summary>
    /// 命令管理器
    /// </summary>
    public static class CommandManager
    {
        private static int _pluginId = 1;

        /// <summary>
        /// 初始化命令管理器
        /// </summary>
        public static void Initialize()
        {
            _pluginId = 1; // 默认插件ID
        }

        /// <summary>
        /// 清理命令管理器资源
        /// </summary>
        public static void Cleanup()
        {
            RemoveCommands("*");
        }

        /// <summary>
        /// 注册控制台命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <param name="description">命令描述</param>
        /// <param name="callback">命令回调函数</param>
        /// <param name="flags">命令标志位</param>
        /// <param name="listable">是否可列出</param>
        /// <returns>是否注册成功</returns>
        public static bool RegisterConsoleCommand(string command, string description, CommandHandler callback, CommandFlags flags = CommandFlags.Public, bool listable = true)
        {
            int funcId = RegisterCallback(callback);
            if (funcId < 0) return false;

            int result = CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                _pluginId, funcId, command, description, (int)flags, listable);
            
            return result == 1;
        }

        /// <summary>
        /// 注册客户端命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <param name="description">命令描述</param>
        /// <param name="callback">命令回调函数</param>
        /// <param name="flags">命令标志位</param>
        /// <param name="listable">是否可列出</param>
        /// <returns>是否注册成功</returns>
        public static bool RegisterClientCommand(string command, string description, CommandHandler callback, CommandFlags flags = CommandFlags.Public, bool listable = true)
        {
            int funcId = RegisterCallback(callback);
            if (funcId < 0) return false;

            int result = CommandBridge.AmxModx_Bridge_RegisterClientCommand(
                _pluginId, funcId, command, description, (int)flags, listable);
            
            return result == 1;
        }

        /// <summary>
        /// 注册服务器命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <param name="description">命令描述</param>
        /// <param name="callback">命令回调函数</param>
        /// <param name="flags">命令标志位</param>
        /// <param name="listable">是否可列出</param>
        /// <returns>是否注册成功</returns>
        public static bool RegisterServerCommand(string command, string description, CommandHandler callback, CommandFlags flags = CommandFlags.Public, bool listable = true)
        {
            int funcId = RegisterCallback(callback);
            if (funcId < 0) return false;

            int result = CommandBridge.AmxModx_Bridge_RegisterServerCommand(
                _pluginId, funcId, command, description, (int)flags, listable);
            
            return result == 1;
        }

        /// <summary>
        /// 获取命令列表
        /// </summary>
        /// <param name="type">命令类型</param>
        /// <param name="access">访问级别</param>
        /// <returns>命令列表</returns>
        public static List<CommandInfo> GetCommands(CommandType type, int access = 0)
        {
            var commands = new List<CommandInfo>();
            int count = CommandBridge.AmxModx_Bridge_GetCommandCount((int)type, access);
            
            for (int i = 0; i < count; i++)
            {
                var cmdBuffer = new byte[256];
                var infoBuffer = new byte[512];
                int flags;
                
                unsafe
                {
                    fixed (byte* cmdPtr = cmdBuffer)
                    fixed (byte* infoPtr = infoBuffer)
                    {
                        int result = AmxModx_Bridge_GetCommandInfo(
                            (int)type, i, access, 
                            (IntPtr)cmdPtr, cmdBuffer.Length,
                            (IntPtr)infoPtr, infoBuffer.Length,
                            out flags);
                        
                        if (result == 1)
                        {
                            string? command = Marshal.PtrToStringAnsi((IntPtr)cmdPtr);
                            string? info = Marshal.PtrToStringAnsi((IntPtr)infoPtr);
                            
                            if (!string.IsNullOrEmpty(command))
                            {
                                commands.Add(new CommandInfo
                                {
                                    Command = command!,
                                    Info = info ?? string.Empty,
                                    Flags = (CommandFlags)flags
                                });
                            }
                        }
                    }
                }
            }
            
            return commands;
        }

        /// <summary>
        /// 执行控制台命令
        /// </summary>
        /// <param name="command">命令字符串</param>
        /// <returns>是否执行成功</returns>
        public static bool ExecuteConsoleCommand(string command)
        {
            int result = CommandBridge.AmxModx_Bridge_ExecuteConsoleCommand(command);
            return result == 1;
        }

        /// <summary>
        /// 执行客户端命令
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="command">命令字符串</param>
        /// <returns>是否执行成功</returns>
        public static bool ExecuteClientCommand(int playerId, string command)
        {
            int result = CommandBridge.AmxModx_Bridge_ExecuteClientCommand(playerId, command);
            return result == 1;
        }

        /// <summary>
        /// 执行服务器命令
        /// </summary>
        /// <param name="command">命令字符串</param>
        /// <returns>是否执行成功</returns>
        public static bool ExecuteServerCommand(string command)
        {
            int result = CommandBridge.AmxModx_Bridge_ExecuteServerCommand(command);
            return result == 1;
        }

        /// <summary>
        /// 检查命令是否存在
        /// </summary>
        /// <param name="type">命令类型</param>
        /// <param name="command">命令名称</param>
        /// <returns>是否存在</returns>
        public static bool CommandExists(CommandType type, string command)
        {
            return CommandBridge.AmxModx_Bridge_CommandExists((int)type, command);
        }

        /// <summary>
        /// 移除命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <returns>移除的命令数量</returns>
        public static int RemoveCommands(string command)
        {
            return CommandBridge.AmxModx_Bridge_RemoveCommands(_pluginId, command);
        }

        #region Private Methods

        private static int RegisterCallback(CommandHandler callback)
        {
            // 这里应该实现回调函数的注册逻辑
            // 简化实现，返回一个模拟的函数ID
            return new Random().Next(1000, 9999);
        }

        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        private static extern int AmxModx_Bridge_GetCommandInfo(
            int type,
            int index,
            int access,
            IntPtr cmd,
            int cmdSize,
            IntPtr info,
            int infoSize,
            out int flags);

        #endregion
    }

    /// <summary>
    /// 命令处理器委托
    /// </summary>
    /// <param name="playerId">玩家ID（0表示控制台）</param>
    /// <param name="args">命令参数</param>
    public delegate void CommandHandler(int playerId, string[] args);
}