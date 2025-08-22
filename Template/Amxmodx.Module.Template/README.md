# 僵尸等级系统 (Zombie Level System)

## 项目概述
这是一个基于GoldSrc引擎的僵尸等级系统，使用C#和AMXX插件开发。系统提供了完整的玩家等级、经验值、技能升级功能。

## 系统特性

### 等级系统
- 玩家通过击杀僵尸和人类获得经验值
- 支持自定义经验值计算公式
- 等级上限可配置

### 技能系统
- **嗜血一击**: 增加僵尸吸血能力
- **致命一击**: 增加僵尸攻击力
- **钢筋铁骨**: 增加人类血量和护甲
- **身轻如燕**: 降低重力
- **急速飞奔**: 增加移动速度
- **隐身之术**: 降低透明度
- **破防之爪**: 增加护甲穿透
- **多重跳跃**: 增加跳跃次数
- **抗武器击退**: 减少武器击退效果

### 经验商店
- 使用经验值购买技能等级
- 支持技能重置功能
- 实时价格计算

### 数据持久化
- 自动保存玩家数据
- 支持数据文件清理
- 玩家重新连接时自动恢复

## 文件结构

### C#核心文件
- `ZombieLevelSystem.cs`: 主要系统逻辑
- `ZombieLevelConfig.cs`: 系统配置
- `ZombieLevelNative.cs`: AMXX原生函数定义

### 桥接层
- `bridge/ZombieLevelBridge.cs`: C# P/Invoke接口
- `bridge/zombie_level_bridge.h`: C++头文件
- `bridge/zombie_level_bridge.cpp`: C++实现

### AMXX插件
- `AmxxScripts/zp_level_system.sma`: 主AMXX插件
- `AmxxScripts/ZP_SimpleLevel.sma`: 简化版本

## 配置说明

### ZombieLevelConfig.cs
包含所有可配置参数：
- 经验值计算公式
- 技能效果倍率
- 文件路径配置
- 菜单显示设置

## 使用方法

1. 编译C#项目生成DLL
2. 编译AMXX插件生成AMXX和AMX文件
3. 将文件放置到相应目录
4. 在游戏中使用菜单或命令操作

## 开发规范

- C# P/Invoke接口使用 `AmxModx.Bridge.<模块名>` 命名空间
- C++桥接文件命名为 `<模块名>_bridge.cpp/.h`
- 接口使用大驼峰命名法
- 所有接口添加XML注释

## 命令列表

- `zp_level_menu`: 打开等级菜单
- `zp_shop_menu`: 打开经验商店
- `zp_reset_skills`: 重置所有技能

## 事件系统

系统提供多种事件委托供扩展：
- 玩家升级事件
- 经验值变化事件
- 技能购买事件