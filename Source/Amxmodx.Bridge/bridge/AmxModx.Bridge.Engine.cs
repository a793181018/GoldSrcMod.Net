// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Engine Bridge for C#
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Engine
{
    /// <summary>
    /// 游戏引擎桥接接口，提供对游戏引擎的访问
    /// </summary>
    public static class EngineBridge
    {
        private const string DllName = "amxmodx_mm";

        #region 服务器信息

        /// <summary>
        /// 获取当前地图名称
        /// </summary>
        /// <returns>地图名称</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetMapName();

        /// <summary>
        /// 获取当前模组名称
        /// </summary>
        /// <returns>模组名称</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetModName();

        /// <summary>
        /// 获取游戏描述
        /// </summary>
        /// <returns>游戏描述</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetGameDescription();

        /// <summary>
        /// 获取游戏时间（秒）
        /// </summary>
        /// <returns>游戏时间</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetGameTime();

        /// <summary>
        /// 获取剩余游戏时间（秒）
        /// </summary>
        /// <returns>剩余时间</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetGameTimeLeft();

        /// <summary>
        /// 获取服务器tick数
        /// </summary>
        /// <returns>tick数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetServerTick();

        /// <summary>
        /// 检查是否为专用服务器
        /// </summary>
        /// <returns>是否为专用服务器</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsDedicatedServer();

        /// <summary>
        /// 检查地图是否有效
        /// </summary>
        /// <param name="mapName">地图名称</param>
        /// <returns>是否有效</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsMapValid([MarshalAs(UnmanagedType.LPStr)] string mapName);

        #endregion

        #region 控制台和日志

        /// <summary>
        /// 向服务器控制台打印消息
        /// </summary>
        /// <param name="message">消息内容</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_ServerPrint([MarshalAs(UnmanagedType.LPStr)] string message);

        /// <summary>
        /// 执行服务器控制台命令
        /// </summary>
        /// <param name="command">命令内容</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_ServerCommand([MarshalAs(UnmanagedType.LPStr)] string command);

        /// <summary>
        /// 对指定玩家执行客户端命令
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="command">命令内容</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_ClientCommand(int playerId, [MarshalAs(UnmanagedType.LPStr)] string command);

        /// <summary>
        /// 记录日志消息
        /// </summary>
        /// <param name="message">消息内容</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_LogMessage([MarshalAs(UnmanagedType.LPStr)] string message);

        /// <summary>
        /// 记录错误消息
        /// </summary>
        /// <param name="message">错误消息</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_LogError([MarshalAs(UnmanagedType.LPStr)] string message);

        #endregion

        #region 声音和特效

        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="entityId">实体ID（0为所有玩家）</param>
        /// <param name="sound">声音文件路径</param>
        /// <param name="volume">音量（0-1）</param>
        /// <param name="attenuation">衰减值</param>
        /// <param name="channel">声音通道</param>
        /// <param name="pitch">音调</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_EmitSound(int entityId, [MarshalAs(UnmanagedType.LPStr)] string sound, float volume, float attenuation, int channel, int pitch);

        /// <summary>
        /// 预加载声音文件
        /// </summary>
        /// <param name="sound">声音文件路径</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_PrecacheSound([MarshalAs(UnmanagedType.LPStr)] string sound);

        /// <summary>
        /// 预加载模型文件
        /// </summary>
        /// <param name="model">模型文件路径</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_PrecacheModel([MarshalAs(UnmanagedType.LPStr)] string model);

        /// <summary>
        /// 预加载通用资源文件
        /// </summary>
        /// <param name="resource">资源文件路径</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_PrecacheGeneric([MarshalAs(UnmanagedType.LPStr)] string resource);

        #endregion

        #region 地图和实体

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="className">实体类名</param>
        /// <returns>实体ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_CreateEntity([MarshalAs(UnmanagedType.LPStr)] string className);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_RemoveEntity(int entityId);

        /// <summary>
        /// 检查实体是否有效
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>是否有效</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsEntityValid(int entityId);

        /// <summary>
        /// 获取实体类名
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>类名</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetEntityClassName(int entityId);

        /// <summary>
        /// 通过类名查找实体
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>实体ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_FindEntityByClassName([MarshalAs(UnmanagedType.LPStr)] string className);

        #endregion

        #region 全局变量和配置

        /// <summary>
        /// 获取本地信息
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetLocalInfo([MarshalAs(UnmanagedType.LPStr)] string key);

        /// <summary>
        /// 设置本地信息
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetLocalInfo([MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

        /// <summary>
        /// 获取Cvar字符串值
        /// </summary>
        /// <param name="cvarName">Cvar名称</param>
        /// <returns>字符串值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetCvarString([MarshalAs(UnmanagedType.LPStr)] string cvarName);

        /// <summary>
        /// 获取Cvar浮点值
        /// </summary>
        /// <param name="cvarName">Cvar名称</param>
        /// <returns>浮点值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_GetCvarFloat([MarshalAs(UnmanagedType.LPStr)] string cvarName);

        /// <summary>
        /// 获取Cvar整数值
        /// </summary>
        /// <param name="cvarName">Cvar名称</param>
        /// <returns>整数值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetCvarInt([MarshalAs(UnmanagedType.LPStr)] string cvarName);

        /// <summary>
        /// 设置Cvar字符串值
        /// </summary>
        /// <param name="cvarName">Cvar名称</param>
        /// <param name="value">字符串值</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetCvarString([MarshalAs(UnmanagedType.LPStr)] string cvarName, [MarshalAs(UnmanagedType.LPStr)] string value);

        /// <summary>
        /// 设置Cvar浮点值
        /// </summary>
        /// <param name="cvarName">Cvar名称</param>
        /// <param name="value">浮点值</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetCvarFloat([MarshalAs(UnmanagedType.LPStr)] string cvarName, float value);

        /// <summary>
        /// 设置Cvar整数值
        /// </summary>
        /// <param name="cvarName">Cvar名称</param>
        /// <param name="value">整数值</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetCvarInt([MarshalAs(UnmanagedType.LPStr)] string cvarName, int value);

        #endregion
    }
}