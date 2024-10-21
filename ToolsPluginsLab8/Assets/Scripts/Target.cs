using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target
{
    public float Size { get; private set; }
    public float Speed { get; private set; }
    public int PointValue { get; private set; }

    public Target(float size, float speed, int pointValue)
    {
        Size = size;
        Speed = speed;
        PointValue = pointValue;
    }    
}
