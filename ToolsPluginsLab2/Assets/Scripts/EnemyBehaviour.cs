using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float attackPt;
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyBehaviour)), CanEditMultipleObjects]
public class EnemyBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        // Select/deselect code
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all enemies"))
            {
                var allEnemyBehaviour = GameObject.FindObjectsOfType<EnemyBehaviour>();
                var allEnemyGameObjects = allEnemyBehaviour.Select(enemy => enemy.gameObject).ToArray();
                Selection.objects = allEnemyGameObjects;
            }
            if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[]
                {
                (target as EnemyBehaviour).gameObject
                };
            }
        }

        // Disable/enable all enemies code
        bool allEnemiesDisabled = AreAllEnemiesDisabled(); 
        GUI.backgroundColor = allEnemiesDisabled ? Color.red : Color.green;

        if (GUILayout.Button(allEnemiesDisabled ? "Enable all enemies" : "Disable all enemies", GUILayout.Height(40)))
        {
            foreach (var enemy in GameObject.FindObjectsOfType<EnemyBehaviour>(true))
            {
                Undo.RecordObject(enemy.gameObject, allEnemiesDisabled ? "Enable all enemies" : "Disable all enemies");
                enemy.gameObject.SetActive(allEnemiesDisabled);
            }
        }
        GUI.backgroundColor = Color.white;

        // Health/attack properties
        serializedObject.Update();
        var health = serializedObject.FindProperty("health");
        var attackPt = serializedObject.FindProperty("attackPt");
        EditorGUILayout.PropertyField(health);
        EditorGUILayout.PropertyField(attackPt);
        serializedObject.ApplyModifiedProperties();

        if (health.floatValue < 0)
        {
            EditorGUILayout.HelpBox("Health value cannot be less than 0!", MessageType.Warning);
        }
        if (attackPt.floatValue > 10)
        {
            EditorGUILayout.HelpBox("Attack value greater than 10 may be too strong", MessageType.Info);
        }
    }
    private bool AreAllEnemiesDisabled()
    {
        var allEnemies = GameObject.FindObjectsOfType<EnemyBehaviour>(true);
        return allEnemies.All(enemy => !enemy.gameObject.activeSelf);
    }

}
#endif
