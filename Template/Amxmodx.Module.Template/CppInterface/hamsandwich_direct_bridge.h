#pragma once

// 前置声明，避免包含vector.h头文件
struct edict_s;
typedef struct edict_s edict_t;

// 直接桥接接口 - 不依赖AMXX函数
extern "C" {

// 实体和偏移验证
bool IsValidEntity(int entityId);
bool IsValidOffset(int offset);

// 直接数据访问
int GetEntityPrivateDataInt(int entityId, int offset);
float GetEntityPrivateDataFloat(int entityId, int offset);
int GetEntityPrivateDataEntity(int entityId, int offset);
void SetEntityPrivateDataInt(int entityId, int offset, int value);
void SetEntityPrivateDataFloat(int entityId, int offset, float value);
void SetEntityPrivateDataEntity(int entityId, int offset, int targetEntity);

// 向量数据访问
void GetEntityPrivateDataVector(int entityId, int offset, float vec[3]);
void SetEntityPrivateDataVector(int entityId, int offset, const float vec[3]);

// 实体变量访问
int GetEntityVarInt(int entityId, int offset);
float GetEntityVarFloat(int entityId, int offset);
void GetEntityVarVector(int entityId, int offset, float vec[3]);
void SetEntityVarInt(int entityId, int offset, int value);
void SetEntityVarFloat(int entityId, int offset, float value);
void SetEntityVarVector(int entityId, int offset, const float vec[3]);

// 直接内存操作
void* GetEntityBasePointer(int entityId);
int GetEntityMemorySize(int entityId);

// 工具函数
int EntityToIndex(edict_t* edict);
edict_t* IndexToEntity(int index);
void* GetPrivateDataPtr(int entityId);

}