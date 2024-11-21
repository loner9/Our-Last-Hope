using UnityEngine;

public class EnemyMeleeHit : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;

    public void TakeDamage()
    {
        ShowHitEffect();
    }

    private void ShowHitEffect()
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected with: " + other.gameObject.name); // Debug log

        if (other.gameObject.CompareTag("Melee") && WeaponManager.isMeleeAttackActive)
        {
            Debug.Log("Hit by Melee Weapon"); // Debug log
            TakeDamage();
        }
    }
}
