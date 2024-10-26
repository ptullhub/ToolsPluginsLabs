using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public interface ISaveable
{
    void Save(int score);
    int Load();
}
