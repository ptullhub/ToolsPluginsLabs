using UnityEngine;

[ExecuteInEditMode]
public class DrawLine : MonoBehaviour
{
    
    public GameObject[] GameObjects;

#if UNITY_EDITOR

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < GameObjects.Length; ++i)
        {
            Gizmos.DrawLine(transform.position, GameObjects[i].transform.position);
        }
        
    }


#endif
}