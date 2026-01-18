using UnityEngine;
using System.Collections;

public class SpikeKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        StartCoroutine(PlayDeathAndDestroy(collision.gameObject));
    }

    private IEnumerator PlayDeathAndDestroy(GameObject player)
    {
        Animator anim = player.GetComponent<Animator>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        Collider2D col = player.GetComponent<Collider2D>();

        // Stop physics & collisions immediately
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.simulated = false;
        }

        if (col != null)
            col.enabled = false;

        // ?? FORCE animation (ignores transitions, parameters, states)
        if (anim != null)
        {
            anim.enabled = true;
            anim.SetTrigger("Death");

        }

        // Wait EXACT animation time
        yield return new WaitForSeconds(1.3f);

        Destroy(player);
    }
}
