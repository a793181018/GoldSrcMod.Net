using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Engine
{
    /// <summary>
    /// 引擎高级特效桥接接口
    /// 提供音效、光照、粒子系统的高级控制功能
    /// </summary>
    public static class EngineEffectsBridge
    {
        private const string EngineEffectsDll = "engine_effects_bridge";

        #region 音效系统

        /// <summary>
        /// 将音效附加到实体播放
        /// </summary>
        /// <param name="entity">目标实体ID</param>
        /// <param name="sample">音效文件路径</param>
        /// <param name="channel">音效通道</param>
        /// <param name="volume">音量 (0.0-1.0)</param>
        /// <param name="attenuation">衰减系数</param>
        /// <param name="pitch">音调</param>
        /// <param name="flags">音效标志</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_SoundPlayAttached(
            int entity,
            [MarshalAs(UnmanagedType.LPStr)] string sample,
            int channel,
            float volume,
            float attenuation,
            int pitch,
            int flags);

        /// <summary>
        /// 设置实体音效音调
        /// </summary>
        /// <param name="entity">目标实体ID</param>
        /// <param name="channel">音效通道</param>
        /// <param name="pitch">新的音调值</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_SoundSetPitch(
            int entity,
            int channel,
            int pitch);

        #endregion

        #region 光照系统

        /// <summary>
        /// 创建动态光源
        /// </summary>
        /// <param name="origin">光源位置数组 [x, y, z]</param>
        /// <param name="radius">光源半径</param>
        /// <param name="red">红色分量 (0-255)</param>
        /// <param name="green">绿色分量 (0-255)</param>
        /// <param name="blue">蓝色分量 (0-255)</param>
        /// <param name="life">光源持续时间 (秒)</param>
        /// <param name="style">光源样式 (0=常亮, 1=闪烁等)</param>
        /// <returns>光源效果ID，失败返回0</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_LightCreate(
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] float[] origin,
            float radius,
            int red,
            int green,
            int blue,
            float life,
            int style);

        /// <summary>
        /// 设置光源颜色
        /// </summary>
        /// <param name="lightId">光源效果ID</param>
        /// <param name="red">红色分量</param>
        /// <param name="green">绿色分量</param>
        /// <param name="blue">蓝色分量</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_LightSetColor(
            int lightId,
            int red,
            int green,
            int blue);

        /// <summary>
        /// 创建闪烁光源效果
        /// </summary>
        /// <param name="lightId">光源效果ID</param>
        /// <param name="flickerSpeed">闪烁速度</param>
        /// <param name="minIntensity">最小亮度</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_LightFlicker(
            int lightId,
            float flickerSpeed,
            float minIntensity);

        /// <summary>
        /// 创建脉冲光源效果
        /// </summary>
        /// <param name="lightId">光源效果ID</param>
        /// <param name="pulseSpeed">脉冲速度</param>
        /// <param name="minIntensity">最小亮度</param>
        /// <param name="maxIntensity">最大亮度</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_LightPulse(
            int lightId,
            float pulseSpeed,
            float minIntensity,
            float maxIntensity);

        /// <summary>
        /// 停止光源效果
        /// </summary>
        /// <param name="lightId">光源效果ID</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_LightStop(int lightId);

        #endregion

        #region 粒子系统

        /// <summary>
        /// 创建粒子系统
        /// </summary>
        /// <param name="origin">粒子起始位置数组 [x, y, z]</param>
        /// <param name="sprite">精灵文件路径</param>
        /// <param name="maxParticles">最大粒子数量</param>
        /// <returns>粒子效果ID，失败返回0</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_ParticleCreate(
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] float[] origin,
            [MarshalAs(UnmanagedType.LPStr)] string sprite,
            int maxParticles);

        /// <summary>
        /// 设置粒子颜色
        /// </summary>
        /// <param name="particleId">粒子效果ID</param>
        /// <param name="red">红色分量</param>
        /// <param name="green">绿色分量</param>
        /// <param name="blue">蓝色分量</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_ParticleSetColor(
            int particleId,
            int red,
            int green,
            int blue);

        /// <summary>
        /// 设置粒子规模
        /// </summary>
        /// <param name="particleId">粒子效果ID</param>
        /// <param name="scale">粒子规模</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_ParticleSetScale(
            int particleId,
            float scale);

        /// <summary>
        /// 停止粒子效果
        /// </summary>
        /// <param name="particleId">粒子效果ID</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int Engine_ParticleStop(int particleId);

        #endregion

        #region 辅助函数

        /// <summary>
        /// 获取精灵索引
        /// </summary>
        /// <param name="spriteName">精灵文件路径</param>
        /// <returns>精灵索引，失败返回-1</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetSpriteIndex(
            [MarshalAs(UnmanagedType.LPStr)] string spriteName);

        /// <summary>
        /// 预缓存模型
        /// </summary>
        /// <param name="modelName">模型文件路径</param>
        /// <returns>模型索引，失败返回-1</returns>
        [DllImport(EngineEffectsDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_PrecacheModel(
            [MarshalAs(UnmanagedType.LPStr)] string modelName);

        #endregion
    }

    /// <summary>
    /// 引擎特效桥接扩展类
    /// 提供C#友好的封装接口
    /// </summary>
    public static class EngineEffectsExtensions
    {
        /// <summary>
        /// 将音效附加到实体播放（简化版）
        /// </summary>
        public static bool PlaySoundAttached(int entity, string sample, float volume = 1.0f, int pitch = 100)
        {
            return EngineEffectsBridge.Engine_SoundPlayAttached(entity, sample, 0, volume, 1.0f, pitch, 0) != 0;
        }

        /// <summary>
        /// 创建动态光源（简化版）
        /// </summary>
        public static int CreateLight(float[] origin, float radius, int red, int green, int blue, float life = 5.0f)
        {
            return EngineEffectsBridge.Engine_LightCreate(origin, radius, red, green, blue, life, 0);
        }

        /// <summary>
        /// 创建爆炸效果
        /// </summary>
        public static int CreateExplosion(float[] origin, string sprite, int particleCount = 50)
        {
            return EngineEffectsBridge.Engine_ParticleCreate(origin, sprite, particleCount);
        }
    }
}