using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScoreSystem;

public class Obstacle : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;
    bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.speed = 0;
        Destroy(gameObject, 100.0f);
        transform.Rotate(new Vector3(0, 0, Random.Range(0,360)));
    }

    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !destroyed)
        {
            destroyed = true;
            if(audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            if(animator == null)
            {
                animator = GetComponent<Animator>();
            }
            animator.speed = 1;
            audioSource.Play();
            Score.score -= 20;
            Destroy(gameObject, 60.0f);
        }
    }
}
