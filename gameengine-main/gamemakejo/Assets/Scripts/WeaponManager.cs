using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponData> equippedWeapons = new List<WeaponData>(); // ���� ������ ���� ����Ʈ
    public Transform weaponSlotParent; // ������� ��ġ�� �θ� ��ü
    public Transform playerPosition; // ������ ���� ���� (�÷��̾� ��ġ)
    public LayerMask enemyLayer; // ���� ���� ���̾�

    private List<float> attackTimers = new List<float>(); // ���⺰ ���� Ÿ�̸�

    public List<WeaponData> allWeapons; // ��� ���� ����Ʈ
    public int maxWeaponSlots = 4; // �ִ� ���� ������ ���� ���� ��

    // ������ ���� ����Ʈ ��ȯ
    public List<WeaponData> GetEquippedWeapons()
    {
        return equippedWeapons;
    }

    // ���� ������ �� �ִ� ���� ����Ʈ ��ȯ
    public List<WeaponData> GetAvailableWeapons()
    {
        return allWeapons.FindAll(w => !equippedWeapons.Contains(w));
    }

    void Start()
    {
        foreach (var weapon in equippedWeapons)
        {
            weapon.ResetAttackTimer();
        }
        attackTimers = new List<float>(new float[equippedWeapons.Count]);
    }

    void Update()
    {
        if (attackTimers.Count != equippedWeapons.Count)
        {
            attackTimers = new List<float>(new float[equippedWeapons.Count]);
        }

        for (int i = 0; i < equippedWeapons.Count; i++)
        {
            attackTimers[i] += Time.deltaTime;

            if (attackTimers[i] >= 1 / equippedWeapons[i].attackSpeed)
            {
                Attack(equippedWeapons[i]);
                attackTimers[i] = 0;
            }
        }
    }

    public void EquipWeapon(WeaponData weapon)
    {
        // �̹� ������ ���� ����Ʈ���� �ش� ���Ⱑ �ִ��� Ȯ��
        foreach (var equippedWeapon in equippedWeapons)
        {
            if (equippedWeapon.weaponName == weapon.weaponName)
            {
                Debug.LogWarning($"{weapon.weaponName}�� �̹� ������ �����Դϴ�!");
                return;  // �̹� ������ ������ �߰����� ����
            }
        }
        if (equippedWeapons.Count >= maxWeaponSlots)
        {
            Debug.LogWarning("���� ������ ���� á���ϴ�!");
            return;
        }

        WeaponData runtimeWeapon = Instantiate(weapon); // ��Ÿ�� ����
        equippedWeapons.Add(runtimeWeapon);
        attackTimers.Add(0f);

        // ���ο� ������ Ÿ�̸� �ʱ�ȭ
        runtimeWeapon.ResetAttackTimer();

        Debug.Log($"{runtimeWeapon.weaponName} ���� �Ϸ�!");
    }

    private void Attack(WeaponData weapon)
    {
        if (weapon.projectilePrefab != null)
        {
            if (weapon.weaponType)
            {
                // weaponName�� "Arrow"�� ��쿡�� �߰� ���� ó��
                switch (weapon.weaponName)
                {
                    case "Arrow":
                        // ArrowData Ÿ������ ����ȯ ��, �� �� �߻� ���� Ȯ��
                        if (weapon is ArrowData arrowData && arrowData.isMultiShot)
                        {
                            // 3�� �߻� (�߾� + �¿�)
                            weapon.CreatePrefab(playerPosition.position, playerPosition.rotation); // �߾� �߻�
                            weapon.CreatePrefab(playerPosition.position, playerPosition.rotation * Quaternion.Euler(0, -5, 0)); // ���� �߻�
                            weapon.CreatePrefab(playerPosition.position, playerPosition.rotation * Quaternion.Euler(0, 5, 0)); // ������ �߻�
                            return; // ���� �߻� ó�� �� ����
                        }
                        break;
                        // ���� �ٸ� ���� Ÿ���� �߰��Ϸ���, ���⿡ case �߰�
                        // case "AnotherWeaponName":
                        //     // �ش� ���� ���� �߰�
                        //     break;
                }

                weapon.CreatePrefab(playerPosition.position, playerPosition.rotation);
            }
            else
            {
                // weaponType�� ������ ��� ó��
                GameObject projectile = weapon.CreatePrefab(playerPosition.position, playerPosition.rotation);
                projectile.transform.SetParent(playerPosition);
            }
        }
    }
}

