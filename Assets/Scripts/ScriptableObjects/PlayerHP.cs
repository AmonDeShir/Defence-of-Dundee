using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Data", menuName = "Data/NpcData")]
public class PlayerHP : ScriptableObject, ISerializationCallbackReceiver
{
    public int level;

    public Vector3 checkpoint;

    public int MaxHP;

    [SerializeField]
    private int hp;

    public int HP
    {
        get => hp;
        set { hp = Math.Clamp(value, 0, MaxHP); }
    }

    public void ResetHP() {
        HP = MaxHP;
    }

    public void OnAfterDeserialize() {
		ResetHP();
    }

    public void OnBeforeSerialize() {}
}
