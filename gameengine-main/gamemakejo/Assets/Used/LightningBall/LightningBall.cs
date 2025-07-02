using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : MonoBehaviour
{
    public float searchRadius = 10f;  // ���� ã�� ����
    private Transform closestEnemy;    // ���� ����� ���� Transform
    private List<Transform> previousEnemies = new List<Transform>();  // ������ �����ߴ� ������ ���
    private int maxChainCount = 3;  // �ִ� ���� Ƚ��
    public float moveSpeed = 30f;  // �̵� �ӵ�
    private Rigidbody rb;  // Rigidbody ������Ʈ
    [SerializeField] private int currentChainCount = 0;  // ���� ���� Ƚ��
    public WeaponData weaponData;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbody ������Ʈ�� �����ɴϴ�.

        // weaponData�� LightningballData�� ��쿡�� ���� Ƚ���� �޾ƿͼ� �ִ� ���� Ƚ���� �ʱ�ȭ
        if (weaponData is LightningballData lightningballData)
        {
            maxChainCount = lightningballData.chainCount;  // LightningballData���� ���� Ƚ�� �� �޾ƿ���
        }
        searchRadius = weaponData.attackRange;
        // ������ ���� �̺�Ʈ�� ����
        MonsterCtrl.OnMonsterDeath += CheckMonsters;
    }

    void OnDestroy()
    {
        // ���� ����
        MonsterCtrl.OnMonsterDeath -= CheckMonsters;
    }

    void Update()
    {
        // Ÿ���� �������� �ʾҴٸ�, ���� ����� ���� ã�´�
        if (closestEnemy == null || !closestEnemy.gameObject.activeInHierarchy)
        {
            // Ÿ���� ������ų� �׾�����, ���ο� Ÿ���� ã�´�
            FindClosestEnemy();
        }

        // Ÿ���� �����Ǿ� �ִٸ�, �� Ÿ���� �����Ѵ�
        if (closestEnemy != null)
        {
            MoveTowardsEnemy();
        }
    }

    void FindClosestEnemy()
    {
        // "Monster"�� "Boss" �±׸� ���� ���� ã��
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject monster in monsters)
        {
            // �̹� Ÿ���� ���ʹ� ����
            if (previousEnemies.Contains(monster.transform))
                continue;

            float distance = Vector3.Distance(transform.position, monster.transform.position);

            // searchRadius ���� ���͸� ���
            if (distance <= searchRadius && distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = monster.transform;
            }
        }

        // ������ ���� ��� ������ �Ÿ� ��
        if (boss != null && !previousEnemies.Contains(boss.transform))
        {
            float bossDistance = Vector3.Distance(transform.position, boss.transform.position);
            if (bossDistance <= searchRadius && bossDistance < minDistance)
            {
                minDistance = bossDistance;
                nearestEnemy = boss.transform;
            }
        }

        // ���� ����� ���� ã����, �װ��� �����Ѵ�
        if (nearestEnemy != null)
        {
            closestEnemy = nearestEnemy;
            Debug.Log("���� ����� �� ����: " + closestEnemy.name);
        }
        else
        {
            // ��ȿ�� Ÿ���� ���� ���� Ƚ���� ���� �ִٸ� �������� �ı�
            if (currentChainCount < maxChainCount)
            {
                Destroy(gameObject);
                Debug.Log("��ȿ�� Ÿ���� �����Ƿ� LightningBall �������� �ı��մϴ�.");
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("��ȿ�� Ÿ���� �����ϴ�. �׷��� ���Ⱑ ����Ǿ����ϴ�.");
            }
        }
    }

    void MoveTowardsEnemy()
    {
        // ���� closestEnemy�� null�̸�, �� �̻� ������ ���� �����Ƿ� �Լ��� ����
        if (closestEnemy == null)
            return;

        // ���µ� ���� ���� �̵�
        Vector3 direction = (closestEnemy.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;  // Rigidbody�� �̿��� ���������� �̵�
    }

    void OnTriggerEnter(Collider other)
    {
        // closestEnemy�� null�� �ƴϰ� "Monster" �Ǵ� "Boss" �±׸� ���� ���� �浹 ��, ���� ���µ� ������ ��
        if (closestEnemy != null && (other.CompareTag("Monster") || other.CompareTag("Boss")) && other.transform == closestEnemy)
        {
            // �浹�� ���� ���� ���� ����
            Debug.Log(closestEnemy.name + "���� ��ҽ��ϴ�. ������ �����մϴ�.");

            // ���ظ� ����
            if (other.CompareTag("Monster"))
            {
                var monster = closestEnemy.gameObject.GetComponentInParent<MonsterCtrl>();  // �θ𿡼� MonsterCtrl�� ã��

                if (monster == null)
                {
                    // �θ� ���� �� �ֻ��� ������Ʈ���� ���� ã��
                    monster = closestEnemy.gameObject.GetComponent<MonsterCtrl>();
                }

                if (monster != null)
                {
                    monster.takeDamage(weaponData.attackPower);
                }
                else
                {
                    Debug.Log("MonsterCtrl not found");
                }
            }
            else if (other.CompareTag("Boss"))
            {
                BossMonster bossCtrl = closestEnemy.GetComponent<BossMonster>();
                if (bossCtrl != null)
                {
                    bossCtrl.takeDamage(weaponData.attackPower);
                    Debug.Log(closestEnemy.name + "���� ���ظ� �������ϴ�.");
                }
            }

            // ù ��° Ÿ���� ���
            previousEnemies.Add(closestEnemy);  // �̹� Ÿ���� ���
            closestEnemy = null;  // �� �̻� ������ ���� �����Ƿ� null�� ����

            // ���� Ƚ�� üũ
            currentChainCount++;

            // �ִ� ���� Ƚ���� �����ߴٸ�, �� �̻� Ÿ���� ã�� �ʴ´�
            if (currentChainCount >= maxChainCount)
            {
                Debug.Log("�ִ� ���� Ƚ���� �����߽��ϴ�.");
                Destroy(gameObject);
            }

            // ���ο� Ÿ���� ã�´�
            FindClosestEnemy();
        }
    }

    // ���Ͱ� ���� ������ ���� ���͸� Ȯ���ϰ�, ��� ���Ͱ� �׾����� �ı�
    void CheckMonsters()
    {
        // "Monster" �±׸� ���� ������Ʈ�� ã�´�
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        // ���Ϳ� ������ ������ �������� �ı�
        if ((monsters.Length == 0 && boss == null) || currentChainCount >= maxChainCount)
        {
            Destroy(gameObject);  // ���� ��ü(������)�� �ı�
            Debug.Log("���Ϳ� ������ ���ų� �ִ� ���� Ƚ���� ���������Ƿ� LightningBall �������� �ı��մϴ�.");
        }
    }
}
