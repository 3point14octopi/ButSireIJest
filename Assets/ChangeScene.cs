using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToGame()
    {
        ScoreManager s = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        s.score = 0f;
        SceneManager.LoadScene("GameScene");
    }


}
