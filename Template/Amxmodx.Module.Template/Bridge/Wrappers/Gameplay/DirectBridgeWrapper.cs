using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich.Direct
{


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