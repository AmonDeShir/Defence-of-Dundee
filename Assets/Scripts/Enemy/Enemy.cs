using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHP;

    [SerializeField]
    private int hp;

    public int HP
    {
        get => hp;
        set { hp = Mathf.Clamp(value, 0, MaxHP); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
