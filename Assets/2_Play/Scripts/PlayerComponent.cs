using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerComponent : MonoBehaviour
{
    public static PlayerComponent instance;

    [SerializeField] float hp;
    private float hpMax;

    SpriteRenderer spriteRenderer;
    /// <summary>
    /// プレイヤーの状態
    /// </summary>
    public enum STATE_PLAYER
    {
        RUN,    // 走行
        ATTACK, // 攻撃
        DAMAGE, // ダメージ
    }
    public STATE_PLAYER state_player;
    [SerializeField] Sprite[] run;
    /// <summary>
    /// プレイヤーの攻撃状態
    /// </summary>
    private enum STATE_PLAYER_ATTACK
    {
        PUNCH,      // パンチ
        KICK,       // キック
        HEADBUTT,   // 頭突き
        ICE1,       // 氷攻撃
        ICE2,       // 氷攻撃
        ROTATION,   // 回転
    }
    private STATE_PLAYER_ATTACK state_player_attack;
    [SerializeField] Sprite[] punch;
    [SerializeField] Sprite[] kick;
    [SerializeField] Sprite[] headbutt;
    [SerializeField] Sprite[] ice1;
    [SerializeField] Sprite[] ice2;
    [SerializeField] Sprite[] attack_rotation;
    private Sprite[] attackSpriteArray;
    [SerializeField] Sprite[] damage;
    private float timer;

    private float intervalAttack;
    private float intervalInvincible;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton
        if (instance == null)
            instance = this;

        // 体力の最大値を設定
        hpMax = hp;

        // プレイヤーの状態の初期化
        spriteRenderer = GetComponent<SpriteRenderer>();
        state_player = STATE_PLAYER.RUN;
        state_player_attack = STATE_PLAYER_ATTACK.PUNCH;
        attackSpriteArray = new Sprite[]{ punch[0], kick[0], headbutt[0], ice1[0], ice2[0], attack_rotation[0] };
        timer = 0.1f;

        intervalInvincible = 0;
        intervalAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームオーバー
        if (hp <= 0)
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(buildIndex + 1);
            return;
        }

        switch (state_player)
        {
            case STATE_PLAYER.RUN:
                if (intervalAttack <= 0)
                    // 攻撃態勢
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        state_player = STATE_PLAYER.ATTACK;
                        int tmp = Random.Range(0, attackSpriteArray.Length);
                        state_player_attack = (STATE_PLAYER_ATTACK)tmp;
                        spriteRenderer.sprite = attackSpriteArray[tmp];
                    }

                PlayerAnimation(run);
                break;
            case STATE_PLAYER.ATTACK:
                switch (state_player_attack)
                {
                    case STATE_PLAYER_ATTACK.PUNCH:
                        PlayerAnimation(punch);
                        break;
                    case STATE_PLAYER_ATTACK.KICK:
                        PlayerAnimation(kick);
                        break;
                    case STATE_PLAYER_ATTACK.HEADBUTT:
                        PlayerAnimation(headbutt);
                        break;
                    case STATE_PLAYER_ATTACK.ICE1:
                        PlayerAnimation(ice1);
                        break;
                    case STATE_PLAYER_ATTACK.ICE2:
                        PlayerAnimation(ice2);
                        break;
                    case STATE_PLAYER_ATTACK.ROTATION:
                        PlayerAnimation(attack_rotation);
                        break;
                }
                break;
            case STATE_PLAYER.DAMAGE:
                PlayerAnimation(damage);
                break;
        }
        // 時間計測
        timer += -Time.deltaTime;
        intervalInvincible += -Time.deltaTime;
        intervalAttack += -Time.deltaTime;
    }

    /// <summary>
    /// HPバーの比率を取得する
    /// </summary>
    public float GetHPBarRatio()
    {
        return hp / hpMax;
    }

    /// <summary>
    /// プレイヤーのアニメーションを行う
    /// </summary>
    /// <param name="spriteArray">アニメーション画像</param>
    private void PlayerAnimation(Sprite[] spriteArray)
    {
        // アニメーション
        if (timer <= 0)
        {
            timer = 0.1f;
            for (int i = 0; i < spriteArray.Length; i++)
                if (spriteRenderer.sprite == spriteArray[i])
                    if (i == spriteArray.Length - 1)
                    {
                        // 走行状態の始めに戻す
                        if (state_player == STATE_PLAYER.ATTACK)
                            intervalAttack = 1.0f;
                        state_player = STATE_PLAYER.RUN;
                        spriteRenderer.sprite = run[0];
                        break;
                    }
                    else
                    {
                        // 次の画像に切り替える
                        spriteRenderer.sprite = spriteArray[i + 1];
                        break;
                    }
        }

        // 半透明
        if (0 < intervalInvincible)
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        else
            spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Enemy"))
            if (intervalInvincible <= 0)
            {
                intervalInvincible = 1.0f;
                state_player = STATE_PLAYER.DAMAGE;
                spriteRenderer.sprite = damage[0];
                hp--;
            }
    }
}
