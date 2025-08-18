using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// 复杂钩子参数处理器，支持TraceResult、ItemInfo等特殊类型
    /// </summary>
    public class HamComplexParameters : HamHookParameters
    {
        public HamComplexParameters(int paramCount, IntPtr parameters) 
            : base(paramCount, parameters)
        {
        }

        /// <summary>
        /// 获取TraceResult参数
        /// </summary>
        public unsafe TraceResult GetTraceResult(int paramIndex)
        {
            if (paramIndex < 0 || paramIndex >= Count)
                return default;

            // 从指针获取TraceResult结构体
            IntPtr ptr = Marshal.ReadIntPtr(Parameters, paramIndex * IntPtr.Size);
            if (ptr == IntPtr.Zero)
                return default;

            return Marshal.PtrToStructure<TraceResult>(ptr);
        }

        /// <summary>
        /// 设置TraceResult参数
        /// </summary>
        public unsafe void SetTraceResult(int paramIndex, TraceResult value)
        {
            if (paramIndex < 0 || paramIndex >= Count)
                return;

            IntPtr ptr = Marshal.ReadIntPtr(Parameters, paramIndex * IntPtr.Size);
            if (ptr != IntPtr.Zero)
            {
                Marshal.StructureToPtr(value, ptr, false);
            }
        }

        /// <summary>
        /// 获取ItemInfo参数
        /// </summary>
        public unsafe ItemInfo GetItemInfo(int paramIndex)
        {
            if (paramIndex < 0 || paramIndex >= Count)
                return default;

            IntPtr ptr = Marshal.ReadIntPtr(Parameters, paramIndex * IntPtr.Size);
            if (ptr == IntPtr.Zero)
                return default;

            return Marshal.PtrToStructure<ItemInfo>(ptr);
        }

        /// <summary>
        /// 设置ItemInfo参数
        /// </summary>
        public unsafe void SetItemInfo(int paramIndex, ItemInfo value)
        {
            if (paramIndex < 0 || paramIndex >= Count)
                return;

            IntPtr ptr = Marshal.ReadIntPtr(Parameters, paramIndex * IntPtr.Size);
            if (ptr != IntPtr.Zero)
            {
                Marshal.StructureToPtr(value, ptr, false);
            }
        }

        /// <summary>
        /// 获取Vector3参数（支持指针和值类型）
        /// </summary>
        public unsafe Vector3 GetVector3(int paramIndex)
        {
            if (paramIndex < 0 || paramIndex >= Count)
                return Vector3.Zero;

            IntPtr ptr = Marshal.ReadIntPtr(Parameters, paramIndex * IntPtr.Size);
            if (ptr == IntPtr.Zero)
                return Vector3.Zero;

            float[] vec = new float[3];
            Marshal.Copy(ptr, vec, 0, 3);
            return new Vector3(vec[0], vec[1], vec[2]);
        }

        /// <summary>
        /// 设置Vector3参数
        /// </summary>
        public unsafe void SetVector3(int paramIndex, Vector3 value)
        {
            if (paramIndex < 0 || paramIndex >= Count)
                return;

            IntPtr ptr = Marshal.ReadIntPtr(Parameters, paramIndex * IntPtr.Size);
            if (ptr != IntPtr.Zero)
            {
                float[] vec = { value.X, value.Y, value.Z };
                Marshal.Copy(vec, 0, ptr, 3);
            }
        }
    }

    /// <summary>
    /// 复杂钩子管理器，支持特殊类型参数
    /// </summary>
    public static class HamComplexHookManager
    {
        /// <summary>
        /// 注册带有TraceResult参数的钩子
        /// </summary>
        public static int RegisterTraceHook(HamHookType type, string entityClass, 
            Action<int, HamComplexParameters, TraceResult> preCallback,
            Action<int, HamComplexParameters, TraceResult> postCallback)
        {
            return HamHookManager.RegisterHook(type, entityClass, 
                (entity, parameters) => {
                    var complexParams = new HamComplexParameters(parameters.Count, parameters.Parameters);
                    var traceResult = complexParams.GetTraceResult(0);
                    preCallback?.Invoke(entity, complexParams, traceResult);
                },
                (entity, parameters) => {
                    var complexParams = new HamComplexParameters(parameters.Count, parameters.Parameters);
                    var traceResult = complexParams.GetTraceResult(0);
                    postCallback?.Invoke(entity, complexParams, traceResult);
                });
        }

        /// <summary>
        /// 注册带有ItemInfo参数的钩子
        /// </summary>
        public static int RegisterItemInfoHook(HamHookType type, string entityClass,
            Action<int, HamComplexParameters, ItemInfo> preCallback,
            Action<int, HamComplexParameters, ItemInfo> postCallback)
        {
            return HamHookManager.RegisterHook(type, entityClass,
                (entity, parameters) => {
                    var complexParams = new HamComplexParameters(parameters.Count, parameters.Parameters);
                    var itemInfo = complexParams.GetItemInfo(0);
                    preCallback?.Invoke(entity, complexParams, itemInfo);
                },
                (entity, parameters) => {
                    var complexParams = new HamComplexParameters(parameters.Count, parameters.Parameters);
                    var itemInfo = complexParams.GetItemInfo(0);
                    postCallback?.Invoke(entity, complexParams, itemInfo);
                });
        }

        /// <summary>
        /// 注册带有Vector3参数的钩子
        /// </summary>
        public static int RegisterVectorHook(HamHookType type, string entityClass,
            Action<int, HamComplexParameters, Vector3> preCallback,
            Action<int, HamComplexParameters, Vector3> postCallback)
        {
            return HamHookManager.RegisterHook(type, entityClass,
                (entity, parameters) => {
                    var complexParams = new HamComplexParameters(parameters.Count, parameters.Parameters);
                    var vector = complexParams.GetVector3(0);
                    preCallback?.Invoke(entity, complexParams, vector);
                },
                (entity, parameters) => {
                    var complexParams = new HamComplexParameters(parameters.Count, parameters.Parameters);
                    var vector = complexParams.GetVector3(0);
                    postCallback?.Invoke(entity, complexParams, vector);
                });
        }
    }
}