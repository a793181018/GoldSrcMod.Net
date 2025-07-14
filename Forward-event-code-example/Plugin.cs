using GoldSrc.Amxmodx.Native;
using GoldSrc.HLSDK;
using GoldSrc.HLSDK.Native;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Module.Global;

#pragma warning disable CS8981
using cell = int;

#pragma warning restore CS8981


using GoldSrc.Amxmodx.Framework.Attributes;

namespace Module
{
    public unsafe class Plugin
    {
        public static AMX_NATIVE_INFO* nativeInfo = null;
        static Plugin()
        {
            nativeInfo = (AMX_NATIVE_INFO*)Marshal.AllocHGlobal(sizeof(AMX_NATIVE_INFO) * 2);

   


            nativeInfo[1].name = null;
            nativeInfo[1].func = nint.Zero;
        }

        public static void FN_META_QUERY()
        {

        }

        public static void FN_META_ATTACH()
        {

        }

        public static void FN_META_DETACH()
        {

        }

        public static void FN_AMXX_QUERY()
        {

        }

        public static int FN_AMXX_CHECKGAME(sbyte* game)
        {
            return AMXX_GAME_OK;
        }

        public static void FN_AMXX_ATTACH()
        {
            // 注册forward监听
            using var forwardName = "skill_respawn_selected".GetNativeString();
            g_fwd_skill_respawn_selected = g_fn_RegisterForward(forwardName, ForwardExecType.ET_IGNORE);
        }

        public static void FN_AMXX_DETACH()
        {
        }

        public static void FN_AMXX_PLUGINSLOADED()
        {
        }

        public static void FN_AMXX_PLUGINSUNLOADING()
        {

        }
        public static void FN_AMXX_PLUGINSUNLOADED()
        {

        }



            // Forward句柄
            public static int g_fwd_skill_respawn_selected = -1;

    

            // 调用native函数
            public static int GetSkillLevel(int index, int itemid)
            {
                return g_fn_skill_get_skill_level(index, itemid);
            }

            // 触发forward事件
            public static void TriggerSkillRespawnSelected(int id, int itemid)
            {
                if (g_fwd_skill_respawn_selected == -1)
                    return;

                g_fn_AmxPush(null, itemid);
                g_fn_AmxPush(null, id);
                g_fn_ExecuteForward(g_fwd_skill_respawn_selected);
            }

            // Forward事件处理器
            [AmxxForward("skill_respawn_selected")]
            public static int OnSkillRespawnSelected(int id, int itemid)
            {
                using var msg = $"Player {id} skill respawn: {itemid}\n".GetNativeString();
                g_engfuncs.pfnServerPrint(msg);
                return 0;
            }
        }
    
}
