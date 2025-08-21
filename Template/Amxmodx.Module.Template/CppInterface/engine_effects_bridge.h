// Engine Effects Bridge Header
// AMX Mod X Engine Module Advanced Effects C# Bridge

#ifndef ENGINE_EFFECTS_BRIDGE_H
#define ENGINE_EFFECTS_BRIDGE_H

#pragma once

#ifdef _WIN32
    #ifdef ENGINE_EFFECTS_BRIDGE_EXPORTS
        #define ENGINE_EFFECTS_BRIDGE_API __declspec(dllexport)
    #else
        #define ENGINE_EFFECTS_BRIDGE_API __declspec(dllimport)
    #endif
#else
    #define ENGINE_EFFECTS_BRIDGE_API __attribute__((visibility("default")))
#endif

// 基本类型定义
typedef int EntityId;
typedef float Vector3[3];
typedef unsigned int EffectId;

// 粒子系统参数结构
struct ParticleParams {
    Vector3 origin;
    Vector3 velocity;
    Vector3 angles;
    float scale;
    int color;
    float life;
    float decay;
    int spriteIndex;
    int flags;
};

// 光照参数结构
struct LightParams {
    Vector3 origin;
    float radius;
    int color[3];  // RGB
    float life;
    float decay;
    int style;     // 闪烁模式
    int flags;
};

// 音效参数结构
struct SoundParams {
    Vector3 origin;
    const char* sample;
    float volume;
    float attenuation;
    int channel;
    int pitch;
    int flags;
    EntityId entity;
};

extern "C" {
    // 音效系统接口
    ENGINE_EFFECTS_BRIDGE_API int Engine_SoundPlayAttached(EntityId entity, const char* sample, int channel, float volume, float attenuation, int pitch, int flags);
    ENGINE_EFFECTS_BRIDGE_API int Engine_SoundSetPitch(EntityId entity, int channel, int pitch);
    
    // 光照系统接口
ENGINE_EFFECTS_BRIDGE_API EffectId Engine_LightCreate(const float origin[3], float radius, int red, int green, int blue, float life, int style);
    ENGINE_EFFECTS_BRIDGE_API int Engine_LightSetColor(EffectId lightId, int red, int green, int blue);
    ENGINE_EFFECTS_BRIDGE_API int Engine_LightFlicker(EffectId lightId, float flickerSpeed, float minIntensity);
    ENGINE_EFFECTS_BRIDGE_API int Engine_LightPulse(EffectId lightId, float pulseSpeed, float minIntensity, float maxIntensity);
    ENGINE_EFFECTS_BRIDGE_API int Engine_LightStop(EffectId lightId);
    
    // 粒子系统接口
ENGINE_EFFECTS_BRIDGE_API EffectId Engine_ParticleCreate(const float origin[3], const char* sprite, int maxParticles);
    ENGINE_EFFECTS_BRIDGE_API int Engine_ParticleSetColor(EffectId particleId, int red, int green, int blue);
    ENGINE_EFFECTS_BRIDGE_API int Engine_ParticleSetScale(EffectId particleId, float scale);
    ENGINE_EFFECTS_BRIDGE_API int Engine_ParticleStop(EffectId particleId);
    
    // 辅助函数
    ENGINE_EFFECTS_BRIDGE_API int Engine_GetSpriteIndex(const char* spriteName);
    ENGINE_EFFECTS_BRIDGE_API int Engine_PrecacheModel(const char* modelName);
}

#endif // ENGINE_EFFECTS_BRIDGE_H