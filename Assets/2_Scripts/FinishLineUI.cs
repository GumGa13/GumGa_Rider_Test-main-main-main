using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLineUI : MonoBehaviour
{
    public GameObject finishPanel;
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI fastestTimeText;

    private bool panelShown = false;

    void Start()
    {
        finishPanel.SetActive(false);
    }

    public void ShowFinishPanel(float time)
    {
        if (panelShown) return;
        panelShown = true;

        finishPanel.SetActive(true);
        currentTimeText.text = "Current Time: " + FormatTime(time);

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (time < bestTime)
        {
            bestTime = time;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        fastestTimeText.text = "Fastest Time: " + FormatTime(bestTime);
    }

    private string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int milliseconds = Mathf.FloorToInt((t * 1000f) % 1000f);
        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    public void OnRetryClicked()
    {
        BGMManager.instance.StopBGM();
        BGMManager.instance.PlayBGM();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
