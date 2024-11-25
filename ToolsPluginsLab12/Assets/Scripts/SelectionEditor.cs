using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(WeatherManager))]
public class SelectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WeatherManager script = (WeatherManager)target;

        GUIContent arrayLabel = new GUIContent("City Name");
        script.arrayIdx = EditorGUILayout.Popup(arrayLabel, script.arrayIdx, script.cityName);

        if (GUILayout.Button("Update Weather"))
        {
            script.StartCoroutine(script.GetWeatherXML(script.OnXMLDataLoaded));
        }
    }
}
