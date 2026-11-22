using UnityEngine;

public class WinStateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player") || other.GetComponentInChildren<Camera>() != null)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.WinGame();
            }
        }
            
    }
}