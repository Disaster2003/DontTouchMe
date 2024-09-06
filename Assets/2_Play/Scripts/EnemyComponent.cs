using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerComponent;

public class EnemyComponent : MonoBehaviour
{
    /// <summary>
    /// 敵の状態
    /// </summary>
    public enum STATE_ENEMY
    {
        NORMAL,
        JUMP,
        QUICK,
        CLEAR,
        INSTANT,
        DEAD,
    }
    public STATE_ENEMY state_enemy;
    [SerializeField] Sprite[] enemy;

    private bool isGrounded;

    private float timer;

    [SerializeField] Sprite[] breakHeart;

    // Start is called before the first frame update
    void Start()
    {
        // 初期配置
        transform.position = new Vector3(10, -4, 0);

        // 画像と動きの決定
        int tmp = Random.Range(0, enemy.Length);
        GetComponent<SpriteRenderer>().sprite = enemy[tmp];
        state_enemy = (STATE_ENEMY)tmp;

        // 地面から始まる
        isGrounded = true;

        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state_enemy)
        {
            case STATE_ENEMY.NORMAL:
                transform.position += new Vector3(5 * -Time.deltaTime, 0, 0); 
                break;
            case STATE_ENEMY.JUMP:
                transform.position += new Vector3(3 * -Time.deltaTime, 0, 0);

                // ジャンプ
                if (isGrounded)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody2D>().gravityScale = 1;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
                }
                // 着地
                else if(transform.position.y <= -4)
                {
                    isGrounded = true;
                    transform.position = new Vector3(transform.position.x, -4, 0);
                    GetComponent<Rigidbody2D>().gravityScale = 0;
                }
                break;
            case STATE_ENEMY.QUICK:
                transform.position += new Vector3(10 * -Time.deltaTime, 0, 0);
                break;
            case STATE_ENEMY.CLEAR:
                // 少しずつプレイヤーに寄ってくる
                if (timer <= 0)
                {
                    timer = 3;
                    transform.position += new Vector3(-2, 0, 0);
                }
                //時間計測
                timer += -Time.deltaTime;
                break;
            case STATE_ENEMY.INSTANT:
                // 画面端まで動く
                if (timer <= 0)
                    transform.position += new Vector3(5 * -Time.deltaTime, 0, 0);
                // 時間計測
                else if (transform.position.x == -5)
                    timer += -Time.deltaTime;
                // プレイヤーの目の前へ
                else if (transform.position.x <= 8)
                {
                    timer = 5;
                    transform.position = new Vector3(-5, -4, 0);
                }
                // 少し顔を出させる
                else
                    transform.position += new Vector3(3 * -Time.deltaTime, 0, 0);
                break;
            case STATE_ENEMY.DEAD:
                // アニメーション
                if (timer <= 0)
                {
                    timer = 0.1f;
                    for (int i = 0; i < breakHeart.Length; i++)
                        if (GetComponent<SpriteRenderer>().sprite == breakHeart[i])
                            if (i == breakHeart.Length - 1)
                            {
                                // 自身を破壊
                                Destroy(gameObject);
                                break;
                            }
                            else
                            {
                                // 次の画像に切り替える
                                GetComponent<SpriteRenderer>().sprite = breakHeart[i + 1];
                                break;
                            }
                }
                timer += -Time.deltaTime;
                break;
        }

        // 自身の破壊
        if(transform.position.x <= -10)
            Destroy(gameObject);
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Dead()
    {
        // ハートの演出の破壊
        if (transform.childCount != 0)
            Destroy(transform.GetChild(0).gameObject);

        // 物理演算の破壊
        if (GetComponent<Rigidbody2D>())
            Destroy(GetComponent<Rigidbody2D>());

        // 死へ
        state_enemy = STATE_ENEMY.DEAD;
        GetComponent<SpriteRenderer>().sprite = breakHeart[0];
        timer = 0.1f;
    }
}
