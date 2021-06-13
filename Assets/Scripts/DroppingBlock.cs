using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScoreSystem;

public class DroppingBlock : MonoBehaviour
{
    public static float speed = 400;
    public bool isStopped;

    public Sprite perfectHitSprite;
    public Sprite hitSprite;
    public Sprite averageHitSprite;
    public Sprite missSprite;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (!isStopped)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - speed * Time.fixedDeltaTime);
    }

    public void ChangeSprite(ScoreType scoreType)
    {
        Image droppingBlockImage = GetComponent<Image>();
        if (scoreType is ScoreType.PerfectHit)
        {
            droppingBlockImage.sprite = perfectHitSprite;
            Vector3 scale = droppingBlockImage.rectTransform.localScale;
            droppingBlockImage.rectTransform.localScale = new Vector3(scale.x, scale.y * 1.6f);
        }
        else if (scoreType is ScoreType.Hit)
        {
            droppingBlockImage.sprite = hitSprite;
        }
        else if (scoreType is ScoreType.AverageHit)
            droppingBlockImage.sprite = averageHitSprite;
        else if (scoreType is ScoreType.Miss)
            droppingBlockImage.sprite = missSprite;

    }
}
