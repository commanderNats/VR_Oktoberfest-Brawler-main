using UnityEngine;

public class SceneBridge : MonoBehaviour
{
    public void OnPlayOrReplayClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.StartOrReplayGame();
    }

    public void OnMainMenuClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LoadMainMenu();
    }

    public void OnCreditsClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LoadCredits();
    }

    public void OnQuitClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.QuitGame();
    }
}
