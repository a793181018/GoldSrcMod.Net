// vim: set ts=4 sw=4 tw=99 noet:
//
// Event System Bridge - C++ Implementation
// Copyright (C) The AMX Mod X Development Team
//
// This software is licensed under the GNU General Public License, version 3 or higher.

#include "event_bridge.h"
#include "CEvent.h"
#include <amxmodx.h>

extern EventsMngr g_events;
extern NativeHandle<EventHook> EventHandles;

// 获取有效的插件
static CPluginMngr::CPlugin* GetValidPlugin()
{
    for (CPluginMngr::iterator iter = g_plugins.begin(); iter; ++iter) {
        CPluginMngr::CPlugin& p = *iter;
        if (p.isValid()) {
            return &p;
        }
    }
    return nullptr;
}

int AmxModx_Bridge_RegisterEvent(const char* eventName, const char* callbackFunc, int flags, const char** conditions, int conditionCount)
{
    if (!eventName || !callbackFunc)
        return 0;

    CPluginMngr::CPlugin* plugin = GetValidPlugin();
    if (!plugin)
        return 0;

    int eventId = g_events.getEventId(eventName);
    if (eventId == 0)
        return 0;

    int forwardId = registerSPForwardByName(plugin->getAMX(), callbackFunc, FP_CELL, FP_DONE);
    if (forwardId == -1)
        return 0;

    int handle = g_events.registerEvent(plugin, forwardId, flags, eventId);
    if (!handle)
        return 0;

    auto event = EventHandles.lookup(handle);
    if (event && event->m_event)
    {
        for (int i = 0; i < conditionCount; ++i)
        {
            if (conditions[i])
                event->m_event->registerFilter(const_cast<char*>(conditions[i]));
        }
    }

    return handle;
}

int AmxModx_Bridge_RegisterEventEx(const char* eventName, const char* callbackFunc, int flags, const char** conditions, int conditionCount)
{
    // register_event_ex 与 register_event 在内部实现上类似
    // 但使用扩展标志处理
    return AmxModx_Bridge_RegisterEvent(eventName, callbackFunc, flags, conditions, conditionCount);
}

int AmxModx_Bridge_EnableEvent(int eventHandle)
{
    auto handle = EventHandles.lookup(eventHandle);
    if (!handle || !handle->m_event)
        return 0;

    handle->m_event->setForwardState(FSTATE_ACTIVE);
    return 1;
}

int AmxModx_Bridge_DisableEvent(int eventHandle)
{
    auto handle = EventHandles.lookup(eventHandle);
    if (!handle || !handle->m_event)
        return 0;

    handle->m_event->setForwardState(FSTATE_STOP);
    return 1;
}

int AmxModx_Bridge_GetEventId(const char* eventName)
{
    if (!eventName)
        return 0;
    return g_events.getEventId(eventName);
}

int AmxModx_Bridge_IsEventValid(int eventHandle)
{
    auto handle = EventHandles.lookup(eventHandle);
    return (handle && handle->m_event) ? 1 : 0;
}

int AmxModx_Bridge_GetEventList(char** buffer, int bufferSize)
{
    // 这里简化实现，实际需要从g_events获取完整事件列表
    // 由于事件系统内部结构复杂，这里返回0表示暂不支持
    return 0;
}