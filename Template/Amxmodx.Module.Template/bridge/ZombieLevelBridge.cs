using GoldSrc.Amxmodx.Native;
using System;
using System.Runtime.InteropServices;
using static Module.Global;

#pragma warning disable CS8981
using cell = int;
#pragma warning restore CS8981

namespace Amxmodx.Bridge.ZombieLevel
{
    internal static class NativeExtensions
    {
        public static unsafe sbyte* GetNativeString(this string str)
        {
            return (sbyte*)Marshal.StringToHGlobalAnsi(str);
        }
    }

    /// <summary>
    /// 僵尸等级系统桥接接口
    /// 基于现有框架的AMXX API封装
    /// </summary>
    public unsafe static class ZombieLevelBridge
    {
        /// <summary>
        /// 获取玩家名称
        /// </summary>
        public static string GetPlayerName(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return string.Empty;
            
            sbyte* name = g_fn_GetPlayerName(playerId);
            return Marshal.PtrToStringAnsi((nint)name) ?? string.Empty;
        }

        /// <summary>
        /// 检查玩家是否为僵尸 (通过ZP原生函数)
        /// </summary>
        public static bool IsPlayerZombie(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return false;
            
            // 使用AMXX的RequestFunction获取ZP原生函数
            fixed (sbyte* funcName = "zp_is_player_zombie".GetNativeString())
            {
                var funcPtr = g_fn_RequestFunction(funcName);
                if (funcPtr == null) return false;
                
                var zpIsZombie = (delegate* unmanaged[Cdecl]<int, int>)funcPtr;
                return zpIsZombie(playerId) != 0;
            }
        }

        /// <summary>
        /// 获取玩家弹药袋 (ZP原生函数)
        /// </summary>
        public static int GetPlayerAmmoPacks(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return 0;
            
            fixed (sbyte* funcName = "zp_get_user_ammo_packs".GetNativeString())
            {
                var funcPtr = g_fn_RequestFunction(funcName);
                if (funcPtr == null) return 0;
                
                var zpGetAmmoPacks = (delegate* unmanaged[Cdecl]<int, int>)funcPtr;
                return zpGetAmmoPacks(playerId);
            }
        }

        /// <summary>
        /// 设置玩家弹药袋 (ZP原生函数)
        /// </summary>
        public static void SetPlayerAmmoPacks(int playerId, int amount)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            fixed (sbyte* funcName = "zp_set_user_ammo_packs".GetNativeString())
            {
                var funcPtr = g_fn_RequestFunction(funcName);
                if (funcPtr == null) return;
                
                var zpSetAmmoPacks = (delegate* unmanaged[Cdecl]<int, int, void>)funcPtr;
                zpSetAmmoPacks(playerId, amount);
            }
        }

        /// <summary>
        /// 获取玩家重力
        /// </summary>
        public static float GetPlayerGravity(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return 1.0f;
            
            return g_fn_GetPlayerGravity(playerId);
        }

