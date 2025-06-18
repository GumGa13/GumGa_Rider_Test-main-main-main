using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float elapsedTime;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);
        timeText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    public void Stop()
    {
        isRunning = false;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
