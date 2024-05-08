using System;
using UnityEngine;

public class AStarNode {
    protected static int DIAGONAL_NEIGHBOR = 14;
    protected static int NORMAL_NEIGHBOR = 10;

    /** Cost of travel from parent node */
    public int G;

    /** How far to the goal node (In straight line) */
    public int H;

    /** G + F */
    public int F; 

    public float W = 2f;


    private AStarNode _parent;

    public AStarNode Parent { get => _parent; set {
        this._parent = value;
        this.G = CalculateCostToNeighbor(value.Position) + value.G;
        this.F = this.G + (int)(W * (float)this.H);
    }}

    public Vector3Int Position;

    public AStarNode(Vector3Int position, AStarNode parent, Vector3Int goal) {
        this.Position = position;
        this._parent = parent;
        this.G = CalculateCostToNeighbor(parent.Position) + parent.G;
        this.H = EstimateCostToPoint(goal);
        this.F = this.G + (int)(W * (float)this.H);
    }

    private AStarNode(Vector3Int position) {
        this.Position = position;
    }

    public static AStarNode CreateStartNode(Vector3Int position) {
        return new AStarNode(position);
    }

    public int CalculateCostToNeighbor(Vector3Int neighbor) {
        var delta = Position - neighbor;

        if (delta.x == 0 || delta.y == 0) {
            return NORMAL_NEIGHBOR;
        }

        return DIAGONAL_NEIGHBOR;
    }

    public int EstimateCostToPoint(Vector3Int point) {
        return (int)(Math.Sqrt(Math.Pow((double)Position.x - (double)point.x, 2) + Math.Pow((double)Position.y - (double)point.y, 2)) * 10.0);
    }
}