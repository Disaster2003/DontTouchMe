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
        // null�`�F�b�N
        if(heart == null)
        {
            Debug.Log("�n�[�g�A�j���[�V�����摜�����ݒ�ł�");
            return;
        }

        // �A�j���[�V����
        if (timer <= 0)
        {
            timer = 0.1f;
            for (int i = 0; i < heart.Length; i++)
            {
                if (GetComponent<SpriteRenderer>().sprite == heart[i])
                {
                    // �n�߂ɖ߂�
                    if (i == heart.Length - 1)
                    {
                        GetComponent<SpriteRenderer>().sprite = heart[0];
                        break;
                    }
                    // ���̉摜�ɐ؂�ւ���
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = heart[i + 1];
                        break;
                    }
                }
            }
        }
        // �A�j���[�V�����̃C���^�[�o��
        timer += -Time.deltaTime;
    }
}
