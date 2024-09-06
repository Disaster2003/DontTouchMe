using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerComponent;

public class EnemyComponent : MonoBehaviour
{
    /// <summary>
    /// �G�̏��
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
        // �����z�u
        transform.position = new Vector3(10, -4, 0);

        // �摜�Ɠ����̌���
        int tmp = Random.Range(0, enemy.Length);
        GetComponent<SpriteRenderer>().sprite = enemy[tmp];
        state_enemy = (STATE_ENEMY)tmp;

        // �n�ʂ���n�܂�
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

                // �W�����v
                if (isGrounded)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody2D>().gravityScale = 1;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
                }
                // ���n
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
                // �������v���C���[�Ɋ���Ă���
                if (timer <= 0)
                {
                    timer = 3;
                    transform.position += new Vector3(-2, 0, 0);
                }
                //���Ԍv��
                timer += -Time.deltaTime;
                break;
            case STATE_ENEMY.INSTANT:
                // ��ʒ[�܂œ���
                if (timer <= 0)
                    transform.position += new Vector3(5 * -Time.deltaTime, 0, 0);
                // ���Ԍv��
                else if (transform.position.x == -5)
                    timer += -Time.deltaTime;
                // �v���C���[�̖ڂ̑O��
                else if (transform.position.x <= 8)
                {
                    timer = 5;
                    transform.position = new Vector3(-5, -4, 0);
                }
                // ��������o������
                else
                    transform.position += new Vector3(3 * -Time.deltaTime, 0, 0);
                break;
            case STATE_ENEMY.DEAD:
                // �A�j���[�V����
                if (timer <= 0)
                {
                    timer = 0.1f;
                    for (int i = 0; i < breakHeart.Length; i++)
                        if (GetComponent<SpriteRenderer>().sprite == breakHeart[i])
                            if (i == breakHeart.Length - 1)
                            {
                                // ���g��j��
                                Destroy(gameObject);
                                break;
                            }
                            else
                            {
                                // ���̉摜�ɐ؂�ւ���
                                GetComponent<SpriteRenderer>().sprite = breakHeart[i + 1];
                                break;
                            }
                }
                timer += -Time.deltaTime;
                break;
        }

        // ���g�̔j��
        if(transform.position.x <= -10)
            Destroy(gameObject);
    }

    /// <summary>
    /// ���S����
    /// </summary>
    public void Dead()
    {
        // �n�[�g�̉��o�̔j��
        if (transform.childCount != 0)
            Destroy(transform.GetChild(0).gameObject);

        // �������Z�̔j��
        if (GetComponent<Rigidbody2D>())
            Destroy(GetComponent<Rigidbody2D>());

        // ����
        state_enemy = STATE_ENEMY.DEAD;
        GetComponent<SpriteRenderer>().sprite = breakHeart[0];
        timer = 0.1f;
    }
}
