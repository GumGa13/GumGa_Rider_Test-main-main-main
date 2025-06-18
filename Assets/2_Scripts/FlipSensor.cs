using UnityEngine;
using UnityEngine.SceneManagement;

public class FlipSensor : MonoBehaviour
{
    public ParticleSystem flipEffect;
    public AudioSource flipSound;
    public float reloadDelay = 2f;

    public SurfaceEffector2D surfaceEffectorToDisable;

    private bool flipped = false;
    private RiderController riderController;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            riderController = player.GetComponent<RiderController>();
        }

        if (riderController == null)
        {
            Debug.LogWarning("FlipSensor: RiderController 찾을 수 없음!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 피니쉬라인 통과 후엔 재시작 안 함
        if (flipped || GameManager.isGameFinished) return;

        if (other.CompareTag("Ground"))
        {
            flipped = true;

            Debug.Log("FlipSensor triggered with: " + other.gameObject.name);

            if (flipEffect != null)
            {
                ParticleSystem effect = Instantiate(flipEffect, transform.position, Quaternion.identity);
                effect.Play(); 
            }

            if (flipSound != null)
            {
                flipSound.Play();
            }

            // 자동차 조작 비활성화
            if (riderController != null)
            {
                riderController.DisableControl();
            }

            // SurfaceEffector2D 비활성화
            if (surfaceEffectorToDisable != null)
            {
                surfaceEffectorToDisable.enabled = false;
            }

            Invoke(nameof(ReloadScene), reloadDelay);
        }
    }

    private void ReloadScene()
    {
        // 재확인: 게임이 종료된 상태면 재시작 금지
        if (GameManager.isGameFinished)
        {
            Debug.Log("Game finished. No reload.");
            return;
        }

        Debug.Log("Reloading scene due to flip...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
