using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Serializable 
{
    public float x, y, z;

    public Vector3Serializable(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Serializable(Vector3 origin)
    {
        this.x = origin.x;
        this.y = origin.y;
        this.z = origin.z;
    }

    public Vector3Serializable()
    {
    }


}
