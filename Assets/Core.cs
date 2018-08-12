using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    GameEngine eng;
    Rigidbody2D rb;

    public float power;
    public float lCharge;

    Color origCol;

	// Use this for initialization
	void Start () {
        eng = FindObjectOfType<GameEngine>();
        rb = GetComponent<Rigidbody2D>();
        power = 100;
        lCharge = 0;

        origCol = GetComponent<SpriteRenderer>().color;
    }
	
	// Update is called once per frame
	void Update () {
        rb.angularVelocity = -5f;

        if(lCharge < 100)
            lCharge += Time.deltaTime * 2 * 0.01f * power;

        if (Input.GetMouseButtonDown(1))
        {
            if(lCharge >= 100)
            {
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, Camera.main.nearClipPlane));
                worldMousePos.z = 0;

                Vector3 dir = worldMousePos - transform.position;
                dir.Normalize();
                //print(dir + " " + Mathf.Atan2(dir.y, dir.x) + " " + Mathf.Rad2Deg * (Mathf.Atan2(dir.y, dir.x)));
                //print(new Quaternion());
                GameObject newLazer = Instantiate(eng.lazerPrefab, transform.position + dir * 7f, new Quaternion());
                newLazer.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color; 
                newLazer.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * (Mathf.Atan2(dir.y, dir.x))));
                //newLazer.transform.Rotate(new Vector3(0, 0, 1), Mathf.Rad2Deg * (Mathf.Atan2(dir.y, dir.x)));

                lCharge = 0;
            }
        }

        //Color col = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(origCol.r - 0.01f * (100 - power), origCol.g, origCol.b, 1);
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

            

            power--;

            FindObjectOfType<MainCamera>().shake += 0.2f;
        }
    }
}
