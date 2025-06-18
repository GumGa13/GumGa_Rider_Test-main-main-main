using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    public float maxVolume = 0.7f;
    public float maxSpeed = 10f;

    private AudioSource engineAudio;
    private Vector3 lastPosition;
    private float currentSpeed;

    void Start()
    {
        engineAudio = GetComponent<AudioSource>();
        engineAudio.loop = true;
        engineAudio.Play();

        lastPosition = transform.position;
    }

    void Update()
    {
        // ���� ��ġ�� ���� ������ ��ġ�� ���̸� �̿��� �ӵ� ����
        float distance = Vector3.Distance(transform.position, lastPosition);
        currentSpeed = distance / Time.deltaTime;
        lastPosition = transform.position;

        // �ӵ��� ���� ������ ��ġ ����
        float volume = Mathf.Clamp01(currentSpeed / maxSpeed) * maxVolume;
        engineAudio.volume = volume;
        engineAudio.pitch = 0.8f + (volume * 0.5f);
    }
}
