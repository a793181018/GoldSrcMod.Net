// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Player Manager Bridge Test
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using System.Collections.Generic;

namespace AmxModx.Bridge.PlayerManager
{
    /// <summary>
    /// 玩家管理器测试类
    /// </summary>
    public static class PlayerManagerTests
    {
        /// <summary>
        /// 运行所有测试
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("=== 玩家管理系统测试开始 ===");

            TestBasicPlayerInfo();
            TestPlayerOperations();
            TestPlayerLocation();
            TestPlayerWeapons();
            TestPlayerFinding();
            TestBatchOperations();

            Console.WriteLine("=== 玩家管理系统测试完成 ===");
        }

        /// <summary>
        /// 测试基础玩家信息
        /// </summary>
        private static void TestBasicPlayerInfo()
        {
            Console.WriteLine("\n--- 基础玩家信息测试 ---");

            int maxClients = PlayerManager.MaxClients;
            Console.WriteLine($"最大客户端数量: {maxClients}");

            int playerCount = PlayerManager.PlayerCount;
            Console.WriteLine($"在线玩家数量: {playerCount}");

            // 测试第一个有效玩家
            for (int i = 1; i <= maxClients; i++)
            {
                if (PlayerManager.IsPlayerValid(i))
                {
                    Console.WriteLine($"玩家 {i} 信息:");
                    Console.WriteLine($"  有效: {PlayerManager.IsPlayerValid(i)}");
                    Console.WriteLine($"  游戏中: {PlayerManager.IsPlayerInGame(i)}");
                    Console.WriteLine($"  存活: {PlayerManager.IsPlayerAlive(i)}");
                    Console.WriteLine($"  机器人: {PlayerManager.IsPlayerBot(i)}");
                    
                    if (PlayerManager.IsPlayerInGame(i))
                    {
                        Console.WriteLine($"  名称: {PlayerManager.GetPlayerName(i)}");
                        Console.WriteLine($"  IP: {PlayerManager.GetPlayerIPAddress(i)}");
                        Console.WriteLine($"  SteamID: {PlayerManager.GetPlayerAuthID(i)}");
                        Console.WriteLine($"  队伍: {PlayerManager.GetPlayerTeam(i)}");
                        Console.WriteLine($"  UserID: {PlayerManager.GetPlayerUserID(i)}");
                        Console.WriteLine($"  击杀: {PlayerManager.GetPlayerFrags(i)}");
                        Console.WriteLine($"  死亡: {PlayerManager.GetPlayerDeaths(i)}");
                        
                        if (PlayerManager.IsPlayerAlive(i))
                        {
                            Console.WriteLine($"  生命值: {PlayerManager.GetPlayerHealth(i)}");
                            Console.WriteLine($"  护甲: {PlayerManager.GetPlayerArmor(i)}");
                        }
                        
                        Console.WriteLine($"  延迟: {PlayerManager.GetPlayerPing(i)}");
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 测试玩家操作
        /// </summary>
        private static void TestPlayerOperations()
        {
            Console.WriteLine("\n--- 玩家操作测试 ---");

            int maxClients = PlayerManager.MaxClients;
            for (int i = 1; i <= maxClients; i++)
            {
                if (PlayerManager.IsPlayerAlive(i))
                {
                    Console.WriteLine($"测试玩家 {i} 的操作:");
                    
                    // 基础操作
                    Console.WriteLine($"  击杀玩家: KillPlayer({i}) - 已准备");
                    Console.WriteLine($"  拍打玩家: SlapPlayer({i}, 25, true) - 已准备");
                    Console.WriteLine($"  传送玩家: TeleportPlayer({i}, 100, 200, 300) - 已准备");
                    
                    // 新操作
                    Console.WriteLine($"  重生玩家: RespawnPlayer({i}) - 已准备");
                    Console.WriteLine($"  剥夺武器: StripWeapons({i}) - 已准备");
                    Console.WriteLine($"  给予武器: GiveWeapon({i}, \"weapon_ak47\", 30) - 已准备");
                    Console.WriteLine($"  设置队伍: SetPlayerTeam({i}, 2) - 已准备");
                    Console.WriteLine($"  冻结玩家: FreezePlayer({i}, true) - 已准备");
                    Console.WriteLine($"  设置生命值: SetPlayerHealth({i}, 75) - 已准备");
                    Console.WriteLine($"  设置护甲值: SetPlayerArmor({i}, 50) - 已准备");
                    Console.WriteLine($"  设置击杀数: SetPlayerFrags({i}, 15) - 已准备");
                    Console.WriteLine($"  设置死亡数: SetPlayerDeaths({i}, 8) - 已准备");
                    
                    break;
                }
            }
        }

        /// <summary>
        /// 测试玩家位置
        /// </summary>
        private static void TestPlayerLocation()
        {
            Console.WriteLine("\n--- 玩家位置测试 ---");

            int maxClients = PlayerManager.MaxClients;
            for (int i = 1; i <= maxClients; i++)
            {
                if (PlayerManager.IsPlayerInGame(i))
                {
                    var origin = PlayerManager.GetPlayerOrigin(i);
                    if (origin.HasValue)
                    {
                        Console.WriteLine($"玩家 {i} 位置: X={origin.Value.x}, Y={origin.Value.y}, Z={origin.Value.z}");
                    }

                    var velocity = PlayerManager.GetPlayerVelocity(i);
                    if (velocity.HasValue)
                    {
                        Console.WriteLine($"玩家 {i} 速度: X={velocity.Value.x}, Y={velocity.Value.y}, Z={velocity.Value.z}");
                    }

                    // 测试设置位置
                    Console.WriteLine($"设置玩家 {i} 位置: 已准备");
                    Console.WriteLine($"设置玩家 {i} 速度: 已准备");
                    
                    break;
                }
            }
        }

        /// <summary>
        /// 测试玩家武器
        /// </summary>
        private static void TestPlayerWeapons()
        {
            Console.WriteLine("\n--- 玩家武器测试 ---");

            int maxClients = PlayerManager.MaxClients;
            for (int i = 1; i <= maxClients; i++)
            {
                if (PlayerManager.IsPlayerAlive(i))
                {
                    int currentWeapon = PlayerManager.GetPlayerCurrentWeapon(i);
                    Console.WriteLine($"玩家 {i} 当前武器: {currentWeapon}");

                    // 测试一些常见武器
                    int[] testWeapons = { 1, 2, 3, 4, 5 }; // 假设的武器ID
                    foreach (var weapon in testWeapons)
                    {
                        int ammo = PlayerManager.GetPlayerAmmo(i, weapon);
                        bool hasWeapon = PlayerManager.PlayerHasWeapon(i, weapon);
                        Console.WriteLine($"  武器 {weapon}: 拥有={hasWeapon}, 弹药={ammo}");
                    }
                    
                    break;
                }
            }
        }

        /// <summary>
        /// 测试玩家查找
        /// </summary>
        private static void TestPlayerFinding()
        {
            Console.WriteLine("\n--- 玩家查找测试 ---");

            // 获取第一个有效玩家作为测试
            int maxClients = PlayerManager.MaxClients;
            int testPlayer = -1;
            string testName = "";
            string testIP = "";
            int testUserID = -1;

            for (int i = 1; i <= maxClients; i++)
            {
                if (PlayerManager.IsPlayerInGame(i))
                {
                    testPlayer = i;
                    testName = PlayerManager.GetPlayerName(i);
                    testIP = PlayerManager.GetPlayerIPAddress(i);
                    testUserID = PlayerManager.GetPlayerUserID(i);
                    break;
                }
            }

            if (testPlayer > 0)
            {
                Console.WriteLine($"使用玩家 {testPlayer} 进行查找测试:");
                
                int foundByUserID = PlayerManager.FindPlayerByUserID(testUserID);
                Console.WriteLine($"通过UserID查找: {testUserID} -> {foundByUserID}");

                if (!string.IsNullOrEmpty(testName))
                {
                    int foundByName = PlayerManager.FindPlayerByName(testName);
                    Console.WriteLine($"通过名称查找: {testName} -> {foundByName}");
                }

                if (!string.IsNullOrEmpty(testIP))
                {
                    int foundByIP = PlayerManager.FindPlayerByIPAddress(testIP);
                    Console.WriteLine($"通过IP查找: {testIP} -> {foundByIP}");
                }
            }
            else
            {
                Console.WriteLine("没有找到在线玩家进行查找测试");
            }
        }

        /// <summary>
        /// 测试批量操作
        /// </summary>
        private static void TestBatchOperations()
        {
            Console.WriteLine("\n--- 批量操作测试 ---");

            var onlinePlayers = PlayerManager.GetAllOnlinePlayers();
            Console.WriteLine($"在线玩家列表: {string.Join(", ", onlinePlayers)}");

            var alivePlayers = PlayerManager.GetAllAlivePlayers();
            Console.WriteLine($"存活玩家列表: {string.Join(", ", alivePlayers)}");

            var allPlayersInfo = PlayerManager.GetAllPlayersInfo();
            Console.WriteLine($"玩家信息总数: {allPlayersInfo.Count}");

            foreach (var player in allPlayersInfo)
            {
                Console.WriteLine($"  玩家 {player.PlayerId}: {player.Name} ({player.Team})");
            }
        }
    }
}