using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPills : Item
{
    [SerializeField]
    protected int heal = 50;

    public override void Effect(Killable target)
    {
        target.Heal(heal);
    }
}
