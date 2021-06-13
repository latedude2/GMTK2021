using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ScoreSystem;
using UnityEngine.UI;


public class Circle : MonoBehaviour
{
    public float movespeed = 1f;
    public float circleRotationSpeed = 20f;
    public float baseDancerRotationSpeed = 20f;
    public float newDancerRotationSpeed = 21f;
    public float loseDancerRotationSpeed = 15f;
    public List<Transform> dancers;
    public int dancerCount = 3;
    public GameObject dancerPrefab;
    public int scoreNeededPerDancer = 20;
    public GameObject musicPlayer;
    private Text spinText;


    // Start is called before the first frame update
    void Start()
    {
        SpawnDancers(dancerCount);
        spinText = GameObject.Find("Canvas").transform.Find("GameStatus").Find("Spin").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAbleToGetNewDancer())
        {
            spinText.text = "Get more dancers!";
        }
        else
        {
            spinText.text = "Need more speed!";
        }
        Movement();
        CircleRotation();
    }

    void Movement()
    {
        /* Old WASD movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * movespeed * Time.deltaTime;

        transform.position += tempVect;
        */

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);


        float step = movespeed * Time.deltaTime;
        // move circle towards mouse
        if(Vector3.Distance(transform.position, worldPosition) > 0.2f)
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, step);
    }

    void CircleRotation()
    {
        transform.Rotate(new Vector3(0, 0, circleRotationSpeed * Time.deltaTime));
        circleRotationSpeed = baseDancerRotationSpeed * Score.score / (scoreNeededPerDancer * dancerCount);

        if (circleRotationSpeed < loseDancerRotationSpeed)
        {
            Score.dancers--;
            dancerCount--;
            musicPlayer.GetComponent<MusicConductor>().RemoveMusicLayer();
            SpawnDancers(dancerCount);
        }
    }

    public bool AddDancer()
    {
        if (circleRotationSpeed > newDancerRotationSpeed)
        {
            Score.dancers++;
            dancerCount++;
            musicPlayer.GetComponent<MusicConductor>().AddMusicLayer();
            SpawnDancers(dancerCount);
            return true;
        }
        return false;
    }

    public bool IsAbleToGetNewDancer()
    {
        return circleRotationSpeed > newDancerRotationSpeed;
    }


    public void SpawnDancers(int num)
    {
        GetComponent<CircleCollider2D>().radius = num / (2 * Mathf.PI) * 0.6f;
        foreach (Transform dancer in dancers)
        {
            Destroy(dancer.gameObject);
        }
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
            Vector3 spawnPos = transform.position + spawnDir * (GetComponent<CircleCollider2D>().radius - 0.1f); // Radius is just the distance away from the point

            /* Now spawn */
            GameObject dancer = Instantiate(dancerPrefab, spawnPos, Quaternion.identity) as GameObject;
            dancer.transform.parent = transform;

            dancer.transform.Rotate(new Vector3(0, 0, radians * Mathf.Rad2Deg + 90));

            dancer.transform.position = new Vector3(dancer.transform.position.x, dancer.transform.position.y, 0);
            dancers.Add(dancer.transform);
        }
    }


}
