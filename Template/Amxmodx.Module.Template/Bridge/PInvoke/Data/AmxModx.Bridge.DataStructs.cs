using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.Data
{
    /// <summary>
    /// 数据结构桥接接口
    /// 提供动态数组和栈结构的C#访问
    /// </summary>
    public static class DataStructsBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 创建动态数组
        /// </summary>
        /// <param name="cellsize">每个元素的大小（以cell为单位）</param>
        /// <param name="reserved">初始预留容量</param>
        /// <returns>数组句柄，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArrayCreate(int cellsize, int reserved = 32);

        /// <summary>
        /// 销毁数组并释放内存
        /// </summary>
        /// <param name="handle">数组句柄（传引用，成功后会置0）</param>
        /// <returns>是否成功销毁</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ArrayDestroy(ref int handle);

        /// <summary>
        /// 获取数组当前元素数量
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <returns>元素数量，失败返回-1</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArraySize(int handle);

        /// <summary>
        /// 调整数组大小
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="count">新的元素数量</param>
        /// <returns>是否成功调整</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ArrayResize(int handle, int count);

        /// <summary>
        /// 获取数组元素（单元格值）
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index">元素索引</param>
        /// <param name="block">数据块索引（默认为0）</param>
        /// <returns>单元格值，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArrayGetCell(int handle, int index, int block = 0);

        /// <summary>
        /// 设置数组元素（单元格值）
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index">元素索引</param>
        /// <param name="value">要设置的值</param>
        /// <param name="block">数据块索引（默认为0）</param>
        /// <returns>是否成功设置</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ArraySetCell(int handle, int index, int value, int block = 0);

        /// <summary>
        /// 向数组末尾添加单元格值
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="value">要添加的值</param>
        /// <returns>新元素的索引，失败返回-1</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArrayPushCell(int handle, int value);

        /// <summary>
        /// 从数组获取字符串
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index">元素索引</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="size">缓冲区大小</param>
        /// <returns>实际复制的字符数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArrayGetString(int handle, int index, [Out] StringBuilder buffer, int size);

        /// <summary>
        /// 向数组设置字符串
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index">元素索引</param>
        /// <param name="str">要设置的字符串</param>
        /// <returns>实际复制的字符数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArraySetString(int handle, int index, [MarshalAs(UnmanagedType.LPStr)] string str);

        /// <summary>
        /// 克隆数组
        /// </summary>
        /// <param name="handle">原数组句柄</param>
        /// <returns>新数组句柄，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArrayClone(int handle);

        /// <summary>
        /// 清空数组内容
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <returns>是否成功清空</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ArrayClear(int handle);

        /// <summary>
        /// 删除指定索引的元素
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index">要删除的索引</param>
        /// <returns>是否成功删除</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ArrayDeleteItem(int handle, int index);

        /// <summary>
        /// 交换两个元素的位置
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index1">第一个索引</param>
        /// <param name="index2">第二个索引</param>
        /// <returns>是否成功交换</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ArraySwap(int handle, int index1, int index2);

        /// <summary>
        /// 在数组中查找字符串
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="str">要查找的字符串</param>
        /// <returns>找到返回索引，未找到返回-1</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArrayFindString(int handle, [MarshalAs(UnmanagedType.LPStr)] string str);

        /// <summary>
        /// 在数组中查找数值
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="value">要查找的值</param>
        /// <returns>找到返回索引，未找到返回-1</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ArrayFindValue(int handle, int value);

        /// <summary>
        /// 安全的获取数组字符串
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index">元素索引</param>
        /// <returns>字符串值</returns>
        public static string GetArrayStringSafe(int handle, int index)
        {
            StringBuilder buffer = new StringBuilder(256);
            int length = AmxModx_Bridge_ArrayGetString(handle, index, buffer, buffer.Capacity);
            return length > 0 ? buffer.ToString() : string.Empty;
        }

        /// <summary>
        /// 安全的设置数组字符串
        /// </summary>
        /// <param name="handle">数组句柄</param>
        /// <param name="index">元素索引</param>
        /// <param name="value">字符串值</param>
        /// <returns>是否成功设置</returns>
        public static bool SetArrayStringSafe(int handle, int index, string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            int result = AmxModx_Bridge_ArraySetString(handle, index, value);
            return result > 0;
        }
    }
}