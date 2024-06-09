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
    public int InitialLives;

    [SerializeField]
    private int lives;

    [SerializeField]
    private int hp;

    public int HP
    {
        get => hp;
        set { hp = Math.Clamp(value, 0, MaxHP); }
    }

    public int Lives
    {
        get => lives;
        set { lives = Math.Max(value, 0); }
    }


    public void ResetHP() {
        HP = MaxHP;
    }

    public void ResetLives() {
        Lives = InitialLives;
    }

    public void OnAfterDeserialize() {
		ResetHP();
        ResetLives();
    }

    public void OnBeforeSerialize() {}
}
