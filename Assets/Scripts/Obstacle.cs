using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScoreSystem;

public class Obstacle : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 100.0f);
        transform.Rotate(new Vector3(0, 0, Random.Range(0,360)));
    }

    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            audioSource.Play();
            ScoreManager.score -= 5;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 2.0f);
        }
    }
}
