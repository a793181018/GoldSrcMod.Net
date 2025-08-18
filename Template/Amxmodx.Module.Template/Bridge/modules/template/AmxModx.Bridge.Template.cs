using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Template
{
    /// <summary>
    /// 模板模块桥接接口
    /// 提供模板模块功能的C#访问
    /// </summary>
    public static class TemplateBridge
    {
        /// <summary>
        /// 模板模块DLL名称常量
        /// </summary>
        private const string TemplateBridgeDll = "template_amxx";

        /// <summary>
        /// 示例函数 - 获取模板信息
        /// </summary>
        /// <param name="info">模板信息缓冲区</param>
        /// <param name="maxLen">缓冲区最大长度</param>
        /// <returns>实际长度</returns>
        [DllImport(TemplateBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Template_GetInfo([Out] byte[] info, int maxLen);

        /// <summary>
        /// 示例函数 - 设置模板参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(TemplateBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Template_SetParam([MarshalAs(UnmanagedType.LPStr)] string paramName, int value);

        /// <summary>
        /// 示例函数 - 执行模板操作
        /// </summary>
        /// <param name="operation">操作类型</param>
        /// <param name="data">操作数据</param>
        /// <returns>操作结果</returns>
        [DllImport(TemplateBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Template_Execute(int operation, IntPtr data);

        /// <summary>
        /// 安全的获取模板信息方法
        /// </summary>
        /// <returns>模板信息字符串</returns>
        public static string GetTemplateInfoSafe()
        {
            byte[] buffer = new byte[256];
            int length = Template_GetInfo(buffer, buffer.Length);
            if (length <= 0) return string.Empty;
            
            return System.Text.Encoding.UTF8.GetString(buffer, 0, Math.Min(length, buffer.Length));
        }

        /// <summary>
        /// 安全的设置模板参数方法
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>是否设置成功</returns>
        public static bool SetTemplateParamSafe(string paramName, int value)
        {
            if (string.IsNullOrEmpty(paramName)) return false;
            return Template_SetParam(paramName, value);
        }
    }
}