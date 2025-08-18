using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// Ham Sandwich桥接管理器，提供简化的API访问
    /// </summary>
    public static class HamSandwichManager
    {
        /// <summary>
        /// 初始化Ham Sandwich桥接
        /// </summary>
        public static bool Initialize()
        {
            return HamSandwichBridge.InitializeHamSandwichBridge();
        }

        /// <summary>
        /// 清理Ham Sandwich桥接资源
        /// </summary>
        public static void Cleanup()
        {
            HamSandwichBridge.CleanupHamSandwichBridge();
        }

        /// <summary>
        /// 注册钩子
        /// </summary>
        public static int RegisterHook(HamHookType hookType, string entityClass, HamHookCallback preCallback, HamHookCallback postCallback)
        {
            return HamSandwichBridge.RegisterHamHook((int)hookType, entityClass, preCallback, postCallback);
        }

        /// <summary>
        /// 取消注册钩子
        /// </summary>
        public static bool UnregisterHook(int hookId)
        {
            return HamSandwichBridge.UnregisterHamHook(hookId);
        }

        /// <summary>
        /// 获取实体私有数据（整数）
        /// </summary>
        public static int GetPrivateDataInt(int entity, string key, int element = 0)
        {
            return HamSandwichBridge.GetPrivateDataInt(entity, key, element);
        }

        /// <summary>
        /// 设置实体私有数据（整数）
        /// </summary>
        public static void SetPrivateDataInt(int entity, string key, int value, int element = 0)
        {
            HamSandwichBridge.SetPrivateDataInt(entity, key, value, element);
        }

        /// <summary>
        /// 获取实体私有数据（浮点数）
        /// </summary>
        public static float GetPrivateDataFloat(int entity, string key, int element = 0)
        {
            return HamSandwichBridge.GetPrivateDataFloat(entity, key, element);
        }

        /// <summary>
        /// 设置实体私有数据（浮点数）
        /// </summary>
        public static void SetPrivateDataFloat(int entity, string key, float value, int element = 0)
        {
            HamSandwichBridge.SetPrivateDataFloat(entity, key, value, element);
        }

        /// <summary>
        /// 获取实体私有数据（字符串）
        /// </summary>
        public static string GetPrivateDataString(int entity, string key, int element = 0)
        {
            var buffer = new StringBuilder(256);
            HamSandwichBridge.GetPrivateDataString(entity, key, buffer, buffer.Capacity, element);
            return buffer.ToString();
        }

        /// <summary>
        /// 设置实体私有数据（字符串）
        /// </summary>
        public static void SetPrivateDataString(int entity, string key, string value, int element = 0)
        {
            HamSandwichBridge.SetPrivateDataString(entity, key, value, element);
        }

        /// <summary>
        /// 获取实体私有数据（向量）
        /// </summary>
        public static Vector3 GetPrivateDataVector(int entity, string key, int element = 0)
        {
            var vector = new float[3];
            HamSandwichBridge.GetPrivateDataVector(entity, key, vector, element);
            return new Vector3(vector[0], vector[1], vector[2]);
        }

        /// <summary>
        /// 设置实体私有数据（向量）
        /// </summary>
        public static void SetPrivateDataVector(int entity, string key, Vector3 value, int element = 0)
        {
            var vector = new[] { value.X, value.Y, value.Z };
            HamSandwichBridge.SetPrivateDataVector(entity, key, vector, element);
        }

        /// <summary>
        /// 获取返回值（整数）
        /// </summary>
        public static int GetReturnValueInt()
        {
            return HamSandwichBridge.GetReturnValueInt();
        }

        /// <summary>
        /// 设置返回值（整数）
        /// </summary>
        public static void SetReturnValueInt(int value)
        {
            HamSandwichBridge.SetReturnValueInt(value);
        }

        /// <summary>
        /// 获取返回值（浮点数）
        /// </summary>
        public static float GetReturnValueFloat()
        {
            return HamSandwichBridge.GetReturnValueFloat();
        }

        /// <summary>
        /// 设置返回值（浮点数）
        /// </summary>
        public static void SetReturnValueFloat(float value)
        {
            HamSandwichBridge.SetReturnValueFloat(value);
        }

        /// <summary>
        /// 获取返回值（字符串）
        /// </summary>
        public static string GetReturnValueString()
        {
            var buffer = new StringBuilder(256);
            HamSandwichBridge.GetReturnValueString(buffer, buffer.Capacity);
            return buffer.ToString();
        }

        /// <summary>
        /// 设置返回值（字符串）
        /// </summary>
        public static void SetReturnValueString(string value)
        {
            HamSandwichBridge.SetReturnValueString(value);
        }

        /// <summary>
        /// 获取返回值（向量）
        /// </summary>
        public static Vector3 GetReturnValueVector()
        {
            var vector = new float[3];
            HamSandwichBridge.GetReturnValueVector(vector);
            return new Vector3(vector[0], vector[1], vector[2]);
        }

        /// <summary>
        /// 设置返回值（向量）
        /// </summary>
        public static void SetReturnValueVector(Vector3 value)
        {
            var vector = new[] { value.X, value.Y, value.Z };
            HamSandwichBridge.SetReturnValueVector(vector);
        }

        /// <summary>
        /// 获取参数值（整数）
        /// </summary>
        public static int GetParameterInt(int paramIndex)
        {
            return HamSandwichBridge.GetParameterInt(paramIndex);
        }

        /// <summary>
        /// 设置参数值（整数）
        /// </summary>
        public static void SetParameterInt(int paramIndex, int value)
        {
            HamSandwichBridge.SetParameterInt(paramIndex, value);
        }

        /// <summary>
        /// 获取参数值（浮点数）
        /// </summary>
        public static float GetParameterFloat(int paramIndex)
        {
            return HamSandwichBridge.GetParameterFloat(paramIndex);
        }

        /// <summary>
        /// 设置参数值（浮点数）
        /// </summary>
        public static void SetParameterFloat(int paramIndex, float value)
        {
            HamSandwichBridge.SetParameterFloat(paramIndex, value);
        }

        /// <summary>
        /// 获取参数值（字符串）
        /// </summary>
        public static string GetParameterString(int paramIndex)
        {
            var buffer = new StringBuilder(256);
            HamSandwichBridge.GetParameterString(paramIndex, buffer, buffer.Capacity);
            return buffer.ToString();
        }

        /// <summary>
        /// 设置参数值（字符串）
        /// </summary>
        public static void SetParameterString(int paramIndex, string value)
        {
            HamSandwichBridge.SetParameterString(paramIndex, value);
        }

        /// <summary>
        /// 获取参数值（向量）
        /// </summary>
        public static Vector3 GetParameterVector(int paramIndex)
        {
            var vector = new float[3];
            HamSandwichBridge.GetParameterVector(paramIndex, vector);
            return new Vector3(vector[0], vector[1], vector[2]);
        }

        /// <summary>
        /// 设置参数值（向量）
        /// </summary>
        public static void SetParameterVector(int paramIndex, Vector3 value)
        {
            var vector = new[] { value.X, value.Y, value.Z };
            HamSandwichBridge.SetParameterVector(paramIndex, vector);
        }

        /// <summary>
        /// 验证实体是否有效
        /// </summary>
        public static bool IsValidEntity(int entity)
        {
            return HamSandwichBridge.IsValidEntity(entity);
        }

        /// <summary>
        /// 获取实体索引
        /// </summary>
        public static int EntityToIndex(int entity)
        {
            return HamSandwichBridge.EntityToIndex(entity);
        }

        /// <summary>
        /// 获取实体指针
        /// </summary>
        public static int IndexToEntity(int index)
        {
            return HamSandwichBridge.IndexToEntity(index);
        }

        /// <summary>
        /// 获取实体变量
        /// </summary>
        public static int GetEntityVarInt(int entity, string varName)
        {
            return HamSandwichBridge.GetEntityVarInt(entity, varName);
        }

        /// <summary>
        /// 设置实体变量
        /// </summary>
        public static void SetEntityVarInt(int entity, string varName, int value)
        {
            HamSandwichBridge.SetEntityVarInt(entity, varName, value);
        }

        /// <summary>
        /// 获取实体类名
        /// </summary>
        public static string GetEntityClassname(int entity)
        {
            var buffer = new StringBuilder(256);
            HamSandwichBridge.GetEntityClassname(entity, buffer, buffer.Capacity);
            return buffer.ToString();
        }

        /// <summary>
        /// 设置实体类名
        /// </summary>
        public static void SetEntityClassname(int entity, string classname)
        {
            HamSandwichBridge.SetEntityClassname(entity, classname);
        }

        /// <summary>
        /// 获取当前游戏
        /// </summary>
        public static string GetCurrentGame()
        {
            var ptr = HamSandwichBridge.GetCurrentGame();
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : string.Empty;
        }

        /// <summary>
        /// 检查是否支持指定游戏
        /// </summary>
        public static bool IsGameSupported(string gameName)
        {
            return HamSandwichBridge.IsGameSupported(gameName);
        }

        /// <summary>
        /// 获取实体原点
        /// </summary>
        public static Vector3 GetEntityOrigin(int entity)
        {
            var origin = new float[3];
            HamSandwichBridge.GetEntityOrigin(entity, origin);
            return new Vector3(origin[0], origin[1], origin[2]);
        }

        /// <summary>
        /// 设置实体原点
        /// </summary>
        public static void SetEntityOrigin(int entity, Vector3 origin)
        {
            var vec = new[] { origin.X, origin.Y, origin.Z };
            HamSandwichBridge.SetEntityOrigin(entity, vec);
        }

        /// <summary>
        /// 获取实体角度
        /// </summary>
        public static Vector3 GetEntityAngles(int entity)
        {
            var angles = new float[3];
            HamSandwichBridge.GetEntityAngles(entity, angles);
            return new Vector3(angles[0], angles[1], angles[2]);
        }

        /// <summary>
        /// 设置实体角度
        /// </summary>
        public static void SetEntityAngles(int entity, Vector3 angles)
        {
            var vec = new[] { angles.X, angles.Y, angles.Z };
            HamSandwichBridge.SetEntityAngles(entity, vec);
        }

        /// <summary>
        /// 计算两个实体间的距离
        /// </summary>
        public static float GetDistanceBetweenEntities(int entity1, int entity2)
        {
            return HamSandwichBridge.GetDistanceBetweenEntities(entity1, entity2);
        }

        /// <summary>
        /// 检查实体是否可见
        /// </summary>
        public static bool IsEntityVisible(int entity, int target)
        {
            return HamSandwichBridge.IsEntityVisible(entity, target);
        }
    }
}