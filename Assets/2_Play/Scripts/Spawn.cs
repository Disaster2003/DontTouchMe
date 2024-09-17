using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // null�`�F�b�N
        if(enemy == null)
        {
            Debug.Log("�G�̃I�u�W�F�N�g�����ݒ�ł�");
            return;
        }

        // �G�̃X�|�[��
        if(timer <= 0)
        {
            timer = Random.Range(2.5f, 4);
            Instantiate(enemy);
        }
        // ���Ԍv��
        timer -= Time.deltaTime;
    }
}
