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
    /// �v���C���[�̏��
    /// </summary>
    public enum STATE_PLAYER
    {
        RUN,    // ���s
        ATTACK, // �U��
        DAMAGE, // �_���[�W
    }
    public STATE_PLAYER state_player;
    [SerializeField] Sprite[] run;
    /// <summary>
    /// �v���C���[�̍U�����
    /// </summary>
    private enum STATE_PLAYER_ATTACK
    {
        PUNCH,      // �p���`
        KICK,       // �L�b�N
        HEADBUTT,   // ���˂�
        ICE1,       // �X�U��
        ICE2,       // �X�U��
        ROTATION,   // ��]
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

        // �̗͂̍ő�l��ݒ�
        hpMax = hp;

        // �v���C���[�̏�Ԃ̏�����
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
        // �Q�[���I�[�o�[
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
                    // �U���Ԑ�
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
        // ���Ԍv��
        timer += -Time.deltaTime;
        intervalInvincible += -Time.deltaTime;
        intervalAttack += -Time.deltaTime;
    }

    /// <summary>
    /// HP�o�[�̔䗦���擾����
    /// </summary>
    public float GetHPBarRatio()
    {
        return hp / hpMax;
    }

    /// <summary>
    /// �v���C���[�̃A�j���[�V�������s��
    /// </summary>
    /// <param name="spriteArray">�A�j���[�V�����摜</param>
    private void PlayerAnimation(Sprite[] spriteArray)
    {
        // �A�j���[�V����
        if (timer <= 0)
        {
            timer = 0.1f;
            for (int i = 0; i < spriteArray.Length; i++)
                if (spriteRenderer.sprite == spriteArray[i])
                    if (i == spriteArray.Length - 1)
                    {
                        // ���s��Ԃ̎n�߂ɖ߂�
                        if (state_player == STATE_PLAYER.ATTACK)
                            intervalAttack = 1.0f;
                        state_player = STATE_PLAYER.RUN;
                        spriteRenderer.sprite = run[0];
                        break;
                    }
                    else
                    {
                        // ���̉摜�ɐ؂�ւ���
                        spriteRenderer.sprite = spriteArray[i + 1];
                        break;
                    }
        }

        // ������
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
