using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float expandDuration = 0.5f; // ���� Ȯ�� �ð�
    [SerializeField] private float holdDuration = 0.5f; // ���� �ð�

    private float currentTime = 0f;
    private Vector3 initialScale;
    private Vector3 targetScale;
    private bool expanding = true;

    // ���� ������ ���� ũ�� ����
    public void SetExplosionSize(float attackRange)
    {
        targetScale = Vector3.one * attackRange;
    }

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (expanding)
        {
            // Ȯ�� ��
            currentTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentTime / expandDuration);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            if (currentTime >= expandDuration)
            {
                expanding = false; // Ȯ�� �Ϸ�
                currentTime = 0f; // �ð� �ʱ�ȭ
            }
        }
        else
        {
            // ���� �� ����
            currentTime += Time.deltaTime;
            if (currentTime >= holdDuration)
            {
                Destroy(gameObject);
            }
        }
    }
}
