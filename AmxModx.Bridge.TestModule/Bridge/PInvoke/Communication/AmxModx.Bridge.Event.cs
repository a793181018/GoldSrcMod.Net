using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Event
{
    /// <summary>
    /// 事件标志枚举
    /// </summary>
    [Flags]
    public enum EventFlags
    {
        /// <summary>
        /// 无特殊标志
        /// </summary>
        None = 0,
        
        /// <summary>
        /// 包含世界事件
        /// </summary>
        IncludeWorld = 1,
        
        /// <summary>
        /// 包含客户端事件
        /// </summary>
        IncludeClient = 2,
        
        /// <summary>
        /// 只执行一次
        /// </summary>
        Once = 4,
        
        /// <summary>
        /// 只包含死亡玩家
        /// </summary>
        DeadOnly = 8,
        
        /// <summary>
        /// 只包含存活玩家
        /// </summary>
        AliveOnly = 16,
        
        /// <summary>
        /// 不包含机器人
        /// </summary>
        NoBots = 32,
        
        /// <summary>
        /// 不包含真实玩家
        /// </summary>
        NoPlayers = 64
    }

    /// <summary>
    /// 事件回调委托
    /// </summary>
    /// <param name="eventData">事件数据</param>
    public delegate void EventCallback(EventData eventData);

    /// <summary>
    /// 事件数据类
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; } = string.Empty;
        
        /// <summary>
        /// 触发事件的玩家ID
        /// </summary>
        public int PlayerId { get; set; }
        
        /// <summary>
        /// 事件参数
        /// </summary>
        public object[] Parameters { get; set; } = Array.Empty<object>();
    }

    /// <summary>
    /// 事件系统桥接接口
    /// </summary>
    public static class EventBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 注册游戏事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="callbackFunc">回调函数名</param>
        /// <param name="flags">事件标志</param>
        /// <param name="conditions">事件条件数组</param>
        /// <param name="conditionCount">条件数量</param>
        /// <returns>事件句柄，失败返回0</returns>
        [DllImport(DllName, EntryPoint = "AmxModx_Bridge_RegisterEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterEvent([MarshalAs(UnmanagedType.LPStr)] string eventName, 
            [MarshalAs(UnmanagedType.LPStr)] string callbackFunc, 
            int flags, 
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] conditions, 
            int conditionCount);

        /// <summary>
        /// 扩展注册游戏事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="callbackFunc">回调函数名</param>
        /// <param name="flags">事件标志</param>
        /// <param name="conditions">事件条件数组</param>
        /// <param name="conditionCount">条件数量</param>
        /// <returns>事件句柄，失败返回0</returns>
        [DllImport(DllName, EntryPoint = "AmxModx_Bridge_RegisterEventEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterEventEx([MarshalAs(UnmanagedType.LPStr)] string eventName, 
            [MarshalAs(UnmanagedType.LPStr)] string callbackFunc, 
            int flags, 
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] conditions, 
            int conditionCount);

        /// <summary>
        /// 启用事件
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, EntryPoint = "AmxModx_Bridge_EnableEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern int EnableEvent(int eventHandle);

        /// <summary>
        /// 禁用事件
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, EntryPoint = "AmxModx_Bridge_DisableEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DisableEvent(int eventHandle);

        /// <summary>
        /// 获取事件ID
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns>事件ID，0表示无效事件</returns>
        [DllImport(DllName, EntryPoint = "AmxModx_Bridge_GetEventId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEventId([MarshalAs(UnmanagedType.LPStr)] string eventName);

        /// <summary>
        /// 检查事件是否有效
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <returns>有效返回1，无效返回0</returns>
        [DllImport(DllName, EntryPoint = "AmxModx_Bridge_IsEventValid", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsEventValid(int eventHandle);

        /// <summary>
        /// 注册事件并绑定回调
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="callback">回调函数</param>
        /// <param name="flags">事件标志</param>
        /// <param name="conditions">事件条件</param>
        /// <returns>事件句柄</returns>
        public static int RegisterEventWithCallback(string eventName, EventCallback callback, EventFlags flags = EventFlags.None, params string[] conditions)
        {
            // 注意：由于AMX Mod X的事件系统需要函数名，
            // 这里需要实现一个映射机制来将C#委托映射到内部函数
            // 简化实现：返回句柄，实际回调需要通过其他机制处理
            return RegisterEvent(eventName, "EventCallbackProxy", (int)flags, conditions, conditions?.Length ?? 0);
        }

        /// <summary>
        /// 注册常见游戏事件
        /// </summary>
        public static class GameEvents
        {
            /// <summary>
            /// 玩家死亡事件
            /// </summary>
            public const string PlayerDeath = "DeathMsg";
            
            /// <summary>
            /// 玩家连接事件
            /// </summary>
            public const string PlayerConnect = "PlayerConnect";
            
            /// <summary>
            /// 玩家断开连接事件
            /// </summary>
            public const string PlayerDisconnect = "PlayerDisconnect";
            
            /// <summary>
            /// 玩家重生事件
            /// </summary>
            public const string PlayerSpawn = "Spawn";
            
            /// <summary>
            /// 回合开始事件
            /// </summary>
            public const string RoundStart = "RoundStart";
            
            /// <summary>
            /// 回合结束事件
            /// </summary>
            public const string RoundEnd = "RoundEnd";
            
            /// <summary>
            /// 炸弹安放事件
            /// </summary>
            public const string BombPlanted = "BombPlanted";
            
            /// <summary>
            /// 炸弹爆炸事件
            /// </summary>
            public const string BombExploded = "BombExploded";
            
            /// <summary>
            /// 炸弹拆除事件
            /// </summary>
            public const string BombDefused = "BombDefused";
            
            /// <summary>
            /// 人质救援事件
            /// </summary>
            public const string HostageRescued = "HostageRescued";
        }
    }
}