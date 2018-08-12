using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public float shake;

	// Use this for initialization
	void Start () {
        shake = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rndVec = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), -10);
        transform.position = rndVec;

        if (shake > 0)
            shake -= Time.deltaTime;

        if (shake < 0)
            shake = 0;
	}
}
