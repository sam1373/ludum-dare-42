using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    GameEngine eng;
    Rigidbody2D rb2d;

    float nextShot;

    public float tripleShot;
	// Use this for initialization
	void Start () {
        eng = FindObjectOfType<GameEngine>();
        rb2d = GetComponent<Rigidbody2D>();

        tripleShot = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float speed = 0.2f;

        if(tripleShot > 0)
        {
            tripleShot -= Time.deltaTime;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity += movement * speed;


        Vector3 mp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, Camera.main.nearClipPlane));
        mp.z = 0;
        if (eng.mouseDown && nextShot < 0)
        {
            Vector3 direction = mp - transform.position;
            direction.Normalize();
            GameObject newShot = Instantiate(eng.shotPrefab, transform.position, new Quaternion());
            newShot.GetComponent<Rigidbody2D>().velocity = direction * 6;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newShot.GetComponent<Collider2D>());

            if (tripleShot > 0)
            {
                float dirAng = Mathf.Atan2(direction.y, direction.x) + 0.1f;
                direction = new Vector3(Mathf.Cos(dirAng), Mathf.Sin(dirAng));
                newShot = Instantiate(eng.shotPrefab, transform.position, new Quaternion());
                newShot.GetComponent<Rigidbody2D>().velocity = direction * 6;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newShot.GetComponent<Collider2D>());

                dirAng -= 0.2f;
                direction = new Vector3(Mathf.Cos(dirAng), Mathf.Sin(dirAng));
                newShot = Instantiate(eng.shotPrefab, transform.position, new Quaternion());
                newShot.GetComponent<Rigidbody2D>().velocity = direction * 6;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newShot.GetComponent<Collider2D>());
            }

            nextShot += 0.1f;
        }else
        {
            nextShot -= Time.deltaTime;
        }
        /*
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 direction = mp - transform.position;
            direction.Normalize();
            GameObject newShot = Instantiate(eng.superShotPrefab, transform.position, new Quaternion());
            newShot.GetComponent<Rigidbody2D>().velocity = direction * 6;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newShot.GetComponent<Collider2D>());
        }*/
    }
}
