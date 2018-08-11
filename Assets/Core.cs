using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    GameEngine eng;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        eng = FindObjectOfType<GameEngine>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.angularVelocity = -5f;
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;

        Enemy en = hit.GetComponent<Enemy>();
        
        if(en != null)
        {
            GameObject newCorr = Instantiate(eng.corrPrefab, en.transform.position, new Quaternion(), this.transform);
            newCorr.transform.localScale *= Random.Range(0.8f, 1.5f);

            en.die();
            //Destroy(en.gameObject);

            Color col = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(col.r - 0.01f, col.g, col.b, 1);
        }
    }
}
