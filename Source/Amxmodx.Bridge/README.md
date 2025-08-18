# Amxmodx.Bridge

这是一个用于连接 GoldSrcMod.Net 与 AMX Mod X 的桥接层项目，提供了从 C# 到 AMX Mod X 原生功能的完整访问接口。

## 项目结构

### bridge 目录
包含基础的 P/Invoke 接口定义，直接映射到 AMX Mod X 的原生函数：

- **AmxModx.Bridge.CVar.cs** - 控制台变量管理接口
- **AmxModx.Bridge.Config.cs** - 配置文件处理接口
- **AmxModx.Bridge.DataPack.cs** - 数据打包/解包接口
- **AmxModx.Bridge.Debugger.cs** - 调试功能接口
- **AmxModx.Bridge.File.cs** - 文件系统接口
- **AmxModx.Bridge.Vault.cs** - 数据存储接口

### csharp 目录
包含高级封装和管理类，提供更友好的 C# 编程接口：

- **AmxModx.Bridge.Core.cs** - 核心功能封装
- **AmxModx.Bridge.Engine.cs** - 引擎接口封装
- **AmxModx.Bridge.Entity.cs** - 实体管理封装
- **AmxModx.Bridge.Messages.cs** - 消息系统封装
- **AmxModx.Bridge.Natives.cs** - 原生函数封装
- **AmxModx.Bridge.Event.cs** - 事件系统封装
- **AmxModx.Bridge.CommandSystem.cs** - 命令系统封装
- **AmxModx.Bridge.PlayerManager.cs** - 玩家管理封装
- **AmxModx.Bridge.Forward.Test.cs** - Forward系统测试
- **AmxModx.Bridge.GameConfigs.cs** - 游戏配置管理
- **AmxModx.Bridge.NewMenus.cs** - 新菜单系统
- **EventManager.cs** - 事件管理器

## 使用说明

### 基础用法
```csharp
using AmxModx.Bridge;

// 初始化桥接层
Core.Initialize();

// 使用控制台变量
var cvar = CVar.Find("mp_timelimit");
if (cvar != null)
{
    Console.WriteLine($"Time limit: {cvar.GetFloat()}");
}

// 注册命令
CommandSystem.Register("my_command", (player, args) => {
    player.PrintToChat("Hello from C#!");
    return true;
});
```

### 事件系统
```csharp
using AmxModx.Bridge;

// 注册事件处理器
EventManager.RegisterEvent("player_spawn", (EventArgs args) => {
    var player = args.GetPlayer("userid");
    player.PrintToChat("Welcome back!");
});
```

## 编译要求

- .NET 8.0 或更高版本
- 允许不安全代码块 (unsafe blocks)
- Windows 平台 (依赖 Windows 特定的 P/Invoke)

## 构建项目

```bash
dotnet build
```

## 注意事项

1. 所有接口都包含完整的 XML 文档注释
2. P/Invoke 接口与 C++ 桥接层保持命名一致性
3. 高级封装类使用委托模式处理事件回调
4. 项目使用 UTF-8 with BOM 编码格式

## 依赖关系

- 需要 AMX Mod X 桥接 DLL
- 需要 GoldSrc 引擎支持
- 需要对应的 C++ 桥接层实现