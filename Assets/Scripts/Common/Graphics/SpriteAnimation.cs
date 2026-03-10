using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SpriteAnimation : MonoBehaviour
{
    public List<Sprite> Sprites;
    public float Timer;
    public SpriteRenderer Renderer;

    float _currentTimer;
    int _currentSpriteIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer.sprite = Sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
