using GoldSrc.Amxmodx;
using GoldSrc.Amxmodx.Native;
using GoldSrc.HLSDK;
using GoldSrc.HLSDK.Native;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Amxmodx.Bridge.ZombieLevel;
using static Module.Global;
#pragma warning disable CS8981
using cell = int;
#pragma warning restore CS8981

namespace Module
{
    /// <summary>
    /// 僵尸等级配置
    /// </summary>
    public static class ZombieLevelConfig
    {
        // 经验相关
        public const int EXEGAIN = 15;
        public const int EXEGAIN2EXE = 30;
        public const int SAVEEXEGAIN = 15;
        public const int NOSAVEEXEGAIN = 15;

        // 人类技能数值
        public const float HealthPerLevel = 15.0f;
        public const float ArmorPerLevel = 15.0f;
        public const float GravityReductionPerLevel = 50.0f;
        public const float SpeedPerLevel = 15.0f;
        public const float InvisibilityPerLevel = 0.1f;

        // 僵尸技能数值
        public const float ZombieHealthPerLevel = 100.0f;
        public const float SkillEffectHealth = 25.0f;
        public const float SkillGainHealth = 20.0f;

        // 获取升级所需经验值
        public static int GetExperienceRequiredForLevel(int level, bool isZombie = false)
        {
            if (isZombie)
            {
                return level * 100; // 僵尸升级经验
            }
            else
            {
                return level * 150; // 人类升级经验
            }
        }
    }

    /// <summary>
    /// 僵尸等级系统 - 将AMXX插件ZP_SimpleLevel重写成C#
    /// </summary>
    public unsafe class ZombieLevelSystem
    {
        // 常量定义
        private const string PLUGIN_NAME = "僵尸等级系统";
        private const string PLUGIN_VERSION = "C# v1.0";
        private const string PLUGIN_AUTHOR = "C#重写版";

        // 经验相关常量
        private const int EXEGAIN = 2;
        private const int EXEGAIN2EXE = 10;
        private const float SAVEEXEGAIN = 1.5f;
        private const float NOSAVEEXEGAIN = 1.0f;

        // 人类技能数值常量
        private const float SKILLHEALTH = 15.0f;
        private const float SKILLARMOR = 15.0f;
        private const float SKILLGRAVITY = 100.0f;
        private const float SKILLSPEED = 50.0f;
        private const float SKILLRENDER = 50.0f;

        // 僵尸技能数值常量
        private const float ZB_SKILLHEALTH = 50.0f;
        private const float SKILLEFFECTHEALTH = 25.0f;
        private const float SKILLGAINHEALTH = 20.0f;

        // 使用ZombieLevelConfig中的配置

        // 技能数量定义
        private const int HUMAN_SKILLNUM = 15; // 人类技能总数
        private const int ZB_SKILLNUM = 7; // 僵尸技能总数

        // 人类技能最大等级
        private static readonly int[] human_LevelSkillMAX = new int[HUMAN_SKILLNUM]
        {
            5,  // 血量提升
            8,  // 护甲提升
            4,  // 重力降低
            3,  // 速度提升
            3,  // 隐身之术
            3,  // 武器火力
            6,  // 无后坐力
            2,  // 致命一击
            3,  // 嗜血一击
            1,  // 手雷补给
            1,  // 多重跳跃
            1,  // 神龟冲击波
            5,  // 弹药更换速度提升
            5,  // 开火速度提升
            5   // 武器击退提升
        };

        // 僵尸技能最大等级
        private static readonly int[] zb_LevelSkillMAX = new int[ZB_SKILLNUM]
        {
            5,  // 血量提升
            4,  // 重力降低
            3,  // 速度提升
            3,  // 隐身之术
            3,  // 破防之爪
            1,  // 多重跳跃
            5   // 抗武器击退
        };

        // 人类技能升级所需点数
        private static readonly int[] human_LevelSkillPonit = new int[HUMAN_SKILLNUM]
        {
            1,  // 血量提升
            2,  // 护甲提升
            3,  // 重力降低
            3,  // 速度提升
            4,  // 隐身之术
            7,  // 武器火力
            4,  // 无后坐力
            5,  // 致命一击
            3,  // 嗜血一击
            3,  // 手雷补给
            7,  // 多重跳跃
            10, // 神龟冲击波
            3,  // 弹药更换速度提升
            3,  // 开火速度提升
            3   // 武器击退提升
        };

