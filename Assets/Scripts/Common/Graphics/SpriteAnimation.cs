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
        if (Sprites.Count == 0) return;

        _currentTimer += Time.deltaTime;

        if (_currentTimer >= Timer)
        {
            _currentTimer = 0f;

            _currentSpriteIndex++;

            if (_currentSpriteIndex >= Sprites.Count)
            {
                _currentSpriteIndex = 0;
            }

            Renderer.sprite = Sprites[_currentSpriteIndex];
        }
    }
}
