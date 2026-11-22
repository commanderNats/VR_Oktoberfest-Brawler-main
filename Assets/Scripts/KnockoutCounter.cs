using TMPro;
using UnityEngine;

public class KnockoutCounter : MonoBehaviour
{
    public int kOutCounter;

    public TextMeshProUGUI knockoutScore;


    // Update is called once per frame
    void Update()
    {
        knockoutScore.text = kOutCounter.ToString();
    }
}
