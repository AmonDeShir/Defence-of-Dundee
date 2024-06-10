using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    private UIDocument MainMenuScreen;
   
    [SerializeField] 
    private UIDocument SettingsScreen;
    
    [SerializeField] 
    private UIDocument CreditsScreen;

    [SerializeField]
    protected AudioSource clickSound;

    private UIDocument current = null;

    protected void ChangeScreen(UIDocument screen) {
        if (current != null) {
            Hide(current);
        }

        current = screen;

        Show(current);
    }

    protected void Show(UIDocument document)
    {
        var buttons = document.rootVisualElement.Query<Button>(className: "button").ToList();
        document.rootVisualElement.style.display = DisplayStyle.Flex;

        foreach (var button in buttons) {
            button.RegisterCallback<ClickEvent>(HandlePress, TrickleDown.TrickleDown);
        }
    }
 
    protected void Hide(UIDocument document)
    {
        var buttons = document.rootVisualElement.Query<Button>(className: "button").ToList();
        document.rootVisualElement.style.display = DisplayStyle.None;

        foreach (var button in buttons) {
            button.UnregisterCallback<ClickEvent>(HandlePress, TrickleDown.TrickleDown);
        }
    }

    protected void HandlePress(EventBase evt)
    {
        Button target = (Button) evt.currentTarget;
        clickSound.Play();
        OnClick(target, evt);
    }

    protected void OnClick(Button button, EventBase evt)
    {
        switch(button.text) {
            case "Play": SceneManager.LoadScene(3); break;
            case "Exit": Application.Quit(); break;
            case "back  to  menu": ChangeScreen(MainMenuScreen); break;
            case "Settings": ChangeScreen(SettingsScreen); break;
            case "credits": ChangeScreen(CreditsScreen); break;
        }
    }

    private void Start() {
        CreditsScreen.rootVisualElement.style.display = DisplayStyle.None;
        SettingsScreen.rootVisualElement.style.display = DisplayStyle.None;

        ChangeScreen(MainMenuScreen);
    }

    private void OnDisable() {
        if (current != null) {
            Hide(current);
        }
    }
}