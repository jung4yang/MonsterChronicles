using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    public WeaponData weaponData;  // WeaponData Ÿ���� ���� (SwordData ��)
    private Collider slashCollider;
    private ParticleSystem slashParticles;

    // Start is called before the first frame update
    void Start()
    {
        // �ݶ��̴��� ��ƼŬ �ý����� ã��
        slashCollider = GetComponent<Collider>();

        // �ڽ� ������Ʈ���� ��ƼŬ �ý����� ã��
        slashParticles = GetComponentInChildren<ParticleSystem>();

        // ���� ������ ����Ͽ� �ݶ��̴� ũ��� ��ƼŬ ũ�� ����
        SetAttackRange(weaponData.attackRange);

        // ���� �ð��� ������ ���� ������Ʈ ����
        Destroy(gameObject, 0.95f);
    }

    // ���� ������ ����Ͽ� �ݶ��̴��� ��ƼŬ ũ�� ����
    void SetAttackRange(float range)
    {
        if (slashCollider != null)
        {
            // �ݶ��̴� ũ�� ���� (attackRange�� ���)
            if (slashCollider is BoxCollider boxCollider)
            {
                boxCollider.size = new Vector3(range, boxCollider.size.y, boxCollider.size.z);
            }
            else if (slashCollider is SphereCollider sphereCollider)
            {
                sphereCollider.radius = range;
            }
        }

        if (slashParticles != null)
        {
            // ��ƼŬ ũ�� ���� (attackRange�� ���)
            var main = slashParticles.main;
            main.startSize = range;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Monster"))
        {
            var monster = coll.gameObject.GetComponentInParent<MonsterCtrl>();  // �θ𿡼� MonsterCtrl�� ã��

            if (monster == null)
            {
                // �θ� ���� �� �ֻ��� ������Ʈ���� ���� ã��
                monster = coll.gameObject.GetComponent<MonsterCtrl>();
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
        else if (coll.CompareTag("Boss"))
        {
            var b_monster = coll.gameObject.GetComponent<BossMonster>();
            if (b_monster != null)
            {
                b_monster.takeDamage(weaponData.attackPower);
            }
        }
    }
}
