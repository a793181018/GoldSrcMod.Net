using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// 射线检测结果结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TraceResult
    {
        public float[] AllSolid;
        public float[] StartSolid;
        public float[] InOpen;
        public float[] InWater;
        public float[] Fraction;
        public float[] EndPos;
        public float[] PlaneNormal;
        public float PlaneDist;
        public int Hit;
        public int HitGroup;
        public int PhysicsBone;
        public float[] Surface;
        public float[] Velocity;
        public int Ent;
        public float HitBox;
        public float[] StartPos;
    }

    /// <summary>
    /// 物品信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ItemInfo
    {
        public IntPtr Name;
        public int MaxClip;
        public int MaxAmmo1;
        public int MaxAmmo2;
        public int AmmoType1;
        public int AmmoType2;
        public int Slot;
        public int Position;
        public int Id;
        public int Flags;
        public int Weight;
    }

    /// <summary>
    /// 向量3D结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    /// <summary>
    /// 实体变量访问器
    /// </summary>
    public class EntityVars
    {
        private readonly int _entityId;

        public EntityVars(int entityId)
        {
            _entityId = entityId;
        }

        public int GetInt(int offset)
        {
            return NativeMethods.GetEntityVarInt(_entityId, offset);
        }

        public float GetFloat(int offset)
        {
            return NativeMethods.GetEntityVarFloat(_entityId, offset);
        }

        public Vector3 GetVector(int offset)
        {
            float[] vec = new float[3];
            NativeMethods.GetEntityVarVector(_entityId, offset, vec);
            return new Vector3(vec[0], vec[1], vec[2]);
        }

        public void SetInt(int offset, int value)
        {
            NativeMethods.SetEntityVarInt(_entityId, offset, value);
        }

        public void SetFloat(int offset, float value)
        {
            NativeMethods.SetEntityVarFloat(_entityId, offset, value);
        }

        public void SetVector(int offset, Vector3 value)
        {
            float[] vec = { value.X, value.Y, value.Z };
            NativeMethods.SetEntityVarVector(_entityId, offset, vec);
        }
    }

    /// <summary>
    /// 游戏特定钩子类型枚举
    /// </summary>
    public enum GameSpecificHookType
    {
        // Counter-Strike
        CsPlayerOnTouchingWeapon = 1000,
        CsItemGetMaxSpeed,
        CsItemCanDrop,
        CsRestart,
        CsRoundRespawn,

        // Team Fortress Classic
        TfcEngineerUse,
        TfcEmpExplode,
        TfcTakeEmpBlast,
        TfcRadiusDamage2,

        // Day of Defeat
        DodWeaponSpecial,
        DodRoundRespawn,
        DodItemCanDrop,

        // The Specialists
        TsBreakableRespawn,
        TsShouldCollide,
        TsCanUsedThroughWalls,

        // Natural Selection
        NsUpdateOnRemove,
        NsGetPointValue,
        NsAwardKill,

        // Earth's Special Forces
        EsfWeaponHolsterWhenMeleed,
        EsfTakeDamage2
    }
}