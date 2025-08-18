using System;
using AmxModx.Bridge.Fakemeta;

namespace FakemetaExamples
{
    /// <summary>
    /// 展示如何使用新的C#回调桥接系统
    /// </summary>
    public static class FakemetaCallbackExamples
    {
        private static int playerConnectHandle = -1;
        private static int playerDisconnectHandle = -1;
        private static int entitySpawnHandle = -1;

        /// <summary>
        /// 初始化并注册所有回调
        /// </summary>
        public static void Initialize()
        {
            // 初始化事件系统
            FakemetaCallbacks.Initialize();

            // 示例1: 使用lambda表达式注册玩家连接事件
            playerConnectHandle = FakemetaEvents.OnPlayerConnect(data =>
            {
                Console.WriteLine($"Player {data.PlayerIndex} connected");
                return ForwardResult.Ignored; // 继续正常处理
            });

            // 示例2: 使用lambda表达式注册玩家断开连接事件
            playerDisconnectHandle = FakemetaEvents.OnPlayerDisconnect(data =>
            {
                Console.WriteLine($"Player {data.PlayerIndex} disconnected");
                return ForwardResult.Ignored;
            });

            // 示例3: 使用lambda表达式注册实体生成事件
            entitySpawnHandle = FakemetaEvents.OnEntitySpawn(data =>
            {
                Console.WriteLine($"Entity {data.EntityIndex} spawned at {data.VectorParam}");
                
                // 可以在这里修改实体属性
                if (data.EntityIndex > 0 && data.EntityIndex <= 32) // 玩家实体
                {
                    // 给玩家设置额外属性
                    return ForwardResult.Ignored;
                }
                
                return ForwardResult.Ignored;
            });

            // 示例4: 使用委托方法注册玩家命令事件
            int commandHandle = FakemetaEvents.OnPlayerCommand(OnPlayerCommandHandler);

            // 示例5: 使用委托方法注册实体使用事件
            int useHandle = FakemetaEvents.OnEntityUse(OnEntityUseHandler);

            // 示例6: 注册实体触碰事件，返回Override来阻止默认行为
            int touchHandle = FakemetaEvents.OnEntityTouch(data =>
            {
                Console.WriteLine($"Entity {data.EntityIndex} touched something");
                
                // 如果需要阻止默认触碰行为
                if (ShouldBlockTouch(data))
                {
                    return ForwardResult.Override;
                }
                
                return ForwardResult.Ignored;
            });

            // 示例7: 注册服务器激活事件
            int activateHandle = FakemetaEvents.OnServerActivate(data =>
            {
                Console.WriteLine("Server activated");
                InitializeGameRules();
                return ForwardResult.Ignored;
            });

            // 示例8: 注册每帧开始事件
            int frameHandle = FakemetaEvents.OnStartFrame(data =>
            {
                // 每帧更新游戏状态
                UpdateGameFrame();
                return ForwardResult.Ignored;
            });
        }

        /// <summary>
        /// 清理所有注册的回调
        /// </summary>
        public static void Cleanup()
        {
            // 注销特定事件
            if (playerConnectHandle > 0)
            {
                FakemetaCallbacks.UnregisterForward(ForwardType.ClientConnect, playerConnectHandle);
                playerConnectHandle = -1;
            }

            if (playerDisconnectHandle > 0)
            {
                FakemetaCallbacks.UnregisterForward(ForwardType.ClientDisconnect, playerDisconnectHandle);
                playerDisconnectHandle = -1;
            }

            if (entitySpawnHandle > 0)
            {
                FakemetaCallbacks.UnregisterForward(ForwardType.Spawn, entitySpawnHandle);
                entitySpawnHandle = -1;
            }

            // 清理整个事件系统
            FakemetaCallbacks.Cleanup();
        }

