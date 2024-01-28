using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boring : MonoBehaviour
{
    public int boredom = 1;
    public KingState king;
    public CrowdState crowd;
    private float timeSinceLastJoke = 0f;
    private float timeSinceLastNeutralize = 0f;

    private int lastJoke = -1;
    private int currentJoke = -1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDump.AddToScore((1 - (boredom)) * Time.deltaTime);



        IncreaseIdleCounter();

    }

    private void IncreaseIdleCounter()
    {
        timeSinceLastJoke += Time.deltaTime;
        if (timeSinceLastJoke >= (10 - boredom))
        {
            timeSinceLastJoke = 0;
            IncreaseBoredom();
        }
    }

    private void IncrreaseNeutralizeCounter()
    {
        timeSinceLastNeutralize += Time.deltaTime;
        if (timeSinceLastNeutralize >= 3 + (10 - boredom))
        {
            timeSinceLastNeutralize = 0;
            king.BoredomNeutralize();
            crowd.BoredomNeutralize();
        }
    }


    public void IncreaseBoredom()
    {
        boredom++;
        if (boredom == 10)
        {
            SceneManager.LoadScene("gameOver");
        }
    }

    public void JokeTold(int jokeType)
    {
        timeSinceLastJoke = 0f;

        if(jokeType == currentJoke && currentJoke == lastJoke) IncreaseBoredom();

        lastJoke = currentJoke;
        currentJoke = jokeType;
    }
}
