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
            Debug.LogWarning("FlipSensor: RiderController ã�� �� ����!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �ǴϽ����� ��� �Ŀ� ����� �� ��
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

            // �ڵ��� ���� ��Ȱ��ȭ
            if (riderController != null)
            {
                riderController.DisableControl();
            }

            // SurfaceEffector2D ��Ȱ��ȭ
            if (surfaceEffectorToDisable != null)
            {
                surfaceEffectorToDisable.enabled = false;
            }

            Invoke(nameof(ReloadScene), reloadDelay);
        }
    }

    private void ReloadScene()
    {
        // ��Ȯ��: ������ ����� ���¸� ����� ����
        if (GameManager.isGameFinished)
        {
            Debug.Log("Game finished. No reload.");
            return;
        }

        Debug.Log("Reloading scene due to flip...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
