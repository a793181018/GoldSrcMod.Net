using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.CStrike;

namespace CStrikeExtensions
{
    /// <summary>
    /// Counter-Strike扩展示例
    /// 展示如何使用C#扩展CS1.6服务端功能
    /// </summary>
    public static class CStrikeExtensionExample
    {
        // 事件处理器
        private static CStrikeBridge.CsPlayerDeathDelegate _playerDeathHandler;
        private static CStrikeBridge.CsBombEventDelegate _bombEventHandler;
        private static CStrikeBridge.CsWeaponPickupDelegate _weaponPickupHandler;

        /// <summary>
        /// 初始化扩展
        /// </summary>
        public static void Initialize()
        {
            Console.WriteLine("[CStrike Extension] Initializing...");

            // 注册事件回调
            _playerDeathHandler = OnPlayerDeath;
            _bombEventHandler = OnBombEvent;
            _weaponPickupHandler = OnWeaponPickup;

            CStrikeBridge.CsRegisterPlayerDeathCallback(_playerDeathHandler);
            CStrikeBridge.CsRegisterBombEventCallback(_bombEventHandler);
            CStrikeBridge.CsRegisterWeaponPickupCallback(_weaponPickupHandler);

            Console.WriteLine("[CStrike Extension] Initialized successfully");
        }

        /// <summary>
        /// 玩家死亡事件处理
        /// </summary>
        /// <param name="victim">受害者索引</param>
        /// <param name="killer">击杀者索引</param>
        /// <param name="weapon">武器ID</param>
        private static void OnPlayerDeath(int victim, int killer, int weapon)
        {
            Console.WriteLine($"[CStrike Extension] Player {victim} was killed by {killer} with weapon {weapon}");
            
            // 示例：给击杀者奖励金钱
            if (killer > 0 && killer <= 32 && killer != victim)
            {
                int currentMoney = CStrikeBridge.CsGetUserMoney(killer);
                CStrikeBridge.CsSetUserMoney(killer, currentMoney + 300);
            }

            // 示例：记录死亡次数
            int deaths = CStrikeBridge.CsGetUserDeaths(victim);
            CStrikeBridge.CsSetUserDeaths(victim, deaths + 1, true);
        }

        /// <summary>
        /// 炸弹事件处理
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="player">玩家索引</param>
        private static void OnBombEvent(int eventType, int player)
        {
            switch (eventType)
            {
                case 0: // 炸弹开始种植
                    Console.WriteLine($"[CStrike Extension] Player {player} started planting bomb");
                    break;
                case 1: // 炸弹种植完成
                    Console.WriteLine($"[CStrike Extension] Player {player} planted the bomb");
                    break;
                case 2: // 炸弹开始拆除
                    Console.WriteLine($"[CStrike Extension] Player {player} started defusing bomb");
                    break;
                case 3: // 炸弹拆除完成
                    Console.WriteLine($"[CStrike Extension] Player {player} defused the bomb");
                    break;
                default:
                    Console.WriteLine($"[CStrike Extension] Unknown bomb event {eventType} from player {player}");
                    break;
            }
        }

        /// <summary>
        /// 武器拾取事件处理
        /// </summary>
        /// <param name="player">玩家索引</param>
        /// <param name="weaponId">武器ID</param>
        private static void OnWeaponPickup(int player, int weaponId)
        {
            Console.WriteLine($"[CStrike Extension] Player {player} picked up weapon {weaponId}");
            
            // 示例：根据武器类型给予奖励
            int reward = weaponId switch
            {
                1 => 100,  // P228
                2 => 150,  // Glock
                3 => 200,  // Scout
                4 => 250,  // HE Grenade
                5 => 300,  // XM1014
                6 => 400,  // C4
                7 => 500,  // MAC-10
                8 => 600,  // AUG
                9 => 700,  // Smoke Grenade
                10 => 800, // Elite
                _ => 50
            };

            if (reward > 0)
            {
                int currentMoney = CStrikeBridge.CsGetUserMoney(player);
                CStrikeBridge.CsSetUserMoney(player, currentMoney + reward);
            }
        }

        /// <summary>
        /// 管理玩家装备
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        public static void ManagePlayerEquipment(int playerIndex)
        {
            if (playerIndex < 1 || playerIndex > 32)
                return;

            // 检查玩家是否有主武器
            bool hasPrimary = CStrikeBridge.CsGetUserHasPrimary(playerIndex);
            if (!hasPrimary)
            {
                Console.WriteLine($"[CStrike Extension] Player {playerIndex} has no primary weapon");
            }

            // 检查护甲
            int armor = CStrikeBridge.CsGetUserArmor(playerIndex);
            if (armor < 100)
            {
                Console.WriteLine($"[CStrike Extension] Player {playerIndex} has low armor: {armor}");
                CStrikeBridge.CsSetUserArmor(playerIndex, 100);
            }

            // 检查夜视镜
            bool hasNvg = CStrikeBridge.CsGetUserNvg(playerIndex);
            if (!hasNvg)
            {
                Console.WriteLine($"[CStrike Extension] Player {playerIndex} has no night vision");
                CStrikeBridge.CsSetUserNvg(playerIndex, true);
            }

            // 检查拆弹器（CT阵营）
            int team = CStrikeBridge.CsGetUserTeam(playerIndex);
            if (team == 2) // CT阵营
            {
                bool hasDefusekit = CStrikeBridge.CsGetUserDefusekit(playerIndex);
                if (!hasDefusekit)
                {
                    Console.WriteLine($"[CStrike Extension] CT player {playerIndex} has no defuse kit");
                    CStrikeBridge.CsSetUserDefusekit(playerIndex, true);
                }
            }
        }

        /// <summary>
        /// 获取物品信息示例
        /// </summary>
        public static void DisplayItemInfo()
        {
            // 获取AK-47的信息
            int ak47Id = CStrikeBridge.CsGetItemId("weapon_ak47");
            if (ak47Id > 0)
            {
                string alias = CStrikeBridge.GetItemAlias(ak47Id);
                string translatedAlias = CStrikeBridge.GetTranslatedItemAlias(ak47Id);
                Console.WriteLine($"[CStrike Extension] AK-47: ID={ak47Id}, Alias={alias}, Translated={translatedAlias}");
            }
        }

        /// <summary>
        /// 清理扩展
        /// </summary>
        public static void Cleanup()
        {
            // 注销事件回调（如果需要）
            Console.WriteLine("[CStrike Extension] Cleaning up...");
        }
    }
}