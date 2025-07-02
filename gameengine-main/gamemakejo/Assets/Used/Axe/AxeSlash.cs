using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSlash : MonoBehaviour
{
    public WeaponData weaponData;
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
            b_monster.takeDamage(weaponData.attackPower);
        }
    } 
}
