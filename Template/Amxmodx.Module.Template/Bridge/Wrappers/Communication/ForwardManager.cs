using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Forward;

namespace AmxModx.Wrappers.Communication
{
    /// <summary>
    /// 转发管理器高级封装
    /// 提供对AMX Mod X转发系统的高级封装
    /// </summary>
    public static class ForwardManager
    {
        /// <summary>
        /// 转发回调委托
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行结果</returns>
        public delegate int ForwardCallback(params object[] parameters);

        private static readonly Dictionary<string, ForwardCallback> _forwardCallbacks = new Dictionary<string, ForwardCallback>();

        /// <summary>
        /// 注册转发
        /// </summary>
        /// <param name="forwardName">转发名称</param>
        /// <param name="callback">回调函数</param>
        /// <param name="paramsTypes">参数类型数组</param>
        public static void RegisterForward(string forwardName, ForwardCallback callback, params int[] paramsTypes)
        {
            if (string.IsNullOrEmpty(forwardName) || callback == null)
                return;

            _forwardCallbacks[forwardName] = callback;

            // 注册到AMX Mod X转发系统
            ForwardBridge.AmxModx_Bridge_RegisterForward(forwardName, paramsTypes, paramsTypes.Length);
        }

        /// <summary>
        /// 取消注册转发
        /// </summary>
        /// <param name="forwardName">转发名称</param>
        public static void UnregisterForward(string forwardName)
        {
            if (string.IsNullOrEmpty(forwardName))
                return;

            if (_forwardCallbacks.ContainsKey(forwardName))
                _forwardCallbacks.Remove(forwardName);
        }

        /// <summary>
        /// 执行转发
        /// </summary>
        /// <param name="forwardName">转发名称</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行结果</returns>
        public static int ExecuteForward(string forwardName, params object[] parameters)
        {
            if (string.IsNullOrEmpty(forwardName))
                return 0;

            if (_forwardCallbacks.ContainsKey(forwardName))
            {
                try
                {
                    return _forwardCallbacks[forwardName](parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Forward execution error: {ex.Message}");
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// 检查转发是否存在
        /// </summary>
        /// <param name="forwardName">转发名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool ForwardExists(string forwardName)
        {
            if (string.IsNullOrEmpty(forwardName))
                return false;

            return ForwardBridge.AmxModx_Bridge_ForwardExists(forwardName) == 1;
        }

        /// <summary>
        /// 获取转发参数数量
        /// </summary>
        /// <param name="forwardName">转发名称</param>
        /// <returns>参数数量</returns>
        public static int GetForwardParamCount(string forwardName)
        {
            if (string.IsNullOrEmpty(forwardName))
                return 0;

            return ForwardBridge.AmxModx_Bridge_GetForwardParamCount(forwardName);
        }

        /// <summary>
        /// 获取转发参数类型
        /// </summary>
        /// <param name="forwardName">转发名称</param>
        /// <param name="paramIndex">参数索引</param>
        /// <returns>参数类型</returns>
        public static int GetForwardParamType(string forwardName, int paramIndex)
        {
            if (string.IsNullOrEmpty(forwardName) || paramIndex < 0)
                return 0;

            return ForwardBridge.AmxModx_Bridge_GetForwardParamType(forwardName, paramIndex);
        }

        /// <summary>
        /// 获取已注册的转发列表
        /// </summary>
        /// <returns>转发名称数组</returns>
        public static string[] GetRegisteredForwards()
        {
            var keys = new string[_forwardCallbacks.Count];
            _forwardCallbacks.Keys.CopyTo(keys, 0);
            return keys;
        }

        /// <summary>
        /// 清除所有转发注册
        /// </summary>
        public static void ClearAllForwards()
        {
            _forwardCallbacks.Clear();
        }

        /// <summary>
        /// 获取标准转发列表
        /// </summary>
        /// <returns>标准转发名称数组</returns>
        public static string[] GetStandardForwards()
        {
            return new string[]
            {
                "client_connect",
                "client_disconnect",
                "client_putinserver",
                "client_command",
                "client_infochanged",
                "server_activate",
                "server_deactivate",
                "plugin_init",
                "plugin_end",
                "plugin_cfg",
                "plugin_natives",
                "plugin_precache",
                "plugin_log",
                "plugin_map_change",
                "plugin_cfg"
            };
        }

        /// <summary>
        /// 注册标准转发
        /// </summary>
        /// <param name="forwardName">转发名称</param>
        /// <param name="callback">回调函数</param>
        public static void RegisterStandardForward(string forwardName, ForwardCallback callback)
        {
            if (string.IsNullOrEmpty(forwardName) || callback == null)
                return;

            RegisterForward(forwardName, callback);
        }

        /// <summary>
        /// 注册客户端连接转发
        /// </summary>
        /// <param name="callback">回调函数</param>
        public static void RegisterClientConnect(ForwardCallback callback)
        {
            RegisterStandardForward("client_connect", callback);
        }

        /// <summary>
        /// 注册客户端断开连接转发
        /// </summary>
        /// <param name="callback">回调函数</param>
        public static void RegisterClientDisconnect(ForwardCallback callback)
        {
            RegisterStandardForward("client_disconnect", callback);
        }

        /// <summary>
        /// 注册客户端进入服务器转发
        /// </summary>
        /// <param name="callback">回调函数</param>
        public static void RegisterClientPutInServer(ForwardCallback callback)
        {
            RegisterStandardForward("client_putinserver", callback);
        }

        /// <summary>
        /// 注册客户端命令转发
        /// </summary>
        /// <param name="callback">回调函数</param>
        public static void RegisterClientCommand(ForwardCallback callback)
        {
            RegisterStandardForward("client_command", callback);
        }

        /// <summary>
        /// 注册插件初始化转发
        /// </summary>
        /// <param name="callback">回调函数</param>
        public static void RegisterPluginInit(ForwardCallback callback)
        {
            RegisterStandardForward("plugin_init", callback);
        }

        /// <summary>
        /// 注册插件结束转发
        /// </summary>
        /// <param name="callback">回调函数</param>
        public static void RegisterPluginEnd(ForwardCallback callback)
        {
            RegisterStandardForward("plugin_end", callback);
        }
    }
}