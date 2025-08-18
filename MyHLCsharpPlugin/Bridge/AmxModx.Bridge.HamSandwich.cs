using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// Ham Sandwich模块的直接桥接接口 - 不依赖AMXX函数
    /// 直接操作底层内存和实体数据
    /// </summary>
    public static class NativeDirectBridge
    {
        private const string DllName = "hamsandwich_amxx";

        // 实体验证
        /// <summary>
        /// 验证实体ID是否有效
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsValidEntity(int entityId);

        /// <summary>
        /// 验证偏移量是否有效
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsValidOffset(int offset);

        // 直接数据访问
        /// <summary>
        /// 获取实体私有数据的整型值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityPrivateDataInt(int entityId, int offset);

        /// <summary>
        /// 获取实体私有数据的浮点值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetEntityPrivateDataFloat(int entityId, int offset);

        /// <summary>
        /// 获取实体私有数据的实体引用
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityPrivateDataEntity(int entityId, int offset);

        /// <summary>
        /// 设置实体私有数据的整型值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityPrivateDataInt(int entityId, int offset, int value);

        /// <summary>
        /// 设置实体私有数据的浮点值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityPrivateDataFloat(int entityId, int offset, float value);

        /// <summary>
        /// 设置实体私有数据的实体引用
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityPrivateDataEntity(int entityId, int offset, int targetEntity);

        // 向量数据访问
        /// <summary>
        /// 获取实体私有数据的向量值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEntityPrivateDataVector(int entityId, int offset, [Out] float[] vec);

        /// <summary>
        /// 设置实体私有数据的向量值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityPrivateDataVector(int entityId, int offset, float[] vec);

        // 实体变量访问
        /// <summary>
        /// 获取实体变量的整型值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityVarInt(int entityId, int offset);

        /// <summary>
        /// 获取实体变量的浮点值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetEntityVarFloat(int entityId, int offset);

        /// <summary>
        /// 获取实体变量的向量值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEntityVarVector(int entityId, int offset, [Out] float[] vec);

        /// <summary>
        /// 设置实体变量的整型值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarInt(int entityId, int offset, int value);

        /// <summary>
        /// 设置实体变量的浮点值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarFloat(int entityId, int offset, float value);

        /// <summary>
        /// 设置实体变量的向量值
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarVector(int entityId, int offset, float[] vec);

        // 内存工具
        /// <summary>
        /// 获取实体基础内存指针
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetEntityBasePointer(int entityId);

        /// <summary>
        /// 获取实体内存大小
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityMemorySize(int entityId);

        /// <summary>
        /// 将实体指针转换为索引
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EntityToIndex(IntPtr edict);

        /// <summary>
        /// 将索引转换为实体指针
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr IndexToEntity(int index);

        /// <summary>
        /// 获取私有数据指针
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetPrivateDataPtr(int entityId);
    }

    /// <summary>
    /// 直接桥接接口的托管包装类
    /// </summary>
    public static class DirectBridgeWrapper
    {
        /// <summary>
        /// 安全地获取实体私有数据整型值
        /// </summary>
        public static int SafeGetEntityPrivateDataInt(int entityId, int offset, int defaultValue = 0)
        {
            if (!NativeDirectBridge.IsValidEntity(entityId) || !NativeDirectBridge.IsValidOffset(offset))
                return defaultValue;

            return NativeDirectBridge.GetEntityPrivateDataInt(entityId, offset);
        }

        /// <summary>
        /// 安全地设置实体私有数据整型值
        /// </summary>
        public static bool SafeSetEntityPrivateDataInt(int entityId, int offset, int value)
        {
            if (!NativeDirectBridge.IsValidEntity(entityId) || !NativeDirectBridge.IsValidOffset(offset))
                return false;

            NativeDirectBridge.SetEntityPrivateDataInt(entityId, offset, value);
            return true;
        }

        /// <summary>
        /// 安全地获取实体私有数据浮点值
        /// </summary>
        public static float SafeGetEntityPrivateDataFloat(int entityId, int offset, float defaultValue = 0.0f)
        {
            if (!NativeDirectBridge.IsValidEntity(entityId) || !NativeDirectBridge.IsValidOffset(offset))
                return defaultValue;

            return NativeDirectBridge.GetEntityPrivateDataFloat(entityId, offset);
        }

        /// <summary>
        /// 安全地设置实体私有数据浮点值
        /// </summary>
        public static bool SafeSetEntityPrivateDataFloat(int entityId, int offset, float value)
        {
            if (!NativeDirectBridge.IsValidEntity(entityId) || !NativeDirectBridge.IsValidOffset(offset))
                return false;

            NativeDirectBridge.SetEntityPrivateDataFloat(entityId, offset, value);
            return true;
        }

        /// <summary>
        /// 安全地获取实体私有数据向量
        /// </summary>
        public static bool SafeGetEntityPrivateDataVector(int entityId, int offset, out float[] vector)
        {
            vector = new float[3];
            if (!NativeDirectBridge.IsValidEntity(entityId) || !NativeDirectBridge.IsValidOffset(offset))
                return false;

            NativeDirectBridge.GetEntityPrivateDataVector(entityId, offset, vector);
            return true;
        }

        /// <summary>
        /// 安全地设置实体私有数据向量
        /// </summary>
        public static bool SafeSetEntityPrivateDataVector(int entityId, int offset, float[] vector)
        {
            if (!NativeDirectBridge.IsValidEntity(entityId) || !NativeDirectBridge.IsValidOffset(offset) || vector == null || vector.Length != 3)
                return false;

            NativeDirectBridge.SetEntityPrivateDataVector(entityId, offset, vector);
            return true;
        }
    }
}