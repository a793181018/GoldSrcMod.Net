using System;
using System.Collections.Generic;
using AmxModx.Wrappers;
using AmxModx.Wrappers.Communication;
using AmxModx.Wrappers.Data;
using AmxModx.Wrappers.Engine;
using AmxModx.Wrappers.System;

namespace AmxModx.Examples
{
    /// <summary>
    /// 高级封装使用示例
    /// 展示如何使用各个模块的高级封装
    /// </summary>
    public static class AdvancedWrapperUsage
    {
        /// <summary>
        /// 初始化示例
        /// </summary>
        public static void InitializeExample()
        {
            // 初始化所有模块
            AmxModxManager.Initialize();

            Console.WriteLine("AMX Mod X高级封装已初始化");
        }

        /// <summary>
        /// CVar使用示例
        /// </summary>
        public static void CVarUsageExample()
        {
            // 创建新的CVar
            var myCVar = CVarManager.Create("my_custom_var", "default_value", 0);
            if (myCVar != null)
            {
                Console.WriteLine($"创建了CVar: {myCVar.Name} = {myCVar.StringValue}");

                // 设置和获取值
                myCVar.StringValue = "new_value";
                myCVar.FloatValue = 123.45f;
                myCVar.IntValue = 789;

                Console.WriteLine($"更新后的值: {myCVar}");
            }

            // 查找现有CVar
            var existingCVar = CVarManager.Find("sv_password");
            if (existingCVar != null)
            {
                Console.WriteLine($"找到CVar: {existingCVar.Name} = {existingCVar.StringValue}");
            }

            // 快速获取值
            string serverName = CVarManager.GetString("hostname");
            float gravity = CVarManager.GetFloat("sv_gravity");
            int maxPlayers = CVarManager.GetInt("sv_maxplayers");

            Console.WriteLine($"服务器信息: {serverName}, 重力: {gravity}, 最大玩家: {maxPlayers}");
        }

        /// <summary>
        /// 事件系统使用示例
        /// </summary>
        public static void EventUsageExample()
        {
            // 注册事件
            EventManager.RegisterEvent("player_death", OnPlayerDeath);
            EventManager.RegisterEvent("round_start", OnRoundStart);
            EventManager.RegisterEvent("bomb_planted", OnBombPlanted);

            Console.WriteLine("事件注册完成");

            // 触发测试事件
            var testArgs = new Dictionary<string, object>
            {
                ["userid"] = 1,
                ["attacker"] = 2,
                ["weapon"] = "ak47",
                ["headshot"] = true
            };

            EventManager.FireEvent("player_death", testArgs);
        }

        /// <summary>
        /// 转发系统使用示例
        /// </summary>
        public static void ForwardUsageExample()
        {
            // 注册标准转发
            ForwardManager.RegisterClientConnect(OnClientConnect);
            ForwardManager.RegisterClientDisconnect(OnClientDisconnect);
            ForwardManager.RegisterClientCommand(OnClientCommand);

            Console.WriteLine("转发注册完成");

            // 执行转发测试
            int result = ForwardManager.ExecuteForward("client_connect", 1, "127.0.0.1", "STEAM_0:1:123456");
            Console.WriteLine($"客户端连接转发结果: {result}");
        }

        /// <summary>
        /// 引擎管理使用示例
        /// </summary>
        public static void EngineUsageExample()
        {
            // 获取游戏信息
            float gameTime = EngineManager.GameTime;
            int entityCount = EngineManager.EntityCount;

            Console.WriteLine($"游戏时间: {gameTime}, 实体数量: {entityCount}");

            // 实体操作示例
            int entityId = 1; // 假设的玩家实体
            if (EngineManager.IsValidEntity(entityId))
            {
                Console.WriteLine($"实体 {entityId} 是有效的");

                // 获取实体信息
                var origin = EngineManager.GetEntityOrigin(entityId);
                var angles = EngineManager.GetEntityAngles(entityId);
                var velocity = EngineManager.GetEntityVelocity(entityId);

                Console.WriteLine($"实体位置: {origin}");
                Console.WriteLine($"实体角度: {angles}");
                Console.WriteLine($"实体速度: {velocity}");

                // 追踪示例
                var start = new AmxModx.Bridge.Engine.EngineBridge.Vector3(0, 0, 0);
                var end = new AmxModx.Bridge.Engine.EngineBridge.Vector3(100, 100, 100);
                var traceResult = EngineManager.TraceLine(start, end, entityId);

                Console.WriteLine($"追踪结果: 成功={traceResult.Success}, 命中实体={traceResult.HitEntity}");
            }
        }

