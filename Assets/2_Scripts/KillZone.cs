using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 씬 다시 불러오기 (재시작)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
