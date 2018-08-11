using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    GameEngine eng;
    Rigidbody2D rb2d;

    
	// Use this for initialization
	void Start () {
        eng = FindObjectOfType<GameEngine>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float speed = 0.1f;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity += movement * speed;


        Vector3 mp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, Camera.main.nearClipPlane));
        mp.z = 0;
        if (eng.mouseDown)
        {
            Vector3 direction = mp - transform.position;
            direction.Normalize();
            GameObject newShot = Instantiate(eng.shotPrefab, transform.position, new Quaternion());
            newShot.GetComponent<Rigidbody2D>().velocity = direction * 6;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newShot.GetComponent<Collider2D>());
        }
    }
}
