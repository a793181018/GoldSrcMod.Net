using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// Ham钩子管理器，提供C#友好的委托封装
    /// </summary>
    public static class HamHookManager
    {
        // 委托定义
        public delegate void HamHookHandler(int entity, HamHookParameters parameters);
        public delegate void HamHookHandlerPost(int entity, HamHookParameters parameters);

        // 钩子信息
        private class HookInfo
        {
            public int HookId { get; set; }
            public HamHookType Function { get; set; }
            public string EntityClass { get; set; } = string.Empty;
            public HamHookHandler? PreHandler { get; set; }
            public HamHookHandlerPost? PostHandler { get; set; }
            public bool IsEnabled { get; set; }
        }

        // 存储所有钩子
        private static readonly Dictionary<int, HookInfo> Hooks = new Dictionary<int, HookInfo>();
        private static readonly Dictionary<string, List<HookInfo>> EntityClassHooks = new Dictionary<string, List<HookInfo>>();

        /// <summary>
        /// 注册Ham钩子
        /// </summary>
        public static int RegisterHook(HamHookType function, string entityClass, 
            HamHookHandler? preHandler = null, HamHookHandlerPost? postHandler = null)
        {
            if (string.IsNullOrEmpty(entityClass))
                throw new ArgumentException("Entity class cannot be null or empty");

            // 创建原生回调
            var preCallback = preHandler != null ? new NativeMethods.HamHookCallback(PreHookCallback) : null;
            var postCallback = postHandler != null ? new NativeMethods.HamHookCallback(PostHookCallback) : null;

            // 注册到原生层
            int hookId = NativeMethods.RegisterHamHook((int)function, entityClass, preCallback, postCallback);
            if (hookId <= 0)
                return 0;

            // 存储钩子信息
            var hookInfo = new HookInfo
            {
                HookId = hookId,
                Function = function,
                EntityClass = entityClass,
                PreHandler = preHandler,
                PostHandler = postHandler,
                IsEnabled = true
            };

            Hooks[hookId] = hookInfo;

            if (!EntityClassHooks.ContainsKey(entityClass))
                EntityClassHooks[entityClass] = new List<HookInfo>();
            EntityClassHooks[entityClass].Add(hookInfo);

            return hookId;
        }

        /// <summary>
        /// 注册实体特定钩子
        /// </summary>
        public static int RegisterHookFromEntity(HamHookType function, int entityId,
            HamHookHandler? preHandler = null, HamHookHandlerPost? postHandler = null)
        {
            if (entityId <= 0)
                throw new ArgumentException("Invalid entity ID");

            // 创建原生回调
            var preCallback = preHandler != null ? new NativeMethods.HamHookCallback(PreHookCallback) : null;
            var postCallback = postHandler != null ? new NativeMethods.HamHookCallback(PostHookCallback) : null;

            // 注册到原生层
            int hookId = NativeMethods.RegisterHamHookFromEntity((int)function, entityId, preCallback, postCallback);
            if (hookId <= 0)
                return 0;

            // 存储钩子信息
            var hookInfo = new HookInfo
            {
                HookId = hookId,
                Function = function,
                EntityClass = $"entity_{entityId}",
                PreHandler = preHandler,
                PostHandler = postHandler,
                IsEnabled = true
            };

            Hooks[hookId] = hookInfo;
            return hookId;
        }

        /// <summary>
        /// 注册玩家钩子
        /// </summary>
        public static int RegisterPlayerHook(HamHookType function,
            HamHookHandler? preHandler = null, HamHookHandlerPost? postHandler = null)
        {
            // 创建原生回调
            var preCallback = preHandler != null ? new NativeMethods.HamHookCallback(PreHookCallback) : null;
            var postCallback = postHandler != null ? new NativeMethods.HamHookCallback(PostHookCallback) : null;

            // 注册到原生层
            int hookId = NativeMethods.RegisterHamHookPlayer((int)function, preCallback, postCallback);
            if (hookId <= 0)
                return 0;

            // 存储钩子信息
            var hookInfo = new HookInfo
            {
                HookId = hookId,
                Function = function,
                EntityClass = "player",
                PreHandler = preHandler,
                PostHandler = postHandler,
                IsEnabled = true
            };

            Hooks[hookId] = hookInfo;
            return hookId;
        }

        /// <summary>
        /// 启用钩子
        /// </summary>
        public static void EnableHook(int hookId)
        {
            if (Hooks.TryGetValue(hookId, out var hookInfo))
            {
                NativeMethods.EnableHamHook(hookId);
                hookInfo.IsEnabled = true;
            }
        }

        /// <summary>
        /// 禁用钩子
        /// </summary>
        public static void DisableHook(int hookId)
        {
            if (Hooks.TryGetValue(hookId, out var hookInfo))
            {
                NativeMethods.DisableHamHook(hookId);
                hookInfo.IsEnabled = false;
            }
        }

        /// <summary>
        /// 检查钩子是否有效
        /// </summary>
        public static bool IsHookValid(HamHookType function)
        {
            return NativeMethods.IsHamHookValid((int)function) != 0;
        }

        /// <summary>
        /// 移除钩子
        /// </summary>
        public static bool RemoveHook(int hookId)
        {
            if (!Hooks.TryGetValue(hookId, out var hookInfo))
                return false;

            // 从实体类钩子列表中移除
            if (EntityClassHooks.TryGetValue(hookInfo.EntityClass, out var classHooks))
            {
                classHooks.Remove(hookInfo);
                if (classHooks.Count == 0)
                    EntityClassHooks.Remove(hookInfo.EntityClass);
            }

            // 禁用钩子
            NativeMethods.DisableHamHook(hookId);
            
            // 从字典中移除
            return Hooks.Remove(hookId);
        }

        /// <summary>
        /// 获取所有钩子
        /// </summary>
        public static IEnumerable<int> GetAllHooks()
        {
            return Hooks.Keys;
        }

        /// <summary>
        /// 获取实体类的所有钩子
        /// </summary>
        public static IEnumerable<int> GetHooksForEntityClass(string entityClass)
        {
            if (EntityClassHooks.TryGetValue(entityClass, out var hooks))
            {
                foreach (var hook in hooks)
                    yield return hook.HookId;
            }
        }

        // 内部回调处理
        private static void PreHookCallback(int entity, int paramCount, IntPtr parameters)
        {
            // 这里需要根据参数类型构建HamHookParameters
            var hookParams = new HamHookParameters(paramCount, parameters);
            
            // 查找对应的钩子并调用
            foreach (var hook in Hooks.Values)
            {
                if (hook.PreHandler != null)
                {
                    hook.PreHandler(entity, hookParams);
                }
            }
        }

        private static void PostHookCallback(int entity, int paramCount, IntPtr parameters)
        {
            // 这里需要根据参数类型构建HamHookParameters
            var hookParams = new HamHookParameters(paramCount, parameters);
            
            // 查找对应的钩子并调用
            foreach (var hook in Hooks.Values)
            {
                if (hook.PostHandler != null)
                {
                    hook.PostHandler(entity, hookParams);
                }
            }
        }
    }
}