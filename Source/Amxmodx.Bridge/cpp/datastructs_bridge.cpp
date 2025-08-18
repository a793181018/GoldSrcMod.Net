// datastructs_bridge.cpp
// 数据结构桥接接口实现

#include "datastructs_bridge.h"
#include "../datastructs.h"
#include <cstring>

int AmxModx_Bridge_ArrayCreate(int cellsize, int reserved)
{
    if (cellsize <= 0) cellsize = 1;
    if (reserved < 0) reserved = 32;
    
    return ArrayHandles.create(cellsize, reserved);
}

int AmxModx_Bridge_ArrayDestroy(int* handle)
{
    if (!handle || *handle <= 0) return 0;
    
    CellArray* vec = ArrayHandles.lookup(*handle);
    if (!vec) return 0;
    
    if (ArrayHandles.destroy(*handle))
    {
        *handle = 0;
        return 1;
    }
    
    return 0;
}

int AmxModx_Bridge_ArraySize(int handle)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    return vec ? static_cast<int>(vec->size()) : -1;
}

int AmxModx_Bridge_ArrayResize(int handle, int count)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    if (count < 0) return 0;
    
    return vec->resize(static_cast<size_t>(count)) ? 1 : 0;
}

int AmxModx_Bridge_ArrayGetCell(int handle, int index, int block)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    if (index < 0 || static_cast<size_t>(index) >= vec->size()) return 0;
    if (block < 0 || static_cast<size_t>(block) >= vec->blocksize()) return 0;
    
    cell* blk = vec->at(static_cast<size_t>(index));
    return blk[block];
}

int AmxModx_Bridge_ArraySetCell(int handle, int index, int value, int block)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    if (index < 0 || static_cast<size_t>(index) >= vec->size()) return 0;
    if (block < 0 || static_cast<size_t>(block) >= vec->blocksize()) return 0;
    
    cell* blk = vec->at(static_cast<size_t>(index));
    blk[block] = value;
    return 1;
}

int AmxModx_Bridge_ArrayPushCell(int handle, int value)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return -1;
    
    cell* blk = vec->push();
    if (!blk) return -1;
    
    *blk = value;
    return static_cast<int>(vec->size() - 1);
}

int AmxModx_Bridge_ArrayGetString(int handle, int index, char* buffer, int size)
{
    if (!buffer || size <= 0) return 0;
    
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    if (index < 0 || static_cast<size_t>(index) >= vec->size()) return 0;
    
    cell* blk = vec->at(static_cast<size_t>(index));
    
    // 将cell数组转换为字符串
    int len = 0;
    char* dst = buffer;
    size_t max_len = (size - 1) < (vec->blocksize() * sizeof(cell)) ? (size - 1) : (vec->blocksize() * sizeof(cell));
    
    for (size_t i = 0; i < max_len && blk[i] != 0; i++)
    {
        dst[len++] = static_cast<char>(blk[i] & 0xFF);
    }
    dst[len] = '\0';
    
    return len;
}

int AmxModx_Bridge_ArraySetString(int handle, int index, const char* str)
{
    if (!str) return 0;
    
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    if (index < 0 || static_cast<size_t>(index) >= vec->size()) return 0;
    
    cell* blk = vec->at(static_cast<size_t>(index));
    
    // 将字符串转换为cell数组
    size_t len = strlen(str);
    if (len > vec->blocksize() - 1) len = vec->blocksize() - 1;
    
    for (size_t i = 0; i < len; i++)
    {
        blk[i] = static_cast<cell>(str[i]);
    }
    blk[len] = 0;
    
    return static_cast<int>(len);
}

int AmxModx_Bridge_ArrayClone(int handle)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    CellArray* cloned = vec->clone();
    if (!cloned) return 0;
    
    return ArrayHandles.clone(cloned);
}

int AmxModx_Bridge_ArrayClear(int handle)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    vec->clear();
    return 1;
}

int AmxModx_Bridge_ArrayDeleteItem(int handle, int index)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    if (index < 0 || static_cast<size_t>(index) >= vec->size()) return 0;
    
    vec->remove(static_cast<size_t>(index));
    return 1;
}

int AmxModx_Bridge_ArraySwap(int handle, int index1, int index2)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return 0;
    
    if (index1 < 0 || static_cast<size_t>(index1) >= vec->size()) return 0;
    if (index2 < 0 || static_cast<size_t>(index2) >= vec->size()) return 0;
    
    return vec->swap(static_cast<size_t>(index1), static_cast<size_t>(index2)) ? 1 : 0;
}

int AmxModx_Bridge_ArrayFindString(int handle, const char* str)
{
    if (!str) return -1;
    
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return -1;
    
    for (size_t i = 0; i < vec->size(); i++)
    {
        cell* blk = vec->at(i);
        
        // 简单的字符串比较
        bool isMatch = true;
        for (size_t j = 0; j < vec->blocksize(); j++)
        {
            char c1 = static_cast<char>(blk[j] & 0xFF);
            char c2 = str[j];
            
            if (c1 != c2)
            {
                isMatch = false;
                break;
            }
            if (c1 == '\0') break;
        }
        
        if (isMatch) return static_cast<int>(i);
    }
    
    return -1;
}

int AmxModx_Bridge_ArrayFindValue(int handle, int value)
{
    CellArray* vec = ArrayHandles.lookup(handle);
    if (!vec) return -1;
    
    for (size_t i = 0; i < vec->size(); i++)
    {
        cell* blk = vec->at(i);
        if (*blk == value) return static_cast<int>(i);
    }
    
    return -1;
}