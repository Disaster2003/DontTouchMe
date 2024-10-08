using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // nullチェック
        if (collision == null)
        {
            Debug.Log("敵が存在しません");
            return;
        }

        // 敵との衝突時の処理
        if (collision.name.Contains("Enemy"))
        {
            PlayerComponent player = GetComponentInParent<PlayerComponent>();

            // 攻撃
            if (player.state_player == PlayerComponent.STATE_PLAYER.ATTACK)
            {
                collision.GetComponent<EnemyComponent>().Dead();
            }
        }
    }
}
