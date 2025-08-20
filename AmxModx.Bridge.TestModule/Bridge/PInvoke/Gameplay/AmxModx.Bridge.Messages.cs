using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Messages
{
    /// <summary>
    /// 消息系统桥接定义
    /// 提供消息注册和处理功能
    /// </summary>
    public static class NativeMethods
    {
        private const string MessagesDll = "messages_bridge";

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <param name="messageId">消息ID</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_RegisterMessage([MarshalAs(UnmanagedType.LPStr)] string messageName, int messageId);

        /// <summary>
        /// 取消注册消息
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_UnregisterMessage(int messageId);

        /// <summary>
        /// 发送消息到客户端
        /// </summary>
        /// <param name="clientIndex">客户端索引</param>
        /// <param name="messageId">消息ID</param>
        /// <param name="parameters">消息参数</param>
        /// <param name="paramCount">参数数量</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_SendMessage(int clientIndex, int messageId, [In] int[] parameters, int paramCount);

        /// <summary>
        /// 广播消息到所有客户端
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="parameters">消息参数</param>
        /// <param name="paramCount">参数数量</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_BroadcastMessage(int messageId, [In] int[] parameters, int paramCount);

        /// <summary>
        /// 获取消息ID
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <returns>消息ID，失败返回-1</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMessageId([MarshalAs(UnmanagedType.LPStr)] string messageName);

        /// <summary>
        /// 注册消息钩子
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="callback">回调函数指针</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_HookMessage(int messageId, IntPtr callback);

        /// <summary>
        /// 取消注册消息钩子
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_UnhookMessage(int messageId);

        /// <summary>
        /// 设置消息参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_SetMessageParam(int paramIndex, int value);

        /// <summary>
        /// 获取消息参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <returns>参数值</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMessageParam(int paramIndex);

        /// <summary>
        /// 注册消息（新版本，支持更多参数）
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <param name="messageId">消息ID</param>
        /// <param name="paramTypes">参数类型数组</param>
        /// <param name="paramCount">参数数量</param>
        /// <param name="callback">回调函数指针</param>
        /// <returns>操作是否成功</returns>
        [DllImport(MessagesDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_RegisterMessageEx([MarshalAs(UnmanagedType.LPStr)] string messageName, int messageId, [In] int[] paramTypes, int paramCount, IntPtr callback);
    }
}