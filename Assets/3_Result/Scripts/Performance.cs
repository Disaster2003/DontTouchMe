using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance : MonoBehaviour
{
    [SerializeField] Sprite[] gameclear;
    [SerializeField] Sprite[] gameover;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.1f;
        if (GameManager.isCleared)
            GetComponent<SpriteRenderer>().sprite = gameclear[0];
        else
            GetComponent<SpriteRenderer>().sprite = gameover[0];

        InvokeRepeating(nameof(PerformanceAnimation), 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        // 時間計測
        timer += -Time.deltaTime;
    }

    /// <summary>
    /// パフォーマンスのアニメーションを行う
    /// </summary>
    private void PerformanceAnimation()
    {
        Sprite[] performanceSprites = (GameManager.isCleared) ? gameclear : gameover;

        // アニメーション
        if (timer <= 0)
        {
            timer = 0.1f;
            for (int i = 0; i < performanceSprites.Length; i++)
                if (GetComponent<SpriteRenderer>().sprite == performanceSprites[i])
                    if (i == performanceSprites.Length - 1)
                    {
                        // 始めに戻す
                        GetComponent<SpriteRenderer>().sprite = performanceSprites[0];
                        break;
                    }
                    else
                    {
                        // 次の画像に切り替える
                        GetComponent<SpriteRenderer>().sprite = performanceSprites[i + 1];
                        break;
                    }
        }
    }
}
