using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private const float POSITION_END = -17.25f;

    // Start is called before the first frame update
    void Start()
    {
        // �����z�u
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // �w�i�����Ɉړ�
        transform.position += new Vector3(5 * -Time.deltaTime, 0, 0);

        // �摜�̓��������ɖ߂��A�����Ƒ����Ă���悤�ɍ��o
        if (transform.position.x <= POSITION_END)
        {
            transform.position = Vector3.zero;
        }
    }
}
