using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, Camera.main.nearClipPlane));
        worldMousePos.z = 0;

        Vector3 corePos = FindObjectOfType<Core>().transform.position;

        Vector3 dir = worldMousePos - corePos;

        RaycastHit2D[] hits = Physics2D.RaycastAll(corePos, dir);

        foreach (RaycastHit2D hit in hits)
        {
            var obj = hit.rigidbody;

            Growth gr = obj.GetComponent<Growth>();

            if (gr != null)
            {
                gr.superShrinkReaction();
            }

            Enemy en = obj.GetComponent<Enemy>();

            if (en != null)
            //if (en.inv < 0)
            {
                en.die();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, Camera.main.nearClipPlane));
        worldMousePos.z = 0;

        Vector3 corePos = FindObjectOfType<Core>().transform.position;

        Vector3 dir = worldMousePos - corePos;
        dir.Normalize();
        transform.SetPositionAndRotation(corePos + dir * 7f, new Quaternion());
        transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * (Mathf.Atan2(dir.y, dir.x))));

        


        float curW = transform.localScale.y;
        transform.localScale = new Vector3(transform.localScale.x, curW - 0.1f, transform.localScale.z);

        if (curW < 0)
            Destroy(gameObject);
	}

    void OnCollisionStay2D(Collision2D collision)
    {
        var hit = collision.gameObject;

        //hits--;

        Growth gr = hit.GetComponent<Growth>();

        if (gr != null)
        {
            gr.superShrinkReaction();
        }

        Enemy en = hit.GetComponent<Enemy>();

        if (en != null)
            //if (en.inv < 0)
            {
                en.die();
            }
    }
}
