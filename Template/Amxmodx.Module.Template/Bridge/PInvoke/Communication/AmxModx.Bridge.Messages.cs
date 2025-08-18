using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Messages
{
    /// <summary>
    /// 消息系统桥接接口
    /// </summary>
    public static class MessageBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 消息目标类型
        /// </summary>
        public enum MessageDestination
        {
            BROADCAST = 0,
            ONE = 1,
            ALL = 2,
            INIT = 3,
            PVS = 4,
            PAS = 5,
            PVS_R = 6,
            PAS_R = 7,
            ONE_UNRELIABLE = 8,
            SPEC = 32
        }

        /// <summary>
        /// 消息参数类型
        /// </summary>
        public enum MessageArgType
        {
            BYTE = 1,
            CHAR = 2,
            SHORT = 3,
            LONG = 4,
            ANGLE = 5,
            COORD = 6,
            STRING = 7,
            ENTITY = 8
        }

        /// <summary>
        /// 开始发送消息
        /// </summary>
        /// <param name="msgDest">消息目标</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="origin">消息原点坐标</param>
        /// <param name="edict">实体索引</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_MessageBegin(
            int msgDest,
            int msgType,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] float[] origin,
            int edict);

        /// <summary>
        /// 结束消息发送
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_MessageEnd();

        /// <summary>
        /// 写入字节数据到消息
        /// </summary>
        /// <param name="value">字节值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteByte(int value);

        /// <summary>
        /// 写入字符数据到消息
        /// </summary>
        /// <param name="value">字符值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteChar(int value);

        /// <summary>
        /// 写入短整型数据到消息
        /// </summary>
        /// <param name="value">短整型值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteShort(int value);

        /// <summary>
        /// 写入长整型数据到消息
        /// </summary>
        /// <param name="value">长整型值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteLong(int value);

        /// <summary>
        /// 写入实体数据到消息
        /// </summary>
        /// <param name="value">实体索引</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteEntity(int value);

        /// <summary>
        /// 写入角度数据到消息
        /// </summary>
        /// <param name="value">角度值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteAngle(float value);

        /// <summary>
        /// 写入坐标数据到消息
        /// </summary>
        /// <param name="value">坐标值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteCoord(float value);

        /// <summary>
        /// 写入字符串数据到消息
        /// </summary>
        /// <param name="str">字符串值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_WriteString([MarshalAs(UnmanagedType.LPStr)] string str);

        /// <summary>
        /// 注册消息钩子
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="callback">回调函数指针</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>是否注册成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RegisterMessage(int msgId, IntPtr callback, int post);

        /// <summary>
        /// 注销消息钩子
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="callback">回调函数指针</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>是否注销成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_UnregisterMessage(int msgId, IntPtr callback, int post);

        /// <summary>
        /// 设置消息阻塞
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="blocking">是否阻塞</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetMessageBlock(int msgId, int blocking);

        /// <summary>
        /// 获取消息阻塞状态
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <returns>阻塞状态</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMessageBlock(int msgId);

        /// <summary>
        /// 获取消息参数数量
        /// </summary>
        /// <returns>参数数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMessageArgs();

        /// <summary>
        /// 获取消息参数类型
        /// </summary>
        /// <param name="argIndex">参数索引</param>
        /// <returns>参数类型</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMessageArgType(int argIndex);

        /// <summary>
        /// 获取消息参数整数值
        /// </summary>
        /// <param name="argIndex">参数索引</param>
        /// <returns>整数值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMessageArgInt(int argIndex);

        /// <summary>
        /// 获取消息参数浮点数值
        /// </summary>
        /// <param name="argIndex">参数索引</param>
        /// <returns>浮点数值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_GetMessageArgFloat(int argIndex);

        /// <summary>
        /// 获取消息参数字符串值
        /// </summary>
        /// <param name="argIndex">参数索引</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>实际复制的字符数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMessageArgString(int argIndex, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int bufferSize);

        /// <summary>
        /// 设置消息参数整数值
        /// </summary>
        /// <param name="argIndex">参数索引</param>
        /// <param name="value">整数值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetMessageArgInt(int argIndex, int value);

        /// <summary>
        /// 设置消息参数浮点数值
        /// </summary>
        /// <param name="argIndex">参数索引</param>
        /// <param name="value">浮点数值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetMessageArgFloat(int argIndex, float value);

        /// <summary>
        /// 设置消息参数字符串值
        /// </summary>
        /// <param name="argIndex">参数索引</param>
        /// <param name="str">字符串值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetMessageArgString(int argIndex, [MarshalAs(UnmanagedType.LPStr)] string str);

        /// <summary>
        /// 消息管理器包装类
        /// </summary>
        public static class MessageManager
        {
            /// <summary>
            /// 开始发送消息
            /// </summary>
            public static void BeginMessage(MessageDestination dest, int msgType, float[] origin = null, int entityIndex = 0)
            {
                float[] originArray = origin ?? new float[3];
                AmxModx_Bridge_MessageBegin((int)dest, msgType, originArray, entityIndex);
            }

            /// <summary>
            /// 结束消息发送
            /// </summary>
            public static void EndMessage()
            {
                AmxModx_Bridge_MessageEnd();
            }

            /// <summary>
            /// 发送简单消息
            /// </summary>
            public static void SendMessage(MessageDestination dest, int msgType, Action writeAction)
            {
                BeginMessage(dest, msgType);
                writeAction?.Invoke();
                EndMessage();
            }

            /// <summary>
            /// 写入字节数据
            /// </summary>
            public static void WriteByte(byte value)
            {
                AmxModx_Bridge_WriteByte(value);
            }

            /// <summary>
            /// 写入字符数据
            /// </summary>
            public static void WriteChar(sbyte value)
            {
                AmxModx_Bridge_WriteChar(value);
            }

            /// <summary>
            /// 写入短整型数据
            /// </summary>
            public static void WriteShort(short value)
            {
                AmxModx_Bridge_WriteShort(value);
            }

            /// <summary>
            /// 写入长整型数据
            /// </summary>
            public static void WriteLong(int value)
            {
                AmxModx_Bridge_WriteLong(value);
            }

            /// <summary>
            /// 写入实体数据
            /// </summary>
            public static void WriteEntity(int entityIndex)
            {
                AmxModx_Bridge_WriteEntity(entityIndex);
            }

            /// <summary>
            /// 写入角度数据
            /// </summary>
            public static void WriteAngle(float angle)
            {
                AmxModx_Bridge_WriteAngle(angle);
            }

            /// <summary>
            /// 写入坐标数据
            /// </summary>
            public static void WriteCoord(float coord)
            {
                AmxModx_Bridge_WriteCoord(coord);
            }

            /// <summary>
            /// 写入字符串数据
            /// </summary>
            public static void WriteString(string text)
            {
                AmxModx_Bridge_WriteString(text);
            }
        }
    }
}