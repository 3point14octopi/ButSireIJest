using CharacterStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdState : MonoBehaviour
{
    public float timeSinceLastTomato = 0f;
    public int favour = 9;

    ICrowdMood[] crowdMoods = {
        new Crowd_GoodMood(),
        new Crowd_NeutralMood(),
        new Crowd_BadMood()
    };

    ICrowdMood currentMood;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; crowdMoods[i].Init(this), i++) ;
        currentMood = crowdMoods[1];
    }

    // Update is called once per frame
    void Update()
    {
        currentMood.OnUpdate();
    }


    public void OnJoke(int jokeType)
    {
        currentMood.OnJoke((JokeType)jokeType);
    }

    public void ThrowTomato(float timeWindow)
    {
        Debug.Log("Launching a tomato, with " + timeWindow.ToString() + " seconds to dodge");

    }

    public void UpdateCrowdEmotion(MoodEnum newMood)
    {
        currentMood = crowdMoods[(int)newMood];
        Debug.Log("Crowd mood changed to: " + newMood.ToString());
    }

    public void BoredomNeutralize()
    {
        currentMood.Neutralize();
    }
}
