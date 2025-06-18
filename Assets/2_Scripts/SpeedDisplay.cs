using TMPro;
using UnityEngine;

public class CarSpeedDisplay : MonoBehaviour
{
    public Rigidbody2D carRb;
    public TextMeshProUGUI speedText;
    public float smoothing = 0.05f; // �������� �� �ε巯��

    private Vector2 lastPosition;
    private float smoothedSpeed;

    void Start()
    {
        lastPosition = carRb.position;
    }

    void FixedUpdate() // �� ������ ����
    {
        float distance = Mathf.Abs(carRb.position.x - lastPosition.x);
        float currentSpeed = distance / Time.fixedDeltaTime * 3.6f; // km/h ��ȯ

        smoothedSpeed = Mathf.Lerp(smoothedSpeed, currentSpeed, smoothing);

        lastPosition = carRb.position;
    }

    void Update()
    {
        speedText.text = $"Speed: {smoothedSpeed:F1} km/h";
    }
}
