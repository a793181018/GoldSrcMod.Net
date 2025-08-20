using System;
using System.Text;
using AmxModx.Bridge.Core;

namespace AmxModx.Wrappers.System
{
    /// <summary>
    /// AMX Mod X核心功能高级封装管理器
    /// 提供对AMX Mod X核心功能的面向对象访问
    /// </summary>
    public static class CoreManager
    {
        /// <summary>
        /// 初始化核心管理器
        /// </summary>
        public static void Initialize()
        {
            // 初始化核心系统
        }
        /// <summary>
        /// 获取当前函数的参数数量
        /// </summary>
        public static int ParameterCount => CoreBridge.AmxModx_Bridge_GetNumArgs();

        /// <summary>
        /// 获取指定索引的参数值
        /// </summary>
        /// <param name="index">参数索引（从0开始）</param>
        /// <param name="offset">数组偏移量（对于数组参数）</param>
        /// <returns>参数值</returns>
        public static int GetParameter(int index, int offset = 0)
        {
            return CoreBridge.AmxModx_Bridge_GetArg(index, offset);
        }

        /// <summary>
        /// 设置指定索引的参数值
        /// </summary>
        /// <param name="index">参数索引（从0开始）</param>
        /// <param name="value">要设置的值</param>
        /// <param name="offset">数组偏移量（对于数组参数）</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetParameter(int index, int value, int offset = 0)
        {
            return CoreBridge.AmxModx_Bridge_SetArg(index, offset, value);
        }

        /// <summary>
        /// 获取可用堆空间大小
        /// </summary>
        public static int HeapSpace => CoreBridge.AmxModx_Bridge_GetHeapSpace();

        /// <summary>
        /// 根据函数名获取函数索引
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <returns>函数索引，-1表示未找到</returns>
        public static int GetFunctionIndex(string functionName)
        {
            if (string.IsNullOrEmpty(functionName))
                return -1;

            return CoreBridge.AmxModx_Bridge_GetFunctionIndex(functionName);
        }

        /// <summary>
        /// 交换32位整数的字节顺序
        /// </summary>
        /// <param name="value">要交换的值</param>
        /// <returns>交换后的值</returns>
        public static int SwapBytes(int value)
        {
            return CoreBridge.AmxModx_Bridge_SwapChars(value);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>包含属性值和名称的元组</returns>
        public static (int value, string name) GetProperty(int id, string name, int value)
        {
            return CoreBridge.GetPropertySafe(id, name, value);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>之前的属性值</returns>
        public static int SetProperty(int id, string name, int value)
        {
            return CoreBridge.SetPropertySafe(id, name, value);
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>被删除的属性值</returns>
        public static int DeleteProperty(int id, string name, int value)
        {
            return CoreBridge.DeletePropertySafe(id, name, value);
        }

        /// <summary>
        /// 检查属性是否存在
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool PropertyExists(int id, string name, int value)
        {
            return CoreBridge.AmxModx_Bridge_PropertyExists(id, name, value);
        }

        /// <summary>
        /// 获取字符串参数
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <returns>字符串值</returns>
        public static string GetStringParameter(int index)
        {
            int param = GetParameter(index);
            return NativeManager.PtrToString(param);
        }

        /// <summary>
        /// 获取浮点数参数
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <returns>浮点数值</returns>
        public static float GetFloatParameter(int index)
        {
            int param = GetParameter(index);
            return BitConverter.Int32BitsToSingle(param);
        }

        /// <summary>
        /// 设置浮点数参数
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <param name="value">浮点数值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetFloatParameter(int index, float value)
        {
            int intValue = BitConverter.SingleToInt32Bits(value);
            return SetParameter(index, intValue);
        }
        /// <summary>
        /// 清理核心管理器资源
        /// </summary>
        public static void Cleanup()
        {
            // 核心功能无需要清理的特定资源
        }
    }
}