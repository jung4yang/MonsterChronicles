using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    Rigidbody rb;
    public WeaponData weaponData;  // WeaponData Ÿ���� ����
    private int pen;  // ����� ����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // weaponData�� ArrowData�� ��쿡�� ������� �޾ƿͼ� pen�� �ʱ�ȭ
        if (weaponData is ArrowData arrowData)
        {
            pen = arrowData.penetration;  // ArrowData���� ����� �� �޾ƿ���
        }

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

    // �浹 ó��
    void OnTriggerEnter(Collider coll)
    {
        // ù ��° �浹���� ���ظ� �ְ� ����
        if (coll.CompareTag("Monster"))
        {
        var monster = coll.gameObject.GetComponentInParent<MonsterCtrl>();  // �θ𿡼� MonsterCtrl�� ã��

        if (monster == null)
        {
            // �θ� ���� �� �ֻ��� ������Ʈ���� ���� ã��
            monster = coll.gameObject.GetComponent<MonsterCtrl>();
            // pen�� 0 �����̸� �߻�ü ����
            if (pen <= 0)
            {
                Destroy(gameObject);  // pen�� 0 ������ �� �߻�ü ����
            }
            else
            {
                pen--;  // ���� �� pen�� 1 ���ҽ��� ������� ����
            }
        }

        if (monster != null)
        {
            monster.takeDamage(weaponData.attackPower);
            // pen�� 0 �����̸� �߻�ü ����
            if (pen <= 0)
            {
                Destroy(gameObject);  // pen�� 0 ������ �� �߻�ü ����
            }
            else
            {
                pen--;  // ���� �� pen�� 1 ���ҽ��� ������� ����
            }
        }
        else
        {
            Debug.Log("MonsterCtrl not found");
        }
    }
        else if (coll.CompareTag("Boss"))
        {
            var b_monster = coll.gameObject.GetComponent<BossMonster>();
            if (b_monster != null)
            {
                b_monster.takeDamage(weaponData.attackPower);

                // pen�� 0 �����̸� �߻�ü ����
                if (pen <= 0)
                {
                    Destroy(gameObject);  // pen�� 0 ������ �� �߻�ü ����
                }
                else
                {
                    pen--;  // ���� �� pen�� 1 ���ҽ��� ������� ����
                }
            }
        }
    }
}
