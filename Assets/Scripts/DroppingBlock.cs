using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScoreSystem;

public class DroppingBlock : MonoBehaviour
{
    public static float speed = 600;
    public bool isStopped;

    public Sprite perfectHitSprite;
    public Sprite hitSprite;
    public Sprite averageHitSprite;
    public Sprite missSprite;

    private void FixedUpdate()
    {
        if (!isStopped)
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.fixedDeltaTime);
    }

    public void ChangeSprite(ScoreType scoreType)
    {
        Image droppingBlockImage = GetComponent<Image>();
        if (scoreType is ScoreType.PerfectHit)
            droppingBlockImage.sprite = perfectHitSprite;
        else if (scoreType is ScoreType.Hit)
            droppingBlockImage.sprite = hitSprite;
        else if (scoreType is ScoreType.AverageHit)
            droppingBlockImage.sprite = averageHitSprite;
        else if (scoreType is ScoreType.Miss)
            droppingBlockImage.sprite = missSprite;

    }
}
