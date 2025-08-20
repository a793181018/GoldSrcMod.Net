using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.CStrike
{
    /// <summary>
    /// Counter-Strike游戏功能桥接类
    /// 提供对CS1.6游戏引擎的直接访问
    /// </summary>
    public static class CStrikeBridge
    {
        private const string DllName = "cstrike_bridge";

        #region Player API

        /// <summary>
        /// 获取玩家金钱
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>玩家当前金钱数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetUserMoney(int playerIndex);

        /// <summary>
        /// 设置玩家金钱
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="money">要设置的金钱数量</param>
        /// <param name="flash">是否显示更新提示(默认1)</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserMoney(int playerIndex, int money, int flash = 1);

        /// <summary>
        /// 获取玩家护甲值
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>玩家当前护甲值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetUserArmor(int playerIndex);

        /// <summary>
        /// 设置玩家护甲值
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="armor">要设置的护甲值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserArmor(int playerIndex, int armor);

        /// <summary>
        /// 获取玩家队伍
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>队伍ID (1=T, 2=CT, 3=SPEC)</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetUserTeam(int playerIndex);

        /// <summary>
        /// 设置玩家队伍
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="team">队伍ID (1=T, 2=CT, 3=SPEC)</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserTeam(int playerIndex, int team);

        /// <summary>
        /// 获取玩家VIP状态
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>是否为VIP</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetUserVip(int playerIndex);

        /// <summary>
        /// 设置玩家VIP状态
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="vip">VIP状态</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserVip(int playerIndex, [MarshalAs(UnmanagedType.Bool)] bool vip);

        /// <summary>
        /// 获取玩家死亡次数
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>死亡次数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetUserDeaths(int playerIndex);

        /// <summary>
        /// 设置玩家死亡次数
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="deaths">死亡次数</param>
        /// <param name="updateScoreboard">是否更新计分板</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserDeaths(int playerIndex, int deaths, [MarshalAs(UnmanagedType.Bool)] bool updateScoreboard = true);

        #endregion

        #region Weapon API

        /// <summary>
        /// 获取武器ID
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <returns>武器ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetWeaponId(int weaponEntity);

        /// <summary>
        /// 获取武器消音器状态
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <returns>是否装备消音器</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetWeaponSilenced(int weaponEntity);

        /// <summary>
        /// 设置武器消音器状态
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <param name="silenced">消音器状态</param>
        /// <param name="drawAnimation">是否播放动画</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetWeaponSilenced(int weaponEntity, [MarshalAs(UnmanagedType.Bool)] bool silenced, int drawAnimation = 1);

        /// <summary>
        /// 获取武器连发模式
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <returns>是否为连发模式</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetWeaponBurstMode(int weaponEntity);

        /// <summary>
        /// 设置武器连发模式
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <param name="burstMode">连发模式</param>
        /// <param name="drawAnimation">是否播放动画</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetWeaponBurstMode(int weaponEntity, [MarshalAs(UnmanagedType.Bool)] bool burstMode, int drawAnimation = 1);

        /// <summary>
        /// 获取武器弹药数量
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <returns>弹药数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetWeaponAmmo(int weaponEntity);

        /// <summary>
        /// 设置武器弹药数量
        /// </summary>
        /// <param name="weaponEntity">武器实体索引</param>
        /// <param name="ammo">弹药数量</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetWeaponAmmo(int weaponEntity, int ammo);

        #endregion

        #region Game State API

        /// <summary>
        /// 检查玩家是否在购物区域
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>是否在购物区域</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetUserInsideBuyzone(int playerIndex);

        /// <summary>
        /// 获取玩家地图区域
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>地图区域标志</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetUserMapzones(int playerIndex);

        /// <summary>
        /// 检查玩家是否有主武器
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>是否有主武器</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetUserHasPrimary(int playerIndex);

        /// <summary>
        /// 检查玩家是否有拆弹器
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>是否有拆弹器</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetUserDefusekit(int playerIndex);

        /// <summary>
        /// 设置玩家拆弹器状态
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="hasKit">是否有拆弹器</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserDefusekit(int playerIndex, [MarshalAs(UnmanagedType.Bool)] bool hasKit);

        /// <summary>
        /// 检查玩家是否有夜视镜
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <returns>是否有夜视镜</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetUserNvg(int playerIndex);

        /// <summary>
        /// 设置玩家夜视镜状态
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="hasNvg">是否有夜视镜</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserNvg(int playerIndex, [MarshalAs(UnmanagedType.Bool)] bool hasNvg);

        #endregion

        #region Model API

        /// <summary>
        /// 获取玩家模型
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="buffer">模型名称缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsGetUserModel(int playerIndex, [Out] StringBuilder buffer, int bufferSize);

        /// <summary>
        /// 设置玩家模型
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        /// <param name="model">模型名称</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetUserModel(int playerIndex, [MarshalAs(UnmanagedType.LPStr)] string model);

        /// <summary>
        /// 重置玩家模型为默认
        /// </summary>
        /// <param name="playerIndex">玩家索引(1-32)</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsResetUserModel(int playerIndex);

        #endregion

        #region Entity API

        /// <summary>
        /// 创建游戏实体
        /// </summary>
        /// <param name="className">实体类名</param>
        /// <returns>实体索引</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsCreateEntity([MarshalAs(UnmanagedType.LPStr)] string className);

        /// <summary>
        /// 按类名查找实体
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="className">实体类名</param>
        /// <returns>实体索引</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsFindEntityByClass(int startIndex, [MarshalAs(UnmanagedType.LPStr)] string className);

        /// <summary>
        /// 按所有者查找实体
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="ownerIndex">所有者玩家索引</param>
        /// <returns>实体索引</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsFindEntityByOwner(int startIndex, int ownerIndex);

        /// <summary>
        /// 设置实体类名
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">新的类名</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsSetEntityClass(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string className);

        #endregion

        #region Item API

        /// <summary>
        /// 通过物品名称获取物品ID
        /// </summary>
        /// <param name="itemName">物品名称</param>
        /// <returns>物品ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetItemId([MarshalAs(UnmanagedType.LPStr)] string itemName);

        /// <summary>
        /// 获取物品别名
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <param name="buffer">别名缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetItemAlias(int itemId, [Out] StringBuilder buffer, int bufferSize);

        /// <summary>
        /// 获取物品翻译后的别名
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <param name="buffer">别名缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetTranslatedItemAlias(int itemId, [Out] StringBuilder buffer, int bufferSize);

        #endregion

        #region Hostage API

        /// <summary>
        /// 获取人质ID
        /// </summary>
        /// <param name="hostageIndex">人质索引</param>
        /// <returns>人质ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CsGetHostageId(int hostageIndex);

        /// <summary>
        /// 获取人质跟随状态
        /// </summary>
        /// <param name="hostageIndex">人质索引</param>
        /// <returns>是否跟随</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetHostageFollow(int hostageIndex);

        /// <summary>
        /// 设置人质跟随状态
        /// </summary>
        /// <param name="hostageIndex">人质索引</param>
        /// <param name="follow">是否跟随</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetHostageFollow(int hostageIndex, [MarshalAs(UnmanagedType.Bool)] bool follow);

        #endregion

        #region Bomb API

        /// <summary>
        /// 获取C4爆炸时间
        /// </summary>
        /// <returns>爆炸时间(秒)</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float CsGetC4ExplodeTime();

        /// <summary>
        /// 设置C4爆炸时间
        /// </summary>
        /// <param name="time">爆炸时间(秒)</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetC4ExplodeTime(float time);

        /// <summary>
        /// 获取C4拆弹状态
        /// </summary>
        /// <returns>是否在拆弹</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CsGetC4Defusing();

        /// <summary>
        /// 设置C4拆弹状态
        /// </summary>
        /// <param name="defusing">拆弹状态</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsSetC4Defusing([MarshalAs(UnmanagedType.Bool)] bool defusing);

        #endregion

        #region Event Callbacks

        // 委托定义
        public delegate void CsPlayerDeathDelegate(int victim, int killer, int weapon);
        public delegate void CsBombEventDelegate(int eventType, int player);
        public delegate void CsWeaponPickupDelegate(int player, int weaponId);

        /// <summary>
        /// 注册玩家死亡事件回调
        /// </summary>
        /// <param name="callback">回调函数</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsRegisterPlayerDeathCallback(CsPlayerDeathDelegate callback);

        /// <summary>
        /// 注册炸弹事件回调
        /// </summary>
        /// <param name="callback">回调函数</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsRegisterBombEventCallback(CsBombEventDelegate callback);

        /// <summary>
        /// 注册武器拾取事件回调
        /// </summary>
        /// <param name="callback">回调函数</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsRegisterWeaponPickupCallback(CsWeaponPickupDelegate callback);

        #endregion

        #region Helper Methods

        /// <summary>
        /// 获取玩家模型名称
        /// </summary>
        /// <param name="playerIndex">玩家索引</param>
        /// <returns>模型名称</returns>
        public static string GetUserModel(int playerIndex)
        {
            var sb = new StringBuilder(256);
            CsGetUserModel(playerIndex, sb, sb.Capacity);
            return sb.ToString();
        }

        /// <summary>
        /// 获取物品别名
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>物品别名</returns>
        public static string GetItemAlias(int itemId)
        {
            var sb = new StringBuilder(64);
            return CsGetItemAlias(itemId, sb, sb.Capacity) ? sb.ToString() : string.Empty;
        }

        /// <summary>
        /// 获取物品翻译别名
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>翻译后的别名</returns>
        public static string GetTranslatedItemAlias(int itemId)
        {
            var sb = new StringBuilder(64);
            return CsGetTranslatedItemAlias(itemId, sb, sb.Capacity) ? sb.ToString() : string.Empty;
        }

        #endregion
    }
}