using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingBlock : MonoBehaviour
{
    public static float speed = 800;
    public bool isStopped;

    private void FixedUpdate()
    {
        if (!isStopped)
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.fixedDeltaTime);
    }
}
