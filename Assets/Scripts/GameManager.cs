using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  
    public static GameManager Instance { get; private set; }

    public string mainMenuScene = "MenuScene";
    public string creditsScene = "CreditsScene";
    public string gameScene = "OKB_BarMap";
    public string winScene = "WinStateScene";
    public string failScene = "FailStateScene";


    public float elapsedTime = 0f;
    public int knockouts = 0;
    private bool isTimerRunning = false;

    void Awake()
    {
        // only ONE GameManager alive 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void AddKnockout()
    {
        knockouts++;
    }

    public void LoadMainMenu()
    {
        ResetStats();
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void StartOrReplayGame()
    {
        ResetStats();
        isTimerRunning = true;
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void WinGame()
    {
        isTimerRunning = false;
        SceneManager.LoadScene(winScene); 
        ResetStats();
    }
    
    public void LoseGame()
    {
        isTimerRunning = false;
        SceneManager.LoadScene(failScene);
        
    }

    public void ResetStats()
    {
        isTimerRunning = false;
        elapsedTime = 0f;
        knockouts = 0;
    }
}
