// Engine Bridge Header
// AMX Mod X Engine Module C# Bridge


#define ENGINE_BRIDGE_H

#define WINDOWS_IGNORE_PACKING_MISMATCH
#include <windows.h>

#pragma once

#ifdef ENGINE_BRIDGE_EXPORTS
#define ENGINE_BRIDGE_API __declspec(dllexport)
#else
#define ENGINE_BRIDGE_API __declspec(dllimport)
#endif

// 基本类型定义
typedef int EntityId;
typedef float Vector3[3];
typedef unsigned int TraceResultType;

// 追踪结果结构
struct TraceResultInfo
{
    int allSolid;
    int startSolid;
    int inOpen;
    int inWater;
    float fraction;
    Vector3 endPos;
    float planeDist;
    Vector3 planeNormal;
    int hitEntity;
    int hitGroup;
};

// 用户命令结构
struct UserCmdInfo
{
    float forwardmove;     // 前后移动
    float sidemove;        // 左右移动
    float upmove;          // 上下移动
    float lerp_msec;       // 插值毫秒
    float msec;            // 毫秒
    float lightlevel;      // 光照等级
    int buttons;           // 按键
    int weaponselect;      // 武器选择
    int impact_index;      // 碰撞索引
    float viewangles[3];   // 视角角度
    float impact_position[3]; // 碰撞位置
};

// 引擎桥接接口声明
extern "C" {
    // 时间相关
    ENGINE_BRIDGE_API float Engine_GetGameTime();
    
    // 实体操作
    ENGINE_BRIDGE_API int Engine_CreateEntity(const char* className);
    ENGINE_BRIDGE_API int Engine_RemoveEntity(int entityId);
    ENGINE_BRIDGE_API int Engine_IsValidEntity(int entityId);
    ENGINE_BRIDGE_API int Engine_GetEntityCount();
    ENGINE_BRIDGE_API float Engine_GetEntityDistance(int entityA, int entityB);
    
    // 追踪系统
    ENGINE_BRIDGE_API int Engine_TraceLine(const Vector3 start, const Vector3 end, int ignoreEntity, TraceResultInfo* result);
    ENGINE_BRIDGE_API int Engine_TraceHull(const Vector3 start, const Vector3 end, int hullType, int ignoreEntity, TraceResultInfo* result);
    ENGINE_BRIDGE_API int Engine_TraceNormal(int entity, const Vector3 start, const Vector3 end, Vector3 normal);
    ENGINE_BRIDGE_API int Engine_TraceForward(const Vector3 start, const Vector3 angles, float give, int ignoreEntity, TraceResultInfo* result);
    
    // 游戏事件
    ENGINE_BRIDGE_API int Engine_PlaybackEvent(int flags, int invoker, unsigned short eventIndex, float delay, 
                                               const Vector3 origin, const Vector3 angles, float fparam1, float fparam2,
                                               int iparam1, int iparam2, int bparam1, int bparam2);
    
    // 范围伤害
    ENGINE_BRIDGE_API int Engine_RadiusDamage(const Vector3 origin, int damageMultiplier, int radiusMultiplier);
    
    // 点内容检查
    ENGINE_BRIDGE_API int Engine_PointContents(const Vector3 point);
    
    // 字符串和索引
    ENGINE_BRIDGE_API int Engine_GetDecalIndex(const char* decalName);
    ENGINE_BRIDGE_API int Engine_GetInfoKeyBuffer(int entity, char* buffer, int bufferSize);
    ENGINE_BRIDGE_API int Engine_GetEngineString(int stringId, char* buffer, int bufferSize);
    
    // 用户命令
    ENGINE_BRIDGE_API int Engine_GetUserCmd(int client, int type, UserCmdInfo* cmd);
    ENGINE_BRIDGE_API int Engine_SetUserCmd(int client, int type, const UserCmdInfo* cmd);
    
    // 说话权限
    ENGINE_BRIDGE_API int Engine_SetSpeak(int client, int speakFlags);
    ENGINE_BRIDGE_API int Engine_GetSpeak(int client);
    
    // 视角控制
    ENGINE_BRIDGE_API int Engine_SetView(int client, int viewEntity, int viewType);
    ENGINE_BRIDGE_API int Engine_AttachView(int client, int targetEntity);
    
    // 光照
    ENGINE_BRIDGE_API int Engine_SetLights(const char* lights);
    
    // 地面放置
    ENGINE_BRIDGE_API int Engine_DropToFloor(int entity);
    
    // 可见性检查
    ENGINE_BRIDGE_API int Engine_IsVisible(int srcEntity, int destEntity);
    ENGINE_BRIDGE_API int Engine_IsInViewCone(int entity, const Vector3 target);
    
    // 实体属性访问
ENGINE_BRIDGE_API int Engine_GetEntityOrigin(int entityId, Vector3 origin);
ENGINE_BRIDGE_API int Engine_SetEntityOrigin(int entityId, const Vector3 origin);
ENGINE_BRIDGE_API int Engine_GetEntityAngles(int entityId, Vector3 angles);
ENGINE_BRIDGE_API int Engine_SetEntityAngles(int entityId, const Vector3 angles);
ENGINE_BRIDGE_API int Engine_GetEntityVelocity(int entityId, Vector3 velocity);
ENGINE_BRIDGE_API int Engine_SetEntityVelocity(int entityId, const Vector3 velocity);
ENGINE_BRIDGE_API int Engine_GetEntityClassName(int entityId, char* buffer, int bufferSize);
ENGINE_BRIDGE_API int Engine_GetEntityModelName(int entityId, char* buffer, int bufferSize);
ENGINE_BRIDGE_API int Engine_GetEntityHealth(int entityId);
ENGINE_BRIDGE_API int Engine_SetEntityHealth(int entityId, int health);
ENGINE_BRIDGE_API int Engine_GetEntityArmor(int entityId);
ENGINE_BRIDGE_API int Engine_SetEntityArmor(int entityId, int armor);

// 实体列表操作
ENGINE_BRIDGE_API int Engine_GetAllEntities(int* entities, int maxCount);
ENGINE_BRIDGE_API int Engine_FindEntitiesByClass(const char* className, int* entities, int maxCount);

// 事件注册（回调机制）
typedef void (*ImpulseCallback)(int client, int impulse);
typedef void (*TouchCallback)(int touched, int toucher);
typedef void (*ThinkCallback)(int entity);

ENGINE_BRIDGE_API int Engine_RegisterImpulse(int impulse, ImpulseCallback callback);
ENGINE_BRIDGE_API int Engine_RegisterTouch(const char* touchedClass, const char* toucherClass, TouchCallback callback);
ENGINE_BRIDGE_API int Engine_RegisterThink(const char* className, ThinkCallback callback);

ENGINE_BRIDGE_API int Engine_UnregisterImpulse(int registerId);
ENGINE_BRIDGE_API int Engine_UnregisterTouch(int registerId);
ENGINE_BRIDGE_API int Engine_UnregisterThink(int registerId);
}
