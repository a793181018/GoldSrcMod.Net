using System;
using System.Collections.Generic;

namespace AmxModx.Bridge.Examples
{
    /// <summary>
    /// AMXModX桥接功能使用示例
    /// </summary>
    public static class UsageExample
    {
        /// <summary>
        /// 文件操作示例
        /// </summary>
        public static void FileOperationsExample()
        {
            try
            {
                // 写入文件
                string testContent = "Hello AMXModX from C#!";
                bool writeSuccess = FileManager.WriteFileText("test.txt", testContent);
                Console.WriteLine($"Write file success: {writeSuccess}");

                // 读取文件
                string readContent = FileManager.ReadFileText("test.txt");
                Console.WriteLine($"Read content: {readContent}");

                // 获取目录文件列表
                string[] files = FileManager.GetFiles(".");
                Console.WriteLine($"Files in current directory: {string.Join(", ", files)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File operations error: {ex.Message}");
            }
        }

        /// <summary>
        /// 控制台变量操作示例
        /// </summary>
        public static void CVarOperationsExample()
        {
            try
            {
                // 创建控制台变量
                int cvarId = CVarManager.CreateCVar("test_cvar", "default_value", 0);
                Console.WriteLine($"Created CVar with ID: {cvarId}");

                // 设置变量值
                bool setSuccess = CVarManager.SetCVarString(cvarId, "new_value");
                Console.WriteLine($"Set CVar success: {setSuccess}");

                // 获取变量值
                string value = CVarManager.GetCVarString(cvarId);
                Console.WriteLine($"CVar value: {value}");

                // 使用浮点值
                int floatCvarId = CVarManager.CreateCVar("test_float", "1.5", 0);
                float floatValue = CVarManager.GetCVarFloat(floatCvarId);
                Console.WriteLine($"Float CVar value: {floatValue}");

                CVarManager.SetCVarFloat(floatCvarId, 2.5f);
                float newFloatValue = CVarManager.GetCVarFloat(floatCvarId);
                Console.WriteLine($"Updated float CVar value: {newFloatValue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CVar operations error: {ex.Message}");
            }
        }

        /// <summary>
        /// 数据包操作示例
        /// </summary>
        public static void DataPackOperationsExample()
        {
            using (var pack = new DataPack())
            {
                // 写入数据
                pack.WriteInt(42);
                pack.WriteFloat(3.14f);
                pack.WriteString("Hello DataPack");

                Console.WriteLine($"DataPack size: {pack.Size}");

                // 重置并读取数据
                pack.Reset();
                int intValue = pack.ReadInt();
                float floatValue = pack.ReadFloat();
                string stringValue = pack.ReadString();

                Console.WriteLine($"Read int: {intValue}");
                Console.WriteLine($"Read float: {floatValue}");
                Console.WriteLine($"Read string: {stringValue}");
            }
        }

        /// <summary>
        /// 游戏配置操作示例
        /// </summary>
        public static void GameConfigExample()
        {
            try
            {
                using (var config = new GameConfig("game.cfg"))
                {
                    string serverName = config.GetValue("hostname");
                    Console.WriteLine($"Server name: {serverName}");

                    string maxPlayers = config.GetValue("sv_maxplayers");
                    Console.WriteLine($"Max players: {maxPlayers}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Game config error: {ex.Message}");
            }
        }

        /// <summary>
        /// 持久化存储操作示例
        /// </summary>
        public static void VaultOperationsExample()
        {
            try
            {
                using (var vault = new Vault("player_data.vault"))
                {
                    // 存储数据
                    bool setSuccess = vault.SetValue("player_score", "1000");
                    Console.WriteLine($"Set vault value success: {setSuccess}");

                    // 读取数据
                    string score = vault.GetValue("player_score");
                    Console.WriteLine($"Player score: {score}");

                    // 存储复杂数据
                    vault.SetValue("player_stats", "kills=50;deaths=20;assists=30");
                    string stats = vault.GetValue("player_stats");
                    Console.WriteLine($"Player stats: {stats}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Vault operations error: {ex.Message}");
            }
        }

        /// <summary>
        /// 运行所有示例
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("=== AMXModX Bridge Usage Examples ===");
            
            Console.WriteLine("\n1. File Operations:");
            FileOperationsExample();
            
            Console.WriteLine("\n2. CVar Operations:");
            CVarOperationsExample();
            
            Console.WriteLine("\n3. DataPack Operations:");
            DataPackOperationsExample();
            
            Console.WriteLine("\n4. Game Config:");
            GameConfigExample();
            
            Console.WriteLine("\n5. Vault Operations:");
            VaultOperationsExample();
            
            Console.WriteLine("\n=== Examples completed ===");
        }
    }
}