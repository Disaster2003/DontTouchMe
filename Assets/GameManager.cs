using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// シーンの状態
    /// </summary>
    enum SCENE_STATE
    {
        TITLE = 0,   // タイトル
        EXPLAIN = 1, // 説明
        PLAY = 2,    // プレイ
        RESULT,      // 結果
    }

    public static bool isCleared = false;

    /// <summary>
    /// クリックしたら、シーン遷移
    /// </summary>
    public void OnClickNextScene()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex == (int)SCENE_STATE.RESULT)
        {
            GameManager.isCleared = false;
            SceneManager.LoadSceneAsync(0);
        }
        else
            SceneManager.LoadSceneAsync(buildIndex + 1);
    }
}
