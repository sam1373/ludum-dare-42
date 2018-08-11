using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GameEngine eng;
    Rigidbody2D rb2d;
    public bool alive;

    public float inv;
    //invincible for a second;

    // Use this for initialization
    void Start()
    {
        eng = FindObjectOfType<GameEngine>();
        rb2d = GetComponent<Rigidbody2D>();
        alive = true;
        inv = 1;
    }

    // Update is called once per frame
    void Update () {
        if (inv > 0)
            inv -= Time.deltaTime;

        if (alive)
        {
            Player pl = FindObjectOfType<Player>();

            Vector3 dir3 = pl.transform.position - transform.position;
            Vector2 dir = new Vector2(dir3.x, dir3.y);
            dir.Normalize();

            if (rb2d.velocity.magnitude < 1f)
                rb2d.velocity += dir * 0.03f;
        }
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
        }
        Destroy(gameObject);
    }
}
