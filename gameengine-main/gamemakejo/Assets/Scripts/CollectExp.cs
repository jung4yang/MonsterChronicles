using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectExp : MonoBehaviour
{
    [SerializeField] private float expRange = 5f;  // ����ġ ������Ʈ�� ������ ����
    [SerializeField] private float expSpeed = 5f;  // ����ġ ������� �ӵ�

    private PlayerCtrl playerCtrl; // �÷��̾� ��Ʈ�� ��ũ��Ʈ ����
    private Collider triggerCollider; // �ڽ� ������Ʈ�� Collider
    private SoundManager soundManager; // ���� �Ŵ��� ����

    // ����ġ ������Ʈ�� �����ϴ� ����Ʈ
    private List<Collider> experienceObjects = new List<Collider>();

    private void Start()
    {
        // �θ� ��ü�� PlayerCtrl ��ũ��Ʈ�� ã��
        playerCtrl = GetComponentInParent<PlayerCtrl>();
        soundManager = FindObjectOfType<SoundManager>();

        // null üũ
        if (playerCtrl == null)
        {
            Debug.LogWarning("PlayerCtrl not found in parent objects.");
        }

        if (soundManager == null)
        {
            Debug.LogWarning("SoundManager not found in the scene.");
        }

        // �ڽ� ������Ʈ�� Collider�� ã�� �ʱ� ���� ����
        triggerCollider = GetComponent<Collider>();
        SetColliderRange(expRange); // �ʱ� ���� ����
    }

    void Update()
    {
        // ����ġ ������Ʈ�� ������� ȹ���ϴ� ����
        CollectExperience();
    }

    private void CollectExperience()
    {
        for (int i = 0; i < experienceObjects.Count; i++)
        {
            Collider collider = experienceObjects[i];

            if (collider != null)
            {
                Vector3 direction = (transform.position - collider.transform.position).normalized;
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, expSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, collider.transform.position) < 1f)
                {
                    if (playerCtrl != null)
                    {
                        playerCtrl.AddExperience(1);
                    }

                    // ���� �Ŵ����� ���� ���� ���
                    if (soundManager != null)
                    {
                        soundManager.PlayExpCollectSound();
                    }

                    experienceObjects.RemoveAt(i);
                    Destroy(collider.gameObject);
                    Debug.Log("Exp collected!");
                    continue;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Exp") && !experienceObjects.Contains(other))
        {
            experienceObjects.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.CompareTag("Exp"))
        {
            experienceObjects.Remove(other);
        }
    }

    public void SetColliderRange(float range)
    {
        if (triggerCollider != null)
        {
            if (triggerCollider is SphereCollider sphereCollider)
            {
                sphereCollider.radius = range;
            }
            else
            {
                Debug.LogWarning("Collider is not a SphereCollider. Cannot change radius.");
            }
        }
    }
}
