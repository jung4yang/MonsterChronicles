using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject[] naturalPrefabs;  // ����, Ǯ �� ���� ������ ������
    public int numberOfSpawns = 10;      // ��ȯ�� ������ ����
    public float spawnRadius = 50f;      // �������� ��ȯ�� �ݰ�
    public Transform parentObject;       // ��ȯ�� ������Ʈ�� �θ� ��ü

    void Start()
    {
        SpawnNaturalObjects();
    }

    void SpawnNaturalObjects()
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            Vector3 randomPosition = GetRandomPositionOnGround();

            if (randomPosition != Vector3.zero)
            {
                // �����ϰ� ������ �ڿ��� �������� ��ȯ�ϰ� �θ� ����
                GameObject prefabToSpawn = naturalPrefabs[Random.Range(0, naturalPrefabs.Length)];
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
                spawnedPrefab.transform.SetParent(parentObject); // �θ� ��ü�� ����
            }
        }
    }

    // Ground �±׸� ���� ������Ʈ ������ ���� ��ġ ���
    Vector3 GetRandomPositionOnGround()
    {
        float randomX = Random.Range(-spawnRadius, spawnRadius);
        float randomZ = Random.Range(-spawnRadius, spawnRadius);
        Vector3 randomPosition = new Vector3(randomX, 0, randomZ);

        RaycastHit hit;
        // Raycast�� �ش� ��ġ���� "Ground" �±׸� ���� ������Ʈ Ȯ��
        if (Physics.Raycast(randomPosition + Vector3.up * 1000, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Ground"))  // Ground �±׸� ���� ������Ʈ�� üũ
            {
                return hit.point; // Ground ���� ��Ȯ�� ��ġ ��ȯ
            }
        }

        return Vector3.zero; // "Ground" �±װ� �ƴ� ������Ʈ ������ ��ȯ���� ����
    }
}
