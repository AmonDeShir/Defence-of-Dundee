using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsScreen : MonoBehaviour
{
    protected UIDocument uIDocument;
    protected List<Button> buttons;

    private VisualElement masterOption;
    private VisualElement musicOption;
    private VisualElement sfxOption;
    private VisualElement uiOption;

    private AudioSource activeSound = null;

    [SerializeField]
    protected AudioSource masterSound;

    [SerializeField]
    protected AudioSource musicSound;

    [SerializeField]
    protected AudioSource sfxSound;

    [SerializeField]
    protected AudioSource uiSound;

    [SerializeField]
    protected AudioMixer mixer;

    protected void OnEnable()
    {
        uIDocument = GetComponent<UIDocument>();
        buttons = uIDocument.rootVisualElement.Query<Button>(className: "slider-plus").ToList();
        buttons.AddRange(uIDocument.rootVisualElement.Query<Button>(className: "slider-minus").ToList());

        foreach (var button in buttons) {
            button.RegisterCallback<ClickEvent>(HandleClick, TrickleDown.TrickleDown);
        }

        masterOption = uIDocument.rootVisualElement.Query<VisualElement>(name: "MASTER").First();
        musicOption = uIDocument.rootVisualElement.Query<VisualElement>(name: "MUSIC").First();
        sfxOption = uIDocument.rootVisualElement.Query<VisualElement>(name: "SFX").First();
        uiOption = uIDocument.rootVisualElement.Query<VisualElement>(name: "UI").First();
        
        RegisterEvents(masterOption);
        RegisterEvents(musicOption);
        RegisterEvents(sfxOption);
        RegisterEvents(uiOption);

        LoadStartSliderValue(masterOption, "MASTER");
        LoadStartSliderValue(musicOption, "MUSIC");
        LoadStartSliderValue(sfxOption, "SFX");
        LoadStartSliderValue(uiOption, "UI");
    }

    protected void OnDisable()
    {
        foreach (var button in buttons) {
            button.UnregisterCallback<ClickEvent>(HandleClick, TrickleDown.TrickleDown);
        }

        UnregisterEvents(masterOption);
        UnregisterEvents(musicOption);
        UnregisterEvents(sfxOption);
        UnregisterEvents(uiOption);
    }

    protected void RegisterEvents(VisualElement element) {
        element.RegisterCallback<MouseOverEvent>(HandleEnter);
        element.RegisterCallback<MouseOutEvent>(HandleExit);
    }

    protected void UnregisterEvents(VisualElement element) {
        element.UnregisterCallback<MouseOverEvent>(HandleEnter);
        element.UnregisterCallback<MouseOutEvent>(HandleExit);
    }

    protected void HandleEnter(EventBase evt) {
        ColorTitle((VisualElement) evt.currentTarget, Color.white);

        switch(((VisualElement) evt.currentTarget).name) {
            case "MASTER": activeSound = masterSound; break;
            case "MUSIC": activeSound = musicSound; break;
            case "SFX": activeSound = sfxSound; break;
            case "UI": activeSound = uiSound; break;
        }
    }

    protected void HandleExit(EventBase evt) {
        ColorTitle((VisualElement) evt.currentTarget, Color.black);
    }

    protected void ColorTitle(VisualElement parent, Color color) {
        var title = parent.Query<Label>().First();
        title.style.color = new StyleColor(color);
    }

    protected void HandleClick(EventBase evt)
    {
        var button = (Button) evt.currentTarget;
        var sliderValue = button.parent.Query<VisualElement>(className: "slider-value").First();
        var value = sliderValue.style.width.value.value;

        if (button.GetClasses().Contains("slider-plus")) {
            value = Mathf.Clamp(value + 5, 0, 100);
        }
        else {
            value = Mathf.Clamp(value - 5, 0, 100);
        }
        
        sliderValue.style.width = new StyleLength(new Length(value, LengthUnit.Percent));
        PlayerPrefs.SetFloat(button.parent.parent.name, value);
        SaveValueToMixer(button.parent.parent.name, value);
    
        activeSound.Play();
    }

    protected float ReadValueFromMixer(string key) {
        mixer.GetFloat(key, out float logVolume);
        float linearVolume = Mathf.Pow(10f, logVolume / 20f);

        return linearVolume * 100f;
    }

    protected void SaveValueToMixer(string key, float percentageValue) {
        float scaledVolume = Mathf.Clamp(percentageValue / 100f, 0.0001f, 1f);
        float logVolume = Mathf.Log10(scaledVolume) * 20;

        mixer.SetFloat(key, logVolume);
    }

    protected void LoadStartSliderValue(VisualElement parent, string mixerKey) {
        var slider = parent.Query<VisualElement>(className: "slider-value").First();
        var value = Mathf.Clamp(ReadValueFromMixer(mixerKey), 0, 100);

        if (PlayerPrefs.HasKey(mixerKey)) {
            value = Mathf.Clamp(PlayerPrefs.GetFloat(mixerKey), 0, 100);
            SaveValueToMixer(mixerKey, value);
        }

        slider.style.width = new StyleLength(new Length(value, LengthUnit.Percent));
    }
}