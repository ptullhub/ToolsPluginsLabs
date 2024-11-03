using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private List<Vector2Int> path = new List<Vector2Int>();
    public Vector2Int start = new Vector2Int(0, 0);
    public Vector2Int goal = new Vector2Int(0, 0);
    private Vector2Int next;
    private Vector2Int current;

    public int gridSize;
    private int width;
    private int height;
    [Range(0, 100)] public int obstacleProbability; 

    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    public int[,] grid = new int[,] { };

    private void OnValidate()
    {
        width = gridSize;
        height = gridSize;
    }
    private void Start()
    {
        grid = new int[height, width];

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                Vector2Int cell = new Vector2Int(y, x);
                grid[y, x] = AddObstacle(cell);
            }
        }

        FindPath(start, goal);
    }

    private void OnDrawGizmos()
    {
        if (grid == null || grid.GetLength(0) != height || grid.GetLength(1) != width)
        {
            grid = new int[height, width];
        }

        float cellSize = 1f;

        DrawRandomGrid(width, height);

        // Draw path
        foreach (var step in path)
        {
            Vector3 cellPosition = new Vector3(step.x * cellSize, 0, step.y * cellSize);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(cellPosition, new Vector3(cellSize, 0.1f, cellSize));
        }

        // Draw start and goal
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(start.x * cellSize, 0, start.y * cellSize), new Vector3(cellSize, 0.1f, cellSize));

        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(goal.x * cellSize, 0, goal.y * cellSize), new Vector3(cellSize, 0.1f, cellSize));
    }

    private void DrawRandomGrid(int width, int height)
    {
        float cellSize = 1f;

        // Draw grid cells
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, 0, y * cellSize);
                Gizmos.color = grid[y, x] == 1 ? Color.black : Color.white;
                Gizmos.DrawCube(cellPosition, new Vector3(cellSize, 0.1f, cellSize));
            }
        }
    }
    public int AddObstacle(Vector2Int position)
    {
        if (position == start || position == goal)
        {
            // Ensure the start and goal positions are always empty
            return 0;
        }

        int rand = Random.Range(0, 101);
        return rand < obstacleProbability ? 1 : 0;
    }
        

    private bool IsInBounds(Vector2Int point)
    {
        return point.x >= 0 && point.x < grid.GetLength(1) && point.y >= 0 && point.y < grid.GetLength(0);
    }

    private void FindPath(Vector2Int start, Vector2Int goal)
    {
        path.Clear();

        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        frontier.Enqueue(start);

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        cameFrom[start] = start;

        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            if (current == goal)
            {
                break;
            }

            foreach (Vector2Int direction in directions)
            {
                next = current + direction;

                if (IsInBounds(next) && grid[next.y, next.x] == 0 && !cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(goal))
        {
            Debug.Log("Path not found.");
            return;
        }

        // Trace path from goal to start
        Vector2Int step = goal;
        while (step != start)
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Add(start);
        path.Reverse();
    }
}
