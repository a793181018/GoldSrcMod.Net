// bridge.cpp
// 桥接接口注册和初始化

#include "datastructs_bridge.h"
#include "stackstructs_bridge.h"
#include "../modules.h"



// 使用模块内部的注册机制
void RegisterBridgeFunctions()
{
    // 注册datastructs桥接函数
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayCreate, "AmxModx_Bridge_ArrayCreate");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayDestroy, "AmxModx_Bridge_ArrayDestroy");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArraySize, "AmxModx_Bridge_ArraySize");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayResize, "AmxModx_Bridge_ArrayResize");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayGetCell, "AmxModx_Bridge_ArrayGetCell");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArraySetCell, "AmxModx_Bridge_ArraySetCell");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayPushCell, "AmxModx_Bridge_ArrayPushCell");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayGetString, "AmxModx_Bridge_ArrayGetString");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArraySetString, "AmxModx_Bridge_ArraySetString");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayClone, "AmxModx_Bridge_ArrayClone");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayClear, "AmxModx_Bridge_ArrayClear");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayDeleteItem, "AmxModx_Bridge_ArrayDeleteItem");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArraySwap, "AmxModx_Bridge_ArraySwap");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayFindString, "AmxModx_Bridge_ArrayFindString");
    MNF_RegisterFunction((void*)AmxModx_Bridge_ArrayFindValue, "AmxModx_Bridge_ArrayFindValue");
    
    // 注册stackstructs桥接函数
    MNF_RegisterFunction((void*)AmxModx_Bridge_CreateStack, "AmxModx_Bridge_CreateStack");
    MNF_RegisterFunction((void*)AmxModx_Bridge_DestroyStack, "AmxModx_Bridge_DestroyStack");
    MNF_RegisterFunction((void*)AmxModx_Bridge_PushStackCell, "AmxModx_Bridge_PushStackCell");
    MNF_RegisterFunction((void*)AmxModx_Bridge_PushStackString, "AmxModx_Bridge_PushStackString");
    MNF_RegisterFunction((void*)AmxModx_Bridge_PushStackArray, "AmxModx_Bridge_PushStackArray");
    MNF_RegisterFunction((void*)AmxModx_Bridge_PopStackCell, "AmxModx_Bridge_PopStackCell");
    MNF_RegisterFunction((void*)AmxModx_Bridge_PopStackString, "AmxModx_Bridge_PopStackString");
    MNF_RegisterFunction((void*)AmxModx_Bridge_PopStackArray, "AmxModx_Bridge_PopStackArray");
}

// 清理桥接资源
void CleanupBridgeResources()
{
    // 清理栈资源
    // 注意：数组资源由ArrayHandles管理，会自动清理
}