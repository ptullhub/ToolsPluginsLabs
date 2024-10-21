using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRegistry
{
    private ITargetBuilder _builder;

    public TargetRegistry(ITargetBuilder builder)
    {
        _builder = builder;
    }

    public Target BuildSmallTarget()
    {
        _builder.SetSize(0.5f);
        _builder.SetSpeed(5f);
        _builder.SetPoints(5);
        return _builder.Build();
    }
    
    public Target BuildMediumTarget()
    {
        _builder.SetSize(1f);
        _builder.SetSpeed(2.5f);
        _builder.SetPoints(10);
        return _builder.Build();
    }

    public Target BuildLargeTarget() 
    {
        _builder.SetSize(2f);
        _builder.SetSpeed(1f);
        _builder.SetPoints(15);
        return _builder.Build();
    }
}
