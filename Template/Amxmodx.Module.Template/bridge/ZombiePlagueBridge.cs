using System;
using System.Runtime.InteropServices;
using GoldSrc.Amxmodx.Native;
using static Module.Global;

namespace Amxmodx.Bridge.ZombieLevel
{
    /// <summary>
    /// Zombie Plague 桥接接口，提供ZP原生函数的C#封装
    /// </summary>
    public static unsafe class ZombiePlagueBridge
    {
        // 原生函数指针
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_zombie;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_first_zombie;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_last_zombie;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_nemesis;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_survivor;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_zombie_maxhealth;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_ammo_packs;
        private static delegate* unmanaged[Cdecl]<int, int, void> _zp_set_user_ammo_packs;
        private static delegate* unmanaged[Cdecl]<int, int, void> _zp_set_user_zombie;
        private static delegate* unmanaged[Cdecl]<int, int, void> _zp_set_user_human;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_zombie_class;
        private static delegate* unmanaged[Cdecl]<int, int, void> _zp_set_user_zombie_class;
        private static delegate* unmanaged[Cdecl]<int, int> _zp_get_user_human_class;
        private static delegate* unmanaged[Cdecl]<int, int, void> _zp_set_user_human_class;

        /// <summary>
        /// 初始化Zombie Plague桥接
        /// </summary>
        public static void Initialize()
        {
            _zp_get_user_zombie = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_zombie");
            _zp_get_user_first_zombie = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_first_zombie");
            _zp_get_user_last_zombie = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_last_zombie");
            _zp_get_user_nemesis = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_nemesis");
            _zp_get_user_survivor = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_survivor");
            _zp_get_zombie_maxhealth = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_zombie_maxhealth");
            _zp_get_user_ammo_packs = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_ammo_packs");
            _zp_set_user_ammo_packs = GetZPFunction<delegate* unmanaged[Cdecl]<int, int, void>>("zp_set_user_ammo_packs");
            _zp_set_user_zombie = GetZPFunction<delegate* unmanaged[Cdecl]<int, int, void>>("zp_set_user_zombie");
            _zp_set_user_human = GetZPFunction<delegate* unmanaged[Cdecl]<int, int, void>>("zp_set_user_human");
            _zp_get_user_zombie_class = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_zombie_class");
            _zp_set_user_zombie_class = GetZPFunction<delegate* unmanaged[Cdecl]<int, int, void>>("zp_set_user_zombie_class");
            _zp_get_user_human_class = GetZPFunction<delegate* unmanaged[Cdecl]<int, int>>("zp_get_user_human_class");
            _zp_set_user_human_class = GetZPFunction<delegate* unmanaged[Cdecl]<int, int, void>>("zp_set_user_human_class");
        }

        /// <summary>
        /// 获取Zombie Plague原生函数
        /// </summary>
        private static T GetZPFunction<T>(string functionName) where T : unmanaged
        {
            using var funcName = functionName.GetNativeString();
            var funcPtr = g_fn_RequestFunction(funcName);
            return funcPtr != 0 ? (T)(object)Marshal.GetDelegateForFunctionPointer<T>((IntPtr)funcPtr) : default;
        }

        /// <summary>
        /// 检查玩家是否为僵尸
        /// </summary>
        public static bool IsPlayerZombie(int playerId)
        {
            return _zp_get_user_zombie != null && _zp_get_user_zombie(playerId) != 0;
        }

        /// <summary>
        /// 检查玩家是否为第一个僵尸
        /// </summary>
        public static bool IsPlayerFirstZombie(int playerId)
        {
            return _zp_get_user_first_zombie != null && _zp_get_user_first_zombie(playerId) != 0;
        }

        /// <summary>
        /// 检查玩家是否为最后一个僵尸
        /// </summary>
        public static bool IsPlayerLastZombie(int playerId)
        {
            return _zp_get_user_last_zombie != null && _zp_get_user_last_zombie(playerId) != 0;
        }

        /// <summary>
        /// 检查玩家是否为复仇女神
        /// </summary>
        public static bool IsPlayerNemesis(int playerId)
        {
            return _zp_get_user_nemesis != null && _zp_get_user_nemesis(playerId) != 0;
        }

        /// <summary>
        /// 检查玩家是否为幸存者
        /// </summary>
        public static bool IsPlayerSurvivor(int playerId)
        {
            return _zp_get_user_survivor != null && _zp_get_user_survivor(playerId) != 0;
        }

        /// <summary>
        /// 获取僵尸最大生命值
        /// </summary>
        public static int GetZombieMaxHealth(int playerId)
        {
            return _zp_get_zombie_maxhealth != null ? _zp_get_zombie_maxhealth(playerId) : 0;
        }

        /// <summary>
        /// 获取玩家弹药包数量
        /// </summary>
        public static int GetPlayerAmmoPacks(int playerId)
        {
            return _zp_get_user_ammo_packs != null ? _zp_get_user_ammo_packs(playerId) : 0;
        }

        /// <summary>
        /// 设置玩家弹药包数量
        /// </summary>
        public static void SetPlayerAmmoPacks(int playerId, int amount)
        {
            _zp_set_user_ammo_packs?.Invoke(playerId, amount);
        }

        /// <summary>
        /// 将玩家设置为僵尸
        /// </summary>
        public static void SetPlayerZombie(int playerId, int zombieClass = 0)
        {
            _zp_set_user_zombie?.Invoke(playerId, zombieClass);
        }

        /// <summary>
        /// 将玩家设置为人类
        /// </summary>
        public static void SetPlayerHuman(int playerId, int humanClass = 0)
        {
            _zp_set_user_human?.Invoke(playerId, humanClass);
        }

        /// <summary>
        /// 获取玩家僵尸类别
        /// </summary>
        public static int GetPlayerZombieClass(int playerId)
        {
            return _zp_get_user_zombie_class != null ? _zp_get_user_zombie_class(playerId) : 0;
        }

        /// <summary>
        /// 设置玩家僵尸类别
        /// </summary>
        public static void SetPlayerZombieClass(int playerId, int zombieClass)
        {
            _zp_set_user_zombie_class?.Invoke(playerId, zombieClass);
        }

        /// <summary>
        /// 获取玩家人类类别
        /// </summary>
        public static int GetPlayerHumanClass(int playerId)
        {
            return _zp_get_user_human_class != null ? _zp_get_user_human_class(playerId) : 0;
        }

        /// <summary>
        /// 设置玩家人类类别
        /// </summary>
        public static void SetPlayerHumanClass(int playerId, int humanClass)
        {
            _zp_set_user_human_class?.Invoke(playerId, humanClass);
        }
    }
}