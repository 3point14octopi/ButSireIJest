using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterStates;
using UnityEngine.UI;

public class KingState : MonoBehaviour
{
    public Sprite[] expressions = new Sprite[4];
    public Image kingRenderer;

    public int kingApproval = 8;
    public int boredomMeter = 1;

    public Boring thisIsPoorProgrammingPractice;

    private IKingMood[] kingMood = { 
        new King_GoodMood(), 
        new King_NeutralMood(), 
        new King_AngryMood(), 
        new King_NuclearMood() 
    };
    private IKingMood currentMood;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; kingMood[i].Init(this), i++) ;
        UpdateEmotionalState(MoodEnum.NEUTRAL);
    }

    // Update is called once per frame
    void Update()
    {
        currentMood.OnUpdate();
    }

    public void UpdateMoodSprite(Sprite spr)
    {
        kingRenderer.sprite = spr;
    }

    public void UpdateEmotionalState(MoodEnum newState)
    {
        currentMood = kingMood[(int)newState];
        currentMood.SetSprite();
    }


    public void OnJoke(int joke)
    {
        currentMood.OnJoke((JokeType)joke);
    }

    public void BoredomNeutralize()
    {
        currentMood.BoredomNeutralizer();
    }

    public void InProlongedRageState()
    {
        thisIsPoorProgrammingPractice.IncreaseBoredom();
    }
}
