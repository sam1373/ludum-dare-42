using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreText : MonoBehaviour {

    GameEngine eng;
    Core core;
    Text text;

	// Use this for initialization
	void Start () {
        eng = FindObjectOfType<GameEngine>();
        core = FindObjectOfType<Core>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "";
        text.text += "Core power:" + (int)core.power + "%\n";
        text.text += "Core lazer charge:" + (int)core.lCharge + "%\n";
        if (core.lCharge >= 100)
        {
            text.text += "Lazer ready!\n";
            text.text += "right-click to fire\n";
        }
    }
}
