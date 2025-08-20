using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Event
{
    /// <summary>
    /// 事件系统桥接定义
    /// 提供事件注册和处理功能
    /// </summary>
    public static class NativeMethods
    {
        private const string EventDll = "event_bridge";

        /// <summary>
        /// 注册游戏事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="forceRegistration">强制注册标志</param>
        /// <returns>事件句柄</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RegisterEvent([MarshalAs(UnmanagedType.LPStr)] string eventName, bool forceRegistration);

        /// <summary>
        /// 取消注册游戏事件
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <returns>操作是否成功</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_UnregisterEvent(int eventHandle);

        /// <summary>
        /// 触发游戏事件
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="dontBroadcast">不广播标志</param>
        /// <returns>操作是否成功</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_FireEvent(int eventHandle, bool dontBroadcast);

        /// <summary>
        /// 设置事件字符串参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_SetEventString(int eventHandle, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

        /// <summary>
        /// 设置事件整型参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_SetEventInt(int eventHandle, [MarshalAs(UnmanagedType.LPStr)] string key, int value);

        /// <summary>
        /// 设置事件浮点参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_SetEventFloat(int eventHandle, [MarshalAs(UnmanagedType.LPStr)] string key, float value);

        /// <summary>
        /// 获取事件字符串参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="maxLength">缓冲区最大长度</param>
        /// <returns>实际复制的字符数</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetEventString(int eventHandle, [MarshalAs(UnmanagedType.LPStr)] string key, [Out] byte[] buffer, int maxLength);

        /// <summary>
        /// 获取事件整型参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetEventInt(int eventHandle, [MarshalAs(UnmanagedType.LPStr)] string key, int defaultValue);

        /// <summary>
        /// 获取事件浮点参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        [DllImport(EventDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_GetEventFloat(int eventHandle, [MarshalAs(UnmanagedType.LPStr)] string key, float defaultValue);
    }
}