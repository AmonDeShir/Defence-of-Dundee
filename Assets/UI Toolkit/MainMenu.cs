using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MenuScreen
{
    protected override void OnClick(Button button, EventBase evt)
    {
        switch(button.text) {
            case "Play": SceneManager.LoadScene(3); break;
            case "Exit": Application.Quit(); break;
        }
    }
}
