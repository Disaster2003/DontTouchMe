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
        // �A�j���[�V����
        if (timer <= 0)
        {
            timer = 0.1f;
            for (int i = 0; i < heart.Length; i++)
                if (GetComponent<SpriteRenderer>().sprite == heart[i])
                    if (i == heart.Length - 1)
                    {
                        // �n�߂ɖ߂�
                        GetComponent<SpriteRenderer>().sprite = heart[0];
                        break;
                    }
                    else
                    {
                        // ���̉摜�ɐ؂�ւ���
                        GetComponent<SpriteRenderer>().sprite = heart[i + 1];
                        break;
                    }
        }
        // ���Ԍv��
        timer += -Time.deltaTime;
    }
}
