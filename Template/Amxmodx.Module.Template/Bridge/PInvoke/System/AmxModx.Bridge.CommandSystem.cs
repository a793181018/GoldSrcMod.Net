// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Command System Bridge for C#
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace AmxModx.Bridge.CommandSystem
{
    /// <summary>
    /// 命令类型枚举
    /// </summary>
    public enum CommandType
    {
        Console = 0,    // 控制台命令
        Client = 1,     // 客户端命令
        Server = 2      // 服务器命令
    }

    /// <summary>
    /// 命令标志位枚举
    /// </summary>
    [Flags]
    public enum CommandFlags
    {
        None = 0,       // 无标志
        Admin = 1,      // 管理员命令
        Rcon = 2,       // RCON命令
        Public = 4,     // 公共命令
        Hidden = 8      // 隐藏命令
    }

    /// <summary>
    /// 命令信息结构体
    /// </summary>
    public struct CommandInfo
    {
        public string Name;
        public string Description;
        public CommandType Type;
        public CommandFlags Flags;
        public bool IsListable;
    }

    /// <summary>
    /// 命令回调委托
    /// </summary>
    /// <param name="playerId">玩家ID（-1表示控制台/服务器）</param>
    /// <param name="args">命令参数</param>
    public delegate void CommandHandler(int playerId, string[] args);

    /// <summary>
    /// 命令系统桥接接口
    /// </summary>
    public static class CommandBridge
    {
        private const string DllName = "amxmodx_mm";

        #region P/Invoke接口定义

        /// <summary>
        /// 注册控制台命令
        /// </summary>
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
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetCommandCount(int type, int access);

        /// <summary>
        /// 获取命令信息
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetCommandInfo(
            int type,
            int index,
            int access,
            [Out] byte[] cmd,
            int cmdSize,
            [Out] byte[] info,
            int infoSize,
            out int flags);

        /// <summary>
        /// 执行控制台命令
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ExecuteConsoleCommand([MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 执行客户端命令
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ExecuteClientCommand(
            int playerId,
            [MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 执行服务器命令
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ExecuteServerCommand([MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 检查命令是否存在
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_CommandExists(int type, [MarshalAs(UnmanagedType.LPStr)] string cmd);

        /// <summary>
        /// 移除命令
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RemoveCommands(int pluginId, [MarshalAs(UnmanagedType.LPStr)] string cmd);

        #endregion
    }

    /// <summary>
    /// 命令管理器，提供高级命令管理功能
    /// </summary>
    public static class CommandManager
    {
        private static readonly Dictionary<string, CommandHandler> _handlers = new Dictionary<string, CommandHandler>();
        private static int _pluginId = 1; // 默认插件ID

        #region 命令注册

        /// <summary>
        /// 设置插件ID
        /// </summary>
        public static void SetPluginId(int pluginId)
        {
            _pluginId = pluginId;
        }

        /// <summary>
        /// 注册控制台命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <param name="description">命令描述</param>
        /// <param name="handler">命令处理器</param>
        /// <param name="flags">命令标志</param>
        /// <param name="listable">是否可列出</param>
        public static bool RegisterConsoleCommand(
            string command,
            string description,
            CommandHandler handler,
            CommandFlags flags = CommandFlags.Public,
            bool listable = true)
        {
            if (string.IsNullOrEmpty(command) || handler == null)
                return false;

            int funcId = _handlers.Count + 1;
            int result = CommandBridge.AmxModx_Bridge_RegisterConsoleCommand(
                _pluginId,
                funcId,
                command,
                description,
                (int)flags,
                listable);

            if (result != 0)
            {
                _handlers[command] = handler;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册客户端命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <param name="description">命令描述</param>
        /// <param name="handler">命令处理器</param>
        /// <param name="flags">命令标志</param>
        /// <param name="listable">是否可列出</param>
        public static bool RegisterClientCommand(
            string command,
            string description,
            CommandHandler handler,
            CommandFlags flags = CommandFlags.Public,
            bool listable = true)
        {
            if (string.IsNullOrEmpty(command) || handler == null)
                return false;

            int funcId = _handlers.Count + 1;
            int result = CommandBridge.AmxModx_Bridge_RegisterClientCommand(
                _pluginId,
                funcId,
                command,
                description,
                (int)flags,
                listable);

            if (result != 0)
            {
                _handlers[command] = handler;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册服务器命令
        /// </summary>
        /// <param name="command">命令名称</param>
        /// <param name="description">命令描述</param>
        /// <param name="handler">命令处理器</param>
        /// <param name="flags">命令标志</param>
        /// <param name="listable">是否可列出</param>
        public static bool RegisterServerCommand(
            string command,
            string description,
            CommandHandler handler,
            CommandFlags flags = CommandFlags.Public,
            bool listable = true)
        {
            if (string.IsNullOrEmpty(command) || handler == null)
                return false;

            int funcId = _handlers.Count + 1;
            int result = CommandBridge.AmxModx_Bridge_RegisterServerCommand(
                _pluginId,
                funcId,
                command,
                description,
                (int)flags,
                listable);

            if (result != 0)
            {
                _handlers[command] = handler;
                return true;
            }
            return false;
        }

        #endregion

        #region 命令执行

        /// <summary>
        /// 执行控制台命令
        /// </summary>
        public static bool ExecuteConsoleCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return false;

            return CommandBridge.AmxModx_Bridge_ExecuteConsoleCommand(command) != 0;
        }

        /// <summary>
        /// 执行客户端命令
        /// </summary>
        public static bool ExecuteClientCommand(int playerId, string command)
        {
            if (string.IsNullOrEmpty(command) || playerId <= 0)
                return false;

            return CommandBridge.AmxModx_Bridge_ExecuteClientCommand(playerId, command) != 0;
        }

        /// <summary>
        /// 执行服务器命令
        /// </summary>
        public static bool ExecuteServerCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return false;

            return CommandBridge.AmxModx_Bridge_ExecuteServerCommand(command) != 0;
        }

        #endregion

        #region 命令查询

        /// <summary>
        /// 获取命令数量
        /// </summary>
        public static int GetCommandCount(CommandType type, int access = 0)
        {
            return CommandBridge.AmxModx_Bridge_GetCommandCount((int)type, access);
        }

        /// <summary>
        /// 获取所有命令信息
        /// </summary>
        public static List<CommandInfo> GetAllCommands(CommandType type, int access = 0)
        {
            var commands = new List<CommandInfo>();
            int count = GetCommandCount(type, access);

            for (int i = 0; i < count; i++)
            {
                var cmdBuffer = new byte[64];
                var infoBuffer = new byte[256];
                int flags;

                if (CommandBridge.AmxModx_Bridge_GetCommandInfo(
                    (int)type, i, access, cmdBuffer, cmdBuffer.Length,
                    infoBuffer, infoBuffer.Length, out flags) != 0)
                {
                    string cmdName = Encoding.UTF8.GetString(cmdBuffer).TrimEnd('\0');
                    string cmdInfo = Encoding.UTF8.GetString(infoBuffer).TrimEnd('\0');

                    commands.Add(new CommandInfo
                    {
                        Name = cmdName,
                        Description = cmdInfo,
                        Type = type,
                        Flags = (CommandFlags)flags
                    });
                }
            }

            return commands;
        }

        /// <summary>
        /// 检查命令是否存在
        /// </summary>
        public static bool CommandExists(CommandType type, string command)
        {
            if (string.IsNullOrEmpty(command))
                return false;

            return CommandBridge.AmxModx_Bridge_CommandExists((int)type, command);
        }

        #endregion

        #region 命令移除

        /// <summary>
        /// 移除指定命令
        /// </summary>
        public static int RemoveCommands(string command)
        {
            if (string.IsNullOrEmpty(command))
                return 0;

            return CommandBridge.AmxModx_Bridge_RemoveCommands(_pluginId, command);
        }

        /// <summary>
        /// 移除所有命令
        /// </summary>
        public static int RemoveAllCommands()
        {
            return CommandBridge.AmxModx_Bridge_RemoveCommands(_pluginId, null);
        }

        #endregion

        #region 快捷方法

        /// <summary>
        /// 向所有玩家发送消息
        /// </summary>
        public static bool BroadcastMessage(string message)
        {
            return ExecuteServerCommand($"say {message}");
        }

        /// <summary>
        /// 向指定玩家发送消息
        /// </summary>
        public static bool SendMessageToPlayer(int playerId, string message)
        {
            return ExecuteClientCommand(playerId, $"say {message}");
        }

        /// <summary>
        /// 踢出玩家
        /// </summary>
        public static bool KickPlayer(int playerId, string reason = "")
        {
            return ExecuteServerCommand($"kick #{playerId} {reason}");
        }

        /// <summary>
        /// 更改地图
        /// </summary>
        public static bool ChangeMap(string mapName)
        {
            return ExecuteServerCommand($"changelevel {mapName}");
        }

        #endregion
    }
}