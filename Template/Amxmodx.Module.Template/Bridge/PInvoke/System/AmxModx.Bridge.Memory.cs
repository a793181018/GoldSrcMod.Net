using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.System
{
    /// <summary>
    /// 内存分配类型枚举
    /// </summary>
    public enum MemoryType
    {
        /// <summary>临时内存</summary>
        Temporary = 0,
        /// <summary>持久内存</summary>
        Permanent = 1,
        /// <summary>插件内存</summary>
        Plugin = 2,
        /// <summary>全局内存</summary>
        Global = 3
    }

    /// <summary>
    /// 内存保护类型枚举
    /// </summary>
    public enum MemoryProtection
    {
        /// <summary>无保护</summary>
        None = 0,
        /// <summary>只读</summary>
        ReadOnly = 1,
        /// <summary>读写</summary>
        ReadWrite = 2,
        /// <summary>执行</summary>
        Execute = 4,
        /// <summary>读写执行</summary>
        ReadWriteExecute = 6
    }

    /// <summary>
    /// 内存系统桥接接口
    /// 提供AMX Mod X内存管理的C#访问
    /// </summary>
    public static class MemoryBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 分配内存
        /// </summary>
        /// <param name="size">内存大小（字节）</param>
        /// <param name="type">内存类型</param>
        /// <returns>分配的内存指针</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_AllocateMemory(long size, MemoryType type);

        /// <summary>
        /// 释放内存
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <param name="type">内存类型</param>
        /// <returns>是否成功释放</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_FreeMemory(IntPtr ptr, MemoryType type);

        /// <summary>
        /// 重新分配内存
        /// </summary>
        /// <param name="ptr">原内存指针</param>
        /// <param name="newSize">新内存大小</param>
        /// <param name="type">内存类型</param>
        /// <returns>重新分配的内存指针</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_ReallocateMemory(IntPtr ptr, long newSize, MemoryType type);

        /// <summary>
        /// 获取内存大小
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <returns>内存大小（字节）</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long AmxModx_Bridge_GetMemorySize(IntPtr ptr);

        /// <summary>
        /// 设置内存保护
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <param name="size">内存大小</param>
        /// <param name="protection">保护类型</param>
        /// <returns>是否成功设置</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetMemoryProtection(
            IntPtr ptr,
            long size,
            MemoryProtection protection);

        /// <summary>
        /// 获取内存保护类型
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <returns>保护类型</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern MemoryProtection AmxModx_Bridge_GetMemoryProtection(IntPtr ptr);

        /// <summary>
        /// 复制内存
        /// </summary>
        /// <param name="dest">目标指针</param>
        /// <param name="src">源指针</param>
        /// <param name="size">复制大小</param>
        /// <returns>是否成功复制</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_CopyMemory(IntPtr dest, IntPtr src, long size);

        /// <summary>
        /// 填充内存
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <param name="value">填充值</param>
        /// <param name="size">填充大小</param>
        /// <returns>是否成功填充</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_FillMemory(IntPtr ptr, byte value, long size);

        /// <summary>
        /// 比较内存
        /// </summary>
        /// <param name="ptr1">第一个内存指针</param>
        /// <param name="ptr2">第二个内存指针</param>
        /// <param name="size">比较大小</param>
        /// <returns>比较结果（0表示相等）</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_CompareMemory(IntPtr ptr1, IntPtr ptr2, long size);

        /// <summary>
        /// 获取内存统计信息
        /// </summary>
        /// <param name="totalAllocated">总分配内存</param>
        /// <param name="totalUsed">已使用内存</param>
        /// <param name="totalFree">空闲内存</param>
        /// <returns>是否成功获取</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_GetMemoryStats(
            out long totalAllocated,
            out long totalUsed,
            out long totalFree);

        /// <summary>
        /// 检查内存指针是否有效
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <returns>是否有效</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_IsMemoryValid(IntPtr ptr);

        /// <summary>
        /// 获取内存类型
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <returns>内存类型</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern MemoryType AmxModx_Bridge_GetMemoryType(IntPtr ptr);

        /// <summary>
        /// 安全的分配内存
        /// </summary>
        /// <param name="size">内存大小（字节）</param>
        /// <param name="type">内存类型</param>
        /// <returns>分配的内存指针</returns>
        public static IntPtr AllocateMemorySafe(long size, MemoryType type)
        {
            if (size <= 0)
                return IntPtr.Zero;

            return AmxModx_Bridge_AllocateMemory(size, type);
        }

        /// <summary>
        /// 安全的释放内存
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <param name="type">内存类型</param>
        /// <returns>是否成功释放</returns>
        public static bool FreeMemorySafe(IntPtr ptr, MemoryType type)
        {
            if (ptr == IntPtr.Zero)
                return false;

            return AmxModx_Bridge_FreeMemory(ptr, type);
        }

        /// <summary>
        /// 安全的重新分配内存
        /// </summary>
        /// <param name="ptr">原内存指针</param>
        /// <param name="newSize">新内存大小</param>
        /// <param name="type">内存类型</param>
        /// <returns>重新分配的内存指针</returns>
        public static IntPtr ReallocateMemorySafe(IntPtr ptr, long newSize, MemoryType type)
        {
            if (newSize <= 0)
                return IntPtr.Zero;

            return AmxModx_Bridge_ReallocateMemory(ptr, newSize, type);
        }

        /// <summary>
        /// 获取内存统计信息
        /// </summary>
        /// <returns>内存统计信息元组</returns>
        public static (long totalAllocated, long totalUsed, long totalFree) GetMemoryStatsSafe()
        {
            bool success = AmxModx_Bridge_GetMemoryStats(
                out long totalAllocated,
                out long totalUsed,
                out long totalFree);

            if (success)
            {
                return (totalAllocated, totalUsed, totalFree);
            }

            return (0, 0, 0);
        }

        /// <summary>
        /// 安全的复制内存
        /// </summary>
        /// <param name="dest">目标指针</param>
        /// <param name="src">源指针</param>
        /// <param name="size">复制大小</param>
        /// <returns>是否成功复制</returns>
        public static bool CopyMemorySafe(IntPtr dest, IntPtr src, long size)
        {
            if (dest == IntPtr.Zero || src == IntPtr.Zero || size <= 0)
                return false;

            return AmxModx_Bridge_CopyMemory(dest, src, size);
        }

        /// <summary>
        /// 安全的填充内存
        /// </summary>
        /// <param name="ptr">内存指针</param>
        /// <param name="value">填充值</param>
        /// <param name="size">填充大小</param>
        /// <returns>是否成功填充</returns>
        public static bool FillMemorySafe(IntPtr ptr, byte value, long size)
        {
            if (ptr == IntPtr.Zero || size <= 0)
                return false;

            return AmxModx_Bridge_FillMemory(ptr, value, size);
        }
    }
}