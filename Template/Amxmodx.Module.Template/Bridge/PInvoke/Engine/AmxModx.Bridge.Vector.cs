// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge
{
    /// <summary>
    /// 向量运算桥接接口
    /// </summary>
    public static class Vector
    {
        private const string DllName = "amxmodx_mm";
        /// <summary>
        /// 将3D向量转换为角度
        /// </summary>
        /// <param name="x">向量的X分量</param>
        /// <param name="y">向量的Y分量</param>
        /// <param name="z">向量的Z分量</param>
        /// <param name="outX">输出的X角度</param>
        /// <param name="outY">输出的Y角度</param>
        /// <param name="outZ">输出的Z角度</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_VectorToAngle(float x, float y, float z, out float outX, out float outY, out float outZ);

        /// <summary>
        /// 将角度转换为3D向量
        /// </summary>
        /// <param name="pitch">俯仰角</param>
        /// <param name="yaw">偏航角</param>
        /// <param name="roll">翻滚角</param>
        /// <param name="type">向量类型：1=前向，2=右向，3=上向</param>
        /// <param name="outX">输出的X分量</param>
        /// <param name="outY">输出的Y分量</param>
        /// <param name="outZ">输出的Z分量</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_AngleVector(float pitch, float yaw, float roll, int type, out float outX, out float outY, out float outZ);

        /// <summary>
        /// 计算3D向量的长度
        /// </summary>
        /// <param name="x">向量的X分量</param>
        /// <param name="y">向量的Y分量</param>
        /// <param name="z">向量的Z分量</param>
        /// <returns>向量的长度</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_VectorLength(float x, float y, float z);

        /// <summary>
        /// 计算两个3D向量之间的距离
        /// </summary>
        /// <param name="x1">第一个向量的X分量</param>
        /// <param name="y1">第一个向量的Y分量</param>
        /// <param name="z1">第一个向量的Z分量</param>
        /// <param name="x2">第二个向量的X分量</param>
        /// <param name="y2">第二个向量的Y分量</param>
        /// <param name="z2">第二个向量的Z分量</param>
        /// <returns>两个向量之间的距离</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_VectorDistance(float x1, float y1, float z1, float x2, float y2, float z2);

        /// <summary>
        /// 通过实体的瞄准方向获取速度向量
        /// </summary>
        /// <param name="entity">实体索引</param>
        /// <param name="velocity">速度大小</param>
        /// <param name="outX">输出的X分量</param>
        /// <param name="outY">输出的Y分量</param>
        /// <param name="outZ">输出的Z分量</param>
        /// <returns>操作是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_VelocityByAim(int entity, int velocity, out float outX, out float outY, out float outZ);
    }

    /// <summary>
    /// 向量类型枚举
    /// </summary>
    public enum AngleVectorType
    {
        /// <summary>
        /// 前向向量
        /// </summary>
        Forward = 1,
        
        /// <summary>
        /// 右向向量
        /// </summary>
        Right = 2,
        
        /// <summary>
        /// 上向向量
        /// </summary>
        Up = 3
    }

    /// <summary>
    /// 3D向量结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// 计算向量长度
        /// </summary>
        public float Length()
        {
            return Vector.AmxModx_Bridge_VectorLength(X, Y, Z);
        }

        /// <summary>
        /// 计算与另一个向量的距离
        /// </summary>
        public float DistanceTo(Vector3 other)
        {
            return Vector.AmxModx_Bridge_VectorDistance(X, Y, Z, other.X, other.Y, other.Z);
        }

        /// <summary>
        /// 转换为角度
        /// </summary>
        public Vector3 ToAngles()
        {
            Vector.AmxModx_Bridge_VectorToAngle(X, Y, Z, out float x, out float y, out float z);
            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// 3D角度结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Angle3
    {
        public float Pitch;
        public float Yaw;
        public float Roll;

        public Angle3(float pitch, float yaw, float roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }

        /// <summary>
        /// 转换为向量
        /// </summary>
        public Vector3 ToVector(AngleVectorType type)
        {
            Vector.AmxModx_Bridge_AngleVector(Pitch, Yaw, Roll, (int)type, out float x, out float y, out float z);
            return new Vector3(x, y, z);
        }
    }
}