/*
 * AMX Mod X Core Bridge Implementation
 * 实现amxcore.cpp中核心接口的C#桥接
 */
#include "amxcore_bridge.h"
#include "amx.h"
#include <cstring>
#include <cstdlib>

// 全局AMX实例指针（需要外部设置）
AMX* g_currentAmx = nullptr;

/**
 * @brief 获取当前函数的参数数量
 * @return 参数数量
 */
int AmxModx_Bridge_GetNumArgs(void)
{
    if (!g_currentAmx)
        return 0;
    
    AMX_HEADER* hdr = (AMX_HEADER*)g_currentAmx->base;
    unsigned char* data = g_currentAmx->data ? g_currentAmx->data : g_currentAmx->base + (int)hdr->dat;
    
    // 参数数量在栈上，位置是 frm + 2*cell
    cell bytes = *(cell*)(data + (int)g_currentAmx->frm + 2 * sizeof(cell));
    return bytes / sizeof(cell);
}

/**
 * @brief 获取指定索引的参数值
 * @param index 参数索引（从0开始）
 * @param offset 数组偏移量（对于数组参数）
 * @return 参数值
 */
int AmxModx_Bridge_GetArg(int index, int offset)
{
    if (!g_currentAmx || index < 0)
        return 0;
    
    AMX_HEADER* hdr = (AMX_HEADER*)g_currentAmx->base;
    unsigned char* data = g_currentAmx->data ? g_currentAmx->data : g_currentAmx->base + (int)hdr->dat;
    
    // 获取基础值
    cell value = *(cell*)(data + (int)g_currentAmx->frm + ((int)index + 3) * sizeof(cell));
    
    // 调整数组访问的地址
    value += offset * sizeof(cell);
    
    // 获取间接值
    return *(cell*)(data + (int)value);
}

/**
 * @brief 设置指定索引的参数值
 * @param index 参数索引（从0开始）
 * @param offset 数组偏移量（对于数组参数）
 * @param value 要设置的值
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_SetArg(int index, int offset, int value)
{
    if (!g_currentAmx || index < 0)
        return 0;
    
    AMX_HEADER* hdr = (AMX_HEADER*)g_currentAmx->base;
    unsigned char* data = g_currentAmx->data ? g_currentAmx->data : g_currentAmx->base + (int)hdr->dat;
    
    // 获取基础地址
    cell addr = *(cell*)(data + (int)g_currentAmx->frm + ((int)index + 3) * sizeof(cell));
    addr += offset * sizeof(cell);
    
    // 验证地址范围
    if (addr < 0 || (addr >= g_currentAmx->hea && addr < g_currentAmx->stk))
        return 0;
    
    // 设置值
    *(cell*)(data + (int)addr) = value;
    return 1;
}

/**
 * @brief 获取可用堆空间大小
 * @return 堆空间字节数
 */
int AmxModx_Bridge_GetHeapSpace(void)
{
    if (!g_currentAmx)
        return 0;
    
    return g_currentAmx->stk - g_currentAmx->hea;
}

/**
 * @brief 根据函数名获取函数索引
 * @param functionName 函数名称
 * @return 函数索引，-1表示未找到
 */
int AmxModx_Bridge_GetFunctionIndex(const char* functionName)
{
    if (!g_currentAmx || !functionName)
        return -1;
    
    int index;
    int err = amx_FindPublic(g_currentAmx, functionName, &index);
    return (err == AMX_ERR_NONE) ? index : -1;
}

/**
 * @brief 交换32位整数的字节顺序
 * @param value 要交换的值
 * @return 交换后的值
 */
int AmxModx_Bridge_SwapChars(int value)
{
    union {
        int i;
        unsigned char b[4];
    } v;
    
    v.i = value;
    
    // 交换字节顺序
    unsigned char temp = v.b[0];
    v.b[0] = v.b[3];
    v.b[3] = temp;
    temp = v.b[1];
    v.b[1] = v.b[2];
    v.b[2] = temp;
    
    return v.i;
}

/**
 * @brief 获取属性值（简化版本，实际属性系统较复杂）
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @param buffer 输出缓冲区
 * @param maxLen 缓冲区最大长度
 * @return 属性值
 */
int AmxModx_Bridge_GetProperty(int id, const char* name, int value, char* buffer, int maxLen)
{
    // 这里实现简化版本，实际应该访问属性系统
    if (!name || !buffer || maxLen <= 0)
        return 0;
    
    // 返回空字符串表示属性未找到
    buffer[0] = '\0';
    return 0;
}

/**
 * @brief 设置属性值（简化版本）
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @param newName 新属性名称（可选）
 * @return 之前的属性值
 */
int AmxModx_Bridge_SetProperty(int id, const char* name, int value, const char* newName)
{
    // 这里实现简化版本，实际应该访问属性系统
    return 0;
}

/**
 * @brief 删除属性（简化版本）
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @return 被删除的属性值
 */
int AmxModx_Bridge_DeleteProperty(int id, const char* name, int value)
{
    // 这里实现简化版本，实际应该访问属性系统
    return 0;
}

/**
 * @brief 检查属性是否存在（简化版本）
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @return 存在返回1，不存在返回0
 */
int AmxModx_Bridge_PropertyExists(int id, const char* name, int value)
{
    // 这里实现简化版本，实际应该访问属性系统
    return 0;
}

// 设置当前AMX实例的辅助函数
void AmxModx_Bridge_SetCurrentAmx(AMX* amx)
{
    g_currentAmx = amx;
}