using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GUI : MonoBehaviour
{
    public PlayerHP data;
    protected UIDocument uIDocument;

    protected Label lives;
    protected VisualElement hp;

    void Start()
    {
        uIDocument = GetComponent<UIDocument>();
        lives = uIDocument.rootVisualElement.Query<Label>(name: "Lives").First();
        hp = uIDocument.rootVisualElement.Query<VisualElement>(name: "HP").First();
        hp.style.width = new StyleLength(new Length(Mathf.Clamp01(data.HP / (float)data.MaxHP) * 100, LengthUnit.Percent));
    }

    void Update()
    {
        lives.text = data.Lives.ToString();

        float hpValue = Mathf.Clamp01(data.HP / (float)data.MaxHP) * 100;
        hpValue = Mathf.Lerp(hp.style.width.value.value, hpValue, Time.deltaTime * 10);
        hp.style.width = new StyleLength(new Length(hpValue, LengthUnit.Percent));
    }
}
