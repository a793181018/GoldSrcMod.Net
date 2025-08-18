// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using AmxModx.Bridge;

namespace VectorBridgeTest
{
    /// <summary>
    /// 向量桥接层测试类
    /// </summary>
    public static class VectorBridgeTest
    {
        /// <summary>
        /// 测试向量转角度功能
        /// </summary>
        public static void TestVectorToAngle()
        {
            Console.WriteLine("=== 测试向量转角度 ===");
            
            // 测试向量 (1, 0, 0) - 指向X轴正方向
            var vector = new Vector3(1, 0, 0);
            var angle = VectorManager.VectorToAngle(vector);
            
            Console.WriteLine($"向量 {vector} -> 角度: Pitch={angle.Pitch}, Yaw={angle.Yaw}, Roll={angle.Roll}");
            
            // 测试向量 (0, 1, 0) - 指向Y轴正方向
            vector = new Vector3(0, 1, 0);
            angle = VectorManager.VectorToAngle(vector);
            
            Console.WriteLine($"向量 {vector} -> 角度: Pitch={angle.Pitch}, Yaw={angle.Yaw}, Roll={angle.Roll}");
            
            // 测试向量 (0, 0, 1) - 指向Z轴正方向
            vector = new Vector3(0, 0, 1);
            angle = VectorManager.VectorToAngle(vector);
            
            Console.WriteLine($"向量 {vector} -> 角度: Pitch={angle.Pitch}, Yaw={angle.Yaw}, Roll={angle.Roll}");
        }

        /// <summary>
        /// 测试角度转向量功能
        /// </summary>
        public static void TestAngleToVector()
        {
            Console.WriteLine("\n=== 测试角度转向量 ===");
            
            // 测试角度 (0, 0, 0) - 水平向前
            var angle = new Angle3(0, 0, 0);
            var forward = VectorManager.AngleToForwardVector(angle);
            var right = VectorManager.AngleToRightVector(angle);
            var up = VectorManager.AngleToUpVector(angle);
            
            Console.WriteLine($"角度 {angle} -> 前向: {forward}");
            Console.WriteLine($"角度 {angle} -> 右向: {right}");
            Console.WriteLine($"角度 {angle} -> 上向: {up}");
            
            // 测试角度 (45, 90, 0)
            angle = new Angle3(45, 90, 0);
            forward = VectorManager.AngleToForwardVector(angle);
            right = VectorManager.AngleToRightVector(angle);
            up = VectorManager.AngleToUpVector(angle);
            
            Console.WriteLine($"角度 {angle} -> 前向: {forward}");
            Console.WriteLine($"角度 {angle} -> 右向: {right}");
            Console.WriteLine($"角度 {angle} -> 上向: {up}");
        }

        /// <summary>
        /// 测试向量长度和距离计算
        /// </summary>
        public static void TestVectorLengthAndDistance()
        {
            Console.WriteLine("\n=== 测试向量长度和距离 ===");
            
            // 测试向量长度
            var vector1 = new Vector3(3, 4, 0);
            var length = VectorManager.GetVectorLength(vector1);
            Console.WriteLine($"向量 {vector1} 的长度: {length} (期望: 5.0)");
            
            // 测试向量距离
            var vector2 = new Vector3(0, 0, 0);
            var distance = VectorManager.GetDistance(vector1, vector2);
            Console.WriteLine($"向量 {vector1} 和 {vector2} 的距离: {distance} (期望: 5.0)");
            
            var vector3 = new Vector3(1, 2, 3);
            var vector4 = new Vector3(4, 6, 8);
            distance = VectorManager.GetDistance(vector3, vector4);
            Console.WriteLine($"向量 {vector3} 和 {vector4} 的距离: {distance}");
        }

        /// <summary>
        /// 测试向量运算扩展方法
        /// </summary>
        public static void TestVectorExtensions()
        {
            Console.WriteLine("\n=== 测试向量扩展方法 ===");
            
            var v1 = new Vector3(1, 2, 3);
            var v2 = new Vector3(4, 5, 6);
            
            Console.WriteLine($"向量1: {v1}");
            Console.WriteLine($"向量2: {v2}");
            Console.WriteLine($"加法: {v1.Add(v2)}");
            Console.WriteLine($"减法: {v1.Subtract(v2)}");
            Console.WriteLine($"点积: {v1.Dot(v2)}");
            Console.WriteLine($"叉积: {v1.Cross(v2)}");
            Console.WriteLine($"标准化向量1: {v1.Normalize()}");
            Console.WriteLine($"缩放向量1 (x2): {v1.Scale(2)}");
        }

        /// <summary>
        /// 运行所有测试
        /// </summary>
        public static void RunAllTests()
        {
            try
            {
                TestVectorToAngle();
                TestAngleToVector();
                TestVectorLengthAndDistance();
                TestVectorExtensions();
                
                Console.WriteLine("\n=== 所有测试完成 ===");
                Console.WriteLine("向量桥接层测试成功完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"测试失败: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            }
        }
    }
}