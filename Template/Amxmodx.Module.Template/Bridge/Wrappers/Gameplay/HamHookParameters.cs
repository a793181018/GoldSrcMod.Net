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
        /// 设置整数参数
        /// </summary>
        public void SetInt(int index, int value)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            NativeMethods.SetParameterInt(index, value);
        }

        /// <summary>
        /// 设置浮点数参数
        /// </summary>
        public void SetFloat(int index, float value)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            NativeMethods.SetParameterFloat(index, value);
        }

        /// <summary>
        /// 设置向量参数
        /// </summary>
        public void SetVector(int index, Vector3 value)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            float[] vec = { value.X, value.Y, value.Z };
            NativeMethods.SetParameterVector(index, vec);
        }

        /// <summary>
        /// 设置实体参数
        /// </summary>
        public void SetEntity(int index, int entity)
        {
            if (index < 0 || index >= _paramCount)
                throw new IndexOutOfRangeException("Parameter index out of range");

            NativeMethods.SetParameterEntity(index, entity);
        }
    }

    /// <summary>
    /// 三维向量结构
    /// </summary>
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}