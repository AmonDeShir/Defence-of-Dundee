using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct Layer { public List<SpriteRenderer> parts; }

[Serializable]
public struct LayerSprite { public Sprite[] variants; }

public class RandomBackground : MonoBehaviour
{
    public LayerSprite[] layersSprite;
    public Layer[] layers = {};

    void Start()
    {
        findLayers();
        SetRandomBackgrounds();
    }

    private void findLayers() {
        layers = new Layer[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            var layer = transform.GetChild(i);
            layers[i] = new Layer { parts = new List<SpriteRenderer>() };

            for (int j = 0; j < layer.childCount; j++) {
                layers[i].parts.Add(layer.GetChild(j).GetComponent<SpriteRenderer>());
            }
        }
    }
    
    public void SetRandomBackgrounds() {
        for (int i = 0; i < layers.Count(); i++) {
            Layer layer = layers[i];

            for (int j = 0; j < layer.parts.Count(); j++) {
                layer.parts[j].sprite = getRandomVariant(i);
            }
        }
    }

    public void AddNew(int layerIndex) {
        Transform layer = transform.GetChild(layerIndex);
        Transform lastInstance = layer.GetChild(layer.childCount - 1);

        GameObject background = Instantiate(lastInstance.gameObject, layer);
        SpriteRenderer backgroundRenderer = background.GetComponent<SpriteRenderer>();

        layers[layerIndex].parts.Add(backgroundRenderer);
        backgroundRenderer.sprite = getRandomVariant(layerIndex);
        background.transform.position += new Vector3(backgroundRenderer.bounds.size.x, 0, 0);
    }

    public float GetLayerSize(int layerIndex) {
        float size = 0f;

        foreach(var part in layers[layerIndex].parts) {
            size += part.bounds.size.x;
        }

        return size;
    }

    protected Sprite getRandomVariant(int layer) {
        Sprite[] variants = layersSprite[layer].variants;

        return variants[UnityEngine.Random.Range(0, variants.Count())];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
