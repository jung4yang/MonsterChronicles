using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAxe", menuName = "Weapon/AxeData")]
public class AxeData : WeaponData
{
    // Start is called before the first frame update
    public override void Upgrade()
    {
        upgradeCount++;

        // ���׷��̵� ȿ�� ����
        switch (upgradeCount)
        {
            case 1:
                attackPower += 2f; // ù ��° ���׷��̵�: ���ݷ� ����
                Debug.Log($"{weaponName}�� ���ݷ��� {attackPower}�� �����߽��ϴ�.");
                break;
            case 2:
                attackPower += 3f; // �� ��° ���׷��̵�: ���ݷ� ����
                Debug.Log($"{weaponName}�� ���ݷ��� {attackPower}�� �����߽��ϴ�.");
                break;
            case 3:
                attackPower += 4f; // �� ��° ���׷��̵�: ���ݷ� ����
                Debug.Log($"{weaponName}�� ���ݷ��� {attackPower}�� �����߽��ϴ�.");
                break;
             case 4:
                attackSpeed++;  // �� ��° ���׷��̵�: ���ݼӵ� ����
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
        AxeSlash bulletScript = attack.GetComponent<AxeSlash>();
        if (bulletScript != null)
        {
            bulletScript.weaponData = this; // �ڽ��� �����͸� ����
        }

        return attack;  // ������ ������ ��ȯ
    }
}
