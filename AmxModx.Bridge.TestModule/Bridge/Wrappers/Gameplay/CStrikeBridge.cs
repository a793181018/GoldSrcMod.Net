using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.Wrappers.Gameplay
{
    /// <summary>
    /// Counter-Strike桥接类，提供CS1.6游戏功能的简化API访问
    /// </summary>
    public static class CStrikeBridge
    {
        #region Player Management

        /// <summary>
        /// 初始化CStrike桥接
        /// </summary>
        /// <returns>初始化结果，0表示成功</returns>
        public static int AmxModx_Bridge_CStrikeInit()
        {
            try
            {
                Console.WriteLine("[CStrikeBridge] Counter-Strike桥接初始化");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 初始化失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 清理CStrike桥接资源
        /// </summary>
        public static void AmxModx_Bridge_CStrikeCleanup()
        {
            try
            {
                Console.WriteLine("[CStrikeBridge] Counter-Strike桥接清理");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 清理失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取最大玩家数
        /// </summary>
        /// <returns>最大玩家数</returns>
        public static int AmxModx_Bridge_GetMaxPlayers()
        {
            try
            {
                return 32; // CS1.6默认最大玩家数
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取最大玩家数失败: {ex.Message}");
                return 32;
            }
        }

        /// <summary>
        /// 获取玩家金钱
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>玩家金钱</returns>
        public static int AmxModx_Bridge_GetPlayerMoney(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserMoney(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取玩家金钱失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 设置玩家金钱
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="money">金钱数量</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerMoney(int playerIndex, int money)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserMoney(playerIndex, money);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家金钱失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取玩家护甲值
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>护甲值</returns>
        public static int AmxModx_Bridge_GetPlayerArmor(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserArmor(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取玩家护甲值失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 设置玩家护甲值
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="armor">护甲值</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerArmor(int playerIndex, int armor)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserArmor(playerIndex, armor);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家护甲值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取玩家队伍
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>队伍ID (1=T, 2=CT, 3=SPEC)</returns>
        public static int AmxModx_Bridge_GetPlayerTeam(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserTeam(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取玩家队伍失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 设置玩家队伍
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="team">队伍ID</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerTeam(int playerIndex, int team)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserTeam(playerIndex, team);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家队伍失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查玩家是否为VIP
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>是否为VIP</returns>
        public static bool AmxModx_Bridge_IsPlayerVip(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserVip(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 检查玩家VIP状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置玩家VIP状态
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="isVip">VIP状态</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerVip(int playerIndex, bool isVip)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserVip(playerIndex, isVip);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家VIP状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取玩家死亡次数
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>死亡次数</returns>
        public static int AmxModx_Bridge_GetPlayerDeaths(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserDeaths(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取玩家死亡次数失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 设置玩家死亡次数
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="deaths">死亡次数</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerDeaths(int playerIndex, int deaths)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserDeaths(playerIndex, deaths, true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家死亡次数失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Weapon Management

        /// <summary>
        /// 获取武器名称
        /// </summary>
        /// <param name="weaponId">武器ID</param>
        /// <returns>武器名称</returns>
        public static string AmxModx_Bridge_GetWeaponName(int weaponId)
        {
            try
            {
                // 根据武器ID返回对应的武器名称
                return weaponId switch
                {
                    1 => "weapon_p228",
                    2 => "weapon_scout",
                    3 => "weapon_hegrenade",
                    4 => "weapon_xm1014",
                    5 => "weapon_c4",
                    6 => "weapon_mac10",
                    7 => "weapon_aug",
                    8 => "weapon_smokegrenade",
                    9 => "weapon_elite",
                    10 => "weapon_fiveseven",
                    11 => "weapon_ump45",
                    12 => "weapon_sg550",
                    13 => "weapon_galil",
                    14 => "weapon_famas",
                    15 => "weapon_usp",
                    16 => "weapon_glock18",
                    17 => "weapon_awp",
                    18 => "weapon_mp5navy",
                    19 => "weapon_m249",
                    20 => "weapon_m3",
                    21 => "weapon_m4a1",
                    22 => "weapon_tmp",
                    23 => "weapon_g3sg1",
                    24 => "weapon_flashbang",
                    25 => "weapon_deagle",
                    26 => "weapon_sg552",
                    27 => "weapon_ak47",
                    28 => "weapon_knife",
                    29 => "weapon_p90",
                    _ => "weapon_unknown"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取武器名称失败: {ex.Message}");
                return "weapon_unknown";
            }
        }

        /// <summary>
        /// 获取武器ID
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <returns>武器ID</returns>
        public static int AmxModx_Bridge_GetWeaponId(int weaponEntity)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetWeaponId(weaponEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取武器ID失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 检查武器是否装备消音器
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <returns>是否装备消音器</returns>
        public static bool AmxModx_Bridge_IsWeaponSilenced(int weaponEntity)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetWeaponSilenced(weaponEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 检查武器消音器状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置武器消音器状态
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <param name="silenced">消音器状态</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetWeaponSilenced(int weaponEntity, bool silenced)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetWeaponSilenced(weaponEntity, silenced);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置武器消音器状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取武器弹药数量
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <returns>弹药数量</returns>
        public static int AmxModx_Bridge_GetWeaponAmmo(int weaponEntity)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetWeaponAmmo(weaponEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取武器弹药数量失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 设置武器弹药数量
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <param name="ammo">弹药数量</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetWeaponAmmo(int weaponEntity, int ammo)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetWeaponAmmo(weaponEntity, ammo);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置武器弹药数量失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Game State

        /// <summary>
        /// 检查玩家是否在购物区域
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>是否在购物区域</returns>
        public static bool AmxModx_Bridge_IsPlayerInBuyzone(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserInsideBuyzone(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 检查玩家购物区域状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查玩家是否有主武器
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>是否有主武器</returns>
        public static bool AmxModx_Bridge_HasPlayerPrimaryWeapon(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserHasPrimary(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 检查玩家主武器状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查玩家是否有拆弹器
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>是否有拆弹器</returns>
        public static bool AmxModx_Bridge_HasPlayerDefusekit(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserDefusekit(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 检查玩家拆弹器状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置玩家拆弹器状态
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="hasKit">拆弹器状态</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerDefusekit(int playerIndex, bool hasKit)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserDefusekit(playerIndex, hasKit);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家拆弹器状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查玩家是否有夜视镜
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>是否有夜视镜</returns>
        public static bool AmxModx_Bridge_HasPlayerNightvision(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetUserNvg(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 检查玩家夜视镜状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置玩家夜视镜状态
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="hasNvg">夜视镜状态</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerNightvision(int playerIndex, bool hasNvg)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserNvg(playerIndex, hasNvg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家夜视镜状态失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Entity Management

        /// <summary>
        /// 创建游戏实体
        /// </summary>
        /// <param name="className">实体类名</param>
        /// <returns>实体索引</returns>
        public static int AmxModx_Bridge_CreateEntity(string className)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsCreateEntity(className);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 创建实体失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 按类名查找实体
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="className">实体类名</param>
        /// <returns>实体索引</returns>
        public static int AmxModx_Bridge_FindEntityByClass(int startIndex, string className)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsFindEntityByClass(startIndex, className);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 查找实体失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 按所有者查找实体
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="ownerIndex">所有者索引</param>
        /// <returns>实体索引</returns>
        public static int AmxModx_Bridge_FindEntityByOwner(int startIndex, int ownerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsFindEntityByOwner(startIndex, ownerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 查找实体失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 设置实体类名
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">实体类名</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetEntityClass(int entityIndex, string className)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsSetEntityClass(entityIndex, className);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置实体类名失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Item Management

        /// <summary>
        /// 通过物品名称获取物品ID
        /// </summary>
        /// <param name="itemName">物品名称</param>
        /// <returns>物品ID</returns>
        public static int AmxModx_Bridge_GetItemId(string itemName)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetItemId(itemName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取物品ID失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 获取物品别名
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>物品别名</returns>
        public static string AmxModx_Bridge_GetItemAlias(int itemId)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.GetItemAlias(itemId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取物品别名失败: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取物品翻译别名
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>翻译后的别名</returns>
        public static string AmxModx_Bridge_GetTranslatedItemAlias(int itemId)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.GetTranslatedItemAlias(itemId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取物品翻译别名失败: {ex.Message}");
                return string.Empty;
            }
        }

        #endregion

        #region Hostage Management

        /// <summary>
        /// 获取人质ID
        /// </summary>
        /// <param name="hostageIndex">人质索引</param>
        /// <returns>人质ID</returns>
        public static int AmxModx_Bridge_GetHostageId(int hostageIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetHostageId(hostageIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取人质ID失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 获取人质跟随状态
        /// </summary>
        /// <param name="hostageIndex">人质索引</param>
        /// <returns>是否跟随</returns>
        public static bool AmxModx_Bridge_IsHostageFollowing(int hostageIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetHostageFollow(hostageIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取人质跟随状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置人质跟随状态
        /// </summary>
        /// <param name="hostageIndex">人质索引</param>
        /// <param name="follow">跟随状态</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetHostageFollow(int hostageIndex, bool follow)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetHostageFollow(hostageIndex, follow);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置人质跟随状态失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Bomb Management

        /// <summary>
        /// 获取C4爆炸时间
        /// </summary>
        /// <returns>爆炸时间(秒)</returns>
        public static float AmxModx_Bridge_GetC4ExplodeTime()
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetC4ExplodeTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取C4爆炸时间失败: {ex.Message}");
                return 0.0f;
            }
        }

        /// <summary>
        /// 设置C4爆炸时间
        /// </summary>
        /// <param name="time">爆炸时间(秒)</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetC4ExplodeTime(float time)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetC4ExplodeTime(time);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置C4爆炸时间失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取C4拆弹状态
        /// </summary>
        /// <returns>是否在拆弹</returns>
        public static bool AmxModx_Bridge_IsC4Defusing()
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.CsGetC4Defusing();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取C4拆弹状态失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置C4拆弹状态
        /// </summary>
        /// <param name="defusing">拆弹状态</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetC4Defusing(bool defusing)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetC4Defusing(defusing);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置C4拆弹状态失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Model Management

        /// <summary>
        /// 获取玩家模型名称
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>模型名称</returns>
        public static string AmxModx_Bridge_GetPlayerModel(int playerIndex)
        {
            try
            {
                return AmxModx.Bridge.CStrike.CStrikeBridge.GetUserModel(playerIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 获取玩家模型失败: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 设置玩家模型
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <param name="model">模型名称</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_SetPlayerModel(int playerIndex, string model)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsSetUserModel(playerIndex, model);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 设置玩家模型失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 重置玩家模型为默认
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>是否成功</returns>
        public static bool AmxModx_Bridge_ResetPlayerModel(int playerIndex)
        {
            try
            {
                AmxModx.Bridge.CStrike.CStrikeBridge.CsResetUserModel(playerIndex);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CStrikeBridge] 重置玩家模型失败: {ex.Message}");
                return false;
            }
        }

        #endregion
    }
}