using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Statistics", menuName = "Data/MovementStatistics")]
public class MovementStats : ScriptableObject
{
    public int speed;
    public int jump;
    public int ultraJump;
    public int fallSpeed;
    public int run;
    public int maxJumpCount;
}
