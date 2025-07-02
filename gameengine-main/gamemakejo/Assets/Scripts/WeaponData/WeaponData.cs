using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string weaponName;
    public bool weaponType; // true: ���Ÿ�, false: �ٰŸ�
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;
    public float attackSpeed;
    public float attackRange;
    public float attackPower;
    public int upgradeCount = 0; // ���׷��̵� Ƚ�� ī��Ʈ

    private float attackTimer;

    public Sprite weaponImage;

    // Equals �޼��� �������̵�: ���� �̸������� ��
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is WeaponData))
            return false;

        WeaponData other = (WeaponData)obj;
        return this.weaponName == other.weaponName; // ���� �̸����� ��
    }

    public override int GetHashCode()
    {
        return weaponName.GetHashCode(); // ���� �̸��� �������� �ؽ� �ڵ� ����
    }
    // ���� ���׷��̵�: �� ���⺰�� ����
    public abstract void Upgrade();

    // �߻� �޼���: �� ���⺰�� �������� �����ϰ� �����͸� �����ϵ��� ó��
    public abstract GameObject CreatePrefab(Vector3 position, Quaternion rotation);
    // ���� Ÿ�̸� �ʱ�ȭ
    public void ResetAttackTimer()
    {
        attackTimer = 0f;
    }
}
