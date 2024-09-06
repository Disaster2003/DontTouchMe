using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerComponent : MonoBehaviour
{
    public static PlayerComponent instance;

    [SerializeField] float hp;
    private float hpMax;

    /// <summary>
    /// �v���C���[�̏��
    /// </summary>
    public enum STATE_PLAYER
    {
        RUN,
        ATTACK,
        DAMAGE,
    }
    public STATE_PLAYER state_player;
    [SerializeField] Sprite[] run;
    /// <summary>
    /// �v���C���[�̍U�����
    /// </summary>
    private enum STATE_PLAYER_ATTACK
    {
        PUNCH,
        KICK,
        HEADBUTT,
        ICE1,
        ICE2,
        ROTATION,
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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hpMax = hp;
        state_player = STATE_PLAYER.RUN;
        state_player_attack = STATE_PLAYER_ATTACK.PUNCH;
        attackSpriteArray = new Sprite[]{ punch[0], kick[0], headbutt[0], ice1[0], ice2[0], attack_rotation[0] };
        timer = 0.1f;
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
                // �U���Ԑ�
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state_player = STATE_PLAYER.ATTACK;
                    int tmp = Random.Range(0, System.Enum.GetValues(typeof(STATE_PLAYER_ATTACK)).Length);
                    state_player_attack = (STATE_PLAYER_ATTACK)tmp;
                    GetComponent<SpriteRenderer>().sprite = attackSpriteArray[tmp];
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
        timer -= Time.deltaTime;
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
                if (GetComponent<SpriteRenderer>().sprite == spriteArray[i])
                    if (i == spriteArray.Length - 1)
                    {
                        // ���s��Ԃ̎n�߂ɖ߂�
                        state_player = STATE_PLAYER.RUN;
                        GetComponent<SpriteRenderer>().sprite = run[0];
                        break;
                    }
                    else
                    {
                        // ���̉摜�ɐ؂�ւ���
                        GetComponent<SpriteRenderer>().sprite = spriteArray[i + 1];
                        break;
                    }
        }
    }
}
