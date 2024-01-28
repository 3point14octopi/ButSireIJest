using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class ScoreDump
{
    static Mutex adder = new Mutex();
    static Stack<float> scoreAdds = new Stack<float>();


    public static void AddToScore(float value)
    {
        if (adder.WaitOne())
        {
            scoreAdds.Push(value);
            adder.ReleaseMutex();
        }
    }

    public static float ResolveScoreDump()
    {
        float retVal = 0f;

        if (adder.WaitOne())
        {
            while (scoreAdds.Count > 0)
            {
                retVal += scoreAdds.Pop();
            }

            adder.ReleaseMutex();
        
        }
        return retVal;
    }

}



public class ScoreManager : MonoBehaviour
{
    public Text textRenderer;
    public float score = 0f;
    public bool displayedOnce = false;

    private float sinceLast = 0f;
    // Start is called before the first frame update

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        UpdateDisplayedScore(0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if(sinceLast > 0.5f)
            {
                UpdateDisplayedScore(ScoreDump.ResolveScoreDump());
                sinceLast = 0f;
            }
            else
            {
                sinceLast += Time.deltaTime;
            }
        }
        else
        {
            if (!displayedOnce)
            {
                GameOverRender();
                displayedOnce = true;
            }
        }
        
    }


    public void UpdateDisplayedScore(float offset)
    {
        score += offset;

        textRenderer.text = "SCORE: " + score.ToString("0");
    }

    public void GameOverRender()
    {
        textRenderer.text = "Final Score: " + score.ToString("0");
    }

}