        // 僵尸技能升级所需点数
        private static readonly int[] zb_LevelSkillPonit = new int[ZB_SKILLNUM]
        {
            1,  // 血量提升
            3,  // 重力降低
            3,  // 速度提升
            4,  // 隐身之术
            7,  // 破防之爪
            7,  // 多重跳跃
            3   // 抗武器击退
        };

        // 人类技能名称
        private static readonly string[] human_szLevelName = new string[HUMAN_SKILLNUM]
        {
            "血量提升",
            "护甲提升",
            "重力降低",
            "速度提升",
            "隐身之术",
            "武器火力",
            "后坐力减少",
            "致命一击",
            "嗜血一击",
            "手雷补给",
            "多重跳跃",
            "神龟冲击波",
            "弹药更换速度提升",
            "开火速度提升",
            "武器击退提升"
        };

        // 僵尸技能名称
        private static readonly string[] zb_szLevelName = new string[ZB_SKILLNUM]
        {
            "血量提升",
            "重力降低",
            "速度提升",
            "隐身之术",
            "破防之爪",
            "多重跳跃",
            "抗武器击退"
        };

        // 玩家数据存储
        private static int[] g_LevelPoint = new int[33];
        private static int[] g_LevelExe = new int[33];
        private static int[] g_Level = new int[33];
        private static int[] g_LevelTotalExe = new int[33];
        private static int[] g_jump = new int[33];
        private static int[] zb_LevelPoint = new int[33];
        private static bool[] g_levelSave = new bool[33];
        private static bool[] g_PD = new bool[33];
        private static bool[] g_explosion = new bool[33];
        private static bool[] g_guishow = new bool[33];
        private static bool[] g_taskid = new bool[33];
        private static bool[] zb_guishow = new bool[33];
        private static int[,] human_LevelNum = new int[33, HUMAN_SKILLNUM];
        private static int[,] zb_LevelNum = new int[33, ZB_SKILLNUM];
        private static int g_MaxLevel = 0;

        // 文件路径
        private const string SAVE_FILE = "addons/amxmodx/configs/Zp_SimpleLevel.ini";

        // 经验获取常量
        private const int EXEGETDAMAGE = 2;      // 伤害获得经验
        private const int EXEGETKILL = 50;       // 击杀获得经验
        private const int BUYEXEPACKS = 10;      // 购买经验所需弹药包

        /// <summary>
        /// 初始化插件
        /// </summary>
        public static void Initialize()
        {
            ZombieLevelBridge.Initialize();
            ZombiePlagueBridge.Initialize();
            RegisterEvents();
        }

        private static void RegisterEvents()
        {
            // 注册ZP事件
            RegisterZPEvents();
            
            // 注册原有事件
            RegisterHamEvents();
            RegisterCmdEvents();
        }

        private static void RegisterZPEvents()
        {
            // 注册ZP感染事件
            g_fn_RegisterForward("ZP_UserInfected".GetNativeString(), ForwardExecType.FP_CELL);
            g_fn_RegisterForward("ZP_UserHumanized".GetNativeString(), ForwardExecType.FP_CELL);
            
            // 注册ZP预感染事件
            g_fn_RegisterForward("ZP_UserInfected_Pre".GetNativeString(), ForwardExecType.FP_CELL);
            g_fn_RegisterForward("ZP_UserHumanized_Pre".GetNativeString(), ForwardExecType.FP_CELL);
        }

        // 替换原有的IsPlayerZombie检查
        private static bool IsPlayerZombie(int playerId)
        {
            return ZombiePlagueBridge.IsPlayerZombie(playerId);
        }

        // 添加ZP事件处理函数
        public static void OnZPUserInfected(int playerId)
        {
            if (!IsPlayerValid(playerId)) return;
            
            // 重置僵尸技能
            ResetZombieSkills(playerId);
            
            // 应用僵尸基础属性
            ApplyZombieBaseStats(playerId);
        }

        public static void OnZPUserHumanized(int playerId)
        {
            if (!IsPlayerValid(playerId)) return;
            
            // 重置人类技能
            ResetHumanSkills(playerId);
            
            // 应用人类基础属性
            ApplyHumanBaseStats(playerId);
        }

