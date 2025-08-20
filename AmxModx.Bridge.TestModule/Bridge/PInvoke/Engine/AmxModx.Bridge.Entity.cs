// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Entity Bridge for C#
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Entity
{
    /// <summary>
    /// 实体管理桥接接口，提供对游戏实体和玩家的访问
    /// </summary>
    public static class EntityBridge
    {
        private const string DllName = "amxmodx_mm";

        #region 实体管理接口

        /// <summary>
        /// 获取服务器最大客户端数量
        /// </summary>
        /// <returns>最大客户端数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMaxClients();

        /// <summary>
        /// 获取服务器最大实体数量
        /// </summary>
        /// <returns>最大实体数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMaxEntities();

        /// <summary>
        /// 获取当前在线玩家数量
        /// </summary>
        /// <returns>在线玩家数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerCount();

        /// <summary>
        /// 检查玩家ID是否有效
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>是否有效</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsPlayerValid(int playerId);

        /// <summary>
        /// 检查玩家是否在游戏中
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>是否在游戏中</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsPlayerInGame(int playerId);

        /// <summary>
        /// 检查玩家是否存活
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>是否存活</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsPlayerAlive(int playerId);

        /// <summary>
        /// 检查玩家是否为机器人
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>是否为机器人</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsPlayerBot(int playerId);

        #endregion

        #region 实体属性访问

        /// <summary>
        /// 获取玩家名称
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>玩家名称</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetPlayerName(int playerId);

        /// <summary>
        /// 获取玩家IP地址
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>IP地址</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetPlayerIPAddress(int playerId);

        /// <summary>
        /// 获取玩家Steam Auth ID
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>Steam Auth ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetPlayerAuthID(int playerId);

        /// <summary>
        /// 获取玩家队伍
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>队伍名称</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string AmxModx_Bridge_GetPlayerTeam(int playerId);

        /// <summary>
        /// 获取玩家UserID
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>UserID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerUserID(int playerId);

        /// <summary>
        /// 获取玩家击杀数
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>击杀数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerFrags(int playerId);

        /// <summary>
        /// 获取玩家死亡数
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>死亡数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerDeaths(int playerId);

        /// <summary>
        /// 获取玩家生命值
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>生命值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerHealth(int playerId);

        /// <summary>
        /// 获取玩家护甲值
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>护甲值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerArmor(int playerId);

        /// <summary>
        /// 获取玩家延迟
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>延迟值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerPing(int playerId);

        #endregion

        #region 实体位置相关

        /// <summary>
        /// 获取玩家位置
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="z">Z坐标</param>
        /// <returns>是否成功获取</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_GetPlayerOrigin(int playerId, out float x, out float y, out float z);

        /// <summary>
        /// 获取玩家速度
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="x">X速度</param>
        /// <param name="y">Y速度</param>
        /// <param name="z">Z速度</param>
        /// <returns>是否成功获取</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_GetPlayerVelocity(int playerId, out float x, out float y, out float z);

        /// <summary>
        /// 设置玩家位置
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="z">Z坐标</param>
        /// <returns>是否成功设置</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetPlayerOrigin(int playerId, float x, float y, float z);

        /// <summary>
        /// 设置玩家速度
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="x">X速度</param>
        /// <param name="y">Y速度</param>
        /// <param name="z">Z速度</param>
        /// <returns>是否成功设置</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetPlayerVelocity(int playerId, float x, float y, float z);

        #endregion

        #region 实体武器相关

        /// <summary>
        /// 获取玩家当前武器
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>武器ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerCurrentWeapon(int playerId);

        /// <summary>
        /// 获取玩家弹药数量
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="weaponId">武器ID</param>
        /// <returns>弹药数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerAmmo(int playerId, int weaponId);

        /// <summary>
        /// 检查玩家是否拥有武器
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="weaponId">武器ID</param>
        /// <returns>是否拥有武器</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_PlayerHasWeapon(int playerId, int weaponId);

        #endregion

        #region 实体操作

        /// <summary>
        /// 击杀玩家
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_KillPlayer(int playerId);

        /// <summary>
        /// 拍打玩家
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="damage">伤害值</param>
        /// <param name="randomVelocity">是否随机速度</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SlapPlayer(int playerId, int damage, [MarshalAs(UnmanagedType.Bool)] bool randomVelocity);

        /// <summary>
        /// 传送玩家
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="z">Z坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_TeleportPlayer(int playerId, float x, float y, float z);

        /// <summary>
        /// 重新生成玩家
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_RespawnPlayer(int playerId);

        /// <summary>
        /// 移除玩家所有武器
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_StripWeapons(int playerId);

        /// <summary>
        /// 给予玩家武器
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="weaponName">武器名称</param>
        /// <param name="ammo">弹药数量</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_GiveWeapon(int playerId, [MarshalAs(UnmanagedType.LPStr)] string weaponName, int ammo);

        /// <summary>
        /// 设置玩家队伍
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="teamId">队伍ID</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetPlayerTeam(int playerId, int teamId);

        /// <summary>
        /// 冻结/解冻玩家
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="freeze">是否冻结</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_FreezePlayer(int playerId, [MarshalAs(UnmanagedType.Bool)] bool freeze);

        /// <summary>
        /// 设置玩家生命值
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="health">生命值</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetPlayerHealth(int playerId, int health);

        /// <summary>
        /// 设置玩家护甲值
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="armor">护甲值</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetPlayerArmor(int playerId, int armor);

        /// <summary>
        /// 设置玩家击杀数
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="frags">击杀数</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetPlayerFrags(int playerId, int frags);

        /// <summary>
        /// 设置玩家死亡数
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        /// <param name="deaths">死亡数</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetPlayerDeaths(int playerId, int deaths);

        #endregion

        #region 实体查找

        /// <summary>
        /// 通过UserID查找玩家
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <returns>玩家ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_FindPlayerByUserID(int userId);

        /// <summary>
        /// 通过名称查找玩家
        /// </summary>
        /// <param name="name">玩家名称</param>
        /// <returns>玩家ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_FindPlayerByName([MarshalAs(UnmanagedType.LPStr)] string name);

        /// <summary>
        /// 通过IP地址查找玩家
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>玩家ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_FindPlayerByIPAddress([MarshalAs(UnmanagedType.LPStr)] string ip);

        #endregion
    }
}