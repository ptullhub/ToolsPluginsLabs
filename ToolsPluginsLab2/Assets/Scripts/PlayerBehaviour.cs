using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float attackPt;
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerBehaviour)), CanEditMultipleObjects]
public class PlayerBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {

        // Select/deselect code
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all players"))
            {
                var allEnemyBehaviour = GameObject.FindObjectsOfType<PlayerBehaviour>();
                var allEnemyGameObjects = allEnemyBehaviour.Select(enemy => enemy.gameObject).ToArray();
                Selection.objects = allEnemyGameObjects;
            }
            if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[]
                {
                (target as PlayerBehaviour).gameObject
                };
            }
        }

        // Disable/enable all enemies code
        bool allEnemiesDisabled = AreAllPlayersDisabled();
        GUI.backgroundColor = allEnemiesDisabled ? Color.red : Color.green;

        if (GUILayout.Button(allEnemiesDisabled ? "Enable all players" : "Disable all players", GUILayout.Height(40)))
        {
            foreach (var enemy in GameObject.FindObjectsOfType<PlayerBehaviour>(true))
            {
                Undo.RecordObject(enemy.gameObject, allEnemiesDisabled ? "Enable all players" : "Disable all players");
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
    private bool AreAllPlayersDisabled()
    {
        var allEnemies = GameObject.FindObjectsOfType<PlayerBehaviour>(true);
        return allEnemies.All(enemy => !enemy.gameObject.activeSelf);
    }

}
#endif
