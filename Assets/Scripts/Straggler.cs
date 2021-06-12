using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straggler : MonoBehaviour
{
    public float speed = 1;
    public Transform circle;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        circle = GameObject.Find("Circle").transform;
        speed = circle.GetComponent<Circle>().movespeed;
    }

    void Update()
    {
        float step = 0;
        if (Vector3.Distance(circle.position, transform.position) < circle.GetComponent<CircleCollider2D>().radius * 2)
        {
            if(!circle.GetComponent<Circle>().IsAbleToGetNewDancer())
            {
                step = -speed * Time.deltaTime;
            }
        }

        // move sprite towards the target location
        transform.position = Vector3.MoveTowards(transform.position, circle.position, step);
        Quaternion rotation = Quaternion.LookRotation(circle.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }


    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (circle == null)
            { 
                circle = GameObject.Find("Circle").transform;
            }
            if (circle.GetComponent<Circle>().AddDancer())
            { 
                audioSource.Play();
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                Destroy(gameObject, 2.0f);
            }
        }
    }

}
