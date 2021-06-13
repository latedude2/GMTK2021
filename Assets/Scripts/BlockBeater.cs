using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScoreSystem;

public class BlockBeater : MonoBehaviour
{

    public GameObject droppingBlockPrefab;

    public AudioClip hitSound;

    [System.NonSerialized]
    public RectTransform onBeatBlockTransform;

    private readonly Queue<DroppingBlock> droppingBlockQueue = new Queue<DroppingBlock>();

    private float targetY;
    private float bottomY;

    private float lowTopMargin;
    private float lowBottomMargin;
    private float highTopMargin;
    private float highBottomMargin;

    private AudioSource effectPlayer;

    private void Start()
    {
        onBeatBlockTransform = gameObject.transform.Find("OnBeatBlock") as RectTransform;

        RectTransform droppingBlockTransform = droppingBlockPrefab.transform as RectTransform;

        targetY = onBeatBlockTransform.position.y;
        bottomY = targetY - onBeatBlockTransform.rect.height / 2 - droppingBlockTransform.rect.height / 2;

        lowTopMargin = targetY + onBeatBlockTransform.rect.height / 2 - droppingBlockTransform.rect.height / 2 + 15;
        lowBottomMargin = targetY - onBeatBlockTransform.rect.height / 2 + droppingBlockTransform.rect.height / 2 - 15;
        highTopMargin = targetY + onBeatBlockTransform.rect.height / 2 + droppingBlockTransform.rect.height / 2;
        highBottomMargin = targetY - onBeatBlockTransform.rect.height / 2 - droppingBlockTransform.rect.height / 2;

        effectPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (droppingBlockQueue.Count > 0)
        {
            if (Input.GetKeyDown("space"))
            {
                DroppingBlock droppingBlock = droppingBlockQueue.Dequeue();
                droppingBlock.isStopped = true;

                ScoreType scoreType = ScoreManager.CalculateScore(droppingBlock.gameObject.transform.position.y, targetY, lowTopMargin, lowBottomMargin, highTopMargin, highBottomMargin);
                droppingBlock.ChangeSprite(scoreType);

                if(scoreType is ScoreType.Hit || scoreType is ScoreType.PerfectHit)
                {
                    effectPlayer.clip = hitSound;
                    effectPlayer.Play();
                }

                Destroy(droppingBlock.gameObject, 0.25f);
            }

            if (droppingBlockQueue.Count > 0 && droppingBlockQueue.Peek().transform.position.y <= bottomY)
            {
                DroppingBlock droppingBlock = droppingBlockQueue.Dequeue();
                droppingBlock.isStopped = true;

                droppingBlock.ChangeSprite(ScoreType.Miss);
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
