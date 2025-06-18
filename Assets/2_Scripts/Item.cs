using UnityEngine;

public class Item : MonoBehaviour
{
    public ParticleSystem collectEffect; // ����Ʈ ������ ����
    public AudioClip collectSound;       // ȿ���� Ŭ�� ����
    public float destroyDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ������ ȹ�� ó��
            GameManager.instance.AddItem();

            // ����Ʈ ����
            if (collectEffect != null)
            {
                ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
                effect.Play(); 
            }

            // ���� ���
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // ������ ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
