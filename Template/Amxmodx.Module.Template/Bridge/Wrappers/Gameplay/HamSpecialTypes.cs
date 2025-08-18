using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// 射线追踪结果结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TraceResult
    {
        public int AllSolid;
        public int StartSolid;
        public int InOpen;
        public int InWater;
        public float Fraction;
        public Vector3 EndPos;
        public float PlaneNormalX;
        public float PlaneNormalY;
        public float PlaneNormalZ;
        public int PlaneDist;
        public int Hit;
        public int HitGroup;
        [MarshalAs(UnmanagedType.LPStr)]
        public string HitTexture;
        public int HitTextureHandle;
        public int SurfaceProps;
        public int Contents;
        public int Entity;
        public int HitBox;
    }

    /// <summary>
    /// 物品信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ItemInfo
    {
        public int Slot;
        public int Position;
        [MarshalAs(UnmanagedType.LPStr)]
        public string Ammo1;
        public int MaxAmmo1;
        [MarshalAs(UnmanagedType.LPStr)]
        public string Ammo2;
        public int MaxAmmo2;
        [MarshalAs(UnmanagedType.LPStr)]
        public string Name;
        public int MaxClip;
        public int Id;
        public int Flags;
        public int Weight;
    }

    /// <summary>
    /// 三维向量结构体
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

        public static Vector3 Zero => new Vector3(0, 0, 0);

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
        private readonly int _entity;

        public EntityVars(int entity)
        {
            _entity = entity;
        }

        public int GetInt(int offset) => NativeMethods.GetEntityVarInt(_entity, offset);
        public float GetFloat(int offset) => NativeMethods.GetEntityVarFloat(_entity, offset);
        public Vector3 GetVector(int offset)
        {
            var vec = new float[3];
            NativeMethods.GetEntityVarVector(_entity, offset, vec);
            return new Vector3(vec[0], vec[1], vec[2]);
        }

        public void SetInt(int offset, int value) => NativeMethods.SetEntityVarInt(_entity, offset, value);
        public void SetFloat(int offset, float value) => NativeMethods.SetEntityVarFloat(_entity, offset, value);
        public void SetVector(int offset, Vector3 value)
        {
            var vec = new[] { value.X, value.Y, value.Z };
            NativeMethods.SetEntityVarVector(_entity, offset, vec);
        }
    }
}