using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// HamSandwich模块的C#包装器
    /// 提供更友好的C#接口封装
    /// </summary>
    public static class HamSandwichWrapper
    {
        /// <summary>
        /// 3D向量结构体
        /// </summary>
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

            public static Vector3 Zero => new Vector3(0, 0, 0);

            public override string ToString()
            {
                return $"({X}, {Y}, {Z})";
            }
        }

        #region 实体验证

        /// <summary>
        /// 验证实体是否有效
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>是否有效</returns>
        public static bool IsValidEntity(int entityId)
        {
            return NativeMethods.IsValidEntity(entityId);
        }

        /// <summary>
        /// 验证偏移量是否有效
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <returns>是否有效</returns>
        public static bool IsValidOffset(int offset)
        {
            return NativeMethods.IsValidOffset(offset);
        }

        #endregion

        #region 实体私有数据访问

        /// <summary>
        /// 获取实体私有数据的整数值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <returns>整数值</returns>
        public static int GetPrivateDataInt(int entityId, int offset)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return 0;
            return NativeMethods.GetPrivateDataInt(entityId, offset);
        }

        /// <summary>
        /// 获取实体私有数据的浮点值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <returns>浮点值</returns>
        public static float GetPrivateDataFloat(int entityId, int offset)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return 0.0f;
            return NativeMethods.GetPrivateDataFloat(entityId, offset);
        }

        /// <summary>
        /// 获取实体私有数据的实体引用
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <returns>实体ID</returns>
        public static int GetPrivateDataEntity(int entityId, int offset)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return -1;
            return NativeMethods.GetPrivateDataEntity(entityId, offset);
        }

        /// <summary>
        /// 设置实体私有数据的整数值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否成功</returns>
        public static bool SetPrivateDataInt(int entityId, int offset, int value)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return false;
            NativeMethods.SetPrivateDataInt(entityId, offset, value);
            return true;
        }

        /// <summary>
        /// 设置实体私有数据的浮点值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否成功</returns>
        public static bool SetPrivateDataFloat(int entityId, int offset, float value)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return false;
            NativeMethods.SetPrivateDataFloat(entityId, offset, value);
            return true;
        }

        /// <summary>
        /// 设置实体私有数据的实体引用
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <param name="targetEntity">目标实体ID</param>
        /// <returns>是否成功</returns>
        public static bool SetPrivateDataEntity(int entityId, int offset, int targetEntity)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset) || !IsValidEntity(targetEntity))
                return false;
            NativeMethods.SetPrivateDataEntity(entityId, offset, targetEntity);
            return true;
        }

        #endregion

        #region 向量数据访问

        /// <summary>
        /// 获取实体私有数据的向量值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <returns>向量值</returns>
        public static Vector3 GetPrivateDataVector(int entityId, int offset)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return Vector3.Zero;

            float[] vec = new float[3];
            NativeMethods.GetPrivateDataVector(entityId, offset, vec);
            return new Vector3(vec[0], vec[1], vec[2]);
        }

        /// <summary>
        /// 设置实体私有数据的向量值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <param name="vector">要设置的向量值</param>
        /// <returns>是否成功</returns>
        public static bool SetPrivateDataVector(int entityId, int offset, Vector3 vector)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return false;

            float[] vec = new float[] { vector.X, vector.Y, vector.Z };
            NativeMethods.SetPrivateDataVector(entityId, offset, vec);
            return true;
        }

        #endregion

        #region 实体变量访问

        /// <summary>
        /// 获取实体变量的整数值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <returns>整数值</returns>
        public static int GetEntityVarInt(int entityId, int offset)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return 0;
            return NativeMethods.GetEntityVarInt(entityId, offset);
        }

        /// <summary>
        /// 获取实体变量的浮点值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <returns>浮点值</returns>
        public static float GetEntityVarFloat(int entityId, int offset)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return 0.0f;
            return NativeMethods.GetEntityVarFloat(entityId, offset);
        }

        /// <summary>
        /// 获取实体变量的向量值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <returns>向量值</returns>
        public static Vector3 GetEntityVarVector(int entityId, int offset)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return Vector3.Zero;

            float[] vec = new float[3];
            NativeMethods.GetEntityVarVector(entityId, offset, vec);
            return new Vector3(vec[0], vec[1], vec[2]);
        }

        /// <summary>
        /// 设置实体变量的整数值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否成功</returns>
        public static bool SetEntityVarInt(int entityId, int offset, int value)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return false;
            NativeMethods.SetEntityVarInt(entityId, offset, value);
            return true;
        }

        /// <summary>
        /// 设置实体变量的浮点值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否成功</returns>
        public static bool SetEntityVarFloat(int entityId, int offset, float value)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return false;
            NativeMethods.SetEntityVarFloat(entityId, offset, value);
            return true;
        }

        /// <summary>
        /// 设置实体变量的向量值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="offset">偏移量</param>
        /// <param name="vector">要设置的向量值</param>
        /// <returns>是否成功</returns>
        public static bool SetEntityVarVector(int entityId, int offset, Vector3 vector)
        {
            if (!IsValidEntity(entityId) || !IsValidOffset(offset))
                return false;

            float[] vec = new float[] { vector.X, vector.Y, vector.Z };
            NativeMethods.SetEntityVarVector(entityId, offset, vec);
            return true;
        }

        #endregion

        #region 内存操作

        /// <summary>
        /// 获取实体的基址指针
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>基址指针</returns>
        public static IntPtr GetEntityBasePointer(int entityId)
        {
            if (!IsValidEntity(entityId))
                return IntPtr.Zero;
            return NativeMethods.GetEntityBasePointer(entityId);
        }

        /// <summary>
        /// 获取实体内存大小
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>内存大小（字节）</returns>
        public static int GetEntityMemorySize(int entityId)
        {
            if (!IsValidEntity(entityId))
                return 0;
            return NativeMethods.GetEntityMemorySize(entityId);
        }

        /// <summary>
        /// 获取实体的私有数据指针
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>私有数据指针</returns>
        public static IntPtr GetPrivateDataPtr(int entityId)
        {
            if (!IsValidEntity(entityId))
                return IntPtr.Zero;
            return NativeMethods.GetPrivateDataPtr(entityId);
        }

        #endregion

        #region 工具函数

        /// <summary>
        /// 将实体指针转换为实体索引
        /// </summary>
        /// <param name="edict">实体指针</param>
        /// <returns>实体索引</returns>
        public static int EntityToIndex(IntPtr edict)
        {
            if (edict == IntPtr.Zero)
                return -1;
            return NativeMethods.EntityToIndex(edict);
        }

        /// <summary>
        /// 将实体索引转换为实体指针
        /// </summary>
        /// <param name="index">实体索引</param>
        /// <returns>实体指针</returns>
        public static IntPtr IndexToEntity(int index)
        {
            if (index < 0 || index >= 2048) // CS 1.6最大实体数
                return IntPtr.Zero;
            return NativeMethods.IndexToEntity(index);
        }

        #endregion
    }
}