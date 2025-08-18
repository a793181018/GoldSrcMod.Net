// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Fun
{
    /// <summary>
    /// Fun模块的C#桥接接口
    /// 提供对CS1.6游戏功能的访问
    /// </summary>
    public static class FunBridge
    {
        private const string FunBridgeDll = "fun_bridge";

        #region 客户端语音监听

        /// <summary>
        /// 获取客户端是否监听其他玩家的语音
        /// </summary>
        /// <param name="receiver">接收者玩家索引</param>
        /// <param name="sender">发送者玩家索引</param>
        /// <returns>1表示监听，0表示不监听</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetClientListening(int receiver, int sender);

        /// <summary>
        /// 设置客户端是否监听其他玩家的语音
        /// </summary>
        /// <param name="receiver">接收者玩家索引</param>
        /// <param name="sender">发送者玩家索引</param>
        /// <param name="listen">1表示监听，0表示不监听</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetClientListening(int receiver, int sender, int listen);

        #endregion

        #region 玩家无敌模式

        /// <summary>
        /// 设置玩家无敌模式
        /// </summary>
        /// <param name="user">玩家索引</param>
        /// <param name="godmode">1开启无敌，0关闭无敌</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserGodmode(int user, int godmode);

        /// <summary>
        /// 获取玩家无敌模式状态
        /// </summary>
        /// <param name="user">玩家索引</param>
        /// <returns>1表示无敌，0表示非无敌</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserGodmode(int user);

        #endregion

        #region 玩家生命值

        /// <summary>
        /// 获取玩家生命值
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>生命值</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetUserHealth(int index);

        /// <summary>
        /// 设置玩家生命值
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="health">生命值</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserHealth(int index, float health);

        #endregion

        #region 物品和武器

        /// <summary>
        /// 给予玩家物品
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="item">物品名称</param>
        /// <returns>物品实体ID，失败返回0或-1</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GiveItem(int index, [MarshalAs(UnmanagedType.LPStr)] string item);

        /// <summary>
        /// 重新生成实体
        /// </summary>
        /// <param name="index">实体索引</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SpawnEntity(int index);

        #endregion

        #region 玩家击杀和护甲

        /// <summary>
        /// 获取玩家击杀数
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>击杀数</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserFrags(int index);

        /// <summary>
        /// 设置玩家击杀数
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="frags">击杀数</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserFrags(int index, int frags);

        /// <summary>
        /// 获取玩家护甲值
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>护甲值</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserArmor(int index);

        /// <summary>
        /// 设置玩家护甲值
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="armor">护甲值</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserArmor(int index, int armor);

        #endregion

        #region 玩家位置和渲染

        /// <summary>
        /// 获取玩家位置
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="origin">位置坐标数组(x,y,z)</param>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetUserOrigin(int index, [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] float[] origin);

        /// <summary>
        /// 设置玩家位置
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="origin">位置坐标数组(x,y,z)</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserOrigin(int index, [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] float[] origin);

        /// <summary>
        /// 获取玩家渲染效果
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="fx">特效类型输出</param>
        /// <param name="r">红色分量输出</param>
        /// <param name="g">绿色分量输出</param>
        /// <param name="b">蓝色分量输出</param>
        /// <param name="render">渲染模式输出</param>
        /// <param name="amount">透明度输出</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserRendering(int index, out int fx, out int r, out int g, out int b, out int render, out float amount);

        /// <summary>
        /// 设置玩家渲染效果
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="fx">特效类型</param>
        /// <param name="r">红色分量</param>
        /// <param name="g">绿色分量</param>
        /// <param name="b">蓝色分量</param>
        /// <param name="render">渲染模式</param>
        /// <param name="amount">透明度</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserRendering(int index, int fx, int r, int g, int b, int render, float amount);

        #endregion

        #region 玩家物理属性

        /// <summary>
        /// 获取玩家最大速度
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>最大速度值</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetUserMaxspeed(int index);

        /// <summary>
        /// 设置玩家最大速度
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="speed">最大速度</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserMaxspeed(int index, float speed);

        /// <summary>
        /// 获取玩家重力
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>重力值</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetUserGravity(int index);

        /// <summary>
        /// 设置玩家重力
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="gravity">重力值</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserGravity(int index, float gravity);

        #endregion

        #region 命中区域

        /// <summary>
        /// 获取玩家命中区域设置
        /// </summary>
        /// <param name="attacker">攻击者索引</param>
        /// <param name="target">目标索引</param>
        /// <returns>命中区域标志</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserHitzones(int attacker, int target);

        /// <summary>
        /// 设置玩家命中区域
        /// </summary>
        /// <param name="attacker">攻击者索引，0表示所有玩家</param>
        /// <param name="target">目标索引，0表示所有玩家</param>
        /// <param name="hitzones">命中区域标志</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserHitzones(int attacker, int target, int hitzones);

        #endregion

        #region 穿墙模式

        /// <summary>
        /// 设置玩家穿墙模式
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="noclip">1开启穿墙，0关闭穿墙</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserNoclip(int index, int noclip);

        /// <summary>
        /// 获取玩家穿墙模式状态
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>1表示穿墙模式，0表示正常模式</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserNoclip(int index);

        #endregion

        #region 脚步声

        /// <summary>
        /// 设置玩家脚步声
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="footsteps">1开启无声脚步，0关闭无声脚步</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetUserFootsteps(int index, int footsteps);

        /// <summary>
        /// 获取玩家脚步声状态
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>1表示无声脚步，0表示正常脚步</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetUserFootsteps(int index);

        #endregion

        #region 武器管理

        /// <summary>
        /// 移除玩家所有武器
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>操作是否成功</returns>
        [DllImport(FunBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StripUserWeapons(int index);

        #endregion

        #region 辅助类和方法

        /// <summary>
        /// 玩家渲染模式枚举
        /// </summary>
        public enum RenderMode
        {
            Normal = 0,
            Color = 1,
            Texture = 2,
            Glow = 3,
            Solid = 4,
            Additive = 5
        }

        /// <summary>
        /// 玩家特效类型枚举
        /// </summary>
        public enum RenderFx
        {
            None = 0,
            PulseSlow = 1,
            PulseFast = 2,
            PulseSlowWide = 3,
            PulseFastWide = 4,
            FadeSlow = 5,
            FadeFast = 6,
            SolidSlow = 7,
            SolidFast = 8,
            StrobeSlow = 9,
            StrobeFast = 10,
            StrobeFaster = 11,
            FlickerSlow = 12,
            FlickerFast = 13,
            NoDissipation = 14,
            Distort = 15,
            Hologram = 16,
            DeadPlayer = 17,
            Explode = 18,
            GlowShell = 19,
            ClampMinScale = 20,
            LightMultiplier = 21
        }

        /// <summary>
        /// 命中区域枚举
        /// </summary>
        [Flags]
        public enum HitZones
        {
            Generic = 1 << 0,
            Head = 1 << 1,
            Chest = 1 << 2,
            Stomach = 1 << 3,
            LeftArm = 1 << 4,
            RightArm = 1 << 5,
            LeftLeg = 1 << 6,
            RightLeg = 1 << 7,
            All = (1 << 8) - 1
        }

        /// <summary>
        /// 获取玩家渲染信息的结构体
        /// </summary>
        public struct PlayerRendering
        {
            public RenderFx Fx;
            public int Red;
            public int Green;
            public int Blue;
            public RenderMode RenderMode;
            public float Amount;
        }

        /// <summary>
        /// 获取玩家渲染信息的便捷方法
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <returns>玩家渲染信息</returns>
        public static PlayerRendering GetPlayerRendering(int index)
        {
            int fx, r, g, b, render;
            float amount;
            
            int result = GetUserRendering(index, out fx, out r, out g, out b, out render, out amount);
            if (result == 0)
                throw new InvalidOperationException("无法获取玩家渲染信息");

            return new PlayerRendering
            {
                Fx = (RenderFx)fx,
                Red = r,
                Green = g,
                Blue = b,
                RenderMode = (RenderMode)render,
                Amount = amount
            };
        }

        /// <summary>
        /// 设置玩家渲染信息的便捷方法
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="rendering">渲染信息</param>
        public static void SetPlayerRendering(int index, PlayerRendering rendering)
        {
            int result = SetUserRendering(index, (int)rendering.Fx, rendering.Red, rendering.Green, 
                rendering.Blue, (int)rendering.RenderMode, rendering.Amount);
            if (result == 0)
                throw new InvalidOperationException("无法设置玩家渲染信息");
        }

        #endregion
    }
}