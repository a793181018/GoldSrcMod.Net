using System;
using AmxModx.Bridge.Event;

namespace AmxModx.Bridge.Event.TestApp
{
    /// <summary>
    /// 事件系统桥接测试程序
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== AMX Mod X 事件系统桥接测试程序 ===");
            Console.WriteLine();

            // 运行测试
            RunBasicTests();
            RunAdvancedTests();

            Console.WriteLine();
            Console.WriteLine("=== 测试完成 ===");
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

        /// <summary>
        /// 运行基础测试
        /// </summary>
        static void RunBasicTests()
        {
            Console.WriteLine("运行基础测试...");
            Console.WriteLine();

            using var eventManager = new EventManager();

            // 测试1: 注册标准事件
            Console.WriteLine("测试1: 注册标准事件");
            try
            {
                int deathHandle = eventManager.RegisterPlayerDeathEvent(OnPlayerDeath);
                int connectHandle = eventManager.RegisterPlayerConnectEvent(OnPlayerConnect);
                int spawnHandle = eventManager.RegisterPlayerSpawnEvent(OnPlayerSpawn);

                Console.WriteLine($"✅ 死亡事件注册成功 (句柄: {deathHandle})");
                Console.WriteLine($"✅ 连接事件注册成功 (句柄: {connectHandle})");
                Console.WriteLine($"✅ 重生事件注册成功 (句柄: {spawnHandle})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 事件注册失败: {ex.Message}");
            }

            Console.WriteLine();

            // 测试2: 事件管理
            Console.WriteLine("测试2: 事件管理");
            try
            {
                int handle = eventManager.RegisterRoundStartEvent(OnRoundStart);
                
                bool disabled = eventManager.DisableEvent(handle);
                Console.WriteLine($"✅ 事件禁用: {disabled}");
                
                bool enabled = eventManager.EnableEvent(handle);
                Console.WriteLine($"✅ 事件启用: {enabled}");
                
                eventManager.UnregisterEvent(handle);
                Console.WriteLine("✅ 事件取消注册");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 事件管理失败: {ex.Message}");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// 运行高级测试
        /// </summary>
        static void RunAdvancedTests()
        {
            Console.WriteLine("运行高级测试...");
            Console.WriteLine();

            using var eventManager = new EventManager();

            // 测试3: 炸弹事件
            Console.WriteLine("测试3: 炸弹事件处理");
            try
            {
                int bombHandle = eventManager.RegisterBombEvents(OnBombEvent);
                Console.WriteLine($"✅ 炸弹事件注册成功 (句柄: {bombHandle})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 炸弹事件注册失败: {ex.Message}");
            }

            Console.WriteLine();

            // 测试4: 条件事件
            Console.WriteLine("测试4: 条件事件注册");
            try
            {
                int handle = eventManager.RegisterEventHandler(
                    "test_condition_event",
                    OnConditionalEvent,
                    EventFlags.None,
                    "weapon=ak47",
                    "map=de_dust2"
                );
                Console.WriteLine($"✅ 条件事件注册成功 (句柄: {handle})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 条件事件注册失败: {ex.Message}");
            }

            Console.WriteLine();

            // 测试5: 事件统计
            Console.WriteLine("测试5: 事件统计");
            try
            {
                int count1 = eventManager.GetEventHandlerCount("player_death");
                int count2 = eventManager.GetEventHandlerCount("player_connect");
                bool hasDeath = eventManager.IsEventRegistered("player_death");
                bool hasTest = eventManager.IsEventRegistered("test_event");

                Console.WriteLine($"✅ 死亡事件处理器数量: {count1}");
                Console.WriteLine($"✅ 连接事件处理器数量: {count2}");
                Console.WriteLine($"✅ 死亡事件已注册: {hasDeath}");
                Console.WriteLine($"✅ 测试事件已注册: {hasTest}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 事件统计失败: {ex.Message}");
            }
        }

        // 事件处理函数
        static int OnPlayerDeath(EventData eventData)
        {
            Console.WriteLine("📊 玩家死亡事件触发");
            return 0;
        }

        static int OnPlayerConnect(EventData eventData)
        {
            Console.WriteLine("📊 玩家连接事件触发");
            return 0;
        }

        static int OnPlayerSpawn(EventData eventData)
        {
            Console.WriteLine("📊 玩家重生事件触发");
            return 0;
        }

        static int OnRoundStart(EventData eventData)
        {
            Console.WriteLine("📊 回合开始事件触发");
            return 0;
        }

        static int OnBombEvent(EventData eventData)
        {
            string eventName = eventData.GetString("name");
            Console.WriteLine($"💣 炸弹事件: {eventName}");
            return 0;
        }

        static int OnConditionalEvent(EventData eventData)
        {
            Console.WriteLine("🎯 条件事件触发");
            return 0;
        }
    }
}