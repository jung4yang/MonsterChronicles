using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody rb;

    public WeaponData weaponData;  // WeaponData�� �����Ͽ� ���� ������ ���ݷ� �ޱ�
    [SerializeField] private GameObject explosionPrefab; // ���� ����Ʈ ������

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // �߻� �������� ���� �־� �Ѿ��� �߻�
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody not found on bullet!");
        }

        // ���� �ð��� ������ �Ѿ��� ��������� ����
        Destroy(gameObject, 3f);
    }

    // �浹�� �߻����� �� ���� ȿ���� ����
    void OnTriggerEnter(Collider coll)
    {
        // ���� �浹���� ���� ����
        if (coll.CompareTag("Monster") || coll.CompareTag("Boss"))
        {
            // ���� ����Ʈ ����
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // ���� ��ü�� ũ�� ����
            ExplosionEffect explosionEffect = explosion.GetComponent<ExplosionEffect>();
            if (explosionEffect != null)
            {
                explosionEffect.SetExplosionSize(weaponData.attackRange); // attackRange ���� ����
            }

            // ���� �� ���鿡�� ���ظ� �ֱ�
            ApplyDamage();

            // �߻�ü ����
            Destroy(gameObject);
        }
    }


    private void ApplyDamage()
    {
        float explosionRadius = weaponData.attackRange;

        // LayerMask targetLayer = LayerMask.GetMask("Monster", "Boss"); // ��� ���̾� ���� (���� ���� ����)
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius/2f /*, targetLayer */);

        foreach (Collider hit in hitEnemies)
        {
            if (hit.CompareTag("Monster"))
            {
                var monster = hit.gameObject.GetComponentInParent<MonsterCtrl>();  // �θ𿡼� MonsterCtrl�� ã��

                if (monster == null)
                {
                    // �θ� ���� �� �ֻ��� ������Ʈ���� ���� ã��
                    monster = hit.gameObject.GetComponent<MonsterCtrl>();
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
            else if (hit.CompareTag("Boss"))
            {
                var boss = hit.GetComponent<BossMonster>();
                if (boss != null)
                {
                    boss.takeDamage(weaponData.attackPower);  // WeaponData�� ���ݷ����� ���� �ֱ�
                }
            }
        }
    }
}