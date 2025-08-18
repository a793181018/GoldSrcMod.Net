using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.CVar;

namespace AmxModx.Wrappers.Data
{
    /// <summary>
    /// 控制台变量管理器高级封装
    /// 提供对AMX Mod X控制台变量功能的高级封装
    /// </summary>
    public class CVarManager
    {
        /// <summary>
        /// CVar包装类
        /// </summary>
        public class CVar
        {
            private readonly IntPtr _handle;
            private readonly string _name;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="handle">CVar句柄</param>
            /// <param name="name">CVar名称</param>
            public CVar(IntPtr handle, string name)
            {
                _handle = handle;
                _name = name;
            }

            /// <summary>
            /// CVar名称
            /// </summary>
            public string Name => _name;

            /// <summary>
            /// 字符串值
            /// </summary>
            public string StringValue
            {
                get => CVarBridge.GetCVarStringSafe(_handle);
                set => CVarBridge.AmxModx_Bridge_SetCVarString(_handle, value);
            }

            /// <summary>
            /// 浮点数值
            /// </summary>
            public float FloatValue
            {
                get => CVarBridge.AmxModx_Bridge_GetCVarFloat(_handle);
                set => CVarBridge.AmxModx_Bridge_SetCVarFloat(_handle, value);
            }

            /// <summary>
            /// 整数值
            /// </summary>
            public int IntValue
            {
                get => CVarBridge.AmxModx_Bridge_GetCVarInt(_handle);
                set => CVarBridge.AmxModx_Bridge_SetCVarInt(_handle, value);
            }

            /// <summary>
            /// 是否存在
            /// </summary>
            public bool Exists => _handle != IntPtr.Zero;

            /// <summary>
            /// 转换为字符串
            /// </summary>
            public override string ToString()
            {
                return $"CVar: {Name} = {StringValue}";
            }
        }

        /// <summary>
        /// 创建新的控制台变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="flags">变量标志</param>
        /// <returns>CVar实例</returns>
        public static CVar Create(string name, string defaultValue, int flags = 0)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("CVar name cannot be null or empty", nameof(name));

            IntPtr handle = CVarBridge.AmxModx_Bridge_CreateCVar(name, defaultValue, flags);
            if (handle == IntPtr.Zero)
                return null;

            return new CVar(handle, name);
        }

        /// <summary>
        /// 查找控制台变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>CVar实例</returns>
        public static CVar Find(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            IntPtr handle = CVarBridge.AmxModx_Bridge_FindCVar(name);
            if (handle == IntPtr.Zero)
                return null;

            return new CVar(handle, name);
        }

        /// <summary>
        /// 获取CVar字符串值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>字符串值</returns>
        public static string GetString(string name)
        {
            return CVarBridge.GetCVarStringByName(name);
        }

        /// <summary>
        /// 获取CVar浮点数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>浮点数值</returns>
        public static float GetFloat(string name)
        {
            return CVarBridge.GetCVarFloatByName(name);
        }

        /// <summary>
        /// 获取CVar整数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>整数值</returns>
        public static int GetInt(string name)
        {
            return CVarBridge.GetCVarIntByName(name);
        }

        /// <summary>
        /// 设置CVar字符串值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="value">字符串值</param>
        public static void SetString(string name, string value)
        {
            CVarBridge.SetCVarStringByName(name, value);
        }

        /// <summary>
        /// 设置CVar浮点数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="value">浮点数值</param>
        public static void SetFloat(string name, float value)
        {
            CVarBridge.SetCVarFloatByName(name, value);
        }

        /// <summary>
        /// 设置CVar整数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="value">整数值</param>
        public static void SetInt(string name, int value)
        {
            CVarBridge.SetCVarIntByName(name, value);
        }

        /// <summary>
        /// 检查CVar是否存在
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool Exists(string name)
        {
            return Find(name) != null;
        }

        /// <summary>
        /// 获取所有已注册的CVar列表
        /// </summary>
        /// <returns>CVar名称数组</returns>
        public static string[] GetAllCVars()
        {
            // 这里简化处理，实际应该从AMX Mod X获取完整列表
            return new string[]
            {
                "amx_show_activity",
                "amx_password_field",
                "amx_reserved_slots",
                "amx_reserved_type",
                "amx_time_display",
                "amx_nextmap_display",
                "amx_slap_damage",
                "amx_slap_health",
                "amx_votemap_display",
                "amx_vote_progress"
            };
        }

        /// <summary>
        /// 按前缀过滤CVar
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>匹配的CVar名称数组</returns>
        public static string[] FilterCVarsByPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return GetAllCVars();

            var allCVars = GetAllCVars();
            return Array.FindAll(allCVars, cvar => cvar.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 获取CVar信息
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>CVar信息字符串</returns>
        public static string GetCVarInfo(string name)
        {
            var cvar = Find(name);
            if (cvar == null)
                return $"CVar '{name}' not found";

            return $"Name: {cvar.Name}\n" +
                   $"String: {cvar.StringValue}\n" +
                   $"Float: {cvar.FloatValue}\n" +
                   $"Integer: {cvar.IntValue}";
        }
    }
}