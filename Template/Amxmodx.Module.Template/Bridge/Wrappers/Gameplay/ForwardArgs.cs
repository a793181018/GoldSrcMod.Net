using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Fakemeta
{
    /// <summary>
    /// 事件参数基类
    /// </summary>
    public abstract class ForwardArgs
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public ForwardType Type { get; protected set; }

        /// <summary>
        /// 执行时机
        /// </summary>
        public ForwardTiming Timing { get; protected set; }

        /// <summary>
        /// 将参数转换为非托管内存
        /// </summary>
        /// <returns>非托管内存指针</returns>
        public abstract IntPtr ToPointer();

        /// <summary>
        /// 从非托管内存释放资源
        /// </summary>
        /// <param name="ptr">非托管内存指针</param>
        public abstract void Free(IntPtr ptr);
    }

    /// <summary>
    /// 实体事件参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EntityForwardArgs
    {
        public int EntityIndex;
    }

    /// <summary>
    /// 字符串事件参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StringForwardArgs
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string StringValue;
    }

    /// <summary>
    /// 实体和字符串事件参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct EntityStringForwardArgs
    {
        public int EntityIndex;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string StringValue;
    }

    /// <summary>
    /// 追踪事件参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TraceForwardArgs
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] StartPos;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] EndPos;
        
        public int NoMonsters;
        public int SkipEntity;
        public IntPtr TraceResult;
    }

    /// <summary>
    /// 声音事件参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SoundForwardArgs
    {
        public int EntityIndex;
        public int Channel;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Sample;
        public float Volume;
        public float Attenuation;
        public int Flags;
        public int Pitch;
    }

    /// <summary>
    /// 通用事件参数实现
    /// </summary>
    public class GenericForwardArgs : ForwardArgs
    {
        private readonly IntPtr _dataPtr;
        private readonly int _dataSize;

        public GenericForwardArgs(ForwardType type, ForwardTiming timing, IntPtr dataPtr, int dataSize)
        {
            Type = type;
            Timing = timing;
            _dataPtr = dataPtr;
            _dataSize = dataSize;
        }

        public override IntPtr ToPointer()
        {
            return _dataPtr;
        }

        public override void Free(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// 创建实体事件参数
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="entityIndex">实体索引</param>
        /// <returns>事件参数</returns>
        public static GenericForwardArgs CreateEntityArgs(ForwardType type, ForwardTiming timing, int entityIndex)
        {
            var args = new EntityForwardArgs { EntityIndex = entityIndex };
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(args));
            Marshal.StructureToPtr(args, ptr, false);
            return new GenericForwardArgs(type, timing, ptr, Marshal.SizeOf(args));
        }

        /// <summary>
        /// 创建字符串事件参数
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="value">字符串值</param>
        /// <returns>事件参数</returns>
        public static GenericForwardArgs CreateStringArgs(ForwardType type, ForwardTiming timing, string value)
        {
            var args = new StringForwardArgs { StringValue = value ?? string.Empty };
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(args));
            Marshal.StructureToPtr(args, ptr, false);
            return new GenericForwardArgs(type, timing, ptr, Marshal.SizeOf(args));
        }

        /// <summary>
        /// 创建实体和字符串事件参数
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="value">字符串值</param>
        /// <returns>事件参数</returns>
        public static GenericForwardArgs CreateEntityStringArgs(ForwardType type, ForwardTiming timing, int entityIndex, string value)
        {
            var args = new EntityStringForwardArgs 
            { 
                EntityIndex = entityIndex, 
                StringValue = value ?? string.Empty 
            };
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(args));
            Marshal.StructureToPtr(args, ptr, false);
            return new GenericForwardArgs(type, timing, ptr, Marshal.SizeOf(args));
        }
    }
}