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
        // 현재 위치와 지난 프레임 위치의 차이를 이용해 속도 추정
        float distance = Vector3.Distance(transform.position, lastPosition);
        currentSpeed = distance / Time.deltaTime;
        lastPosition = transform.position;

        // 속도에 따라 볼륨과 피치 조절
        float volume = Mathf.Clamp01(currentSpeed / maxSpeed) * maxVolume;
        engineAudio.volume = volume;
        engineAudio.pitch = 0.8f + (volume * 0.5f);
    }
}
