using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour {

    GameEngine eng;

    float nextSpawn;
    public float maxSize;

    int maxCreate;

    bool superShrink;

    Collider2D coll;
    SpriteRenderer rend;

    // Use this for initialization
    void Start () {
        nextSpawn = 5;
        eng = FindObjectOfType<GameEngine>();
        //transform.localScale = new Vector3(0.01f, 0.01f, 1);
        maxCreate = 1;

        //print(transform.position.magnitude);
        //if (transform.position.magnitude > 6)
        //    Destroy(this.gameObject);
        coll = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().WakeUp();
  
        
        Vector3 mp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, Camera.main.nearClipPlane));
        mp.z = 0;
        /*
        if (Input.GetMouseButtonDown(1))
        {
            if (coll.OverlapPoint(mp))
            {
                superShrinkReaction();
            }
        }*/


        float td = Time.deltaTime;

        float curSize = transform.localScale.x;
        Vector3 globScale = transform.lossyScale;

        //print(curSize);
        if(superShrink)
        {
            //superShrinkReaction();
            transform.localScale = new Vector3(curSize - td * 0.3f, curSize - td * 0.3f);
            Color curCol = rend.color;
            rend.color = new Color(curCol.r - 0.01f, curCol.g, curCol.b, curCol.a);
        }
        else if (curSize < maxSize)
        {
            transform.localScale = new Vector3(curSize + td * eng.growRate, curSize + td * eng.growRate);
        }

        if (curSize < 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if(curSize > 2)
            nextSpawn -= td;

        if (nextSpawn < 0 && maxCreate > 0 && eng.totalGrowth < 50)
        {
            Vector3 rndVec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            rndVec.Normalize();
            rndVec *= 6;

            Vector3 dir = (rndVec - transform.position);
            dir.Normalize();


            dir *= globScale.x * Random.Range(0.4f, 0.5f);
            if ((transform.position + rndVec).magnitude < 6)
            {


                GameObject newGrowth = Instantiate(eng.growthPrefab, transform.position + dir, new Quaternion());

                newGrowth.transform.localScale = new Vector3(0.01f, 0.01f, 1);

                //Color rndCol = new Color(Random.Range(0.7f, 1f), Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), 1);
                newGrowth.GetComponent<SpriteRenderer>().color = rend.color;

                newGrowth.GetComponent<Growth>().maxSize = Random.Range(1.5f, 3);

                nextSpawn = 6 * (0.1f / eng.growRate);
                maxCreate--;
                eng.lastGrowthSpawn = 0;
            }
        }


        
	}

    public void superShrinkReaction()
    {
        superShrink = true;
        Growth[] grs = FindObjectsOfType<Growth>();
        print(grs.Length);
        foreach(Growth gr in grs)
        {
            //print(gr.superShrink + " " + Physics2D.IsTouching(coll, gr.coll));
            //print(coll + " " + gr.coll);
            if(gr.superShrink == false)
                if (Physics2D.IsTouching(coll, gr.coll))
                {
                    gr.superShrinkReaction();

                    return;
                }
        }
    }

    public void reduceSize(float x)
    {
        float curSize = transform.localScale.x;
        transform.localScale = new Vector3(curSize - x, curSize - x);
    }
}
