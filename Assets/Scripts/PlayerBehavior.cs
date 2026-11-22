using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int maxHealth = 100;
    public int actualHealth;
    private int kCounter;

    void Start() {
        actualHealth = maxHealth;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            actualHealth = actualHealth - 10;

            if (actualHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.LoseGame();
        }
    }
}
