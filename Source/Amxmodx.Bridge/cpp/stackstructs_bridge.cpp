// stackstructs_bridge.cpp
// 栈结构桥接接口实现

#include "stackstructs_bridge.h"
#include <vector>
#include <cstring>

// 简化的栈实现用于桥接
class BridgeStack
{
public:
    struct StackItem
    {
        enum Type { CELL, STRING, ARRAY } type;
        int value;
        char* str_data;
        
        StackItem() : type(CELL), value(0), str_data(nullptr) {}
        ~StackItem()
        {
            delete[] str_data;
        }
    };
    
    BridgeStack() {}
    ~BridgeStack() { Clear(); }
    
    bool PushCell(int value)
    {
        StackItem* item = new StackItem();
        item->type = StackItem::CELL;
        item->value = value;
        items.push_back(item);
        return true;
    }
    
    bool PushString(const char* str)
    {
        if (!str) return false;
        
        StackItem* item = new StackItem();
        item->type = StackItem::STRING;
        item->value = 0;
        
        size_t len = strlen(str);
        item->str_data = new char[len + 1];
        strcpy(item->str_data, str);
        
        items.push_back(item);
        return true;
    }
    
    bool PushArray(int arrayHandle)
    {
        if (arrayHandle <= 0) return false;
        
        StackItem* item = new StackItem();
        item->type = StackItem::ARRAY;
        item->value = arrayHandle;
        
        items.push_back(item);
        return true;
    }
    
    bool PopCell(int* value)
    {
        if (items.empty()) return false;
        
        StackItem* item = items.back();
        if (item->type != StackItem::CELL)
        {
            return false;
        }
        
        *value = item->value;
        delete item;
        items.pop_back();
        return true;
    }
    
    bool PopString(char* buffer, int size)
    {
        if (items.empty() || !buffer || size <= 0) return false;
        
        StackItem* item = items.back();
        if (item->type != StackItem::STRING || !item->str_data)
        {
            return false;
        }
        
        strncpy(buffer, item->str_data, size - 1);
        buffer[size - 1] = '\0';
        
        delete item;
        items.pop_back();
        return true;
    }
    
    bool PopArray(int* arrayHandle)
    {
        if (items.empty() || !arrayHandle) return false;
        
        StackItem* item = items.back();
        if (item->type != StackItem::ARRAY)
        {
            return false;
        }
        
        *arrayHandle = item->value;
        delete item;
        items.pop_back();
        return true;
    }
    
    int GetDepth() const
    {
        return static_cast<int>(items.size());
    }
    
    void Clear()
    {
        for (StackItem* item : items)
        {
            delete item;
        }
        items.clear();
    }
    
private:
    std::vector<StackItem*> items;
};

// 句柄管理
static std::vector<BridgeStack*> g_stacks;
static int g_next_handle = 1;

static BridgeStack* GetStack(int handle)
{
    if (handle <= 0 || handle > static_cast<int>(g_stacks.size())) return nullptr;
    return g_stacks[handle - 1];
}

extern "C" {

int AmxModx_Bridge_CreateStack()
{
    BridgeStack* stack = new BridgeStack();
    if (!stack) return 0;
    
    g_stacks.push_back(stack);
    return g_next_handle++;
}

int AmxModx_Bridge_DestroyStack(int* handle)
{
    if (!handle || *handle <= 0) return 0;
    
    BridgeStack* stack = GetStack(*handle);
    if (!stack) return 0;
    
    delete stack;
    g_stacks[*handle - 1] = nullptr;
    *handle = 0;
    return 1;
}

int AmxModx_Bridge_PushStackCell(int handle, int value)
{
    BridgeStack* stack = GetStack(handle);
    return stack && stack->PushCell(value) ? 1 : 0;
}

int AmxModx_Bridge_PushStackString(int handle, const char* str)
{
    BridgeStack* stack = GetStack(handle);
    return stack && stack->PushString(str) ? 1 : 0;
}

int AmxModx_Bridge_PushStackArray(int handle, int arrayHandle)
{
    BridgeStack* stack = GetStack(handle);
    return stack && stack->PushArray(arrayHandle) ? 1 : 0;
}

int AmxModx_Bridge_PopStackCell(int handle, int* value)
{
    BridgeStack* stack = GetStack(handle);
    if (!stack || !value) return 0;
    
    return stack->PopCell(value) ? 1 : 0;
}

int AmxModx_Bridge_PopStackString(int handle, char* buffer, int size)
{
    BridgeStack* stack = GetStack(handle);
    if (!stack || !buffer || size <= 0) return 0;
    
    return stack->PopString(buffer, size) ? static_cast<int>(strlen(buffer)) : 0;
}

int AmxModx_Bridge_PopStackArray(int handle, int* arrayHandle)
{
    BridgeStack* stack = GetStack(handle);
    if (!stack || !arrayHandle) return 0;
    
    return stack->PopArray(arrayHandle) ? 1 : 0;
}

int AmxModx_Bridge_GetStackDepth(int handle)
{
    BridgeStack* stack = GetStack(handle);
    return stack ? stack->GetDepth() : -1;
}

int AmxModx_Bridge_ClearStack(int handle)
{
    BridgeStack* stack = GetStack(handle);
    if (!stack) return 0;
    
    stack->Clear();
    return 1;
}

} // extern "C"