# AMX Mod X .NET 高级封装层

## 📋 概述

本项目为AMX Mod X提供了完整的.NET高级封装层，将原始的P/Invoke接口转换为易于使用的面向对象API。

## 🏗️ 项目结构

```
Bridge/
├── PInvoke/              # P/Invoke接口声明层
│   ├── Communication/    # 通信模块P/Invoke
│   ├── Data/            # 数据模块P/Invoke
│   ├── Engine/          # 引擎模块P/Invoke
│   ├── Gameplay/        # 游戏模块P/Invoke
│   └── System/          # 系统模块P/Invoke
├── Wrappers/            # 高级封装层
│   ├── Communication/   # 通信模块高级封装
│   ├── Data/            # 数据模块高级封装
│   ├── Engine/          # 引擎模块高级封装
│   ├── Gameplay/        # 游戏模块高级封装
│   └── System/          # 系统模块高级封装
└── Examples/            # 使用示例
```

## 🚀 快速开始

### 1. 初始化

```csharp
using AmxModx.Wrappers;

// 初始化所有模块
AmxModxManager.Initialize();
```

### 2. 使用示例

#### 系统模块
```csharp
// 获取参数
int paramCount = CoreManager.ParameterCount;
string paramValue = CoreManager.GetStringParameter(0);

// 设置属性
var result = CoreManager.SetProperty(1, "test", 123);
```

#### 引擎模块
```csharp
// 实体操作
if (EngineManager.IsValidEntity(entityId))
{
    var origin = EngineManager.GetEntityOrigin(entityId);
    var distance = EngineManager.GetEntityDistance(entity1, entity2);
}

// 追踪
var trace = EngineManager.TraceLine(startPos, endPos, ignoreEntity);
```

#### 数据模块
```csharp
// CVar操作
var cvar = CVarManager.Create("my_var", "default_value");
string value = CVarManager.GetString("hostname");
CVarManager.SetFloat("sv_gravity", 800.0f);
```

#### 通信模块
```csharp
// 事件系统
EventManager.RegisterEvent("player_death", OnPlayerDeath);
EventManager.FireEvent("round_start", new Dictionary<string, object>());

// 转发系统
ForwardManager.RegisterClientConnect(OnClientConnect);
int result = ForwardManager.ExecuteForward("client_command", clientId, command);
```

## 📚 模块文档

### System 模块
- **CoreManager**: 核心功能封装
- **NativeManager**: 本地函数封装
- **CommandManager**: 命令系统封装
- **TaskManager**: 任务系统封装

### Engine 模块
- **EngineManager**: 引擎功能封装
- **TraceResult**: 追踪结果封装

### Data 模块
- **CVarManager**: 控制台变量封装
- **CVar**: CVar实例封装

### Communication 模块
- **EventManager**: 事件系统封装
- **ForwardManager**: 转发系统封装

### Gameplay 模块
- **HamSandwichManager**: HAM系统封装
- **HamSandwichWrapper**: HAM包装器

## 🔧 开发指南

### 创建新的高级封装

1. **分析P/Invoke接口**: 查看对应的P/Invoke文件
2. **设计封装结构**: 确定类和方法的命名
3. **实现封装**: 在对应模块目录下创建封装类
4. **添加示例**: 在Examples目录下添加使用示例

### 命名规范

- **命名空间**: `AmxModx.Wrappers.[模块名]`
- **类名**: `[功能]Manager` 或 `[功能]Wrapper`
- **方法名**: 使用PascalCase，描述性命名

### 错误处理

所有高级封装都包含完整的错误处理：
- 参数验证
- 空值检查
- 异常捕获
- 错误日志

## 📖 API 参考

### 统一入口

使用 `AmxModxManager` 作为统一入口：

```csharp
// 系统模块
AmxModxManager.System.Core.GetParameter(0);
AmxModxManager.System.Native.PtrToString(ptr);

// 引擎模块
AmxModxManager.Engine.Engine.IsValidEntity(entityId);

// 数据模块
AmxModxManager.Data.CVar.GetString("hostname");

// 通信模块
AmxModxManager.Communication.Event.RegisterEvent("death", callback);
```

## 🎯 特性

- ✅ **类型安全**: 强类型API，避免指针操作
- ✅ **面向对象**: 面向对象设计，易于使用
- ✅ **错误处理**: 完整的错误处理和验证
- ✅ **性能优化**: 最小化P/Invoke调用
- ✅ **文档完整**: 详细的XML注释和示例
- ✅ **扩展性**: 易于扩展和自定义

## 🔍 调试

### 日志记录

所有模块都包含详细的日志记录：
```csharp
// 启用调试日志
AmxModxManager.EnableDebugLogging = true;
```

### 性能监控

```csharp
// 获取性能统计
var stats = AmxModxManager.GetPerformanceStats();
```

## 📞 支持

如有问题或建议，请查看项目文档或提交Issue。

## 📝 许可证

本项目采用MIT许可证，详见LICENSE文件。