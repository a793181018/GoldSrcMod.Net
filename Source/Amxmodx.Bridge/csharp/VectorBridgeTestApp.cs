using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge;

namespace VectorBridgeTestApp
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        static void Main(string[] args)
        {
            Console.WriteLine("=== AMX Mod X Vector Bridge Test ===");
            
            // 测试向量转角度
            TestVectorToAngle();
            
            // 测试角度转向量
            TestAngleToVector();
            
            // 测试向量长度
            TestVectorLength();
            
            // 测试向量距离
            TestVectorDistance();
            
            Console.WriteLine("所有测试完成！");
            Console.ReadKey();
        }

        static void TestVectorToAngle()
        {
            Console.WriteLine("\n--- 测试向量转角度 ---");
            
            float x = 100.0f, y = 0.0f, z = 0.0f;
            float outX = 0, outY = 0, outZ = 0;
            
            Vector.VectorToAngle(x, y, z, ref outX, ref outY, ref outZ);
            
            Console.WriteLine($"输入向量: ({x}, {y}, {z})");
            Console.WriteLine($"输出角度: ({outX}, {outY}, {outZ})");
        }

        static void TestAngleToVector()
        {
            Console.WriteLine("\n--- 测试角度转向量 ---");
            
            float pitch = 0.0f, yaw = 90.0f, roll = 0.0f;
            float outX = 0, outY = 0, outZ = 0;
            
            Vector.AngleVector(pitch, yaw, roll, (int)AngleVectorType.FORWARD, ref outX, ref outY, ref outZ);
            
            Console.WriteLine($"输入角度: ({pitch}, {yaw}, {roll})");
            Console.WriteLine($"输出向量 (FORWARD): ({outX}, {outY}, {outZ})");
        }

        static void TestVectorLength()
        {
            Console.WriteLine("\n--- 测试向量长度 ---");
            
            float x = 3.0f, y = 4.0f, z = 0.0f;
            float length = Vector.VectorLength(x, y, z);
            
            Console.WriteLine($"向量 ({x}, {y}, {z}) 的长度: {length}");
        }

        static void TestVectorDistance()
        {
            Console.WriteLine("\n--- 测试向量距离 ---");
            
            float x1 = 0.0f, y1 = 0.0f, z1 = 0.0f;
            float x2 = 3.0f, y2 = 4.0f, z2 = 0.0f;
            float distance = Vector.VectorDistance(x1, y1, z1, x2, y2, z2);
            
            Console.WriteLine($"点 ({x1}, {y1}, {z1}) 到 ({x2}, {y2}, {z2}) 的距离: {distance}");
        }
    }
}