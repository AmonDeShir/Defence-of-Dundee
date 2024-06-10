using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public abstract class MenuScreen : MonoBehaviour
{
    protected UIDocument uIDocument;

    [SerializeField]
    protected AudioSource clickSound;

    protected List<Button> buttons;

    protected virtual void OnEnable()
    {
        uIDocument = GetComponent<UIDocument>();
        buttons = uIDocument.rootVisualElement.Query<Button>(className: "button").ToList();

        foreach (var button in buttons) {
            button.RegisterCallback<ClickEvent>(HandlePress, TrickleDown.TrickleDown);
        }
    }
 
    protected virtual  void OnDisable()
    {
        foreach (var button in buttons) {
            button.UnregisterCallback<ClickEvent>(HandlePress, TrickleDown.TrickleDown);
        }
    }

    protected virtual void HandlePress(EventBase evt)
    {
        Button target = (Button) evt.currentTarget;
        clickSound.Play();
        OnClick(target, evt);
    }

    protected abstract void OnClick(Button button, EventBase evt);
}
