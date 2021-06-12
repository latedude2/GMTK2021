using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScoreSystem;

public class BlockBeater : MonoBehaviour
{

    public GameObject droppingBlockPrefab;

    [System.NonSerialized]
    public RectTransform onBeatBlockTransform;

    private readonly Queue<DroppingBlock> droppingBlockQueue = new Queue<DroppingBlock>();

    private float targetY;
    private float bottomY;

    private float lowTopMargin;
    private float lowBottomMargin;
    private float highTopMargin;
    private float highBottomMargin;

    private void Start()
    {
        onBeatBlockTransform = gameObject.transform.Find("OnBeatBlock") as RectTransform;

        RectTransform droppingBlockTransform = droppingBlockPrefab.transform as RectTransform;

        targetY = onBeatBlockTransform.position.y;
        bottomY = droppingBlockTransform.rect.height / 2;

        lowTopMargin = targetY + onBeatBlockTransform.rect.height / 2 - droppingBlockTransform.rect.height / 2;
        lowBottomMargin = targetY - onBeatBlockTransform.rect.height / 2 + droppingBlockTransform.rect.height / 2;
        highTopMargin = targetY + onBeatBlockTransform.rect.height / 2 + droppingBlockTransform.rect.height / 2;
        highBottomMargin = targetY - onBeatBlockTransform.rect.height / 2 - droppingBlockTransform.rect.height / 2;
    }

    private void Update()
    {
        if (droppingBlockQueue.Count > 0)
        {
            if (Input.GetKeyDown("space"))
            {
                DroppingBlock droppingBlock = droppingBlockQueue.Dequeue();
                droppingBlock.isStopped = true;
                float yBlock = droppingBlock.gameObject.transform.position.y;

                ScoreType scoreType = ScoreManager.CalculateScore(yBlock, lowTopMargin, lowBottomMargin, highTopMargin, highBottomMargin);

                Image droppingBlockImage = droppingBlock.GetComponent<Image>();
                if (scoreType is ScoreType.Hit)
                    droppingBlockImage.color = Color.green;
                else if (scoreType is ScoreType.AverageHit)
                    droppingBlockImage.color = Color.yellow;
                else if(scoreType is ScoreType.Miss)
                    droppingBlockImage.color = Color.red;

                Destroy(droppingBlock.gameObject, 0.25f);
            }

            if (droppingBlockQueue.Count > 0 && droppingBlockQueue.Peek().transform.position.y <= bottomY)
            {
                DroppingBlock droppingBlock = droppingBlockQueue.Dequeue();

                Image droppingBlockImage = droppingBlock.GetComponent<Image>();
                droppingBlockImage.color = Color.red;
                ScoreManager.MissedTarget();
                Destroy(droppingBlock.gameObject, 0.25f);
            }
        }
    }

    public void CreateNewDroppingBlock()
    {
        GameObject newBlock = Instantiate(droppingBlockPrefab, transform);
        droppingBlockQueue.Enqueue(newBlock.GetComponent<DroppingBlock>());
    }
}
