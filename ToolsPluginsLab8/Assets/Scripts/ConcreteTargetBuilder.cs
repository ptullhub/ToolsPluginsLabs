using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteTargetBuilder : ITargetBuilder
{
    private float _size;
    private float _speed;
    private int _pointValue;

    public void SetSize(float size)
    {
        _size = size;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetPoints(int pointValue)
    {
        _pointValue = pointValue;
    }

    public Target Build()
    {
        return new Target(_size, _speed, _pointValue);
    }
}
