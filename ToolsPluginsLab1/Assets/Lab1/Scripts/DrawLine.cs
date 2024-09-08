using UnityEngine;

[ExecuteInEditMode]
public class DrawLine : MonoBehaviour
{
    public GameObject[] GameObjects;
    public string color = "green";

#if UNITY_EDITOR

    void OnDrawGizmosSelected()
    {
        switch (color)
        {
            case "green":
                Gizmos.color = Color.green;
                break;
            case "white":
                Gizmos.color = Color.white;
                break;
            case "red":
                Gizmos.color = Color.red;
                break;
            case "blue":
                Gizmos.color = Color.blue;
                break;
            case "black":
                Gizmos.color = Color.black;
                break;
            case "yellow":
                Gizmos.color = Color.yellow;
                break;
            default:
                Gizmos.color = Color.green;
                break;
        }
        

        // Check if GameObjects array is not null
        if (GameObjects != null && GameObjects.Length > 0)
        {
            for (int i = 0; i < GameObjects.Length; ++i)
            {
                // Ensure each GameObject in the array is not null
                if (GameObjects[i] != null)
                {
                    Gizmos.DrawLine(transform.position, GameObjects[i].transform.position);
                }
            }
        }
    }

#endif
}
