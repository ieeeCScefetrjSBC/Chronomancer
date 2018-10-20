using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class PathFinding : MonoBehaviour {

    static public PathFinding instance;

    public bool displayGizmos;
    public float nodeSize;

    public LayerMask unwalkableLayer;

    //Important
    Vector2 worldSize;
    int width;
    int height;
    Node[,] grid;

    //Utilities
    Vector2Int[,] nodeParents;
    [HideInInspector] public List<Vector3> path = new List<Vector3>();
    
    class Node
    {
        public Node(Vector2 _worldPosition, bool _walkable)
        {
            worldPosition = _worldPosition;
            walkable = _walkable;
        }

        public Vector2 worldPosition;
        public bool walkable;
    }

    sbyte[] dx = { 0, 0, 1, -1, 1, -1, -1, 1 };
    sbyte[] dy = { 1, -1, 0, 0, -1, 1, -1, 1 };


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    
    public void InitMap(Vector2 v)
    {
        worldSize = v;
        width = Mathf.RoundToInt(worldSize.x / nodeSize);
        height = Mathf.RoundToInt(worldSize.y / nodeSize);

        grid = new Node[width, height];
        nodeParents = new Vector2Int[width, height];

        ScanArea();
    }

    void ScanArea()
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                Vector3 worldPoint = new Vector3(x * nodeSize + nodeSize / 2, y * nodeSize + nodeSize / 2) + transform.position;
                bool walkable = !Physics2D.OverlapCircle(worldPoint, (nodeSize / 2), unwalkableLayer);
                grid[x, y] = new Node(worldPoint, walkable);
            }
    }

    Node WorldToNode(Vector3 worldPoint)
    {
        Vector2 pos = worldPoint - transform.position;// - transform.position;
        pos.x -= nodeSize / 2;
        pos.y -= nodeSize / 2;
        pos.x /= worldSize.x;
        pos.y /= worldSize.y;

        int x = Mathf.RoundToInt((width) * pos.x);
        int y = Mathf.RoundToInt((height) * pos.y);

        x = Mathf.Clamp(x, 0, width - 1);
        y = Mathf.Clamp(y, 0, height - 1);

        return grid[x, y];
    }

    Vector2Int WorldToGrid(Vector3 worldPoint)
    {
        Vector2 pos = worldPoint - transform.position;// - transform.position;
        pos.x -= nodeSize / 2;
        pos.y -= nodeSize / 2;
        pos.x /= worldSize.x;
        pos.y /= worldSize.y;

        int x = Mathf.RoundToInt((width) * pos.x);
        int y = Mathf.RoundToInt((height) * pos.y);

        x = Mathf.Clamp(x, 0, width - 1);
        y = Mathf.Clamp(y, 0, height - 1);

        return new Vector2Int(x, y);
    }



    public bool FindPath(Vector3 s, Vector3 g)
    {
        Vector2Int start = WorldToGrid(s);
        Vector2Int goal = WorldToGrid(g);


        if (!InBounds(start) || !InBounds(goal) || WorldToGrid(s) == WorldToGrid(g)) return false;

        float[,] heuristicScore = new float[width, height];
        float[,] distanceFromStart = new float[width, height];
        byte[,] nodeDirections = new byte[width, height];

        bool success = false;

        for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) distanceFromStart[x, y] = int.MaxValue;


        heuristicScore[start.x, start.y] = HeuristicEuclidian(start, goal);
        distanceFromStart[start.x, start.y] = 0;

        SimplePriorityQueue<Vector2Int, float> priorityQueue = new SimplePriorityQueue<Vector2Int, float>();
        priorityQueue.Enqueue(start, heuristicScore[start.x, start.y]);

        while (priorityQueue.Count > 0)
        {

            Vector2Int currentNode = priorityQueue.Dequeue();

            if (currentNode == goal)
            {
                path.Clear();

                byte oldDir = 99;
                while (currentNode != start)
                {
                    byte newDir = nodeDirections[currentNode.x, currentNode.y];
                    if (oldDir != newDir)
                    {
                        path.Add(grid[currentNode.x, currentNode.y].worldPosition);
                        oldDir = newDir;
                    }
                    currentNode = nodeParents[currentNode.x, currentNode.y];
                }
                success = true;
                path.Reverse();
                break;
            }

            for (byte d = 0; d < 8; d++)
            {
                Vector2Int node = currentNode;
                node.x += dx[d];
                node.y += dy[d];

                if (!InBounds(node) || !grid[node.x, node.y].walkable) continue;

                float score = distanceFromStart[currentNode.x, currentNode.y] + (d < 4 ? 1.0f : 1.4f); // + weight

                if (score < distanceFromStart[node.x, node.y])
                {
                    nodeParents[node.x, node.y] = currentNode;
                    nodeDirections[node.x, node.y] = d;
                    distanceFromStart[node.x, node.y] = score;
                    heuristicScore[node.x, node.y] = score + HeuristicEuclidian(node, goal);

                    if (!priorityQueue.Contains(node))
                    {
                        priorityQueue.Enqueue(node, heuristicScore[node.x, node.y]);
                    }

                }
            }


        }

        return success;
    }

    bool InBounds(Vector2Int node)
    {
        return !(node.x < 0 || node.y < 0 || node.x >= width || node.y >= height);
    }

    private float HeuristicEuclidian(Vector2Int node, Vector2Int goal)
    {
        return Vector2Int.Distance(node, goal);
    }


    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject != gameObject) return;

        if (!displayGizmos) return;
        

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable ? Color.white : Color.red);
                if (n.walkable) Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeSize * .9f));
            }
        }

    }
}