        /// <summary>
        /// 系统管理使用示例
        /// </summary>
        public static void SystemUsageExample()
        {
            // 参数操作
            int paramCount = CoreManager.ParameterCount;
            Console.WriteLine($"当前参数数量: {paramCount}");

            // 获取参数
            if (paramCount > 0)
            {
                int firstParam = CoreManager.GetParameter(0);
                string firstString = CoreManager.GetStringParameter(0);
                float firstFloat = CoreManager.GetFloatParameter(0);

                Console.WriteLine($"参数0: 整数值={firstParam}, 字符串值={firstString}, 浮点值={firstFloat}");
            }

            // 属性操作
            int heapSpace = CoreManager.HeapSpace;
            Console.WriteLine($"堆空间: {heapSpace}");

            // 函数索引
            int funcIndex = CoreManager.GetFunctionIndex("register_plugin");
            Console.WriteLine($"register_plugin函数索引: {funcIndex}");
        }

        /// <summary>
        /// 综合使用示例
        /// </summary>
        public static void ComprehensiveExample()
        {
            Console.WriteLine("=== AMX Mod X高级封装综合示例 ===");

            // 1. 初始化
            InitializeExample();

            // 2. CVar操作
            CVarUsageExample();

            // 3. 事件系统
            EventUsageExample();

            // 4. 转发系统
            ForwardUsageExample();

            // 5. 引擎操作
            EngineUsageExample();

            // 6. 系统操作
            SystemUsageExample();

            Console.WriteLine("=== 示例完成 ===");
        }

        /// <summary>
        /// 事件回调：玩家死亡
        /// </summary>
        private static void OnPlayerDeath(string eventName, Dictionary<string, object> args)
        {
            Console.WriteLine($"玩家死亡事件: {eventName}");
            foreach (var kvp in args)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }

        /// <summary>
        /// 事件回调：回合开始
        /// </summary>
        private static void OnRoundStart(string eventName, Dictionary<string, object> args)
        {
            Console.WriteLine($"回合开始事件: {eventName}");
        }

        /// <summary>
        /// 事件回调：炸弹安放
        /// </summary>
        private static void OnBombPlanted(string eventName, Dictionary<string, object> args)
        {
            Console.WriteLine($"炸弹安放事件: {eventName}");
        }

        /// <summary>
        /// 转发回调：客户端连接
        /// </summary>
        private static int OnClientConnect(params object[] parameters)
        {
            if (parameters.Length >= 3)
            {
                int clientId = (int)parameters[0];
                string ipAddress = (string)parameters[1];
                string steamId = (string)parameters[2];

                Console.WriteLine($"客户端连接: ID={clientId}, IP={ipAddress}, SteamID={steamId}");
                return 1; // 允许连接
            }
            return 0;
        }

        /// <summary>
        /// 转发回调：客户端断开连接
        /// </summary>
        private static int OnClientDisconnect(params object[] parameters)
        {
            if (parameters.Length >= 1)
            {
                int clientId = (int)parameters[0];
                Console.WriteLine($"客户端断开连接: ID={clientId}");
            }
            return 1;
        }

        /// <summary>
        /// 转发回调：客户端命令
        /// </summary>
        private static int OnClientCommand(params object[] parameters)
        {
            if (parameters.Length >= 2)
            {
                int clientId = (int)parameters[0];
                string command = (string)parameters[1];
                Console.WriteLine($"客户端命令: ID={clientId}, Command={command}");
            }
            return 1;
        }

        /// <summary>
        /// 清理示例
        /// </summary>
        public static void CleanupExample()
        {
            // 清理所有注册
            EventManager.ClearAllEvents();
            ForwardManager.ClearAllForwards();
            AmxModxManager.Cleanup();

            Console.WriteLine("AMX Mod X高级封装已清理");
        }
    }
}