using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGrid : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    Vector3[] points;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLineList(points);
    }
#endif
}
