// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Event System Bridge - C# Test
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.

using System;
using System.Collections.Generic;
using AmxModx.Bridge;
using AmxModx.Bridge.Event;

namespace AmxModx.Bridge.Tests
{
    /// <summary>
    /// 事件系统桥接测试类
    /// </summary>
    public static class EventBridgeTests
    {
        /// <summary>
        /// 测试事件注册功能
        /// </summary>
        public static void TestEventRegistration()
        {
            Console.WriteLine("=== 事件注册测试 ===");

            // 测试注册玩家死亡事件
            string deathEvent = "DeathMsg";
            int deathHandle = EventBridge.RegisterEvent(deathEvent, "OnPlayerDeath", EventFlags.None);
            Console.WriteLine($"注册死亡事件: {deathEvent}, 句柄: {deathHandle}");

            // 测试注册回合开始事件
            string roundStartEvent = "RoundStart";
            int roundStartHandle = EventBridge.RegisterEventEx(roundStartEvent, "OnRoundStart", EventFlags.IncludeWorld, "team=CT");
            Console.WriteLine($"注册回合开始事件: {roundStartEvent}, 句柄: {roundStartHandle}");

            // 测试事件有效性检查
            bool isDeathValid = EventBridge.IsEventValid(deathHandle) == 1;
            bool isRoundValid = EventBridge.IsEventValid(roundStartHandle) == 1;
            Console.WriteLine($"死亡事件有效性: {isDeathValid}, 回合开始事件有效性: {isRoundValid}");
        }

        /// <summary>
        /// 测试事件启用/禁用功能
        /// </summary>
        public static void TestEventEnableDisable()
        {
            Console.WriteLine("=== 事件启用/禁用测试 ===");

            // 注册测试事件
            string testEvent = "PlayerConnect";
            int handle = EventBridge.RegisterEvent(testEvent, "OnPlayerConnect", EventFlags.None);
            Console.WriteLine($"注册连接事件: {testEvent}, 句柄: {handle}");

            if (handle > 0)
            {
                // 测试禁用事件
                bool disableResult = EventBridge.DisableEvent(handle) == 1;
                Console.WriteLine($"禁用事件结果: {disableResult}");

                // 测试启用事件
                bool enableResult = EventBridge.EnableEvent(handle) == 1;
                Console.WriteLine($"启用事件结果: {enableResult}");
            }
        }

        /// <summary>
        /// 测试事件管理器类
        /// </summary>
        public static void TestEventManager()
        {
            Console.WriteLine("=== 事件管理器测试 ===");

            using (var eventManager = new EventManager())
            {
                // 注册多个事件
                int deathHandle = eventManager.RegisterEvent("DeathMsg", "OnPlayerDeath", EventFlags.None);
                int spawnHandle = eventManager.RegisterEvent("Spawn", "OnPlayerSpawn", EventFlags.IncludeClient, "team=T");
                int disconnectHandle = eventManager.RegisterEvent("PlayerDisconnect", "OnPlayerDisconnect", EventFlags.None);

                Console.WriteLine($"注册事件数量: {eventManager.Count}");
                Console.WriteLine($"死亡事件句柄: {deathHandle}");
                Console.WriteLine($"重生事件句柄: {spawnHandle}");
                Console.WriteLine($"断开连接事件句柄: {disconnectHandle}");

                // 测试启用/禁用
                bool enableResult = eventManager.EnableEvent("DeathMsg");
                bool disableResult = eventManager.DisableEvent("Spawn");
                Console.WriteLine($"启用死亡事件: {enableResult}, 禁用重生事件: {disableResult}");
            }
        }

        /// <summary>
        /// 测试事件ID获取
        /// </summary>
        public static void TestEventIdLookup()
        {
            Console.WriteLine("=== 事件ID查询测试 ===");

            string[] testEvents = { "DeathMsg", "RoundStart", "RoundEnd", "PlayerConnect", "PlayerDisconnect", "Spawn" };

            foreach (var eventName in testEvents)
            {
                int eventId = EventBridge.GetEventId(eventName);
                bool exists = eventId > 0;
                Console.WriteLine($"事件: {eventName}, ID: {eventId}, 存在: {exists}");
            }
        }

        /// <summary>
        /// 运行所有测试
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("开始AMX Mod X事件系统桥接测试...");
            Console.WriteLine();

            try
            {
                TestEventIdLookup();
                Console.WriteLine();

                TestEventRegistration();
                Console.WriteLine();

                TestEventEnableDisable();
                Console.WriteLine();

                TestEventManager();
                Console.WriteLine();

                Console.WriteLine("所有测试完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"测试过程中发生错误: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            }
        }
    }
}