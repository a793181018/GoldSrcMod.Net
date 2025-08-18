using System;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// Ham Sandwich桥接使用示例
    /// </summary>
    public static class UsageExample
    {
        /// <summary>
        /// 基础使用示例
        /// </summary>
        public static void BasicUsageExample()
        {
            // 初始化桥接
            if (!HamSandwichManager.Initialize())
            {
                Console.WriteLine("Failed to initialize Ham Sandwich bridge");
                return;
            }

            try
            {
                // 注册基础钩子
                var hookId = HamSandwichManager.RegisterHook(
                    HamHookType.Spawn, 
                    "weapon_ak47", 
                    OnWeaponSpawnPre, 
                    OnWeaponSpawnPost);

                if (hookId > 0)
                {
                    Console.WriteLine($"Successfully registered hook with ID: {hookId}");
                }

                // 检查游戏支持
                if (HamSandwichManager.IsGameSupported("cstrike"))
                {
                    // 注册CS特定钩子
                    var csHookId = HamSandwichManager.RegisterHook(
                        HamHookType.CsItemGetMaxSpeed,
                        "weapon_ak47",
                        OnCsItemGetMaxSpeedPre,
                        OnCsItemGetMaxSpeedPost);
                }
            }
            finally
            {
                // 清理资源
                HamSandwichManager.Cleanup();
            }
        }

        /// <summary>
        /// 高级使用示例 - 实体数据操作
        /// </summary>
        public static void AdvancedUsageExample()
        {
            // 初始化桥接
            if (!HamSandwichManager.Initialize())
            {
                return;
            }

            try
            {
                // 注册玩家跳跃钩子
                var jumpHookId = HamSandwichManager.RegisterHook(
                    HamHookType.PlayerJump,
                    "player",
                    OnPlayerJumpPre,
                    OnPlayerJumpPost);

                // 注册武器主攻击钩子
                var attackHookId = HamSandwichManager.RegisterHook(
                    HamHookType.WeaponPrimaryAttack,
                    "weapon_ak47",
                    OnWeaponPrimaryAttackPre,
                    OnWeaponPrimaryAttackPost);
            }
            finally
            {
                HamSandwichManager.Cleanup();
            }
        }

        /// <summary>
        /// 实体数据操作示例
        /// </summary>
        public static void EntityDataExample(int entity)
        {
            if (!HamSandwichManager.IsValidEntity(entity))
            {
                return;
            }

            // 获取实体类名
            var className = HamSandwichManager.GetEntityClassname(entity);
            Console.WriteLine($"Entity class: {className}");

            // 获取实体位置
            var origin = HamSandwichManager.GetEntityOrigin(entity);
            Console.WriteLine($"Entity position: {origin}");

            // 设置私有数据
            HamSandwichManager.SetPrivateDataInt(entity, "m_iClip", 30);
            var clip = HamSandwichManager.GetPrivateDataInt(entity, "m_iClip");
            Console.WriteLine($"Current clip: {clip}");
        }

        /// <summary>
        /// 向量操作示例
        /// </summary>
        public static void VectorExample()
        {
            var vec1 = new Vector3(1.0f, 2.0f, 3.0f);
            var vec2 = new Vector3(4.0f, 5.0f, 6.0f);

            // 向量加法
            var result = vec1 + vec2;
            Console.WriteLine($"Vector addition: {result}");

            // 向量标准化
            var normalized = Vector3.Normalize(vec1);
            Console.WriteLine($"Normalized vector: {normalized}");

            // 计算距离
            var distance = Vector3.Distance(vec1, vec2);
            Console.WriteLine($"Distance: {distance}");
        }

        /// <summary>
        /// 游戏特定功能示例
        /// </summary>
        public static void GameSpecificExample()
        {
            var currentGame = HamSandwichManager.GetCurrentGame();
            Console.WriteLine($"Current game: {currentGame}");

            switch (currentGame.ToLower())
            {
                case "cstrike":
                    RegisterCsSpecificHooks();
                    break;
                case "tfc":
                    RegisterTfcSpecificHooks();
                    break;
                case "dod":
                    RegisterDodSpecificHooks();
                    break;
            }
        }

        #region 回调函数示例

        private static int OnWeaponSpawnPre(int[] parameters)
        {
            Console.WriteLine("Weapon spawn pre-hook");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnWeaponSpawnPost(int[] parameters)
        {
            Console.WriteLine("Weapon spawn post-hook");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnCsItemGetMaxSpeedPre(int[] parameters)
        {
            var entity = parameters[0];
            Console.WriteLine($"CS Item GetMaxSpeed pre-hook for entity {entity}");
            
            // 修改返回值
            HamSandwichManager.SetReturnValueFloat(250.0f);
            return (int)HamReturnStatus.Override;
        }

        private static int OnCsItemGetMaxSpeedPost(int[] parameters)
        {
            Console.WriteLine("CS Item GetMaxSpeed post-hook");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnPlayerJumpPre(int[] parameters)
        {
            var player = parameters[0];
            Console.WriteLine($"Player {player} jumping");
            
            // 检查玩家速度
            var velocity = HamSandwichManager.GetPrivateDataVector(player, "m_vecVelocity");
            if (velocity.Length > 300.0f)
            {
                Console.WriteLine("Player moving too fast, preventing jump");
                return (int)HamReturnStatus.Supercede;
            }
            
            return (int)HamReturnStatus.Continue;
        }

        private static int OnPlayerJumpPost(int[] parameters)
        {
            var player = parameters[0];
            Console.WriteLine($"Player {player} jumped successfully");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnWeaponPrimaryAttackPre(int[] parameters)
        {
            var weapon = parameters[0];
            var owner = parameters[1];
            
            Console.WriteLine($"Weapon {weapon} primary attack by owner {owner}");
            
            // 检查弹药
            var clip = HamSandwichManager.GetPrivateDataInt(weapon, "m_iClip");
            if (clip <= 0)
            {
                Console.WriteLine("No ammo, preventing attack");
                return (int)HamReturnStatus.Supercede;
            }
            
            return (int)HamReturnStatus.Continue;
        }

        private static int OnWeaponPrimaryAttackPost(int[] parameters)
        {
            var weapon = parameters[0];
            Console.WriteLine($"Weapon {weapon} attack completed");
            return (int)HamReturnStatus.Continue;
        }

        #endregion

        #region 游戏特定钩子注册

        private static void RegisterCsSpecificHooks()
        {
            // 注册CS特定钩子
            HamSandwichManager.RegisterHook(
                HamHookType.CsPlayerOnTouchingWeapon,
                "player",
                OnCsPlayerTouchingWeaponPre,
                OnCsPlayerTouchingWeaponPost);
        }

        private static void RegisterTfcSpecificHooks()
        {
            // 注册TFC特定钩子
            HamSandwichManager.RegisterHook(
                HamHookType.TfcEngineerUse,
                "player",
                OnTfcEngineerUsePre,
                OnTfcEngineerUsePost);
        }

        private static void RegisterDodSpecificHooks()
        {
            // 注册DOD特定钩子
            HamSandwichManager.RegisterHook(
                HamHookType.DodWeaponSpecial,
                "weapon_ak47",
                OnDodWeaponSpecialPre,
                OnDodWeaponSpecialPost);
        }

        #endregion

        #region 游戏特定回调

        private static int OnCsPlayerTouchingWeaponPre(int[] parameters)
        {
            var player = parameters[0];
            var weapon = parameters[1];
            Console.WriteLine($"CS Player {player} touching weapon {weapon}");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnCsPlayerTouchingWeaponPost(int[] parameters)
        {
            Console.WriteLine("CS Player touching weapon post-hook");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnTfcEngineerUsePre(int[] parameters)
        {
            var player = parameters[0];
            Console.WriteLine($"TFC Engineer {player} using");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnTfcEngineerUsePost(int[] parameters)
        {
            Console.WriteLine("TFC Engineer use post-hook");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnDodWeaponSpecialPre(int[] parameters)
        {
            var weapon = parameters[0];
            Console.WriteLine($"DOD Weapon {weapon} special action");
            return (int)HamReturnStatus.Continue;
        }

        private static int OnDodWeaponSpecialPost(int[] parameters)
        {
            Console.WriteLine("DOD Weapon special post-hook");
            return (int)HamReturnStatus.Continue;
        }

        #endregion
    }
}