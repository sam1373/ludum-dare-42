using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

    float existTime;

	// Use this for initialization
	void Start () {
        existTime = Random.Range(2f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        existTime -= Time.deltaTime;

        Light2D.LightSprite l = GetComponentInChildren<Light2D.LightSprite>();
        l.transform.localScale = new Vector3(existTime * 0.5f, existTime * 0.5f, 1);

        if (existTime < 0)
            Destroy(gameObject);
	}
}
