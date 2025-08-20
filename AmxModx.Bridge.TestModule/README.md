# AMX Mod X 桥接接口测试套件

## 概述

本测试套件提供了一套完整的桥接接口验证机制，通过并行运行Pawn插件和C#模块的测试，对比结果来验证cpp桥接接口的正确性。

## 项目结构

```
GoldSrcMod.Net/AmxModx.Bridge.TestModule/
├── Tests/
│   ├── BridgeInterfaceTests.cs    # C#接口测试
│   ├── TestPlugin.cs              # 测试插件管理
│   └── ComparisonAnalyzer.cs      # 结果对比分析
├── Bridge/
│   ├── PInvoke/                   # P/Invoke接口定义
│   └── Wrappers/                  # 包装器实现
├── Module.cs                      # 模块入口
└── README.md                      # 本文档

plugins/testsuite/
├── bridge_test_pawn.sma           # Pawn测试插件
└── compile_pawn.bat              # Pawn编译脚本
```

## 使用方法

### 1. 编译Pawn插件

在 `plugins/testsuite/` 目录下运行：
```bash
compile_pawn.bat
```

### 2. 编译C#模块

在 `GoldSrcMod.Net/AmxModx.Bridge.TestModule/` 目录下运行：
```bash
dotnet build
```

### 3. 运行测试

#### Pawn插件测试
在游戏控制台或RCON中输入：
```
bridge_pawn_test          # 基础测试
bridge_pawn_test_all      # 全量测试
bridge_pawn_test_system   # 系统接口测试
bridge_pawn_test_data     # 数据接口测试
bridge_pawn_test_engine   # 引擎接口测试
bridge_pawn_test_game     # 游戏接口测试
```

#### C#模块测试
在游戏控制台或RCON中输入：
```
bridge_test               # 基础测试
bridge_test_all           # 全量测试
bridge_test_system        # 系统接口测试
bridge_test_data          # 数据接口测试
bridge_test_engine        # 引擎接口测试
bridge_test_game          # 游戏接口测试
bridge_analyze            # 对比分析测试结果
bridge_compare            # 显示对比结果
```

### 4. 结果文件

测试完成后，结果文件将保存在：
- `cstrike/addons/amxmodx/logs/bridge_test_pawn_results.txt` - Pawn测试结果
- `cstrike/addons/amxmodx/logs/bridge_test_csharp_results.txt` - C#测试结果
- `cstrike/addons/amxmodx/logs/bridge_test_analysis.txt` - 对比分析结果

## 测试覆盖范围

### 系统接口测试
- 日志系统
- 命令注册
- 调试器接口
- 服务器信息获取
- 游戏时间获取
- 地图名称获取

### 数据接口测试
- CVar系统
- 配置管理
- 文件操作
- 数据打包

### 引擎接口测试
- 引擎函数
- 实体系统
- 向量运算
- 模型预缓存
- 客户端消息

### 游戏特定接口测试
- Counter-Strike接口
- FakeMeta接口
- HamSandwich接口
- 事件转发

## 对比验证机制

### 工作原理
1. **并行测试**：Pawn插件和C#模块同时运行相同的测试逻辑
2. **统一格式**：两个测试使用相同的输出格式
3. **自动对比**：分析工具自动比较两个结果文件
4. **差异报告**：生成详细的对比分析报告

### 验证标准
- 测试名称一致性
- 结果状态一致性（PASS/FAIL）
- 详细信息匹配度
- 测试覆盖率

## 故障排除

### 常见问题

1. **Pawn插件编译失败**
   - 检查AMX Mod X安装路径
   - 确认所有依赖的include文件存在
   - 检查编译器路径配置

2. **C#模块编译失败**
   - 确认.NET 9.0 SDK已安装
   - 检查项目引用
   - 验证Amxmodx.Net包版本

3. **测试结果不一致**
   - 检查cpp桥接接口实现
   - 验证P/Invoke定义
   - 检查参数传递和返回值处理

### 调试技巧

1. **查看详细日志**
   - 检查 `addons/amxmodx/logs/` 目录下的日志文件
   - 使用 `bridge_compare` 命令查看对比结果

2. **逐步验证**
   - 先运行单个接口测试
   - 逐步增加测试复杂度
   - 对比每个步骤的结果

## 扩展指南

### 添加新测试

1. **Pawn插件**
   - 在 `bridge_test_pawn.sma` 中添加新的测试函数
   - 使用 `LogTestResult` 记录结果
   - 注册新的测试命令

2. **C#模块**
   - 在 `BridgeInterfaceTests.cs` 中添加对应的测试方法
   - 使用 `LogTest` 记录结果
   - 确保测试名称与Pawn版本一致

### 示例：添加新测试

**Pawn版本：**
```pawn
// 在CmdPawnTestSystem中添加
new testValue = get_cvar_num("test_cvar")
LogTestResult("GetCVarNumber", testValue != 0 ? "PASS" : "FAIL", "获取数值CVar")
```

**C#版本：**
```csharp
// 在TestSystemInterfaces中添加
int testValue = 1337; // 模拟获取CVar数值
LogTest("GetCVarNumber", true, testValue.ToString());
```

## 最佳实践

1. **测试命名**：确保Pawn和C#版本使用相同的测试名称
2. **结果格式**：保持输出格式完全一致
3. **错误处理**：添加适当的异常处理和错误报告
4. **日志记录**：详细记录每个测试步骤
5. **定期验证**：每次修改桥接代码后重新运行测试

## 技术支持

如果遇到问题，请：
1. 检查所有日志文件
2. 验证测试用例同步
3. 检查cpp桥接实现
4. 确认P/Invoke定义正确性