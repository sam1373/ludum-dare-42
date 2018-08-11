using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour {

    GameEngine eng;

    float nextSpawn;
    public float maxSize;

    int maxCreate;

	// Use this for initialization
	void Start () {
        nextSpawn = 5;
        eng = FindObjectOfType<GameEngine>();
        //transform.localScale = new Vector3(0.01f, 0.01f, 1);
        maxCreate = 1;

        //print(transform.position.magnitude);
        //if (transform.position.magnitude > 6)
        //    Destroy(this.gameObject);
        
    }
	
	// Update is called once per frame
	void Update () {
        float td = Time.deltaTime;

        float curSize = transform.localScale.x;
        Vector3 globScale = transform.lossyScale;

        //print(curSize);
        if (curSize < maxSize)
        {
            transform.localScale = new Vector3(curSize + td * 0.1f, curSize + td * 0.1f);
        }

        if(curSize > 2)
            nextSpawn -= td;

        if (nextSpawn < 0 && maxCreate > 0 && eng.totalGrowth < 50)
        {
            Vector3 rndVec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            rndVec.Normalize();
            rndVec = rndVec * globScale.x * Random.Range(0.4f, 0.5f);
            if ((transform.position + rndVec).magnitude < 6)
            {


                GameObject newGrowth = Instantiate(eng.growthPrefab, transform.position + rndVec, new Quaternion());

                newGrowth.transform.localScale = new Vector3(0.01f, 0.01f, 1);

                //Color rndCol = new Color(Random.Range(0.7f, 1f), Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), 1);
                newGrowth.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;

                newGrowth.GetComponent<Growth>().maxSize = Random.Range(1.5f, 3);

                nextSpawn = 6;
                maxCreate--;
                eng.lastGrowthSpawn = 0;
            }
        }
        
	}
}
