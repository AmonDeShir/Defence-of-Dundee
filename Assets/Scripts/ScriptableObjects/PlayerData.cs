using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Data/PlayerData")]
public class PlayerData : NPCData
{
    public int level;
    public Vector3 checkpoint;
}
