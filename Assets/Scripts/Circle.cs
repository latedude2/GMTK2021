using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleMovement : MonoBehaviour
{
    public float movespeed = 1f;
    public float circleRotationSpeed = 15f;
    public float baseDancerRotationSpeed = 20f;
    public float newDancerRotationSpeed = 25f;
    public float loseDancerRotationSpeed = 5f;
    public List<Transform> dancers;
    public int dancerCount = 3;
    public GameObject dancerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnDancers(dancerCount);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CircleRotation();
    }
    
    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * movespeed * Time.deltaTime;

        transform.position += tempVect;
    }

    void CircleRotation()
    {
        transform.Rotate(new Vector3(0, 0, circleRotationSpeed * Time.deltaTime));
        if (circleRotationSpeed > newDancerRotationSpeed)
        {
            dancerCount++;
            circleRotationSpeed = baseDancerRotationSpeed;
            SpawnDancers(dancerCount);
        }
        else if (circleRotationSpeed < loseDancerRotationSpeed)
        {
            dancerCount--;
            circleRotationSpeed = baseDancerRotationSpeed;
            SpawnDancers(dancerCount);
        }
    }

    public void SpawnDancers(int num)
    {
        dancers = new List<Transform>();
        for (int i = 0; i < num; i++)
        {
            /* Distance around the circle */
            float radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);

            Vector3 spawnDir = new Vector3(horizontal, vertical, 1);

            /* Get the spawn position */
            Vector3 spawnPos = transform.position + spawnDir * GetComponent<CircleCollider2D>().radius; // Radius is just the distance away from the point

            /* Now spawn */
            GameObject dancer = Instantiate(dancerPrefab, spawnPos, Quaternion.identity) as GameObject;
            dancer.transform.parent = transform;

            dancer.transform.Rotate(new Vector3(0, 0, radians * Mathf.Rad2Deg + 90));

            dancers.Add(dancer.transform);
        }
    }


}
