// vim: set ts=4 sw=4 tw=99 noet:
#include "natives_bridge.h"
#include "amx.h"
#include <cstring>

// 使用标准AMX原生函数实现方式
int AmxModx_Bridge_RegisterNative(AMX *amx, const char *name, AMX_NATIVE func)
{
    if (!amx || !name || !func)
        return 0;
    
    AMX_NATIVE_INFO native_info = { const_cast<char*>(name), func };
    return amx_Register(amx, &native_info, 1) == AMX_ERR_NONE ? 1 : 0;
}

int AmxModx_Bridge_UnregisterNative(AMX *amx, const char *name)
{
    if (!amx || !name)
        return 0;
    
    // 在AMX中，注销原生函数通过重新注册为nullptr实现
    AMX_NATIVE_INFO native_info = { const_cast<char*>(name), nullptr };
    return amx_Register(amx, &native_info, 1) == AMX_ERR_NONE ? 1 : 0;
}

int AmxModx_Bridge_RegisterLibrary(AMX *amx, const char *libname)
{
    if (!amx || !libname)
        return 0;
    
    // 库注册需要特殊处理，返回0表示不支持
    return 0;
}

int AmxModx_Bridge_GetNativeCount(AMX *amx)
{
    if (!amx)
        return 0;
    
    int count = 0;
    return amx_NumNatives(amx, &count) == AMX_ERR_NONE ? count : 0;
}

const char *AmxModx_Bridge_GetNativeName(AMX *amx, int index)
{
    if (!amx || index < 0)
        return nullptr;
    
    char name[256];
    if (amx_GetNative(amx, index, name) == AMX_ERR_NONE)
    {
        static char buffer[256];
        strcpy(buffer, name);
        return buffer;
    }
    
    return nullptr;
}

bool AmxModx_Bridge_NativeExists(AMX *amx, const char *name)
{
    if (!amx || !name)
        return false;
    
    int index = 0;
    return amx_FindNative(amx, const_cast<char*>(name), &index) == AMX_ERR_NONE;
}

void AmxModx_Bridge_ClearLibraries(AMX *amx)
{
    if (!amx)
        return;
    
    // 库清除需要特殊处理
}

// 字符串处理
const char *AmxModx_Bridge_GetString(AMX *amx, cell param, int *length)
{
    if (!amx || !param)
        return nullptr;
    
    cell *phys = nullptr;
    if (amx_GetAddr(amx, param, &phys) != AMX_ERR_NONE)
        return nullptr;
    
    int str_len = 0;
    if (amx_StrLen(phys, &str_len) != AMX_ERR_NONE)
        return nullptr;
    
    if (length)
        *length = str_len;
    
    static char buffer[2048];
    if (amx_GetString(buffer, phys, 0, sizeof(buffer)) == AMX_ERR_NONE)
        return buffer;
    
    return nullptr;
}

int AmxModx_Bridge_SetString(AMX *amx, cell param, const char *string, int maxlen)
{
    if (!amx || !param || !string)
        return 0;
    
    cell *phys = nullptr;
    if (amx_GetAddr(amx, param, &phys) != AMX_ERR_NONE)
        return 0;
    
    return amx_SetString(phys, const_cast<char*>(string), 0, 0, maxlen) == AMX_ERR_NONE ? 1 : 0;
}

// 参数访问
cell AmxModx_Bridge_GetParam(AMX *amx, int index)
{
    if (!amx || index < 1)
        return 0;
    
    cell *params = nullptr;
    cell addr = amx->frm + (index * sizeof(cell));
    if (amx_GetAddr(amx, addr, &params) != AMX_ERR_NONE)
        return 0;
    
    return *params;
}

cell *AmxModx_Bridge_GetParamAddress(AMX *amx, int index)
{
    if (!amx || index < 1)
        return nullptr;
    
    cell *params = nullptr;
    cell addr = amx->frm + (index * sizeof(cell));
    if (amx_GetAddr(amx, addr, &params) != AMX_ERR_NONE)
        return nullptr;
    
    return params;
}

int AmxModx_Bridge_SetParam(AMX *amx, int index, cell value)
{
    if (!amx || index < 1)
        return 0;
    
    cell *params = nullptr;
    cell addr = amx->frm + (index * sizeof(cell));
    if (amx_GetAddr(amx, addr, &params) != AMX_ERR_NONE)
        return 0;
    
    *params = value;
    return 1;
}

// 浮点数处理
float AmxModx_Bridge_GetFloat(AMX *amx, int index)
{
    if (!amx || index < 1)
        return 0.0f;
    
    cell value = AmxModx_Bridge_GetParam(amx, index);
    return amx_ctof(value);
}

int AmxModx_Bridge_SetFloat(AMX *amx, int index, float value)
{
    if (!amx || index < 1)
        return 0;
    
    cell cell_value = amx_ftoc(value);
    return AmxModx_Bridge_SetParam(amx, index, cell_value);
}

// 数组处理
int AmxModx_Bridge_GetArray(AMX *amx, cell param, cell *dest, int size)
{
    if (!amx || !param || !dest || size <= 0)
        return 0;
    
    cell *phys = nullptr;
    if (amx_GetAddr(amx, param, &phys) != AMX_ERR_NONE)
        return 0;
    
    memcpy(dest, phys, size * sizeof(cell));
    return 1;
}

int AmxModx_Bridge_SetArray(AMX *amx, cell param, const cell *source, int size)
{
    if (!amx || !param || !source || size <= 0)
        return 0;
    
    cell *phys = nullptr;
    if (amx_GetAddr(amx, param, &phys) != AMX_ERR_NONE)
        return 0;
    
    memcpy(phys, source, size * sizeof(cell));
    return 1;
}

// 错误处理
void AmxModx_Bridge_LogError(AMX *amx, int err, const char *message)
{
    if (!amx || !message)
        return;
    
    amx_RaiseError(amx, err);
}