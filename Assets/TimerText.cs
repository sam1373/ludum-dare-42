using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{

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
        text.text += "Time survived " + (int)eng.timer;
    }
}
