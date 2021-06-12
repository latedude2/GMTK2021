using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    public Animator animator;
    public Transform circle;
    public float random;
    public float beginShuffle = 0.2f;
    public float addedShuffleMax = 0.4f;
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
        transform.localPosition = 0.3f * new Vector3(Mathf.PerlinNoise(Mathf.Sin(random + Time.time), Mathf.Cos(random + Time.time)) - 0.5f,
            Mathf.PerlinNoise(Mathf.Cos(random + Time.time), Mathf.Sin(random + Time.time)) - 0.5f,
            1);
    }
}
