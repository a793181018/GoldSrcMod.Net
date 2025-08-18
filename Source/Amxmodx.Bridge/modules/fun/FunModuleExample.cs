// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using AmxModx.Bridge.Fun;

namespace FunModuleExample
{
    /// <summary>
    /// Fun模块C#桥接使用示例
    /// </summary>
    public class FunBridgeExample
    {
        /// <summary>
        /// 示例：设置玩家无敌模式
        /// </summary>
        public static void ExampleSetGodmode()
        {
            int playerIndex = 1; // 玩家索引从1开始
            
            // 开启无敌模式
            FunBridge.SetUserGodmode(playerIndex, 1);
            
            // 检查是否成功开启
            bool isGodmode = FunBridge.GetUserGodmode(playerIndex) != 0;
            Console.WriteLine($"玩家 {playerIndex} 无敌模式: {isGodmode}");
        }

        /// <summary>
        /// 示例：给予玩家武器
        /// </summary>
        public static void ExampleGiveWeapon()
        {
            int playerIndex = 1;
            string weaponName = "weapon_ak47";
            
            int result = FunBridge.GiveItem(playerIndex, weaponName);
            if (result > 0)
            {
                Console.WriteLine($"成功给予玩家 {playerIndex} {weaponName}");
            }
            else if (result == -1)
            {
                Console.WriteLine($"物品创建成功但无法拾取: {weaponName}");
            }
            else
            {
                Console.WriteLine($"无法创建物品: {weaponName}");
            }
        }

        /// <summary>
        /// 示例：设置玩家属性
        /// </summary>
        public static void ExampleSetPlayerAttributes()
        {
            int playerIndex = 1;
            
            // 设置生命值
            FunBridge.SetUserHealth(playerIndex, 100);
            
            // 设置护甲
            FunBridge.SetUserArmor(playerIndex, 100);
            
            // 设置击杀数
            FunBridge.SetUserFrags(playerIndex, 10);
            
            // 设置重力
            FunBridge.SetUserGravity(playerIndex, 0.5f);
            
            // 设置最大速度
            FunBridge.SetUserMaxspeed(playerIndex, 500.0f);
            
            Console.WriteLine($"玩家 {playerIndex} 属性已更新");
        }

        /// <summary>
        /// 示例：传送玩家
        /// </summary>
        public static void ExampleTeleportPlayer()
        {
            int playerIndex = 1;
            float[] newPosition = { 100.0f, 200.0f, 300.0f }; // X, Y, Z坐标
            
            int result = FunBridge.SetUserOrigin(playerIndex, newPosition);
            if (result != 0)
            {
                Console.WriteLine($"玩家 {playerIndex} 已传送到 ({newPosition[0]}, {newPosition[1]}, {newPosition[2]})");
            }
            else
            {
                Console.WriteLine($"传送失败");
            }
        }

        /// <summary>
        /// 示例：设置玩家渲染效果
        /// </summary>
        public static void ExampleSetRendering()
        {
            int playerIndex = 1;
            
            // 使用便捷方法设置渲染
            var rendering = new FunBridge.PlayerRendering
            {
                Fx = FunBridge.RenderFx.GlowShell,
                Red = 255,
                Green = 0,
                Blue = 0,
                RenderMode = FunBridge.RenderMode.Glow,
                Amount = 100.0f
            };
            
            FunBridge.SetPlayerRendering(playerIndex, rendering);
            Console.WriteLine($"玩家 {playerIndex} 渲染效果已设置");
        }

        /// <summary>
        /// 示例：获取玩家渲染信息
        /// </summary>
        public static void ExampleGetRendering()
        {
            int playerIndex = 1;
            
            try
            {
                var rendering = FunBridge.GetPlayerRendering(playerIndex);
                Console.WriteLine($"玩家 {playerIndex} 渲染信息:");
                Console.WriteLine($"  特效: {rendering.Fx}");
                Console.WriteLine($"  颜色: RGB({rendering.Red}, {rendering.Green}, {rendering.Blue})");
                Console.WriteLine($"  模式: {rendering.RenderMode}");
                Console.WriteLine($"  透明度: {rendering.Amount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取渲染信息失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 示例：设置命中区域
        /// </summary>
        public static void ExampleSetHitzones()
        {
            int attackerIndex = 1;
            int targetIndex = 2;
            
            // 只允许命中头部
            FunBridge.HitZones headOnly = FunBridge.HitZones.Head;
            FunBridge.SetUserHitzones(attackerIndex, targetIndex, (int)headOnly);
            
            Console.WriteLine($"设置玩家 {attackerIndex} 对玩家 {targetIndex} 只能命中头部");
        }

        /// <summary>
        /// 示例：设置穿墙模式
        /// </summary>
        public static void ExampleSetNoclip()
        {
            int playerIndex = 1;
            
            // 开启穿墙模式
            FunBridge.SetUserNoclip(playerIndex, 1);
            
            bool isNoclip = FunBridge.GetUserNoclip(playerIndex) != 0;
            Console.WriteLine($"玩家 {playerIndex} 穿墙模式: {isNoclip}");
        }

        /// <summary>
        /// 示例：设置无声脚步
        /// </summary>
        public static void ExampleSetFootsteps()
        {
            int playerIndex = 1;
            
            // 开启无声脚步
            FunBridge.SetUserFootsteps(playerIndex, 1);
            
            bool isSilent = FunBridge.GetUserFootsteps(playerIndex) != 0;
            Console.WriteLine($"玩家 {playerIndex} 无声脚步: {isSilent}");
        }

        /// <summary>
        /// 示例：移除武器
        /// </summary>
        public static void ExampleStripWeapons()
        {
            int playerIndex = 1;
            
            int result = FunBridge.StripUserWeapons(playerIndex);
            if (result != 0)
            {
                Console.WriteLine($"玩家 {playerIndex} 的武器已移除");
            }
            else
            {
                Console.WriteLine($"移除武器失败");
            }
        }

        /// <summary>
        /// 完整示例：创建管理员命令
        /// </summary>
        public static void ExampleAdminCommands()
        {
            Console.WriteLine("=== 管理员命令示例 ===");
            
            int targetPlayer = 1;
            
            // 给予AK47
            FunBridge.GiveItem(targetPlayer, "weapon_ak47");
            
            // 设置满血满甲
            FunBridge.SetUserHealth(targetPlayer, 100);
            FunBridge.SetUserArmor(targetPlayer, 100);
            
            // 设置正常属性
            FunBridge.SetUserGravity(targetPlayer, 1.0f);
            FunBridge.SetUserMaxspeed(targetPlayer, 250.0f);
            
            // 关闭特殊模式
            FunBridge.SetUserGodmode(targetPlayer, 0);
            FunBridge.SetUserNoclip(targetPlayer, 0);
            FunBridge.SetUserFootsteps(targetPlayer, 0);
            
            Console.WriteLine($"玩家 {targetPlayer} 已重置为标准状态");
        }

        /// <summary>
        /// 运行所有示例
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("开始运行Fun模块示例...\n");
            
            ExampleSetGodmode();
            ExampleGiveWeapon();
            ExampleSetPlayerAttributes();
            ExampleTeleportPlayer();
            ExampleSetRendering();
            ExampleGetRendering();
            ExampleSetHitzones();
            ExampleSetNoclip();
            ExampleSetFootsteps();
            ExampleStripWeapons();
            ExampleAdminCommands();
            
            Console.WriteLine("\n所有示例运行完成！");
        }
    }
}