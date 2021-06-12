using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject StragglerPrefab;
    public GameObject ObstaclePrefab;
    public Transform circle;
    public float spawnTime = 20f;
    // Start is called before the first frame update
    void Start()
    {
        circle = GameObject.Find("Circle").transform;
        StartCoroutine(SpawnObject(ObstaclePrefab, spawnTime));
        StartCoroutine(SpawnObject(ObstaclePrefab, spawnTime));
        StartCoroutine(SpawnObject(StragglerPrefab, spawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObject(GameObject unit, float repeatRate)
    {
        while (true)
        {
            float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            while(Vector3.Distance(new Vector3(spawnX, spawnY, 0), circle.position) < circle.GetComponent<CircleCollider2D>().radius + 0.5f)
            {
                spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            }

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            Instantiate(unit, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(repeatRate);
        }
    }
}
