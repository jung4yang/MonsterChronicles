using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFireball", menuName = "Weapon/FireballData")]
public class FireballData : WeaponData
{
    public bool isRapidFire = false; // ���� �߻� ����
    public override void Upgrade()
    {
        upgradeCount++;

        // ���׷��̵� ȿ�� ����
        switch (upgradeCount)
        {
            case 1:
                attackPower += 1f; // ù ��° ���׷��̵�: ���ݷ� ����
                Debug.Log($"{weaponName}�� ���ݷ��� {attackPower}�� �����߽��ϴ�.");
                break;
            case 2:
                attackPower += 1f; // �� ��° ���׷��̵�: ���ݷ� ����, ���ݹ��� ����
                attackRange += 1f; 
                Debug.Log($"{weaponName}�� ���ݼӵ��� {attackSpeed}�� �����߽��ϴ�.");
                break;
            case 3:
                attackPower += 1f;  // �� ��° ���׷��̵�: ���ݹ��� ����, ���ݷ� ����
                attackRange += 1f; 
                Debug.Log($"{weaponName}�� ���ݹ����� {attackRange}�� �����߽��ϴ�.");
                break;
            case 4:
                attackPower += 4f;  // �� ��° ���׷��̵�: ���ݷ� ����, ���� ���� ����
                attackRange += 3f;
                Debug.Log($"{weaponName}�� ���ݼӵ��� {attackSpeed}�� �����߽��ϴ�.");
                break;
            default:
                Debug.Log($"{weaponName}�� ���׷��̵尡 �� �̻� ������� �ʽ��ϴ�.");
                break;
        }
    }
    // Fireball �������� �����ϰ� �����͸� �����ϴ� �޼���
    public override GameObject CreatePrefab(Vector3 position, Quaternion rotation)
    {
        // Fireball �������� ����
        GameObject fireball = Instantiate(weaponPrefab, position, rotation);

        // �����տ� `WeaponData`�� ����
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.weaponData = this; // �ڽ��� �����͸� ����
        }

        return fireball;  // ������ ������ ��ȯ
    }
}
