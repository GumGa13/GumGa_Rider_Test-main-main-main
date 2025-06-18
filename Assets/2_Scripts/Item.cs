using UnityEngine;

public class Item : MonoBehaviour
{
    public ParticleSystem collectEffect; // 이펙트 프리팹 연결
    public AudioClip collectSound;       // 효과음 클립 연결
    public float destroyDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 아이템 획득 처리
            GameManager.instance.AddItem();

            // 이펙트 생성
            if (collectEffect != null)
            {
                ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
                effect.Play(); 
            }

            // 사운드 재생
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // 아이템 오브젝트 제거
            Destroy(gameObject);
        }
    }
}
