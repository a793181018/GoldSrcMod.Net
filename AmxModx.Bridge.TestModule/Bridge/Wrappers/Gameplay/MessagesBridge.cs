using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Messages;

namespace AmxModx.Bridge.Wrappers.Gameplay
{
    /// <summary>
    /// 消息桥接包装器
    /// 提供消息系统的高级接口
    /// </summary>
    public static class MessagesBridge
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <param name="messageId">消息ID</param>
        /// <returns>操作是否成功</returns>
        public static bool RegisterMessage(string messageName, int messageId)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_RegisterMessage(messageName, messageId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 注册消息失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 取消注册消息
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns>操作是否成功</returns>
        public static bool UnregisterMessage(int messageId)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_UnregisterMessage(messageId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 取消注册消息失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 发送消息到客户端
        /// </summary>
        /// <param name="clientIndex">客户端索引</param>
        /// <param name="messageId">消息ID</param>
        /// <param name="parameters">消息参数</param>
        /// <returns>操作是否成功</returns>
        public static bool SendMessage(int clientIndex, int messageId, params int[] parameters)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_SendMessage(clientIndex, messageId, parameters, parameters?.Length ?? 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 发送消息失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 广播消息到所有客户端
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="parameters">消息参数</param>
        /// <returns>操作是否成功</returns>
        public static bool BroadcastMessage(int messageId, params int[] parameters)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_BroadcastMessage(messageId, parameters, parameters?.Length ?? 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 广播消息失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取消息ID
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <returns>消息ID，失败返回-1</returns>
        public static int GetMessageId(string messageName)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetMessageId(messageName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 获取消息ID失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 注册消息钩子
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="callback">回调函数</param>
        /// <returns>操作是否成功</returns>
        public static bool HookMessage(int messageId, IntPtr callback)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_HookMessage(messageId, callback);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 注册消息钩子失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 取消注册消息钩子
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns>操作是否成功</returns>
        public static bool UnhookMessage(int messageId)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_UnhookMessage(messageId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 取消注册消息钩子失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置消息参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        public static bool SetMessageParam(int paramIndex, int value)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_SetMessageParam(paramIndex, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 设置消息参数失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取消息参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <returns>参数值</returns>
        public static int GetMessageParam(int paramIndex)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetMessageParam(paramIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 获取消息参数失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 注册消息（扩展版本）
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <param name="messageId">消息ID</param>
        /// <param name="paramTypes">参数类型数组</param>
        /// <param name="callback">回调函数</param>
        /// <returns>操作是否成功</returns>
        public static bool RegisterMessageEx(string messageName, int messageId, int[] paramTypes, IntPtr callback)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_RegisterMessageEx(messageName, messageId, paramTypes, paramTypes?.Length ?? 0, callback);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessagesBridge] 注册消息扩展失败: {ex.Message}");
                return false;
            }
        }
    }
}