        /// <summary>
        /// 设置玩家重力
        /// </summary>
        public static void SetPlayerGravity(int playerId, float gravity)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            g_fn_SetPlayerGravity(playerId, gravity);
        }

        /// <summary>
        /// 获取玩家最大速度
        /// </summary>
        public static float GetPlayerMaxSpeed(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return 250.0f;
            
            return g_fn_GetPlayerMaxSpeed(playerId);
        }

        /// <summary>
        /// 设置玩家最大速度
        /// </summary>
        public static void SetPlayerMaxSpeed(int playerId, float maxSpeed)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            g_fn_SetPlayerMaxSpeed(playerId, maxSpeed);
        }

        /// <summary>
        /// 显示消息给玩家
        /// </summary>
        public static void ShowMessage(int playerId, string message)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            fixed (sbyte* msg = message.GetNativeString())
            {
                g_fn_ClientPrint(playerId, (int)PrintDest.PRINT_CHAT, msg);
            }
        }

        /// <summary>
        /// 显示HUD消息
        /// </summary>
        public static void ShowHudMessage(int playerId, string message, int r, int g, int b, float x, float y, int channel)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            fixed (sbyte* msg = message.GetNativeString())
            {
                g_fn_ShowHudMessage(playerId, msg, x, y, r, g, b, channel);
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        public static string ReadFile(string filePath)
        {
            fixed (sbyte* path = filePath.GetNativeString())
            {
                sbyte* content = g_fn_ReadFile(path);
                return Marshal.PtrToStringAnsi((nint)content) ?? string.Empty;
            }
        }

        /// <summary>
        /// 写入文件内容
        /// </summary>
        public static void WriteFile(string filePath, string content, bool append = false)
        {
            fixed (sbyte* path = filePath.GetNativeString())
            fixed (sbyte* text = content.GetNativeString())
            {
                g_fn_WriteFile(path, text, append ? 1 : 0);
            }
        }

        /// <summary>
        /// 获取玩家武器ID
        /// </summary>
        public static int GetPlayerCurrentWeapon(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return 0;
            
            return g_fn_GetPlayerCurrentWeapon(playerId);
        }

        /// <summary>
        /// 给予玩家物品
        /// </summary>
        public static void GivePlayerItem(int playerId, string itemName)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            fixed (sbyte* item = itemName.GetNativeString())
            {
                g_fn_GivePlayerItem(playerId, item);
            }
        }

        /// <summary>
        /// 创建爆炸效果
        /// </summary>
        public static void CreateExplosion(float[] origin, float magnitude, float radius)
        {
            fixed (float* originPtr = origin)
            {
                g_fn_CreateExplosion(originPtr, magnitude, radius, 0);
            }
        }

        /// <summary>
        /// 检查玩家是否有效
        /// </summary>
        public static bool IsPlayerValid(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return false;
            return g_fn_IsPlayerValid(playerId) != 0;
        }

        /// <summary>
        /// 检查玩家是否为机器人
        /// </summary>
        public static bool IsPlayerBot(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return false;
            return g_fn_IsPlayerBot(playerId) != 0;
        }

        /// <summary>
        /// 获取玩家健康值
        /// </summary>
        public static int GetPlayerHealth(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return 0;
            
            return g_fn_GetPlayerHealth(playerId);
        }

        /// <summary>
        /// 设置玩家健康值
        /// </summary>
        public static void SetPlayerHealth(int playerId, int health)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            g_fn_SetPlayerHealth(playerId, health);
        }

        /// <summary>
        /// 获取玩家护甲值
        /// </summary>
        public static int GetPlayerArmor(int playerId)
        {
            if (playerId <= 0 || playerId >= 33) return 0;
            
            return g_fn_GetPlayerArmor(playerId);
        }

        /// <summary>
        /// 设置玩家护甲值
        /// </summary>
        public static void SetPlayerArmor(int playerId, int armor)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            g_fn_SetPlayerArmor(playerId, armor);
        }

        /// <summary>
        /// 执行客户端命令
        /// </summary>
        public static void ClientCommand(int playerId, string command)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            fixed (sbyte* cmd = command.GetNativeString())
            {
                g_fn_ServerCommand(cmd);
            }
        }

        /// <summary>
        /// 显示屏幕淡出效果
        /// </summary>
        public static void ShowScreenFade(int playerId, float duration, ushort holdTime, int r, int g, int b, int alpha)
        {
            if (playerId <= 0 || playerId >= 33) return;
            
            g_fn_ShowScreenFade(playerId, duration, holdTime, r, g, b, alpha);
        }

        /// <summary>
        /// 设置玩家渲染模式
        /// </summary>
        public static void SetPlayerRenderMode(int playerId, int alpha)
        {
            if (!IsPlayerValid(playerId)) return;
            
            // 使用控制台命令设置渲染模式
            string cmd = $"set_rendering {playerId} {alpha}";
            ClientCommand(playerId, cmd);
        }

        /// <summary>
        /// 获取玩家当前武器
        /// </summary>
        public static string GetPlayerWeapon(int playerId)
        {
            if (!IsPlayerValid(playerId)) return "";
            
            // 简化实现，返回空字符串
            return "";
        }
    }
}