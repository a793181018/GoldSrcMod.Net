using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.NewMenus
{
    /// <summary>
    /// 新菜单系统桥接接口
    /// </summary>
    public static class NewMenuBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 菜单属性类型
        /// </summary>
        public enum MenuProperty
        {
            PER_PAGE = 0,
            EXIT = 1,
            BACK = 2,
            NEXT = 3,
            TITLE = 4,
            EXITBUTTON = 5,
            NOVOTE = 6,
            NOMORE = 7,
            TIME = 8,
            KEYS = 9
        }

        /// <summary>
        /// 菜单选项类型
        /// </summary>
        public enum MenuItemType
        {
            NORMAL = 0,
            BLANK = 1,
            TEXT = 2,
            SPACER = 3
        }

        /// <summary>
        /// 创建新菜单
        /// </summary>
        /// <param name="title">菜单标题</param>
        /// <param name="pageCallback">页面回调</param>
        /// <param name="exitCallback">退出回调</param>
        /// <param name="backCallback">返回回调</param>
        /// <param name="nextCallback">下一页回调</param>
        /// <returns>菜单句柄</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuCreate(
            [MarshalAs(UnmanagedType.LPStr)] string title,
            IntPtr pageCallback,
            IntPtr exitCallback,
            IntPtr backCallback,
            IntPtr nextCallback);

        /// <summary>
        /// 销毁菜单
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuDestroy(int menu);

        /// <summary>
        /// 添加菜单项
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="text">菜单文本</param>
        /// <param name="callback">回调函数</param>
        /// <param name="info">附加信息</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuAddItem(
            int menu,
            [MarshalAs(UnmanagedType.LPStr)] string text,
            IntPtr callback,
            [MarshalAs(UnmanagedType.LPStr)] string info);

        /// <summary>
        /// 添加空白菜单项
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="callback">回调函数</param>
        /// <param name="info">附加信息</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuAddBlank(
            int menu,
            IntPtr callback,
            [MarshalAs(UnmanagedType.LPStr)] string info);

        /// <summary>
        /// 添加文本菜单项
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="text">菜单文本</param>
        /// <param name="callback">回调函数</param>
        /// <param name="info">附加信息</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuAddText(
            int menu,
            [MarshalAs(UnmanagedType.LPStr)] string text,
            IntPtr callback,
            [MarshalAs(UnmanagedType.LPStr)] string info);

        /// <summary>
        /// 显示菜单给玩家
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="player">玩家索引</param>
        /// <param name="page">页码</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuDisplay(int menu, int player, int page);

        /// <summary>
        /// 设置菜单属性
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="prop">属性类型</param>
        /// <param name="value">属性值</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuSetProperty(
            int menu,
            int prop,
            int value);

        /// <summary>
        /// 获取菜单属性
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="prop">属性类型</param>
        /// <returns>属性值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuGetProperty(int menu, int prop);

        /// <summary>
        /// 获取菜单项信息
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="item">菜单项索引</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="info">附加信息缓冲区</param>
        /// <param name="infoSize">附加信息缓冲区大小</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuGetItemInfo(
            int menu,
            int item,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer,
            int bufferSize,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder info,
            int infoSize);

        /// <summary>
        /// 获取菜单项数量
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <returns>菜单项数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuGetItems(int menu);

        /// <summary>
        /// 获取菜单页数
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <returns>页数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuGetPages(int menu);

        /// <summary>
        /// 获取当前页菜单项数量
        /// </summary>
        /// <param name="menu">菜单句柄</param>
        /// <param name="page">页码</param>
        /// <returns>当前页菜单项数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuGetPageItems(int menu, int page);

        /// <summary>
        /// 关闭玩家菜单
        /// </summary>
        /// <param name="player">玩家索引</param>
        /// <returns>是否成功</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_MenuClose(int player);

        /// <summary>
        /// 获取玩家当前菜单
        /// </summary>
        /// <param name="player">玩家索引</param>
        /// <returns>菜单句柄</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerMenu(int player);

        /// <summary>
        /// 获取玩家菜单页
        /// </summary>
        /// <param name="player">玩家索引</param>
        /// <returns>当前页码</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerMenuPage(int player);

        /// <summary>
        /// 菜单管理器包装类
        /// </summary>
        public static class MenuManager
        {
            /// <summary>
            /// 创建新菜单
            /// </summary>
            public static int CreateMenu(string title)
            {
                return AmxModx_Bridge_MenuCreate(title, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }

            /// <summary>
            /// 创建带回调的菜单
            /// </summary>
            public static int CreateMenu(
                string title,
                IntPtr pageCallback = default,
                IntPtr exitCallback = default,
                IntPtr backCallback = default,
                IntPtr nextCallback = default)
            {
                return AmxModx_Bridge_MenuCreate(title, pageCallback, exitCallback, backCallback, nextCallback);
            }

            /// <summary>
            /// 添加菜单项
            /// </summary>
            public static bool AddMenuItem(int menu, string text, IntPtr callback = default, string info = "")
            {
                return AmxModx_Bridge_MenuAddItem(menu, text, callback, info) != 0;
            }

            /// <summary>
            /// 添加空白菜单项
            /// </summary>
            public static bool AddBlankItem(int menu, IntPtr callback = default, string info = "")
            {
                return AmxModx_Bridge_MenuAddBlank(menu, callback, info) != 0;
            }

            /// <summary>
            /// 添加文本菜单项
            /// </summary>
            public static bool AddTextItem(int menu, string text, IntPtr callback = default, string info = "")
            {
                return AmxModx_Bridge_MenuAddText(menu, text, callback, info) != 0;
            }

            /// <summary>
            /// 显示菜单给玩家
            /// </summary>
            public static bool DisplayMenu(int menu, int player, int page = 0)
            {
                return AmxModx_Bridge_MenuDisplay(menu, player, page) != 0;
            }

            /// <summary>
            /// 设置菜单每页显示数量
            /// </summary>
            public static bool SetItemsPerPage(int menu, int count)
            {
                return AmxModx_Bridge_MenuSetProperty(menu, (int)MenuProperty.PER_PAGE, count) != 0;
            }

            /// <summary>
            /// 设置菜单标题
            /// </summary>
            public static bool SetMenuTitle(int menu, string title)
            {
                return AmxModx_Bridge_MenuSetProperty(menu, (int)MenuProperty.TITLE, 0) != 0;
            }

            /// <summary>
            /// 获取菜单信息
            /// </summary>
            public static string GetMenuItemText(int menu, int item)
            {
                var buffer = new StringBuilder(256);
                var info = new StringBuilder(256);
                if (AmxModx_Bridge_MenuGetItemInfo(menu, item, buffer, buffer.Capacity, info, info.Capacity) != 0)
                {
                    return buffer.ToString();
                }
                return string.Empty;
            }

            /// <summary>
            /// 获取菜单项信息
            /// </summary>
            public static (string text, string info) GetMenuItemInfo(int menu, int item)
            {
                var buffer = new StringBuilder(256);
                var info = new StringBuilder(256);
                if (AmxModx_Bridge_MenuGetItemInfo(menu, item, buffer, buffer.Capacity, info, info.Capacity) != 0)
                {
                    return (buffer.ToString(), info.ToString());
                }
                return (string.Empty, string.Empty);
            }

            /// <summary>
            /// 销毁菜单
            /// </summary>
            public static bool DestroyMenu(int menu)
            {
                return AmxModx_Bridge_MenuDestroy(menu) != 0;
            }
        }
    }
}