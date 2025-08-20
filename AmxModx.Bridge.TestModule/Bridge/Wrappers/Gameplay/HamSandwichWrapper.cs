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
                return new Vector3(0, 0, 0);

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

        #region 内存操作（简化实现）

        /// <summary>
        /// 获取实体的基址指针（当前不支持）
        /// </summary>
        public static IntPtr GetEntityBasePointer(int entityId)
        {
            // 当前实现不支持内存访问
            return IntPtr.Zero;
        }

        /// <summary>
        /// 获取实体内存大小（当前不支持）
        /// </summary>
        public static int GetEntityMemorySize(int entityId)
        {
            // 当前实现不支持内存访问
            return 0;
        }

        /// <summary>
        /// 获取实体的私有数据指针（当前不支持）
        /// </summary>
        public static IntPtr GetPrivateDataPtr(int entityId)
        {
            // 当前实现不支持内存访问
            return IntPtr.Zero;
        }

        #endregion

        #region 工具函数（简化实现）

        /// <summary>
        /// 将实体指针转换为实体索引（当前不支持）
        /// </summary>
        public static int EntityToIndex(IntPtr edict)
        {
            // 当前实现不支持指针转换
            return -1;
        }

        /// <summary>
        /// 将实体索引转换为实体指针（当前不支持）
        /// </summary>
        public static IntPtr IndexToEntity(int index)
        {
            // 当前实现不支持指针转换
            return IntPtr.Zero;
        }

        #endregion
    }
}