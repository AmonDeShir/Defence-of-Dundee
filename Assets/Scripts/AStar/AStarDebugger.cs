using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

[Serializable]
public struct AStarDebuggerColors {
    public Color open;

    public Color closed;
    public Color path;

    public Color start;
    public Color updated;

    public Color goal;
}


public class AStarDebugger : MonoBehaviour {
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Tilemap tilemap;


    [SerializeField]
    private Tile tile;

    [SerializeField]
    private AStarDebuggerColors colors = new AStarDebuggerColors {
        open = Color.yellow,
        closed = Color.red,
        path = Color.blue,
        start = Color.magenta,
        updated = Color.cyan,
        goal = Color.green,
    };

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private AStarNodeText nodeTextPrefab;

    private Dictionary<Vector3Int, AStarNodeText> texts = new();

    public void Start() {
    }

    public void UpdateNode(AStarNode node) {
        var text = texts[node.Position];
        text.node = node;
        text.Draw();
        ColorTile(node.Position, colors.updated);
    }

    public void CreateOpen(AStarNode node) {
        ColorTile(node.Position, colors.open);

        AStarNodeText text;

        if (!texts.ContainsKey(node.Position)) {
            text = Instantiate(nodeTextPrefab, canvas.transform);
            text.transform.position = tilemap.CellToWorld(node.Position) + new UnityEngine.Vector3(0.5f, 0.5f, 0f);
            texts.Add(node.Position, text);
        }
        else {
            text = texts[node.Position];
        }
        
        text.node = node;
        text.Draw();
    }

    public void MarkPath(AStarNode node) {
        ColorTile(node.Position, colors.path);
    }

    public void MarkClosest(AStarNode node) {
        ColorTile(node.Position, colors.closed);
    }

    public void CreateStartAndGoal(Vector3Int start, Vector3Int goal) {
        ColorTile(start, colors.start);
        ColorTile(goal, colors.goal);
    }

    public void ColorTile(Vector3Int pos, Color color) {
        tilemap.SetTile(pos, tile);
        tilemap.SetTileFlags(pos, TileFlags.None);
        tilemap.SetColor(pos, color);
    }

    public void Clear() {
        foreach(var text in texts.Values) {
            Destroy(text.gameObject);
        }

        texts.Clear();
        tilemap.ClearAllTiles();
    }
}