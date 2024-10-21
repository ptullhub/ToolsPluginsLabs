using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetBuilder
{
    void SetSize(float size);
    void SetSpeed(float speed);
    void SetPoints(int points);
    Target Build();
}
