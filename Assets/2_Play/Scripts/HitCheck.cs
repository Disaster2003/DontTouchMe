using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // �G�Ƃ̏Փˎ��̏���
        if (collision.name.Contains("Enemy"))
        {
            PlayerComponent player = GetComponentInParent<PlayerComponent>();

            // �U��
            if (player.state_player == PlayerComponent.STATE_PLAYER.ATTACK)
                collision.GetComponent<EnemyComponent>().Dead();
        }
    }
}
