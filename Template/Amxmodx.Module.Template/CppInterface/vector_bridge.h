// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#ifndef _VECTOR_BRIDGE_H_
#define _VECTOR_BRIDGE_H_

#include "amxmodx.h"

#ifdef __cplusplus
extern "C" {
#endif

// 向量转角度
void AmxModx_Bridge_VectorToAngle(float x, float y, float z, float* outX, float* outY, float* outZ);

// 角度转向量
void AmxModx_Bridge_AngleVector(float pitch, float yaw, float roll, int type, float* outX, float* outY, float* outZ);

// 向量长度
float AmxModx_Bridge_VectorLength(float x, float y, float z);

// 向量距离
float AmxModx_Bridge_VectorDistance(float x1, float y1, float z1, float x2, float y2, float z2);

// 通过实体瞄准方向获取速度
bool AmxModx_Bridge_VelocityByAim(int entity, int velocity, float* outX, float* outY, float* outZ);

#ifdef __cplusplus
}
#endif

#endif // _VECTOR_BRIDGE_H_