        /// <summary>
        /// 玩家命令处理程序
        /// </summary>
        private static ForwardResult OnPlayerCommandHandler(ref EventData data)
        {
            string command = data.StringParam?.ToLower() ?? "";
            
            switch (command)
            {
                case "menu":
                    ShowPlayerMenu(data.PlayerIndex);
                    return ForwardResult.Override; // 阻止默认处理
                    
                case "stats":
                    ShowPlayerStats(data.PlayerIndex);
                    return ForwardResult.Override;
                    
                default:
                    return ForwardResult.Ignored; // 继续正常处理
            }
        }

        /// <summary>
        /// 实体使用处理程序
        /// </summary>
        private static ForwardResult OnEntityUseHandler(ref EventData data)
        {
            Console.WriteLine($"Entity {data.EntityIndex} used by player {data.PlayerIndex}");
            
            // 检查使用权限
            if (!CanUseEntity(data.PlayerIndex, data.EntityIndex))
            {
                return ForwardResult.Override; // 阻止使用
            }
            
            return ForwardResult.Ignored; // 允许使用
        }

        #region 辅助方法

        private static bool ShouldBlockTouch(EventData data)
        {
            // 实现触碰阻止逻辑
            return false;
        }

        private static void InitializeGameRules()
        {
            // 初始化游戏规则
            Console.WriteLine("Game rules initialized");
        }

        private static void UpdateGameFrame()
        {
            // 每帧更新逻辑
        }

        private static void ShowPlayerMenu(int playerIndex)
        {
            // 显示玩家菜单
            Console.WriteLine($"Showing menu to player {playerIndex}");
        }

        private static void ShowPlayerStats(int playerIndex)
        {
            // 显示玩家统计
            Console.WriteLine($"Showing stats to player {playerIndex}");
        }

        private static bool CanUseEntity(int playerIndex, int entityIndex)
        {
            // 检查使用权限
            return true;
        }

        #endregion
    }

    /// <summary>
    /// 高级用法示例：事件过滤器
    /// </summary>
    public static class EventFilterExamples
    {
        private static Dictionary<int, int> activeFilters = new Dictionary<int, int>();

        /// <summary>
        /// 注册实体过滤器，只处理特定类型的实体
        /// </summary>
        public static void RegisterEntityFilter(string entityClass, Func<EventData, ForwardResult> handler)
        {
            int handle = FakemetaEvents.OnEntitySpawn(data =>
            {
                // 检查实体类型
                if (IsEntityOfClass(data.EntityIndex, entityClass))
                {
                    return handler(data);
                }
                return ForwardResult.Ignored;
            });

            activeFilters[handle] = entityClass.GetHashCode();
        }

        /// <summary>
        /// 注册玩家特定事件过滤器
        /// </summary>
        public static void RegisterPlayerFilter(int playerIndex, ForwardType eventType, Func<EventData, ForwardResult> handler)
        {
            int handle = FakemetaCallbacks.RegisterPreForward(eventType, data =>
            {
                if (data.PlayerIndex == playerIndex)
                {
                    return handler(data);
                }
                return ForwardResult.Ignored;
            });

            activeFilters[handle] = playerIndex;
        }

        private static bool IsEntityOfClass(int entityIndex, string entityClass)
        {
            // 实现实体类型检查
            return true;
        }
    }

    /// <summary>
    /// 性能监控示例
    /// </summary>
    public static class PerformanceMonitor
    {
        private static int frameCounter = 0;
        private static DateTime lastFrameTime = DateTime.Now;

        /// <summary>
        /// 注册性能监控事件
        /// </summary>
        public static void RegisterPerformanceMonitor()
        {
            // 监控每帧性能
            FakemetaEvents.OnStartFrame(data =>
            {
                frameCounter++;
                var now = DateTime.Now;
                var elapsed = now - lastFrameTime;

                if (elapsed.TotalSeconds >= 1.0)
                {
                    Console.WriteLine($"FPS: {frameCounter}");
                    frameCounter = 0;
                    lastFrameTime = now;
                }

                return ForwardResult.Ignored;
            });
        }
    }
}