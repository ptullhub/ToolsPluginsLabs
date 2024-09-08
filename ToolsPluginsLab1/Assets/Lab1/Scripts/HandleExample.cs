using UnityEngine;
using UnityEditor;
public class HandleExample : MonoBehaviour
{
    public float value = 1.0f;
}

// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(HandleExample))]
public class ExampleEditor : Editor
{
   
    public void OnSceneGUI()
    {
        var t = target as HandleExample;
        var tr = t.transform;
        var pos = tr.position;
        // Changed the disk color
        var color = new Color(1, 0.5f, 0.9f, 1);
        Handles.color = color;
        Handles.DrawWireDisc(pos, tr.forward, t.value);
        // Made the disk size scale with float value
        GUI.color = color;
        Handles.Label(pos, t.value.ToString("F1"));
    }
}