using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    public Animator animator;
    public Transform circle;
    public float random;
    public float beginShuffle = 0.3f;
    public float addedShuffleMax = 0.5f;
    public float fallSpeed = 0.5f;
    void Start()
    {
        random = Random.Range(-10.0f, 10.0f);
        circle = GameObject.Find("Circle").transform;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        animator.speed = circle.GetComponent<Circle>().circleRotationSpeed / circle.GetComponent<Circle>().baseDancerRotationSpeed;
        transform.localPosition = 0.8f * new Vector3(Mathf.PerlinNoise(Mathf.Sin(random + Time.time), Mathf.Cos(random + Time.time)) - 0.5f,
            Mathf.PerlinNoise(Mathf.Cos(random + Time.time), Mathf.Sin(random + Time.time)) - 0.5f,
            1);

        if(transform.localScale.x > 1.0f)
        {
            transform.localScale = new Vector3(transform.localScale.x - fallSpeed * Time.deltaTime, transform.localScale.y - fallSpeed * 2 * Time.deltaTime, 1);
        }
    }

    public void Bounce()
    {
        transform.localScale = new Vector3(1.2f, 2.4f, 1);
    }
}
