using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] heart;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        // nullチェック
        if(heart == null)
        {
            Debug.Log("ハートアニメーション画像が未設定です");
            return;
        }

        // アニメーション
        if (timer <= 0)
        {
            timer = 0.1f;
            for (int i = 0; i < heart.Length; i++)
            {
                if (GetComponent<SpriteRenderer>().sprite == heart[i])
                {
                    // 始めに戻す
                    if (i == heart.Length - 1)
                    {
                        GetComponent<SpriteRenderer>().sprite = heart[0];
                        break;
                    }
                    // 次の画像に切り替える
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = heart[i + 1];
                        break;
                    }
                }
            }
        }
        // アニメーションのインターバル
        timer += -Time.deltaTime;
    }
}
