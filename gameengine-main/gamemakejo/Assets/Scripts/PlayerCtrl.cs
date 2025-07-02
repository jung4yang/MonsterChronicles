using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ����� ���� �߰�

public class PlayerCtrl : MonoBehaviour
{
    public SoundManager soundManager; // SoundManager ����

    private float v = 0;     //���� �Է��� ������ ���� (�յ� �̵�)
    private float h = 0;     //���� �Է��� ������ ���� (�¿� �̵�)

    private float v1 = 0;    //������ ���� �̵� ���� �����ϴ� ����
    private float h1 = 0;    //������ ���� �̵� ���� �����ϴ� ����

    private Vector3 moveDir = new Vector3(0, 0, 0); //�̵� ������ ��Ÿ���� ����

    [SerializeField] private float speed = 10f;   //ĳ������ �̵� �ӵ�
    [SerializeField] private float runSpeed = 20f;  //�޸��� �ӵ�
    [SerializeField] private float rotSpeed = 500f;  //ĳ������ ȸ�� �ӵ�

    private Animator _animator;

    public GameObject bullet;        // �Ѿ� Prefab
    public Transform fireTr;         // �Ѿ� �߻� ��ġ (�÷��̾� ��)

    public float playerHp = 5;
    [SerializeField] private int playerExp = 0;
    [SerializeField] private int maxExp = 10;
    private int playerLv = 1;

    public WeaponManagerUI weaponManagerUI;
    public GameObject gameOverUI;    // ���� ���� UI ����

    public TextMeshProUGUI hpText;   // TextMeshPro�� HP�� ǥ���� UI ���
    public TextMeshProUGUI expText;  // TextMeshPro�� EXP�� ǥ���� UI ���

    // Start is called before the first frame update
    void Start()
    {
        if (soundManager == null)
        {
            soundManager = FindObjectOfType<SoundManager>(); // �ڵ����� ã��
        }
        _animator = GetComponent<Animator>();  // Animator ������Ʈ �ʱ�ȭ

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);  // ���� ���� �� Game Over UI ��Ȱ��ȭ
        }

        UpdateHpUI(); // �ʱ� HP UI ������Ʈ
        UpdateExpUI(); // �ʱ� EXP UI ������Ʈ
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp <= 0)
        {
            GameOver();
            return; // ��� ó�� ���� Update ���� �ߴ�
        }

        if (playerExp >= maxExp)
        {
            LevelUP();
        }

        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        v1 = v * Mathf.Sqrt(1 - (Mathf.Pow(h, 2) / 2));
        h1 = h * Mathf.Sqrt(1 - (Mathf.Pow(v, 2) / 2));

        float currentSpeed = speed;

        if (v > 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            currentSpeed = runSpeed;
        }

        moveDir = (Vector3.forward * v1 + Vector3.right * h1);

        // ĳ���� �̵�
        transform.Translate(moveDir * currentSpeed * Time.deltaTime, Space.Self);
        // ȸ��
        transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        _animator.SetFloat("p_V", v1);
        _animator.SetFloat("p_H", h1);

        // �Ѿ� �߻� (���콺 ���� ��ư Ŭ��)
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
            soundManager.PlayShootSound();  // �Ѿ� �߻� ���� ȣ��
            _animator.SetTrigger("Attack");
        }
    }

    // �Ѿ� �߻� �޼���
    private void FireBullet()
    {
        if (bullet != null && fireTr != null)
        {
            // �Ѿ� �ν��Ͻ��� �߻� ��ġ���� ����
            Instantiate(bullet, fireTr.position, fireTr.rotation);
        }
        else
        {
            Debug.LogWarning("Bullet or Fire Transform is not set up!");
        }
    }

    // ����ġ ���� �޼���
    public void AddExperience(int amount)
    {
        playerExp += amount;
        Debug.Log("Player Experience: " + playerExp);

        UpdateExpUI(); // ����ġ UI ������Ʈ

        if (playerExp >= maxExp)
        {
            LevelUP();
        }
    }

    void LevelUP()
    {
        playerExp -= maxExp;
        maxExp += 5;
        playerLv += 1;
        playerHp += 2f;
        Debug.Log("Level UP");

        UpdateHpUI();
        UpdateExpUI(); // ������ �� EXP UI ������Ʈ

        // ������ �� WeaponManagerUI�� ����
        if (weaponManagerUI != null)
        {
            weaponManagerUI.ShowWeaponUI(); // ������ �� WeaponManagerUI�� Ȱ��ȭ
        }
    }

    // ���� ���� ó��
    private void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.GetComponent<GameOverUI>().ShowGameOverUI();
        }

        Debug.Log("Game Over");
    }

    // HP UI ������Ʈ �޼���
    public void UpdateHpUI()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {playerHp}"; // UI�� ���� HP ���� ������Ʈ
            Debug.Log($"HP UI Updated: {playerHp}");
        }
        else
        {
            Debug.LogWarning("hpText is not assigned!");
        }
    }

    // EXP UI ������Ʈ �޼���
    public void UpdateExpUI()
    {
        if (expText != null)
        {
            expText.text = $"EXP: {playerExp}/{maxExp}"; // UI�� ���� EXP ���� ������Ʈ
            Debug.Log($"EXP UI Updated: {playerExp}/{maxExp}");
        }
        else
        {
            Debug.LogWarning("expText is not assigned!");
        }
    }

    // �������� �޾��� �� ȣ��Ǵ� �޼���
    public void TakeDamage(float damage)
    {
        playerHp -= damage;
        if (playerHp < 0)
        {
            playerHp = 0;
        }
        UpdateHpUI(); // HP ���� �ø��� UI ������Ʈ
    }

    // ���� �ʱ�ȭ �޼���
    public void ResetPlayerState()
    {
        playerHp = 5; // �ʱ� HP ��
        playerExp = 0; // �ʱ� ����ġ ��
        maxExp = 10; // �ʱ� �ִ� ����ġ
        playerLv = 1; // �ʱ� ���� ��

        UpdateHpUI(); // �ʱ�ȭ �� UI ������Ʈ
        UpdateExpUI(); // �ʱ�ȭ �� EXP UI ������Ʈ

        // �ִϸ��̼� ���� �ʱ�ȭ
        _animator.SetBool("p_Attack", false);
        _animator.SetFloat("p_V", 0);
        _animator.SetFloat("p_H", 0);

        Debug.Log("Player state has been reset.");
    }
}
