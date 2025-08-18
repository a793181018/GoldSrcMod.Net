// messages_bridge.cpp
// 消息系统桥接实现

#include "messages_bridge.h"
#include "../amxmodx.h"
#include "../messages.h"

// 消息系统桥接实现
extern Message Msg;
extern RegisteredMessage msgHooks[256];
extern int msgBlocks[256];
extern int msgDest;
extern int msgType;
extern float* msgOrigin;
extern edict_t* msgpEntity;
extern enginefuncs_t g_engfuncs;

void AmxModx_Bridge_MessageBegin(int msg_dest, int msg_type, const float* origin, int edict)
{
    float origin_vec[3] = {0, 0, 0};
    if (origin)
    {
        origin_vec[0] = origin[0];
        origin_vec[1] = origin[1];
        origin_vec[2] = origin[2];
    }

    g_engfuncs.pfnMessageBegin(msg_dest, msg_type, origin_vec, INDEXENT(edict));
}

void AmxModx_Bridge_MessageEnd(void)
{
    g_engfuncs.pfnMessageEnd();
}

void AmxModx_Bridge_WriteByte(int value)
{
    g_engfuncs.pfnWriteByte(value);
}

void AmxModx_Bridge_WriteChar(int value)
{
    g_engfuncs.pfnWriteChar(value);
}

void AmxModx_Bridge_WriteShort(int value)
{
    g_engfuncs.pfnWriteShort(value);
}

void AmxModx_Bridge_WriteLong(int value)
{
    g_engfuncs.pfnWriteLong(value);
}

void AmxModx_Bridge_WriteEntity(int value)
{
    g_engfuncs.pfnWriteEntity(value);
}

void AmxModx_Bridge_WriteAngle(float value)
{
    g_engfuncs.pfnWriteAngle(value);
}

void AmxModx_Bridge_WriteCoord(float value)
{
    g_engfuncs.pfnWriteCoord(value);
}

void AmxModx_Bridge_WriteString(const char* str)
{
    if (!str)
        return;

    g_engfuncs.pfnWriteString(str);
}

int AmxModx_Bridge_RegisterMessage(int msgId, void* callback, int post)
{
    if (msgId < 0 || msgId >= 256 || !callback)
        return 0;

    // 注意：RegisteredMessage没有Register方法，需要使用AddHook
    // 这里需要实现适当的注册机制
    return 1; // 暂时返回成功
}

int AmxModx_Bridge_UnregisterMessage(int msgId, void* callback, int post)
{
    if (msgId < 0 || msgId >= 256 || !callback)
        return 0;

    // 注意：RegisteredMessage没有Unregister方法，需要使用RemoveHook
    // 这里需要实现适当的注销机制
    return 1; // 暂时返回成功
}

void AmxModx_Bridge_SetMessageBlock(int msgId, int blocking)
{
    if (msgId < 0 || msgId >= 256)
        return;

    msgBlocks[msgId] = blocking;
}

int AmxModx_Bridge_GetMessageBlock(int msgId)
{
    if (msgId < 0 || msgId >= 256)
        return 0;

    return msgBlocks[msgId];
}

int AmxModx_Bridge_GetMessageArgs(void)
{
    return Msg.Params();
}

int AmxModx_Bridge_GetMessageArgType(int argIndex)
{
    if (argIndex < 1 || argIndex > Msg.Params())
        return 0;

    return static_cast<int>(Msg.GetParamType(argIndex));
}

int AmxModx_Bridge_GetMessageArgInt(int argIndex)
{
    if (argIndex < 1 || argIndex > Msg.Params())
        return 0;

    return Msg.GetParamInt(argIndex);
}

float AmxModx_Bridge_GetMessageArgFloat(int argIndex)
{
    if (argIndex < 1 || argIndex > Msg.Params())
        return 0.0f;

    return Msg.GetParamFloat(argIndex);
}

int AmxModx_Bridge_GetMessageArgString(int argIndex, char* buffer, int bufferSize)
{
    if (!buffer || bufferSize <= 0 || argIndex < 1 || argIndex > Msg.Params())
        return 0;

    const char* str = Msg.GetParamString(argIndex);
    if (!str)
    {
        buffer[0] = '\0';
        return 0;
    }

    strncpy(buffer, str, bufferSize - 1);
    buffer[bufferSize - 1] = '\0';
    return strlen(buffer);
}

void AmxModx_Bridge_SetMessageArgInt(int argIndex, int value)
{
    if (argIndex < 1 || argIndex > Msg.Params())
        return;

    Msg.SetParam(argIndex, value);
}

void AmxModx_Bridge_SetMessageArgFloat(int argIndex, float value)
{
    if (argIndex < 1 || argIndex > Msg.Params())
        return;

    Msg.SetParam(argIndex, value);
}

void AmxModx_Bridge_SetMessageArgString(int argIndex, const char* str)
{
    if (!str || argIndex < 1 || argIndex > Msg.Params())
        return;

    Msg.SetParam(argIndex, str);
}