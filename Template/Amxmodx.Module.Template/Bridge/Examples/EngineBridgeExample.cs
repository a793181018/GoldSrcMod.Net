using System;
using AmxModx.Bridge.Engine;

namespace AmxModx.Bridge.Examples
{
    /// <summary>
    /// Engine桥接使用示例
    /// </summary>
    public static class EngineBridgeExample
    {
        /// <summary>
        /// 实体操作示例
        /// </summary>
        public static void EntityOperationExample()
        {
            // 获取所有实体
            int[] allEntities = EngineBridgeExtensions.GetAllEntities(1000);
            Console.WriteLine($"找到 {allEntities.Length} 个实体");

            // 查找特定类型的实体
            int[] players = EngineBridgeExtensions.FindEntitiesByClass("player");
            Console.WriteLine($"找到 {players.Length} 个玩家实体");

            if (players.Length > 0)
            {
                int playerId = players[0];

                // 获取实体位置
                if (EngineBridgeExtensions.GetEntityOrigin(playerId, out var origin))
                {
                    Console.WriteLine($"玩家位置: ({origin.X}, {origin.Y}, {origin.Z})");
                }

                // 获取实体健康值
                int health = EngineBridgeExtensions.GetEntityHealth(playerId);
                Console.WriteLine($"玩家健康值: {health}");

                // 获取实体类名
                string className = EngineBridgeExtensions.GetEntityClassName(playerId);
                Console.WriteLine($"实体类名: {className}");
            }
        }

        /// <summary>
        /// 实体属性设置示例
        /// </summary>
        public static void EntityPropertyExample()
        {
            int[] players = EngineBridgeExtensions.FindEntitiesByClass("player");
            if (players.Length > 0)
            {
                int playerId = players[0];

                // 设置健康值
                bool healthSet = EngineBridgeExtensions.SetEntityHealth(playerId, 100);
                Console.WriteLine($"设置健康值: {(healthSet ? "成功" : "失败")}");

                // 设置护甲值
                bool armorSet = EngineBridgeExtensions.SetEntityArmor(playerId, 50);
                Console.WriteLine($"设置护甲值: {(armorSet ? "成功" : "失败")}");

                // 设置位置
                var newPosition = new EngineBridge.Vector3(100.0f, 200.0f, 300.0f);
                bool positionSet = EngineBridgeExtensions.SetEntityOrigin(playerId, newPosition);
                Console.WriteLine($"设置位置: {(positionSet ? "成功" : "失败")}");
            }
        }

        /// <summary>
        /// 事件注册示例
        /// </summary>
        public static void EventRegistrationExample()
        {
            // 注册脉冲事件
            int impulseId = EngineEventDelegates.EngineEventManager.RegisterImpulseEvent(100, OnPlayerImpulse);
            Console.WriteLine($"注册脉冲事件，ID: {impulseId}");

            // 注册触碰事件
            int touchId = EngineEventDelegates.EngineEventManager.RegisterTouchEvent("player", "weapon_crowbar", OnPlayerTouch);
            Console.WriteLine($"注册触碰事件，ID: {touchId}");

            // 注册思考事件
            int thinkId = EngineEventDelegates.EngineEventManager.RegisterThinkEvent("player", OnPlayerThink);
            Console.WriteLine($"注册思考事件，ID: {thinkId}");
        }

        /// <summary>
        /// 事件取消注册示例
        /// </summary>
        public static void EventUnregistrationExample()
        {
            // 假设已经注册了事件
            int impulseId = 1;
            int touchId = 2;
            int thinkId = 3;

            // 取消注册
            bool impulseUnregistered = EngineEventDelegates.EngineEventManager.UnregisterImpulseEvent(impulseId);
            bool touchUnregistered = EngineEventDelegates.EngineEventManager.UnregisterTouchEvent(touchId);
            bool thinkUnregistered = EngineEventDelegates.EngineEventManager.UnregisterThinkEvent(thinkId);

            Console.WriteLine($"取消注册脉冲事件: {(impulseUnregistered ? "成功" : "失败")}");
            Console.WriteLine($"取消注册触碰事件: {(touchUnregistered ? "成功" : "失败")}");
            Console.WriteLine($"取消注册思考事件: {(thinkUnregistered ? "成功" : "失败")}");
        }

        private static void OnPlayerImpulse(int client, int impulse)
        {
            Console.WriteLine($"玩家 {client} 触发脉冲 {impulse}");
        }

        private static void OnPlayerTouch(int touched, int toucher)
        {
            Console.WriteLine($"实体 {touched} 被实体 {toucher} 触碰");
        }

        private static void OnPlayerThink(int entity)
        {
            Console.WriteLine($"实体 {entity} 正在思考");
        }

        /// <summary>
        /// 完整的使用示例
        /// </summary>
        public static void CompleteExample()
        {
            Console.WriteLine("=== Engine桥接完整示例 ===");

            // 1. 实体操作
            EntityOperationExample();

            // 2. 属性设置
            EntityPropertyExample();

            // 3. 事件注册
            EventRegistrationExample();

            Console.WriteLine("=== 示例完成 ===");
        }
    }
}