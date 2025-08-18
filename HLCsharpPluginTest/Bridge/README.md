# AmxModx Bridge 模板文档

## 目录结构

```
Bridge/
├── bridge/                 # 核心桥接层
│   ├── AmxModx.Bridge.Config.cs     # 配置文件处理
│   ├── AmxModx.Bridge.Core.cs       # 核心功能
│   └── ...
├── modules/                # 模块特定桥接
│   └── template/           # 模板模块示例
│       └── AmxModx.Bridge.Template.cs
```

## 使用规范

### 1. 命名规范
- **命名空间**: `AmxModx.Bridge.<模块名>`
- **文件命名**: `AmxModx.Bridge.<模块名>.cs`
- **DLL名称**: `<模块名>_amxx.dll`

### 2. 文件结构
```csharp
using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.<模块名>
{
    /// <summary>
    /// 模块桥接接口
    /// </summary>
    public static class <模块名>Bridge
    {
        private const string <模块名>BridgeDll = "<模块名>_amxx";
        
        [DllImport(<模块名>BridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int <函数名>(...);
        
        // 安全封装方法
        public static string <函数名>Safe(...)
        {
            // 安全实现
        }
    }
}
```

### 3. 创建新模块

1. 在 `modules/` 目录下创建新模块文件夹
2. 创建 `AmxModx.Bridge.<模块名>.cs` 文件
3. 遵循上述命名规范
4. 确保DLL名称与实际编译的dll文件名匹配

### 4. 示例

参考 `modules/template/AmxModx.Bridge.Template.cs` 文件作为模板。