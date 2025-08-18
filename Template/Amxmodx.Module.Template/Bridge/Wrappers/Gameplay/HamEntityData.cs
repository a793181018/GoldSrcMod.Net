using System;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// 实体私有数据访问器
    /// </summary>
    public static class HamEntityData
    {
        /// <summary>
        /// 获取实体私有数据（整数）
        /// </summary>
        public static int GetInt(int entity, int offset)
        {
            return NativeMethods.GetPrivateDataInt(entity, offset);
        }

        /// <summary>
        /// 获取实体私有数据（浮点数）
        /// </summary>
        public static float GetFloat(int entity, int offset)
        {
            return NativeMethods.GetPrivateDataFloat(entity, offset);
        }

        /// <summary>
        /// 获取实体私有数据（实体）
        /// </summary>
        public static int GetEntity(int entity, int offset)
        {
            return NativeMethods.GetPrivateDataEntity(entity, offset);
        }

        /// <summary>
        /// 设置实体私有数据（整数）
        /// </summary>
        public static void SetInt(int entity, int offset, int value)
        {
            NativeMethods.SetPrivateDataInt(entity, offset, value);
        }

        /// <summary>
        /// 设置实体私有数据（浮点数）
        /// </summary>
        public static void SetFloat(int entity, int offset, float value)
        {
            NativeMethods.SetPrivateDataFloat(entity, offset, value);
        }

        /// <summary>
        /// 设置实体私有数据（实体）
        /// </summary>
        public static void SetEntity(int entity, int offset, int entityValue)
        {
            NativeMethods.SetPrivateDataEntity(entity, offset, entityValue);
        }
    }

    /// <summary>
    /// Ham返回值管理器
    /// </summary>
    public static class HamReturnValue
    {
        /// <summary>
        /// 获取返回状态
        /// </summary>
        public static HamReturnStatus GetStatus()
        {
            return (HamReturnStatus)NativeMethods.GetReturnStatus();
        }

        /// <summary>
        /// 获取返回值（整数）
        /// </summary>
        public static int GetInt()
        {
            return NativeMethods.GetReturnValueInt();
        }

        /// <summary>
        /// 获取返回值（浮点数）
        /// </summary>
        public static float GetFloat()
        {
            return NativeMethods.GetReturnValueFloat();
        }

        /// <summary>
        /// 获取返回值（向量）
        /// </summary>
        public static Vector3 GetVector()
        {
            float[] vec = new float[3];
            NativeMethods.GetReturnValueVector(vec);
            return new Vector3(vec[0], vec[1], vec[2]);
        }

        /// <summary>
        /// 获取返回值（实体）
        /// </summary>
        public static int GetEntity()
        {
            return NativeMethods.GetReturnValueEntity();
        }

        /// <summary>
        /// 设置返回值（整数）
        /// </summary>
        public static void SetInt(int value)
        {
            NativeMethods.SetReturnValueInt(value);
        }

        /// <summary>
        /// 设置返回值（浮点数）
        /// </summary>
        public static void SetFloat(float value)
        {
            NativeMethods.SetReturnValueFloat(value);
        }

        /// <summary>
        /// 设置返回值（向量）
        /// </summary>
        public static void SetVector(Vector3 value)
        {
            float[] vec = { value.X, value.Y, value.Z };
            NativeMethods.SetReturnValueVector(vec);
        }

        /// <summary>
        /// 设置返回值（实体）
        /// </summary>
        public static void SetEntity(int entity)
        {
            NativeMethods.SetReturnValueEntity(entity);
        }
    }
}