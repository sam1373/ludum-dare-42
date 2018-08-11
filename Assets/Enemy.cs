using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GameEngine eng;
    Rigidbody2D rb2d;
    SpriteRenderer rend;
    public bool alive;

    public float inv;
    //invincible for a second;

    float outOfBounds;

    bool goAfterCore;

    float nextChoice;

    // Use this for initialization
    void Start()
    {
        eng = FindObjectOfType<GameEngine>();
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        alive = true;
        inv = 1;
        outOfBounds = 5;
        nextChoice = 3;

        rend.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
    }

    // Update is called once per frame
    void Update () {
        if (inv > 0)
            inv -= Time.deltaTime;
        if(transform.position.magnitude > 6)
            outOfBounds -= Time.deltaTime;

        if (rb2d.velocity.magnitude < 0.01f)
            outOfBounds -= Time.deltaTime;

        if (alive)
        {
            
            Player pl = FindObjectOfType<Player>();
            Core cr = FindObjectOfType<Core>();
            Vector3 target;
            if (goAfterCore)
                target = cr.transform.position;
            else
                target = pl.transform.position;

            Vector3 dir3 = target - transform.position;
            Vector2 dir = new Vector2(dir3.x, dir3.y);
            dir.Normalize();

            if (rb2d.velocity.magnitude < 1f)
                rb2d.velocity += dir * 0.03f;

            if (nextChoice > 0)
                nextChoice -= Time.deltaTime;
            else
            {
                nextChoice = 3;
                int rnd = Random.Range(0, 2);
                goAfterCore = (rnd == 1);
            }
        }

        if (outOfBounds < 0)
            Destroy(this.gameObject);
	}

    public void die()
    {
        //alive = false;
        //GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        //return;
        int chks = Random.Range(9, 12);
        for(int i = 0; i < chks; i++)
        {
            GameObject newCh = Instantiate(eng.chunkPrefab, transform.position, new Quaternion(0, 0, Random.Range(0f, 1f), 0));
            newCh.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 8;
            newCh.GetComponent<SpriteRenderer>().color = rend.color;
        }
        Destroy(gameObject);
    }
}
