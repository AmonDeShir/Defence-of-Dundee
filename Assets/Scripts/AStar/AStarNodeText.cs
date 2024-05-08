using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum AStarNodeTexts {
    G = 0,
    H = 1,
    F = 2,
}

public class AStarNodeText : MonoBehaviour
{
    public AStarNode node;

    protected void SetText(AStarNodeTexts text, int value) {
        this.transform.GetChild((int) text).GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void Draw() {
        SetText(AStarNodeTexts.G, node.G);
        SetText(AStarNodeTexts.H, node.H);
        SetText(AStarNodeTexts.F, node.F);
    }
}
