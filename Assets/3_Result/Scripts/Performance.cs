using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance : MonoBehaviour
{
    [SerializeField] Sprite[] gameclear;
    [SerializeField] Sprite[] gameover;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.1f;
        if (GameManager.isCleared)
            GetComponent<SpriteRenderer>().sprite = gameclear[0];
        else
            GetComponent<SpriteRenderer>().sprite = gameover[0];

        InvokeRepeating(nameof(PerformanceAnimation), 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        // ���Ԍv��
        timer += -Time.deltaTime;
    }

    /// <summary>
    /// �p�t�H�[�}���X�̃A�j���[�V�������s��
    /// </summary>
    private void PerformanceAnimation()
    {
        Sprite[] performanceSprites = (GameManager.isCleared) ? gameclear : gameover;

        // �A�j���[�V����
        if (timer <= 0)
        {
            timer = 0.1f;
            for (int i = 0; i < performanceSprites.Length; i++)
                if (GetComponent<SpriteRenderer>().sprite == performanceSprites[i])
                    if (i == performanceSprites.Length - 1)
                    {
                        // �n�߂ɖ߂�
                        GetComponent<SpriteRenderer>().sprite = performanceSprites[0];
                        break;
                    }
                    else
                    {
                        // ���̉摜�ɐ؂�ւ���
                        GetComponent<SpriteRenderer>().sprite = performanceSprites[i + 1];
                        break;
                    }
        }
    }
}
