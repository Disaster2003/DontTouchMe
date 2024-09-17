using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] Sprite[] background;

    // Start is called before the first frame update
    void Start()
    {
        // null�`�F�b�N
        if (background == null)
        {
            Debug.Log("�w�i�摜�����ݒ�ł�");
            return;
        }

        // �Q�[���N���A���
        if (GameManager.isCleared)
        {
            GetComponent<SpriteRenderer>().sprite = background[0];
        }
        // �Q�[���I�[�o�[���
        else
        {
            GetComponent<SpriteRenderer>().sprite = background[1];
        }
    }
}
