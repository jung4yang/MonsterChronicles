using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLightningball", menuName = "Weapon/LightningballData")]
public class LightningballData : WeaponData
{
    public int chainCount = 3;
    public override void Upgrade()
    {
        upgradeCount++;

        // ���׷��̵� ȿ�� ����
        switch (upgradeCount)
        {
            case 1:
                attackPower += 1; // ù ��° ���׷��̵�: ���ݷ� ����
                Debug.Log($"{weaponName}�� ���ݷ��� {attackPower}�� �����߽��ϴ�.");
                break;
            case 2:
                chainCount++; ; // �� ��° ���׷��̵�: ���� Ƚ�� ����, ���ݷ� ����
                attackPower += 1;
                Debug.Log($"{weaponName}�� ���ݼӵ��� {attackSpeed}�� �����߽��ϴ�.");
                break;
            case 3:
                chainCount += 2;   // �� ��° ���׷��̵�: ���� Ƚ�� ����, ���ݷ� ����
                attackPower += 1;  
                Debug.Log($"{weaponName}�� ���ݹ����� {attackRange}�� �����߽��ϴ�.");
                break;
            case 4:
                chainCount += 5;   // �� ��° ���׷��̵�: ���� Ƚ�� ����, ���ݷ� ����, ���� ���� ����, ���� �ӵ� ����
                attackPower += 3;
                attackRange = 30f;
                attackSpeed += 1f;
                Debug.Log($"{weaponName}�� ���ݹ����� {attackRange}�� �����߽��ϴ�.");
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
        LightningBall bulletScript = attack.GetComponent<LightningBall>();
        if (bulletScript != null)
        {
            bulletScript.weaponData = this; // �ڽ��� �����͸� ����
        }

        return attack;  // ������ ������ ��ȯ
    }
}
