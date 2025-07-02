using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;         // ������ Ÿ�� (�÷��̾�)
    [SerializeField] private float distance = 10f;  // ī�޶�� �÷��̾� �� �Ÿ�
    [SerializeField] private float height = 5f;     // ī�޶��� ����
    [SerializeField] private float leftOffsetValue = -2f; // ī�޶� �÷��̾� �������� �̵��ϴ� �Ÿ�

    private bool isCursorLocked = true;  // Ŀ�� ��� ���� Ȯ�ο�

    void Start()
    {
        // Ŀ���� �⺻ ��� ���·� ����
        LockCursor();
    }

    void Update()
    {
        // Ÿ���� ���󰡴� ī�޶� ����
        FollowPlayer();
    }

    /// <summary>
    /// ī�޶� �÷��̾ �����ϴ� ����
    /// </summary>
    void FollowPlayer()
    {
        if (target == null) return;  // Ÿ���� �������� ���� ��� �������� ����

        // ī�޶��� ��ǥ ��ġ ���: �÷��̾� ���� + ���� + ����
        Vector3 leftOffset = -target.right * leftOffsetValue;  // �������� �̵��ϴ� �Ÿ�
        Vector3 heightOffset = Vector3.up * height;           // �÷��̾� ���� ������ ����
        Vector3 targetPosition = target.position - target.forward * distance + leftOffset + heightOffset;

        // ī�޶��� ��ġ�� ȸ���� ����
        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);  // �÷��̾��� y ȸ������ �����ϰ� ����
    }

    /// <summary>
    /// Ŀ���� ��װ� ����
    /// </summary>
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Ŀ�� ���
        Cursor.visible = false;                    // Ŀ�� �����
        isCursorLocked = true;
    }

    /// <summary>
    /// Ŀ�� ����� �����ϰ� ǥ��
    /// </summary>
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;  // Ŀ�� ��� ����
        Cursor.visible = true;                   // Ŀ�� ǥ��
        isCursorLocked = false;
    }
}
