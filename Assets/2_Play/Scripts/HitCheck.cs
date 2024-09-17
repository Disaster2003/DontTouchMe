using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // nullƒ`ƒFƒbƒN
        if (collision == null)
        {
            Debug.Log("“G‚ª‘¶İ‚µ‚Ü‚¹‚ñ");
            return;
        }

        // “G‚Æ‚ÌÕ“Ë‚Ìˆ—
        if (collision.name.Contains("Enemy"))
        {
            PlayerComponent player = GetComponentInParent<PlayerComponent>();

            // UŒ‚
            if (player.state_player == PlayerComponent.STATE_PLAYER.ATTACK)
            {
                collision.GetComponent<EnemyComponent>().Dead();
            }
        }
    }
}
