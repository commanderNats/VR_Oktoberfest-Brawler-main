using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public int Seconds = 10;
    public int Maxseconds;
    public bool count = true;
    public TextMeshProUGUI textScore;
    public static float score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Maxseconds = Seconds;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = Seconds.ToString();

        if (count)
        {
            StartCoroutine(Lecture());
            Seconds = Seconds - 1;
        }
        if (Seconds == 0)
        {
            count = false;
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator Lecture()
    {
        count = false;
        yield return new WaitForSeconds(1);
        count = true;
    }
}
