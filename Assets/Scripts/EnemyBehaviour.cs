
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float maxHealth = 100f;
    private float currentHealth;

    private float damage = 20f;
    
    void OnEnable()
    {
        currentHealth = maxHealth;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddKnockout();
                }
                Die();
            }
        } 
    }

    void Die()
    {
        gameObject.SetActive(false);
    }


}