        private static void ResetZombieSkills(int playerId)
        {
            for (int i = 0; i < ZB_SKILLNUM; i++)
            {
                zb_LevelNum[playerId, i] = 0;
            }
        }

        private static void ResetHumanSkills(int playerId)
        {
            for (int i = 0; i < HUMAN_SKILLNUM; i++)
            {
                human_LevelNum[playerId, i] = 0;
            }
        }

        private static void ApplyZombieBaseStats(int playerId)
        {
            int maxHealth = ZombiePlagueBridge.GetZombieMaxHealth(playerId);
            g_fn_SetPlayerHealth(playerId, maxHealth);
        }

        private static void ApplyHumanBaseStats(int playerId)
        {
            g_fn_SetPlayerHealth(playerId, 100);
            g_fn_SetPlayerArmor(playerId, 0);
        }

        // 更新经验获取逻辑，使用ZombiePlagueBridge
        private static void GiveExperienceOnDamage(int attacker, int victim, int damage)
        {
            if (!IsPlayerValid(attacker) || !IsPlayerValid(victim)) return;
            if (attacker == victim) return;

            bool attackerIsZombie = IsPlayerZombie(attacker);
            bool victimIsZombie = IsPlayerZombie(victim);

            // 人类打僵尸获得经验
            if (!attackerIsZombie && victimIsZombie)
            {
                AddExperience(attacker, damage * EXEGETDAMAGE);
            }
            // 僵尸打人类获得经验
            else if (attackerIsZombie && !victimIsZombie)
            {
                AddExperience(attacker, damage * EXEGETDAMAGE);
            }
        }

        // 更新击杀经验逻辑
        private static void GiveExperienceOnKill(int killer, int victim)
        {
            if (!IsPlayerValid(killer) || !IsPlayerValid(victim)) return;
            if (killer == victim) return;

            bool killerIsZombie = IsPlayerZombie(killer);
            bool victimIsZombie = IsPlayerZombie(victim);

            // 人类击杀僵尸获得经验
            if (!killerIsZombie && victimIsZombie)
            {
                AddExperience(killer, EXEGETKILL);
            }
            // 僵尸击杀人类获得经验
            else if (killerIsZombie && !victimIsZombie)
            {
                AddExperience(killer, EXEGETKILL);
            }
        }

