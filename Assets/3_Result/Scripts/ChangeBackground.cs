using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] Sprite[] background;

    // Start is called before the first frame update
    void Start()
    {
        // nullチェック
        if (background == null)
        {
            Debug.Log("背景画像が未設定です");
            return;
        }

        // ゲームクリア画面
        if (GameManager.isCleared)
        {
            GetComponent<SpriteRenderer>().sprite = background[0];
        }
        // ゲームオーバー画面
        else
        {
            GetComponent<SpriteRenderer>().sprite = background[1];
        }
    }
}
