# Engine模块桥接接口文档

## 概述
Engine模块桥接接口提供了在C#中操作GoldSrc引擎实体的完整功能，包括实体操作、属性访问、事件处理等。

## 文件结构

### C++桥接层
- `engine_bridge.h` - C++头文件，定义所有桥接接口
- `engine_bridge.cpp` - C++实现文件，包含具体功能实现

### C# P/Invoke层
- `EngineBridgeExtensions.cs` - P/Invoke接口定义和便捷包装方法
- `EngineEventDelegates.cs` - 事件委托定义和管理器
- `EngineBridgeExample.cs` - 使用示例

## 核心功能

### 1. 实体操作

#### 获取实体列表
```csharp
// 获取所有实体
int[] allEntities = EngineBridgeExtensions.GetAllEntities(1000);

// 按类名查找实体
int[] players = EngineBridgeExtensions.FindEntitiesByClass("player");
```

#### 实体属性访问
```csharp
// 位置操作
EngineBridge.Vector3 origin;
if (EngineBridgeExtensions.GetEntityOrigin(entityId, out origin))
{
    Console.WriteLine($"位置: {origin}");
}

EngineBridgeExtensions.SetEntityOrigin(entityId, new Vector3(100, 200, 300));

// 健康值和护甲值
int health = EngineBridgeExtensions.GetEntityHealth(entityId);
EngineBridgeExtensions.SetEntityHealth(entityId, 100);

int armor = EngineBridgeExtensions.GetEntityArmor(entityId);
EngineBridgeExtensions.SetEntityArmor(entityId, 50);
```

#### 字符串属性
```csharp
string className = EngineBridgeExtensions.GetEntityClassName(entityId);
string modelName = EngineBridgeExtensions.GetEntityModelName(entityId);
```

### 2. 事件系统

#### 事件委托定义
```csharp
// 脉冲事件
EngineEventDelegates.ImpulseEventHandler impulseHandler = (client, impulse) => {
    Console.WriteLine($"玩家{client}触发脉冲{impulse}");
};

// 触碰事件
EngineEventDelegates.TouchEventHandler touchHandler = (touched, toucher) => {
    Console.WriteLine($"实体{touched}被{toucher}触碰");
};

// 思考事件
EngineEventDelegates.ThinkEventHandler thinkHandler = (entity) => {
    Console.WriteLine($"实体{entity}正在思考");
};
```

#### 事件注册和管理
```csharp
// 注册事件
int impulseId = EngineEventDelegates.EngineEventManager.RegisterImpulseEvent(100, impulseHandler);
int touchId = EngineEventDelegates.EngineEventManager.RegisterTouchEvent("player", "weapon", touchHandler);
int thinkId = EngineEventDelegates.EngineEventManager.RegisterThinkEvent("npc", thinkHandler);

// 取消注册
EngineEventDelegates.EngineEventManager.UnregisterImpulseEvent(impulseId);
EngineEventDelegates.EngineEventManager.UnregisterTouchEvent(touchId);
EngineEventDelegates.EngineEventManager.UnregisterThinkEvent(thinkId);
```

## 命名规范

### C++桥接接口
- 使用大驼峰命名法
- 前缀为`Engine_`
- 示例：`Engine_GetEntityOrigin`, `Engine_SetEntityHealth`

### C# P/Invoke接口
- 与C++接口名称保持一致
- 使用`Engine_`前缀
- 示例：`Engine_GetEntityOrigin`, `Engine_SetEntityHealth`

### C#便捷包装方法
- 移除`Engine_`前缀
- 使用PascalCase命名
- 示例：`GetEntityOrigin`, `SetEntityHealth`

## 数据类型映射

| C++类型 | C#类型 | 说明 |
|---------|---------|------|
| `int` | `int` | 整数 |
| `float[3]` | `float[]` | 3D向量 |
| `char*` | `byte[]` | 字符串缓冲区 |
| `int*` | `int[]` | 整数数组 |

## 错误处理

所有接口返回整数值：
- `1` 表示成功
- `0` 表示失败

便捷包装方法将整数值转换为布尔值：
- `true` 表示成功
- `false` 表示失败

## 使用示例

完整的使用示例请参考`EngineBridgeExample.cs`文件中的`CompleteExample`方法。

## 注意事项

1. **内存管理**：使用byte数组作为字符串缓冲区时，确保缓冲区大小足够
2. **线程安全**：桥接接口不是线程安全的，请在主线程中调用
3. **性能考虑**：频繁调用大量实体操作时注意性能影响
4. **错误检查**：始终检查返回值，确保操作成功

## 扩展指南

如需添加新的桥接功能：

1. **C++层**：在`engine_bridge.h`中添加接口声明，在`engine_bridge.cpp`中实现
2. **C#层**：在`EngineBridgeExtensions.cs`中添加P/Invoke定义和便捷包装
3. **事件**：如需事件支持，在`EngineEventDelegates.cs`中添加委托定义
4. **测试**：在`EngineBridgeExample.cs`中添加使用示例

## 版本历史

- v1.0.0: 初始版本，包含基本的实体操作和属性访问
- v1.1.0: 添加事件系统支持
- v1.2.0: 优化性能，修复内存管理问题