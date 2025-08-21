// hamsandwich_structs_bridge.h
// 复杂结构体的桥接头文件

#ifndef HAMSANDWICH_STRUCTS_BRIDGE_H
#define HAMSANDWICH_STRUCTS_BRIDGE_H

#ifdef __cplusplus
extern "C" {
#endif

// TraceResult结构体访问函数
void GetTraceResultAllSolid(void* traceResult, int* allSolid);
void GetTraceResultStartSolid(void* traceResult, int* startSolid);
void GetTraceResultFraction(void* traceResult, float* fraction);
void GetTraceResultEndPos(void* traceResult, float endPos[3]);
void GetTraceResultPlaneNormal(void* traceResult, float normal[3]);
void GetTraceResultHit(void* traceResult, int* hit);
void GetTraceResultHitGroup(void* traceResult, int* hitGroup);

// ItemInfo结构体访问函数
void GetItemInfoName(void* itemInfo, char* name, int maxLen);
void GetItemInfoMaxClip(void* itemInfo, int* maxClip);
void GetItemInfoSlot(void* itemInfo, int* slot);
void GetItemInfoPosition(void* itemInfo, int* position);
void GetItemInfoId(void* itemInfo, int* id);
void GetItemInfoFlags(void* itemInfo, int* flags);

void SetItemInfoMaxClip(void* itemInfo, int maxClip);
void SetItemInfoSlot(void* itemInfo, int slot);
void SetItemInfoPosition(void* itemInfo, int position);
void SetItemInfoFlags(void* itemInfo, int flags);

// 向量操作辅助函数
void VectorNormalize(float vec[3]);
float VectorLength(const float vec[3]);
void VectorAdd(const float a[3], const float b[3], float result[3]);
void VectorSubtract(const float a[3], const float b[3], float result[3]);
void VectorScale(const float vec[3], float scale, float result[3]);

// 实体变换矩阵访问
void GetEntityOrigin(int entity, float origin[3]);
void GetEntityAngles(int entity, float angles[3]);
void SetEntityOrigin(int entity, const float origin[3]);
void SetEntityAngles(int entity, const float angles[3]);

// 距离和可见性检查
float GetDistanceBetweenEntities(int entity1, int entity2);
bool IsEntityVisible(int entity, int target);

#ifdef __cplusplus
}
#endif

#endif // HAMSANDWICH_STRUCTS_BRIDGE_H