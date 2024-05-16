using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AStarDebugger))]
public class AStar : MonoBehaviour {
    public bool Debug = false;
    public bool FullDebug = false;

    private AStarDebugger debugger;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Dictionary<Vector3Int, AStarNode> nodes = new ();
    private HashSet<AStarNode> queue = new ();
    private HashSet<AStarNode> done = new ();

    private int steps = 100;

    public void Start() {
        debugger = GetComponent<AStarDebugger>();
    }

    public Stack<Vector3> Find(Vector2 from, Vector2 to, int stepsLimit) {
        queue.Clear();
        done.Clear();
        nodes.Clear();
        steps = stepsLimit;

        var goal = grid.WorldToCell(to);
        var start = AStarNode.CreateStartNode(grid.WorldToCell(from));

        if (goal.Equals(start.Position)) {
            return new(new Vector3[] { to });
        }

        nodes.Add(start.Position, start);

        if (Debug) {
            debugger.Clear();
            debugger.CreateStartAndGoal(start.Position, goal);
        }
        
        CalculateAround(start, goal);
            
        AStarNode closest;

        if (queue.Count == 0) {
            return new Stack<Vector3>();
        }

        do {
            closest = findClosest();
            queue.Remove(closest);
            done.Add(closest);

            if (FullDebug) {
                debugger.MarkClosest(closest);
            }

            CalculateAround(closest, goal);
            steps -= 1;

            if (steps <= 0) {
                break;
            }
        }
        while(!closest.Position.Equals(goal));

        Stack<Vector3> points = new();
        while (closest != null) {
            if (Debug) {
                debugger.MarkPath(closest);
            }

            points.Push(tilemap.CellToWorld(closest.Position));
            closest = closest.Parent;
        }

        return points;
    }

    protected void CalculateAround(AStarNode node, Vector3Int goal) {
        var pos = node.Position;

        Vector3Int[] neighbors = {
            new(pos.x - 1, pos.y - 1),
            new(pos.x - 0, pos.y - 1),
            new(pos.x + 1, pos.y - 1),

            new(pos.x - 1, pos.y - 0),
            new(pos.x + 1, pos.y - 0),

            new(pos.x - 1, pos.y + 1),
            new(pos.x - 0, pos.y + 1),
            new(pos.x + 1, pos.y + 1),
        };

        foreach (var neighbor in neighbors) {
            if (IsObstacle(neighbor)) {
                continue;
            }

            if (!nodes.ContainsKey(neighbor)) {
                var neighborNode = new AStarNode(neighbor, node, goal);                
                nodes[neighbor] = neighborNode;
                queue.Add(neighborNode);

                if (FullDebug) {
                    debugger.CreateOpen(neighborNode);
                }
            }
            else {
                var neighborNode = nodes[neighbor];

                if (neighborNode.Parent == null) {
                    continue;
                }

                if (neighborNode.Parent.G > node.G) {
                    neighborNode.Parent = node;

                    if (FullDebug) {
                        debugger.UpdateNode(neighborNode);
                    }
                }
            }
        }
    }

    protected bool IsObstacle(Vector3Int pos) {
        return tilemap.HasTile(pos);
    }
    
    protected AStarNode findClosest() {
        AStarNode closest = queue.First();
        
        for (int i = 1; i < queue.Count; i++) {
            var node = queue.ElementAt(i);
            
            if (node.F < closest.F) {
                closest = node;
            }
            else if (node.F == closest.F && node.H < closest.H) {
                closest = node;
            }
        }

        return closest;
    }
}