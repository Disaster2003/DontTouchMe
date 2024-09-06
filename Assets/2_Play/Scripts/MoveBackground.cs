using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 初期配置
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // 背景を左に移動
        transform.position += new Vector3(5 * -Time.deltaTime, 0, 0);

        // 画像の同じ部分に戻し、ずっと続いているように錯覚
        if (transform.position.x <= -17.25f)
            transform.position = Vector3.zero;
    }
}
