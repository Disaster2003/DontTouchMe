using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 30;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームクリア
        if (timer <= 0)
        {
            GameManager.isCleared = true;
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(buildIndex + 1);
        }
        // 時間計測
        timer -= Time.deltaTime;
        GetComponent<Text>().text = timer.ToString("f1") + "s";
    }
}
