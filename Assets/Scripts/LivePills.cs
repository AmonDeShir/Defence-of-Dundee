using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePills : Item
{
    public override void Effect(Killable target)
    {
        var player = target as PlayerKillable;

        if (player != null) {
            player.data.Lives += 1;
        }
    }
}
