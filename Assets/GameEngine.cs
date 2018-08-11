using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

    public GameObject growthPrefab;

    public int totalGrowth;

    public float lastGrowthSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, Camera.main.nearClipPlane));
        worldMousePos.z = 0;



        if (Input.GetMouseButtonDown(0))
        {
            GameObject newGrowth = Instantiate(growthPrefab, worldMousePos, new Quaternion());
            Color rndCol = new Color(Random.Range(0.7f, 1f), Random.Range(0.5f, 0.9f), Random.Range(0.3f, 0.7f), 1);
            newGrowth.GetComponent<SpriteRenderer>().color = rndCol;
            newGrowth.transform.localScale = new Vector3(0.01f, 0.01f, 1);
            newGrowth.GetComponent<Growth>().maxSize = 3;
            lastGrowthSpawn = 0;
        }

        totalGrowth = FindObjectsOfType<Growth>().Length;

        lastGrowthSpawn += Time.deltaTime;

        if(lastGrowthSpawn > 10 && totalGrowth < 50)
        {
            Vector3 rndVec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            rndVec.Normalize();
            rndVec *= 6.8f;
            GameObject newGrowth = Instantiate(growthPrefab, rndVec, new Quaternion());
            Color rndCol = new Color(Random.Range(0.7f, 1f), Random.Range(0.5f, 0.9f), Random.Range(0.3f, 0.7f), 1);
            newGrowth.GetComponent<SpriteRenderer>().color = rndCol;
            newGrowth.transform.localScale = new Vector3(0.01f, 0.01f, 1);
            newGrowth.GetComponent<Growth>().maxSize = 3;
            lastGrowthSpawn = 0;
        }
    }
}
