using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public float disableDuration = 2.0f; 

    private Collider2D spikeCollider;
    private bool isDisabled = false;

    private void Start()
    {
        spikeCollider = GetComponent<Collider2D>();
        if (spikeCollider == null)
        {
            Debug.LogError("Collider2D not found on the spike object.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDisabled && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                StartCoroutine(DisableColliderTemporarily());
            }
        }
    }

    private System.Collections.IEnumerator DisableColliderTemporarily()
    {
        // Disable the collider
        spikeCollider.enabled = false;
        isDisabled = true;

        // Wait for the disable duration
        yield return new WaitForSeconds(disableDuration);

        // Re-enable the collider
        spikeCollider.enabled = true;
        isDisabled = false;
    }
}
