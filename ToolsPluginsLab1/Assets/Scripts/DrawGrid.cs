using UnityEngine;

public class DrawGridUsingLoops : MonoBehaviour
{
    public int gridSize = 8;  
    public float cellSize = 1f;  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Draw horizontal lines
        for (int y = 0; y <= gridSize; y++)
        {
            Vector3 startPos = new Vector3(0, y * cellSize, 0);
            Vector3 endPos = new Vector3(gridSize * cellSize, y * cellSize, 0);
            Gizmos.DrawLine(startPos, endPos);
        }

        // Draw vertical lines
        for (int x = 0; x <= gridSize; x++)
        {
            Vector3 startPos = new Vector3(x * cellSize, 0, 0);
            Vector3 endPos = new Vector3(x * cellSize, gridSize * cellSize, 0);
            Gizmos.DrawLine(startPos, endPos);
        }
    }
}
