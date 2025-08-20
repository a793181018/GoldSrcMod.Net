using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// Ham钩子参数封装类
    /// </summary>
    public class HamHookParameters
    {
        private readonly int _paramCount;
        private readonly IntPtr _parameters;

        public HamHookParameters(int paramCount, IntPtr parameters)
        {
            _paramCount = paramCount;
            _parameters = parameters;
        }

        /// <summary>
        /// 获取参数数量
        /// </summary>
        public int Count => _paramCount;

        /// <summary>
        /// 获取参数指针
        /// </summary>
        public IntPtr Parameters => _parameters;

        /// <summary>
        /// 获取整数参数
        /// </summary>
        public int GetInt(int index)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            IntPtr paramPtr = IntPtr.Add(_parameters, index * IntPtr.Size);
            return Marshal.ReadInt32(Marshal.ReadIntPtr(paramPtr));
        }

        /// <summary>
        /// 获取浮点数参数
        /// </summary>
        public float GetFloat(int index)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            IntPtr paramPtr = IntPtr.Add(_parameters, index * IntPtr.Size);
            float[] value = new float[1];
            Marshal.Copy(Marshal.ReadIntPtr(paramPtr), value, 0, 1);
            return value[0];
        }

        /// <summary>
        /// 获取向量参数
        /// </summary>
        public Vector3 GetVector(int index)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            IntPtr paramPtr = IntPtr.Add(_parameters, index * IntPtr.Size);
            float[] values = new float[3];
            Marshal.Copy(Marshal.ReadIntPtr(paramPtr), values, 0, 3);
            return new Vector3(values[0], values[1], values[2]);
        }

        /// <summary>
        /// 获取实体参数
        /// </summary>
        public int GetEntity(int index)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            IntPtr paramPtr = IntPtr.Add(_parameters, index * IntPtr.Size);
            return Marshal.ReadInt32(Marshal.ReadIntPtr(paramPtr));
        }

        /// <summary>
        /// 获取字符串参数
        /// </summary>
        public string GetString(int index)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            IntPtr paramPtr = IntPtr.Add(_parameters, index * IntPtr.Size);
            return Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(paramPtr));
        }

        /// <summary>
        /// 设置整数参数（当前不支持，仅作占位）
        /// </summary>
        public void SetInt(int index, int value)
        {
            // 当前实现不支持参数修改
            Console.WriteLine($"[HamHookParameters] 设置整数参数 {index} = {value} - 当前不支持");
        }

        /// <summary>
        /// 设置浮点数参数（当前不支持，仅作占位）
        /// </summary>
        public void SetFloat(int index, float value)
        {
            // 当前实现不支持参数修改
            Console.WriteLine($"[HamHookParameters] 设置浮点参数 {index} = {value} - 当前不支持");
        }

        /// <summary>
        /// 设置向量参数（当前不支持，仅作占位）
        /// </summary>
        public void SetVector(int index, Vector3 value)
        {
            // 当前实现不支持参数修改
            Console.WriteLine($"[HamHookParameters] 设置向量参数 {index} = ({value.X}, {value.Y}, {value.Z}) - 当前不支持");
        }

        /// <summary>
        /// 设置实体参数（当前不支持，仅作占位）
        /// </summary>
        public void SetEntity(int index, int entity)
        {
            // 当前实现不支持参数修改
            Console.WriteLine($"[HamHookParameters] 设置实体参数 {index} = {entity} - 当前不支持");
        }
    }


}