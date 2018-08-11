using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    int hits;

	// Use this for initialization
	void Start () {
        hits = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.magnitude > 7)
            Destroy(this.gameObject);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;

        hits--;

        Growth gr = hit.GetComponent<Growth>();

        if(gr != null)
        {
            gr.reduceSize(0.03f);
            /*
            int rnd = Random.Range(0, 50);
            if(rnd == 0)
            {
                GameEngine eng = FindObjectOfType<GameEngine>();
                //for (int i = 0; i < 3; i++)
                //{
                    GameObject newEn = Instantiate(eng.enemyPrefab, transform.position, new Quaternion());
                //}
            }
            */
        }

        Enemy en = hit.GetComponent<Enemy>();
        
        if(en != null)
            if(en.inv < 0)
            {
                en.die();
                /*
                SpriteRenderer ren = en.GetComponent<SpriteRenderer>();
                Color curCol = ren.color;
                float newR = Mathf.Max(0.5f, curCol.r - 0.04f);
                float newG = Mathf.Min(0.5f, curCol.g + 0.04f);
                float newB = Mathf.Max(0.5f, curCol.b - 0.04f);
                ren.color = new Color(newR, newG, newB, 1);
            
                if (newR <= 0.5f)
                {
                    en.die();
                }

                Light2D.LightSprite l = en.GetComponentInChildren<Light2D.LightSprite>();
                float curSz = l.transform.localScale.x;
                l.transform.localScale = new Vector3(curSz - 0.1f, curSz - 0.1f, 1);
                */
            }

        if (hits <= 0)
            Destroy(gameObject);
    }
}
