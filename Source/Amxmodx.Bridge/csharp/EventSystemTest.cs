using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge;

namespace EventSystemTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== AMX Mod X 事件系统桥接测试 ===");
            
            // 测试事件ID获取
            TestEventIdLookup();
            
            // 测试事件管理器
            TestEventManager();
            
            Console.WriteLine("测试完成，按任意键退出...");
            Console.ReadKey();
        }
        
        static void TestEventIdLookup()
        {
            Console.WriteLine("\n1. 测试事件ID获取:");
            
            string[] testEvents = { "player_death", "player_connect", "round_start", "bomb_planted" };
            
            foreach (var eventName in testEvents)
            {
                int eventId = EventBridge.GetEventId(eventName);
                Console.WriteLine($"  {eventName}: ID = {eventId}");
            }
        }
        
        static void TestEventManager()
        {
            Console.WriteLine("\n2. 测试事件管理器:");
            
            try
            {
                var eventManager = new EventManager();
                
                // 测试注册玩家死亡事件
                eventManager.RegisterPlayerDeath((data) =>
                {
                    Console.WriteLine($"玩家死亡事件触发: {data.GetString("weapon")}");
                });
                
                // 测试注册回合开始事件
                eventManager.RegisterRoundStart((data) =>
                {
                    Console.WriteLine("回合开始事件触发");
                });
                
                Console.WriteLine("  事件管理器初始化成功");
                Console.WriteLine($"  已注册事件数量: {eventManager.GetRegisteredEventCount()}");
                
                // 测试事件启用/禁用
                Console.WriteLine("  测试事件启用/禁用...");
                eventManager.EnableAllEvents();
                eventManager.DisableAllEvents();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  事件管理器测试失败: {ex.Message}");
            }
        }
    }
}