// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#include "vector_bridge.h"
#include "amxmodx.h"

extern "C" void AmxModx_Bridge_VectorToAngle(float x, float y, float z, float* outX, float* outY, float* outZ)
{
    Vector vVector = Vector(x, y, z);
    Vector vAngle = Vector(0, 0, 0);
    
    VEC_TO_ANGLES(vVector, vAngle);
    
    *outX = vAngle.x;
    *outY = vAngle.y;
    *outZ = vAngle.z;
}

extern "C" void AmxModx_Bridge_AngleVector(float pitch, float yaw, float roll, int type, float* outX, float* outY, float* outZ)
{
    Vector v_angles, v_forward, v_right, v_up;
    
    v_angles.x = pitch;
    v_angles.y = yaw;
    v_angles.z = roll;
    
    g_engfuncs.pfnAngleVectors(v_angles, v_forward, v_right, v_up);
    
    Vector v_return;
    switch (type)
    {
    case 1: // FORWARD
        v_return = v_forward;
        break;
    case 2: // RIGHT
        v_return = v_right;
        break;
    case 3: // UP
        v_return = v_up;
        break;
    default:
        v_return = Vector(0, 0, 0);
        break;
    }
    
    *outX = v_return.x;
    *outY = v_return.y;
    *outZ = v_return.z;
}

extern "C" float AmxModx_Bridge_VectorLength(float x, float y, float z)
{
    Vector vVector = Vector(x, y, z);
    return vVector.Length();
}

extern "C" float AmxModx_Bridge_VectorDistance(float x1, float y1, float z1, float x2, float y2, float z2)
{
    Vector vVector1 = Vector(x1, y1, z1);
    Vector vVector2 = Vector(x2, y2, z2);
    return (vVector1 - vVector2).Length();
}

extern "C" bool AmxModx_Bridge_VelocityByAim(int entity, int velocity, float* outX, float* outY, float* outZ)
{
    Vector vVector = Vector(0, 0, 0);
    edict_t *pEnt = NULL;

    if (entity < 0 || entity > gpGlobals->maxEntities)
    {
        return false;
    }
    else
    {
        if (entity > 0 && entity <= gpGlobals->maxClients)
        {
            if (!GET_PLAYER_POINTER_I(entity)->ingame)
            {
                return false;
            }
            pEnt = GET_PLAYER_POINTER_I(entity)->pEdict;
        } else {
            pEnt = TypeConversion.id_to_edict(entity);
        }
    }

    if (!pEnt)
    {
        return false;
    }

    MAKE_VECTORS(pEnt->v.v_angle);
    vVector = gpGlobals->v_forward * velocity;

    *outX = vVector.x;
    *outY = vVector.y;
    *outZ = vVector.z;

    return true;
}