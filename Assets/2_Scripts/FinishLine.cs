using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public ParticleSystem finishEffect;
    public AudioSource finishSound;
    public GameTimer timer;
    public FinishLineUI finishLineUI;

    public SurfaceEffector2D surfaceEffectorToDisable;

    private bool finished = false;
    private RiderController riderController;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            riderController = player.GetComponent<RiderController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (finished) return;

        if (other.CompareTag("Player"))
        {
            finished = true;
            GameManager.isGameFinished = true;

            if (finishEffect != null)
            {
                Instantiate(finishEffect, transform.position, Quaternion.identity);
            }

            if (riderController != null)
            {
                riderController.DisableControl();
            }

            if (surfaceEffectorToDisable != null)
            {
                surfaceEffectorToDisable.enabled = false; // SurfaceEffector2D 비활성화
            }

            if (finishSound != null)
            {
                finishSound.Play();
            }

            if (finishLineUI != null && timer != null)
            {
                finishLineUI.ShowFinishPanel(timer.GetElapsedTime());
            }
        }
    }
}
