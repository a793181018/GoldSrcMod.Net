using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Fakemeta
{
    /// <summary>
    /// 提供fakemeta模块的C#回调桥接功能
    /// </summary>
    public static class FakemetaCallbacks
    {
        private static readonly Dictionary<int, ForwardCallback> registeredCallbacks = new Dictionary<int, ForwardCallback>();
        private static readonly object lockObject = new object();

        /// <summary>
        /// 注册前置事件回调
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <param name="callback">回调函数</param>
        /// <returns>注册句柄</returns>
        public static int RegisterPreForward(ForwardType forwardType, ForwardCallback callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            lock (lockObject)
            {
                int handle = NativeMethods.FMB_RegisterForward(
                    (FMB_ForwardType)forwardType,
                    FMB_ForwardTiming.Pre,
                    OnForwardCallback,
                    IntPtr.Zero
                );

                if (handle > 0)
                {
                    registeredCallbacks[handle] = callback;
                }

                return handle;
            }
        }

        /// <summary>
        /// 注册后置事件回调
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <param name="callback">回调函数</param>
        /// <returns>注册句柄</returns>
        public static int RegisterPostForward(ForwardType forwardType, ForwardCallback callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            lock (lockObject)
            {
                int handle = NativeMethods.FMB_RegisterForward(
                    (FMB_ForwardType)forwardType,
                    FMB_ForwardTiming.Post,
                    OnForwardCallback,
                    IntPtr.Zero
                );

                if (handle > 0)
                {
                    registeredCallbacks[handle] = callback;
                }

                return handle;
            }
        }

        /// <summary>
        /// 使用lambda表达式注册前置事件回调
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <param name="callback">lambda回调函数</param>
        /// <returns>注册句柄</returns>
        public static int RegisterPreForward(ForwardType forwardType, Func<EventData, ForwardResult> callback)
        {
            return RegisterPreForward(forwardType, (ref EventData data) => callback(data));
        }

        /// <summary>
        /// 使用lambda表达式注册后置事件回调
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <param name="callback">lambda回调函数</param>
        /// <returns>注册句柄</returns>
        public static int RegisterPostForward(ForwardType forwardType, Func<EventData, ForwardResult> callback)
        {
            return RegisterPostForward(forwardType, (ref EventData data) => callback(data));
        }

        /// <summary>
        /// 注销事件回调
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <param name="handle">注册句柄</param>
        /// <returns>是否成功注销</returns>
        public static bool UnregisterForward(ForwardType forwardType, int handle)
        {
            lock (lockObject)
            {
                if (registeredCallbacks.ContainsKey(handle))
                {
                    bool result = NativeMethods.FMB_UnregisterForward(
                        (FMB_ForwardType)forwardType,
                        FMB_ForwardTiming.Pre,
                        handle
                    ) > 0;

                    if (result)
                    {
                        registeredCallbacks.Remove(handle);
                    }

                    return result;
                }

                return false;
            }
        }

        /// <summary>
        /// 获取事件回调数量
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <param name="timing">事件时机</param>
        /// <returns>回调数量</returns>
        public static int GetForwardCount(ForwardType forwardType, ForwardTiming timing)
        {
            return NativeMethods.FMB_GetForwardCount(
                (FMB_ForwardType)forwardType,
                (FMB_ForwardTiming)timing
            );
        }

        /// <summary>
        /// 检查事件回调是否已注册
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <param name="handle">注册句柄</param>
        /// <returns>是否已注册</returns>
        public static bool IsForwardRegistered(ForwardType forwardType, int handle)
        {
            return NativeMethods.FMB_IsForwardRegistered(
                (FMB_ForwardType)forwardType,
                FMB_ForwardTiming.Pre,
                handle
            ) > 0;
        }

        /// <summary>
        /// 获取事件类型名称
        /// </summary>
        /// <param name="forwardType">事件类型</param>
        /// <returns>类型名称</returns>
        public static string GetForwardTypeName(ForwardType forwardType)
        {
            return Marshal.PtrToStringAnsi(
                NativeMethods.FMB_GetForwardTypeName((FMB_ForwardType)forwardType)
            );
        }

        /// <summary>
        /// 获取事件时机名称
        /// </summary>
        /// <param name="timing">事件时机</param>
        /// <returns>时机名称</returns>
        public static string GetForwardTimingName(ForwardTiming timing)
        {
            return Marshal.PtrToStringAnsi(
                NativeMethods.FMB_GetForwardTimingName((FMB_ForwardTiming)timing)
            );
        }

        /// <summary>
        /// 获取事件结果名称
        /// </summary>
        /// <param name="result">事件结果</param>
        /// <returns>结果名称</returns>
        public static string GetForwardResultName(ForwardResult result)
        {
            return Marshal.PtrToStringAnsi(
                NativeMethods.FMB_GetForwardResultName((FMB_ForwardResult)result)
            );
        }

        /// <summary>
        /// 初始化事件系统
        /// </summary>
        public static void Initialize()
        {
            if (NativeMethods.FMB_InitializeForwardSystem() <= 0)
            {
                throw new InvalidOperationException("Failed to initialize forward system");
            }
        }

        /// <summary>
        /// 清理事件系统
        /// </summary>
        public static void Cleanup()
        {
            lock (lockObject)
            {
                registeredCallbacks.Clear();
                NativeMethods.FMB_CleanupForwardSystem();
            }
        }

        /// <summary>
        /// 内部回调处理函数
        /// </summary>
        private static FMB_ForwardResult OnForwardCallback(ref FMB_EventData data, IntPtr userData)
        {
            try
            {
                // 查找对应的C#回调
                var eventData = new EventData
                {
                    EntityIndex = data.entityIndex,
                    PlayerIndex = data.playerIndex,
                    FloatParam = data.floatParam,
                    IntParam = data.intParam,
                    StringParam = Marshal.PtrToStringAnsi(data.stringParam),
                    VectorParam = new Vector3(data.vectorParam[0], data.vectorParam[1], data.vectorParam[2])
                };

                lock (lockObject)
                {
                    foreach (var callback in registeredCallbacks.Values)
                    {
                        var result = callback(ref eventData);
                        if (result != ForwardResult.Ignored)
                        {
                            return (FMB_ForwardResult)result;
                        }
                    }
                }

                return FMB_ForwardResult.Ignored;
            }
            catch (Exception ex)
            {
                // 记录异常但不中断执行
                Console.WriteLine($"Error in forward callback: {ex.Message}");
                return FMB_ForwardResult.Ignored;
            }
        }
    }

    /// <summary>
    /// 提供常用事件的便捷注册方法
    /// </summary>
    public static class FakemetaEvents
    {
        /// <summary>
        /// 注册玩家连接事件
        /// </summary>
        public static int OnPlayerConnect(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.ClientConnect, callback);
        }

        /// <summary>
        /// 注册玩家断开连接事件
        /// </summary>
        public static int OnPlayerDisconnect(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.ClientDisconnect, callback);
        }

        /// <summary>
        /// 注册玩家生成事件
        /// </summary>
        public static int OnPlayerSpawn(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.ClientSpawn, callback);
        }

        /// <summary>
        /// 注册玩家命令事件
        /// </summary>
        public static int OnPlayerCommand(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.ClientCommand, callback);
        }

        /// <summary>
        /// 注册实体生成事件
        /// </summary>
        public static int OnEntitySpawn(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.Spawn, callback);
        }

        /// <summary>
        /// 注册实体移除事件
        /// </summary>
        public static int OnEntityRemove(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.RemoveEntity, callback);
        }

        /// <summary>
        /// 注册实体使用事件
        /// </summary>
        public static int OnEntityUse(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.Use, callback);
        }

        /// <summary>
        /// 注册实体触碰事件
        /// </summary>
        public static int OnEntityTouch(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.Touch, callback);
        }

        /// <summary>
        /// 注册实体思考事件
        /// </summary>
        public static int OnEntityThink(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.Think, callback);
        }

        /// <summary>
        /// 注册实体阻挡事件
        /// </summary>
        public static int OnEntityBlocked(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.Blocked, callback);
        }

        /// <summary>
        /// 注册服务器激活事件
        /// </summary>
        public static int OnServerActivate(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.ServerActivate, callback);
        }

        /// <summary>
        /// 注册服务器停用事件
        /// </summary>
        public static int OnServerDeactivate(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.ServerDeactivate, callback);
        }

        /// <summary>
        /// 注册每帧开始事件
        /// </summary>
        public static int OnStartFrame(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.StartFrame, callback);
        }

        /// <summary>
        /// 注册玩家预思考事件
        /// </summary>
        public static int OnPlayerPreThink(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.PlayerPreThink, callback);
        }

        /// <summary>
        /// 注册玩家后思考事件
        /// </summary>
        public static int OnPlayerPostThink(Func<EventData, ForwardResult> callback)
        {
            return FakemetaCallbacks.RegisterPreForward(ForwardType.PlayerPostThink, callback);
        }
    }
}