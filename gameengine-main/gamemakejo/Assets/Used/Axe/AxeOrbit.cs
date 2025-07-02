using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeOrbit : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1200f;
    
    [SerializeField]private float duration = 0.35f; // ȸ�� ���� �ð�

    private float timer = 0f;

    void Update()
    {
        /// ���� ȸ��
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // ��� �ð� ����
        timer += Time.deltaTime;

        // Ư�� ������ �Ҹ� ���
        if (timer >= 0.1f && timer < 0.2f)
        {
            // �ֵθ��� �Ҹ� ��� (�� ���� ����ǵ��� üũ �ʿ�)
            Debug.Log("�ֵθ��� �Ҹ� ���!");
        }

        // �ð��� ������ ����
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
