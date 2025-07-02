using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArrow", menuName = "Weapon/ArrowData")]
public class ArrowData : WeaponData
{
    public bool isMultiShot = false; // ���� �߻� ����
    public int penetration = 0;
    // Start is called before the first frame update
    public override void Upgrade()
    {
        upgradeCount++;

        // ���׷��̵� ȿ�� ����
        switch (upgradeCount)
        {
            case 1:
                attackSpeed += 0.5f; // ù ��° ���׷��̵�: ���ݼӵ� ����
                Debug.Log($"{weaponName}�� ���ݼӵ��� {attackSpeed}�� �����߽��ϴ�.");
                break;
            case 2:
                attackSpeed += 0.5f; // �� ��° ���׷��̵�: ���ݼӵ� ����, ����� ����
                penetration++;
                Debug.Log($"{weaponName}�� ���ݼӵ��� {attackSpeed}�� �����߽��ϴ�.");
                break;
            case 3:
                attackPower += 1f; // �� ��° ���׷��̵�: ���ݷ� ����, ���ݼӵ� ����
                attackSpeed += 0.5f;
                Debug.Log($"{weaponName}�� ���ݷ��� {attackPower}�� �����߽��ϴ�.");
                break;
            case 4:
                isMultiShot = true; // �� ��° ���׷��̵�: ����ü ����, ����� ����, ���ݼӵ� ����
                penetration++;
                attackSpeed += 1f;
                Debug.Log($"{weaponName}��(��) �� �� ���� �߻� ����� Ȱ��ȭ�߽��ϴ�.");
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
        BulletCtrl bulletScript = attack.GetComponent<BulletCtrl>();
        if (bulletScript != null)
        {
            bulletScript.weaponData = this; // �ڽ��� �����͸� ����
        }

        return attack;  // ������ ������ ��ȯ
    }
}
