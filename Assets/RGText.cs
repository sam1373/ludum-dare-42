using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGText : MonoBehaviour {

    GameEngine eng;
    Text text;
    // Use this for initialization
    void Start()
    {
        eng = FindObjectOfType<GameEngine>();
        //core = FindObjectOfType<Core>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "";
        if (eng.rapidGrowthPeriod > 0)
            text.text += "Rapid Growth 0:0" + (int)eng.rapidGrowthPeriod;
    }
}
