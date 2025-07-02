using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSword", menuName = "Weapon/SwordData")]
public class SwordData : WeaponData
{
    // Start is called before the first frame update
    public override void Upgrade()
    {
        upgradeCount++;

        // ���׷��̵� ȿ�� ����
        switch (upgradeCount)
        {
            case 1:
                attackPower += 1f; // ù ��° ���׷��̵�: ���ݷ� ����, ���� ���� ����
                attackRange += 1f;
                Debug.Log($"{weaponName}�� ���ݷ��� {attackPower}�� �����߽��ϴ�.");
                break;
            case 2:
                attackSpeed += 0.5f; // �� ��° ���׷��̵�: ���ݼӵ� ����, ���� ���� ����
                attackRange += 1f;
                Debug.Log($"{weaponName}�� ���ݼӵ��� {attackSpeed}�� �����߽��ϴ�.");
                break;
            case 3:
                attackRange += 1.5f; // �� ��° ���׷��̵�: ���ݹ��� ����, ���ݷ� ����
                attackPower += 1f;
                Debug.Log($"{weaponName}�� ���ݹ����� {attackRange}�� �����߽��ϴ�.");
                break;
            case 4:
                attackPower += 4f;  // �� ��° ���׷��̵�: ���ݷ� ����, ���ݼӵ� ����, ���� ���� ����
                attackRange += 2f;
                attackSpeed += 1.5f;
                Debug.Log($"{weaponName}�� ���ݼӵ��� {attackSpeed}�� �����߽��ϴ�.");
                break;
            default:
                Debug.Log($"{weaponName}�� ���׷��̵尡 �� �̻� ������� �ʽ��ϴ�.");
                break;
        }
    }
    public override GameObject CreatePrefab(Vector3 position, Quaternion rotation)
    {
        // Fireball �������� ����
        GameObject attack = Instantiate(weaponPrefab, position, rotation);

        // �����տ� `WeaponData`�� ����
        SlashScript bulletScript = attack.GetComponent<SlashScript>();
        if (bulletScript != null)
        {
            bulletScript.weaponData = this; // �ڽ��� �����͸� ����
        }

        return attack;  // ������ ������ ��ȯ
    }
}
