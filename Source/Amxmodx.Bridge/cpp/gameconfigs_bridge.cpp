// gameconfigs_bridge.cpp
// 游戏配置系统桥接实现

#include "gameconfigs_bridge.h"
#include "../amxmodx.h"
#include "../CGameConfigs.h"

// 游戏配置句柄管理
static NativeHandle<GameConfigNative> g_GameConfigHandles;

int AmxModx_Bridge_LoadGameConfigFile(const char* fileName, char* errorBuffer, int errorBufferSize)
{
    if (!fileName || !errorBuffer || errorBufferSize <= 0)
        return 0;

    IGameConfig* config = nullptr;
    
    if (!ConfigManager.LoadGameConfigFile(fileName, &config, errorBuffer, errorBufferSize))
    {
        ConfigManager.CloseGameConfigFile(config);
        return 0;
    }

    int handle = g_GameConfigHandles.create();
    GameConfigNative* configHandle = g_GameConfigHandles.lookup(handle);
    
    if (!configHandle)
    {
        ConfigManager.CloseGameConfigFile(config);
        return 0;
    }

    configHandle->m_config = config;
    return handle;
}

int AmxModx_Bridge_GameConfGetOffset(int handle, const char* key)
{
    if (handle <= 0 || !key)
        return -1;

    GameConfigNative* handleObj = g_GameConfigHandles.lookup(handle);
    if (!handleObj || !handleObj->m_config)
        return -1;

    TypeDescription value;
    if (!handleObj->m_config->GetOffset(key, &value))
        return -1;

    return value.fieldOffset;
}

int AmxModx_Bridge_GameConfGetClassOffset(int handle, const char* className, const char* key)
{
    if (handle <= 0 || !className || !key)
        return -1;

    GameConfigNative* handleObj = g_GameConfigHandles.lookup(handle);
    if (!handleObj || !handleObj->m_config)
        return -1;

    TypeDescription value;
    if (!handleObj->m_config->GetOffsetByClass(className, key, &value))
        return -1;

    return value.fieldOffset;
}

int AmxModx_Bridge_GameConfGetKeyValue(int handle, const char* key, char* buffer, int bufferSize)
{
    if (handle <= 0 || !key || !buffer || bufferSize <= 0)
        return 0;

    GameConfigNative* handleObj = g_GameConfigHandles.lookup(handle);
    if (!handleObj || !handleObj->m_config)
        return 0;

    const char* value = handleObj->m_config->GetKeyValue(key);
    if (!value)
        return 0;

    strncpy(buffer, value, bufferSize - 1);
    buffer[bufferSize - 1] = '\0';
    return 1;
}

uintptr_t AmxModx_Bridge_GameConfGetAddress(int handle, const char* name)
{
    if (handle <= 0 || !name)
        return 0;

    GameConfigNative* handleObj = g_GameConfigHandles.lookup(handle);
    if (!handleObj || !handleObj->m_config)
        return 0;

    void* address = nullptr;
    if (!handleObj->m_config->GetAddress(name, &address))
        return 0;

    return reinterpret_cast<uintptr_t>(address);
}

int AmxModx_Bridge_CloseGameConfigFile(int* handle)
{
    if (!handle || *handle <= 0)
        return 0;

    GameConfigNative* handleObj = g_GameConfigHandles.lookup(*handle);
    if (!handleObj)
        return 0;

    if (handleObj->m_config)
    {
        ConfigManager.CloseGameConfigFile(handleObj->m_config);
        handleObj->m_config = nullptr;
    }

    if (g_GameConfigHandles.destroy(*handle))
    {
        *handle = 0;
        return 1;
    }

    return 0;
}