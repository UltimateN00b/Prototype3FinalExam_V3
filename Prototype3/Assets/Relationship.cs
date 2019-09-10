using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relationship : MonoBehaviour
{
    public string characterName;
    public float startingProgressToNextLevel;
    public Sprite characterSprite;
    public Color characterColour;
    private int _currLevel;
    private float _currProgressToNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        _currLevel = 7;
        _currProgressToNextLevel = startingProgressToNextLevel;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string GetCharacterName()
    {
        return characterName;
    }

    public int GetCurrLevel()
    {
        return _currLevel;
    }

    public float GetProgress()
    {
        return _currProgressToNextLevel;
    }

    public Sprite GetCharacterSprite()
    {
        return characterSprite;
    }

    public Color GetCharacterColour()
    {
        return characterColour;
    }

    public void SetCurrLevel(int newLevel)
    {
        _currLevel = newLevel;
    }

    public void SetProgress(float newProgress)
    {
        _currProgressToNextLevel = newProgress;
    }
}