        // 更新购买经验逻辑
        private static void BuyExperienceWithAmmoPacks(int playerId, int amount)
        {
            if (!IsPlayerValid(playerId)) return;

            int currentAmmoPacks = ZombiePlagueBridge.GetPlayerAmmoPacks(playerId);
            int cost = amount * BUYEXEPACKS;

            if (currentAmmoPacks >= cost)
            {
                AddExperience(playerId, amount);
                ZombiePlagueBridge.SetPlayerAmmoPacks(playerId, currentAmmoPacks - cost);
                ZombieLevelBridge.ShowMessage(playerId, $"购买 {amount} 经验成功！消耗 {cost} 弹药包");
            }
            else
            {
                ZombieLevelBridge.ShowMessage(playerId, "弹药包不足！");
            }
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        private static void RegisterCommands()
        {
            // 注册菜单命令
            g_fn_RegisterFunction(null, "ZombieLevel_MainMenu", &MainMenuCommand);
            g_fn_RegisterFunction(null, "ZombieLevel_ResetLevel", &ResetLevelCommand);
        }



        /// <summary>
        /// 注册转发
        /// </summary>
        private static void RegisterForwards()
        {
            // 注册ZP事件
            g_fn_RegisterFunction(null, "ZombieLevel_ZPHumanizedPost", &ZPHumanizedPost);
            g_fn_RegisterFunction(null, "ZombieLevel_ZPInfectedPost", &ZPInfectedPost);
        }

        /// <summary>
        /// 主菜单命令
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell MainMenuCommand(AMX* amx, cell* @params)
        {
            int id = @params[1];
            ShowMainMenu(id);
            return 1;
        }

        /// <summary>
        /// 重置等级命令
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ResetLevelCommand(AMX* amx, cell* @params)
        {
            int id = @params[1];
            ResetPlayerLevel(id);
            return 1;
        }

        /// <summary>
        /// 玩家加入服务器
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ClientPutInServer(AMX* amx, cell* @params)
        {
            int id = @params[1];
            InitializePlayer(id);
            return 1;
        }

        /// <summary>
        /// 玩家断开连接
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ClientDisconnect(AMX* amx, cell* @params)
        {
            int id = @params[1];
            SavePlayerData(id);
            return 1;
        }

        /// <summary>
        /// 客户端伤害事件
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ClientDamage(AMX* amx, cell* @params)
        {
            int attacker = @params[1];
            int victim = @params[2];
            int damage = @params[3];
            int weapon = @params[4];
            int hitplace = @params[5];

            HandleClientDamage(attacker, victim, damage, weapon, hitplace);
            return 1;
        }

        /// <summary>
        /// 客户端死亡事件
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ClientDeath(AMX* amx, cell* @params)
        {
            int killer = @params[1];
            int victim = @params[2];

            HandleClientDeath(killer, victim);
            return 1;
        }

        /// <summary>
        /// 客户端预思考
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ClientPreThink(AMX* amx, cell* @params)
        {
            int id = @params[1];
            HandleClientPreThink(id);
            return 1;
        }

        /// <summary>
        /// 人类化后事件
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ZPHumanizedPost(AMX* amx, cell* @params)
        {
            int id = @params[1];
            SetHumanSkills(id);
            return 1;
        }

        /// <summary>
        /// 感染后事件
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static cell ZPInfectedPost(AMX* amx, cell* @params)
        {
            int id = @params[1];
            SetZombieSkills(id);
            return 1;
        }

        /// <summary>
        /// 初始化玩家数据
        /// </summary>
        private static void InitializePlayer(int id)
        {
            g_Level[id] = 1;
            g_LevelTotalExe[id] = ZombieLevelConfig.GetExperienceRequiredForLevel(1);
            g_LevelExe[id] = LoadPlayerExperience(id);
            g_LevelPoint[id] = 0;
            zb_LevelPoint[id] = 0;
            g_jump[id] = 0;

            // 初始化技能等级
            for (int i = 0; i < HUMAN_SKILLNUM; i++)
                human_LevelNum[id, i] = 0;
            for (int i = 0; i < ZB_SKILLNUM; i++)
                zb_LevelNum[id, i] = 0;

            g_levelSave[id] = g_LevelExe[id] > 0;
            g_PD[id] = g_LevelExe[id] == 0;
            g_taskid[id] = false;

            // 显示等级HUD
            ShowLevelHud(id);
        }

        /// <summary>
        /// 设置人类技能
        /// </summary>
        private static void SetHumanSkills(int id)
        {
            if (!IsPlayerValid(id)) return;

            // 应用血量提升
            if (human_LevelNum[id, 0] > 0)
            {
                int currentHealth = ZombieLevelBridge.GetPlayerHealth(id);
                ZombieLevelBridge.SetPlayerHealth(id, currentHealth + (ZombieLevelConfig.HealthPerLevel * human_LevelNum[id, 0]));
            }

            // 应用护甲提升
            if (human_LevelNum[id, 1] > 0)
            {
                int currentArmor = ZombieLevelBridge.GetPlayerArmor(id);
                ZombieLevelBridge.SetPlayerArmor(id, currentArmor + (ZombieLevelConfig.ArmorPerLevel * human_LevelNum[id, 1]));
            }

            // 应用重力降低
            if (human_LevelNum[id, 2] > 0)
            {
                float newGravity = 1.0f - (ZombieLevelConfig.GravityReductionPerLevel * human_LevelNum[id, 2]);
                ZombieLevelBridge.SetPlayerGravity(id, newGravity);
            }

            // 应用速度提升
            if (human_LevelNum[id, 3] > 0)
            {
                float newSpeed = 250.0f * (1.0f + ZombieLevelConfig.SpeedPerLevel * human_LevelNum[id, 3]);
                ZombieLevelBridge.SetPlayerMaxSpeed(id, newSpeed);
            }

            // 应用隐身之术
            if (human_LevelNum[id, 4] > 0)
            {
                int alpha = 255 - (int)(ZombieLevelConfig.InvisibilityPerLevel * 255 * human_LevelNum[id, 4]);
                ZombieLevelBridge.SetPlayerRenderMode(id, alpha);
            }

            // 设置手雷补给
            if (human_LevelNum[id, 9] > 0)
            {
                // 定时补给手雷 - 使用控制台命令
                string cmd = $"give_item {id} weapon_hegrenade";
                ZombieLevelBridge.ClientCommand(id, cmd);
            }

            // 设置神龟冲击波
            if (human_LevelNum[id, 11] > 0)
            {
                g_explosion[id] = true;
            }
        }

        /// <summary>
        /// 设置僵尸技能
        /// </summary>
        private static void SetZombieSkills(int id)
        {
            if (!IsPlayerValid(id)) return;

            // 应用血量提升
            if (zb_LevelNum[id, 0] > 0)
            {
                int currentHealth = ZombieLevelBridge.GetPlayerHealth(id);
                ZombieLevelBridge.SetPlayerHealth(id, currentHealth + (ZombieLevelConfig.HealthPerLevel * 5 * zb_LevelNum[id, 0]));
            }

            // 应用重力降低
            if (zb_LevelNum[id, 1] > 0)
            {
                float newGravity = 1.0f - (ZombieLevelConfig.GravityReductionPerLevel * zb_LevelNum[id, 1]);
                ZombieLevelBridge.SetPlayerGravity(id, newGravity);
            }

            // 应用速度提升
            if (zb_LevelNum[id, 2] > 0)
            {
                float newSpeed = 250.0f * (1.0f + ZombieLevelConfig.SpeedPerLevel * zb_LevelNum[id, 2]);
                ZombieLevelBridge.SetPlayerMaxSpeed(id, newSpeed);
            }

            // 应用隐身之术
            if (zb_LevelNum[id, 3] > 0)
            {
                int alpha = 255 - (int)(ZombieLevelConfig.InvisibilityPerLevel * 255 * zb_LevelNum[id, 3]);
                ZombieLevelBridge.SetPlayerRenderMode(id, alpha);
            }

            // 应用破防之爪
            if (zb_LevelNum[id, 4] > 0)
            {
                // 破防之爪效果在伤害处理中实现
            }

            // 应用多重跳跃
            if (zb_LevelNum[id, 5] > 0)
            {
                // 多重跳跃效果在跳跃逻辑中实现
            }

            // 应用抗武器击退
            if (zb_LevelNum[id, 6] > 0)
            {
                // 抗击退效果在击退逻辑中实现
            }
        }

        /// <summary>
        /// 处理客户端伤害
        /// </summary>
        private static void HandleClientDamage(int attacker, int victim, int damage, int weapon, int hitplace)
        {
            if (attacker <= 0 || attacker >= 33 || victim <= 0 || victim >= 33)
                return;

            // 检查是否为人类攻击僵尸
            bool attackerIsHuman = !ZombieLevelBridge.IsPlayerZombie(attacker);
            bool victimIsZombie = ZombieLevelBridge.IsPlayerZombie(victim);

            if (attackerIsHuman && victimIsZombie)
            {
                // 致命一击技能
                if (human_LevelNum[attacker, 7] > 0)
                {
                    if (new Random().Next(0, 100) == 100)
                    {
                        int criticalDamage = (int)(damage * SKILLEFFECTHEALTH * human_LevelNum[attacker, 7]);
                        ZombieLevelBridge.ShowScreenFade(attacker, 0.2f, 0x0000, 150, 20, 20, 130, 255);
                        ZombieLevelBridge.ShowMessage(attacker, $"技能 {human_szLevelName[7]} 发动，造成 {criticalDamage} 伤害");
                    }
                }

                // 嗜血一击技能
                if (human_LevelNum[attacker, 8] > 0)
                {
                    if (new Random().Next(0, 100) == 100)
                    {
                        int healAmount = (int)(damage * SKILLGAINHEALTH * human_LevelNum[attacker, 8]);
                        int currentHealth = g_fn_GetPlayerHealth(attacker);
                        g_fn_SetPlayerHealth(attacker, currentHealth + healAmount);
                        ZombieLevelBridge.ShowMessage(attacker, $"技能 {human_szLevelName[8]} 发动，恢复 {healAmount} 生命值");
                    }
                }

                // 经验获取
                if (g_Level[attacker] < g_MaxLevel)
                {
                    float multiplier = g_levelSave[attacker] ? SAVEEXEGAIN : NOSAVEEXEGAIN;
                    AddExperience(attacker, (int)(damage * 5 * multiplier));
                }
            }
        }

        /// <summary>
        /// 处理客户端死亡
        /// </summary>
        private static void HandleClientDeath(int killer, int victim)
        {
            if (killer <= 0 || killer >= 33 || victim <= 0 || victim >= 33)
                return;

            // 检查是否为人类杀死僵尸
            bool killerIsHuman = !ZombieLevelBridge.IsPlayerZombie(killer);
            bool victimIsZombie = ZombieLevelBridge.IsPlayerZombie(victim);

            if (killerIsHuman && victimIsZombie && EXEGAIN == 2 && g_Level[killer] < g_MaxLevel)
            {
                float multiplier = g_levelSave[killer] ? SAVEEXEGAIN : NOSAVEEXEGAIN;
                AddExperience(killer, (int)(EXEGAIN2EXE * 5 * multiplier));
            }
        }

        /// <summary>
        /// 处理客户端预思考
        /// </summary>
        private static void HandleClientPreThink(int id)
        {
            if (!IsPlayerValid(id)) return;

            // 检查升级
            CheckLevelUp(id);

            // 显示HUD
            ShowLevelHud(id);
        }

        /// <summary>
        /// 检查等级提升
        /// </summary>
        private static void CheckLevelUp(int id)
        {
            if (g_Level[id] >= g_MaxLevel) return;

            if (g_LevelExe[id] >= g_LevelTotalExe[id])
            {
                g_LevelExe[id] -= g_LevelTotalExe[id];
                g_Level[id]++;
                g_LevelTotalExe[id] = ZombieLevelConfig.GetExperienceRequiredForLevel(g_Level[id]);
                g_LevelPoint[id]++;
                zb_LevelPoint[id]++;

                ZombieLevelBridge.ShowMessage(id, $"恭喜升级到 {g_Level[id]} 级！获得1技能点");
                
                // 保存玩家数据
                SavePlayerData(id);
            }
        }

        /// <summary>
        /// 添加经验
        /// </summary>
        private static void AddExperience(int id, int amount)
        {
            g_LevelExe[id] += amount;
        }

        /// <summary>
        /// 显示主菜单
        /// </summary>
        private static void ShowMainMenu(int id)
        {
            // 这里应该实现菜单系统
            // 由于AMXX菜单系统较复杂，这里简化为显示信息
            ZombieLevelBridge.ShowMessage(id, "=== 僵尸等级系统 ===");
            ZombieLevelBridge.ShowMessage(id, $"等级: {g_Level[id]}/{g_MaxLevel}");
            ZombieLevelBridge.ShowMessage(id, $"经验: {g_LevelExe[id]}/{g_LevelTotalExe[id]}");
            ZombieLevelBridge.ShowMessage(id, $"技能点: {g_LevelPoint[id]}");
            
            if (!ZombieLevelBridge.IsPlayerZombie(id))
            {
                ShowHumanSkillMenu(id);
            }
            else
            {
                ShowZombieSkillMenu(id);
            }
        }

        /// <summary>
        /// 显示人类技能菜单
        /// </summary>
        private static void ShowHumanSkillMenu(int id)
        {
            ZombieLevelBridge.ShowMessage(id, "=== 人类技能 ===");
            for (int i = 0; i < HUMAN_SKILLNUM; i++)
            {
                if (human_LevelNum[id, i] < human_LevelSkillMAX[i])
                {
                    ZombieLevelBridge.ShowMessage(id, 
                        $"{i + 1}. {human_szLevelName[i]} - 等级: {human_LevelNum[id, i]}/{human_LevelSkillMAX[i]} - 需要: {human_LevelSkillPonit[i]}点");
                }
            }
        }

        /// <summary>
        /// 显示僵尸技能菜单
        /// </summary>
        private static void ShowZombieSkillMenu(int id)
        {
            ZombieLevelBridge.ShowMessage(id, "=== 僵尸技能 ===");
            for (int i = 0; i < ZB_SKILLNUM; i++)
            {
                if (zb_LevelNum[id, i] < zb_LevelSkillMAX[i])
                {
                    ZombieLevelBridge.ShowMessage(id, 
                        $"{i + 1}. {zb_szLevelName[i]} - 等级: {zb_LevelNum[id, i]}/{zb_LevelSkillMAX[i]} - 需要: {zb_LevelSkillPonit[i]}点");
                }
            }
        }

        /// <summary>
        /// 升级人类技能
        /// </summary>
        public static void UpgradeHumanSkill(int id, int skillIndex)
        {
            if (skillIndex < 0 || skillIndex >= HUMAN_SKILLNUM) return;
            if (human_LevelNum[id, skillIndex] >= human_LevelSkillMAX[skillIndex]) return;
            if (g_LevelPoint[id] < human_LevelSkillPonit[skillIndex]) return;

            human_LevelNum[id, skillIndex]++;
            g_LevelPoint[id] -= human_LevelSkillPonit[skillIndex];

            ApplyHumanSkillEffect(id, skillIndex);
            
            ZombieLevelBridge.ShowMessage(id, 
                $"{human_szLevelName[skillIndex]} 升级到 {human_LevelNum[id, skillIndex]} 级！");
        }

        /// <summary>
        /// 升级僵尸技能
        /// </summary>
        public static void UpgradeZombieSkill(int id, int skillIndex)
        {
            if (skillIndex < 0 || skillIndex >= ZB_SKILLNUM) return;
            if (zb_LevelNum[id, skillIndex] >= zb_LevelSkillMAX[skillIndex]) return;
            if (zb_LevelPoint[id] < zb_LevelSkillPonit[skillIndex]) return;

            zb_LevelNum[id, skillIndex]++;
            zb_LevelPoint[id] -= zb_LevelSkillPonit[skillIndex];

            ApplyZombieSkillEffect(id, skillIndex);
            
            ZombieLevelBridge.ShowMessage(id, 
                $"{zb_szLevelName[skillIndex]} 升级到 {zb_LevelNum[id, skillIndex]} 级！");
        }

        /// <summary>
        /// 应用人类技能效果
        /// </summary>
        private static void ApplyHumanSkillEffect(int id, int skillIndex)
        {
            switch (skillIndex)
            {
                case 0: // 血量提升
                    int currentHealth = ZombieLevelBridge.GetPlayerHealth(id);
                    ZombieLevelBridge.SetPlayerHealth(id, currentHealth + (int)ZombieLevelConfig.HealthPerLevel);
                    break;
                case 1: // 护甲提升
                    int currentArmor = ZombieLevelBridge.GetPlayerArmor(id);
                    ZombieLevelBridge.SetPlayerArmor(id, currentArmor + (int)ZombieLevelConfig.ArmorPerLevel);
                    break;
                case 2: // 重力降低
                    float currentGravity = ZombieLevelBridge.GetPlayerGravity(id);
                    ZombieLevelBridge.SetPlayerGravity(id, currentGravity - (ZombieLevelConfig.GravityReductionPerLevel / 1000.0f));
                    break;
                case 3: // 速度提升
                    float currentSpeed = ZombieLevelBridge.GetPlayerMaxSpeed(id);
                    ZombieLevelBridge.SetPlayerMaxSpeed(id, currentSpeed + ZombieLevelConfig.SpeedPerLevel);
                    break;
                case 4: // 隐身之术
                    int alpha = 255 - (int)(ZombieLevelConfig.InvisibilityPerLevel * 255 * human_LevelNum[id, 4]);
                    ZombieLevelBridge.SetPlayerRenderMode(id, alpha);
                    break;
                case 9: // 手雷补给
                    // 设置定时补给
                    break;
                case 11: // 神龟冲击波
                    g_explosion[id] = true;
                    break;
            }
        }

        /// <summary>
        /// 应用僵尸技能效果
        /// </summary>
        private static void ApplyZombieSkillEffect(int id, int skillIndex)
        {
            switch (skillIndex)
            {
                case 0: // 血量提升
                    int currentHealth = ZombieLevelBridge.GetPlayerHealth(id);
                    ZombieLevelBridge.SetPlayerHealth(id, currentHealth + (int)ZombieLevelConfig.ZombieHealthPerLevel);
                    break;
                case 1: // 重力降低
                    float currentGravity = ZombieLevelBridge.GetPlayerGravity(id);
                    ZombieLevelBridge.SetPlayerGravity(id, currentGravity - (ZombieLevelConfig.GravityReductionPerLevel / 1000.0f));
                    break;
                case 2: // 速度提升
                    float currentSpeed = ZombieLevelBridge.GetPlayerMaxSpeed(id);
                    ZombieLevelBridge.SetPlayerMaxSpeed(id, currentSpeed + ZombieLevelConfig.SpeedPerLevel);
                    break;
                case 3: // 隐身之术
                    int alpha = 255 - (int)(ZombieLevelConfig.InvisibilityPerLevel * 255 * zb_LevelNum[id, 3]);
                    ZombieLevelBridge.SetPlayerRenderMode(id, alpha);
                    break;
            }
        }

        /// <summary>
        /// 重置玩家等级
        /// </summary>
        public static void ResetPlayerLevel(int id)
        {
            g_LevelExe[id] = 0;
            g_LevelTotalExe[id] = 1000;
            g_Level[id] = 1;
            g_LevelPoint[id] = 0;
            zb_LevelPoint[id] = 0;
            g_PD[id] = true;
            g_levelSave[id] = false;
            g_taskid[id] = false;

            // 重置技能
            for (int i = 0; i < HUMAN_SKILLNUM; i++)
                human_LevelNum[id, i] = 0;
            for (int i = 0; i < ZB_SKILLNUM; i++)
                zb_LevelNum[id, i] = 0;

            SavePlayerData(id);
            ZombieLevelBridge.ShowMessage(id, "等级设置已全部重置");
        }

        /// <summary>
        /// 显示等级HUD
        /// </summary>
        private static void ShowLevelHud(int id)
        {
            if (!IsPlayerValid(id)) return;

            string message = $"等级: {g_Level[id]}/{g_MaxLevel} 经验: {g_LevelExe[id]}/{g_LevelTotalExe[id]} 技能点: {g_LevelPoint[id]}";
            
            // 使用控制台命令显示HUD消息
            string cmd = $"hudmessage {id} \"{message}\"";
            ZombieLevelBridge.ClientCommand(id, cmd);
        }

        /// <summary>
        /// 加载玩家经验数据
        /// </summary>
        private static int LoadPlayerExperience(int id)
        {
            if (!IsPlayerValid(id)) return 0;

            string playerName = ZombieLevelBridge.GetPlayerName(id);
            string fileContent = ZombieLevelBridge.ReadFile(SAVE_FILE);
            
            if (string.IsNullOrEmpty(fileContent)) return 0;

            string[] lines = fileContent.Split('\n');
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";")) continue;
                
