using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum MODE {
    LOAD_NEXT,
    LOAD_PREVIOUS,
    LOAD_SELECTED,
}

public class SceneTeleport : MonoBehaviour
{
    public MODE mode;
    public int selected = 0;
    public string TargetTag = "Player";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(TargetTag))
        {
            int index = SceneManager.GetActiveScene().buildIndex;

            if (mode == MODE.LOAD_NEXT)
            {
                SceneManager.LoadScene(index + 1);
            }

            else if (mode == MODE.LOAD_PREVIOUS)
            {
                SceneManager.LoadScene(index - 1);
            }

            else if (mode == MODE.LOAD_SELECTED)
            {
                SceneManager.LoadScene(selected);
            }
        }
    }
}
