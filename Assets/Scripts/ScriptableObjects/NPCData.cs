using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Data", menuName = "Data/NpcData")]
public class NPCData : ScriptableObject
{
    public new string name;
    public string hp;
    public int attack;

    public int speed;
    public int jump;
    public int ultraJump;
    public int fallSpeed;
}
