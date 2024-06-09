using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Win : MenuScreen
{
    protected override void OnClick(Button button, EventBase evt)
    {
        switch(button.text) {
            case "back  to  menu": SceneManager.LoadScene(0); break;
        }
    }
}