                string[] parts = line.Split('`');
                if (parts.Length >= 2 && parts[0] == playerName)
                {
                    if (int.TryParse(parts[1], out int experience))
                    {
                        return experience;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// 保存玩家数据
        /// </summary>
        private static void SavePlayerData(int id)
        {
            if (!IsPlayerValid(id) || ZombieLevelBridge.IsPlayerBot(id)) return;

            string playerName = ZombieLevelBridge.GetPlayerName(id);
            string data = $"{playerName}`{g_LevelExe[id]}";
            
            ZombieLevelBridge.WriteFile(SAVE_FILE, data, true);
        }

        /// <summary>
        /// 加载所有玩家数据
        /// </summary>
        private static void LoadPlayerData()
        {
            // 在插件启动时加载数据
        }

        /// <summary>
        /// 获取玩家等级
        /// </summary>
        public static int GetPlayerLevel(int playerId, bool isZombie = false)
        {
            if (playerId <= 0 || playerId >= 33) return 0;
            
            if (isZombie)
            {
                return zb_LevelNum[playerId, 0]; // 返回僵尸基础等级
            }
            else
            {
                return human_LevelNum[playerId, 0]; // 返回人类基础等级
            }
        }

        /// <summary>
        /// 获取玩家经验值
        /// </summary>
        public static int GetPlayerExperience(int playerId, bool isZombie = false)
        {
            if (playerId <= 0 || playerId >= 33) return 0;
            
            if (isZombie)
            {
                return zb_Experience[playerId];
            }
            else
            {
                return human_Experience[playerId];
            }
        }

        /// <summary>
        /// 检查玩家是否有效
        /// </summary>
        private static bool IsPlayerValid(int id)
        {
            return id > 0 && id < 33 && ZombieLevelBridge.IsPlayerValid(id);
        }
    }
}