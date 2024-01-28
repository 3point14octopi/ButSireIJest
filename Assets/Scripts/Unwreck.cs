using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unwreck : MonoBehaviour
{
    public string RendererName;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager s = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        s.textRenderer = GameObject.Find(RendererName).GetComponent<Text>();
        s.displayedOnce = false;
    }

}
