using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : BossMonster
{
    private Vector3 targetPos;
    private float distance;
    private float speed;
    private bool isJumping = false;
    private float jumpCooldownTimer = 0f;

    [SerializeField] private float minAttackRange = 10f;   // �ּ� �Ÿ�
    [SerializeField] private float maxAttackRange = 50f;  // �ִ� �Ÿ�
    [SerializeField] private float jumpCooldown = 10f;    // ���� ���� ��Ÿ��
    [SerializeField] private float attackRange = 9f;
    public MeshRenderer meshRenderer;
    private GameObject meshObject;

    [SerializeField] private ParticleSystem white;
    [SerializeField] private ParticleSystem brown;
    [SerializeField] private ParticleSystem green;


    [SerializeField] private GameObject rock;
    [SerializeField] private float rockCooldown = 3f;
    [SerializeField] private float rockCooldownTimer = 0f;
    [SerializeField] private Transform attackTr;

    public void Attack()
    {
         if (rock != null)
        {
            if(rockCooldownTimer <= 0f)
            {
                float distance = Vector3.Distance(transform.position, _target.transform.position);
                if (distance <= 10f)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("Attack");
                    }
                }
            }
            else
            {
                rockCooldownTimer -= Time.deltaTime;
            }
        }
    }
    public override void Attack1()
    {
        if (jumpCooldownTimer <= 0f)
        {
            distance = Vector3.Distance(transform.position, _target.transform.position);
            if (distance >= minAttackRange && distance <= maxAttackRange)
            {
                // ���� ����
                targetPos = _target.transform.position; // �÷��̾��� ��ġ ���
                speed = distance / 1.1f; // ���� �ӵ� ��� (�ִϸ��̼� ���̿� �°�)
                animator.SetTrigger("Attack1"); // ���� �ִϸ��̼� ����
                jumpCooldownTimer = jumpCooldown; // ��Ÿ�� �ʱ�ȭ

                // ���� ���� ǥ�� Ȱ��ȭ
                ShowAttackRange(targetPos);
            }
        }
        else
        {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    void ShowAttackRange(Vector3 position)
    {
        if (meshObject == null && meshRenderer != null)
        {
            meshObject = Instantiate(meshRenderer.gameObject);
        }
        
        if (meshObject != null)
        {
            meshObject.SetActive(true);
            meshObject.transform.position = position;
            meshObject.transform.localScale = new Vector3(attackRange * 2f, 1f, attackRange * 2f);  // ũ�� ����            
        }
    }

    void HideAttackRange()
    {
        if (meshRenderer != null)
        {
            meshObject.SetActive(false);
        }
    }
    void OnThrow()
    {
        var Rock = Instantiate(rock, attackTr.position, attackTr.rotation);
        Rock.SetActive(true);
        rockCooldownTimer = rockCooldown;
    }
    // �ִϸ��̼� �̺�Ʈ: ���� ���
    void OnIdle()
    {
        b_Agent.isStopped = true; // �̵� ����
    }

    // �ִϸ��̼� �̺�Ʈ: ���� ����
    void OnJump()
    {
        b_Agent.isStopped = false;
        isJumping = true;
    }

    // �ִϸ��̼� �̺�Ʈ: �������
    void OnSlam()
    {
        b_Agent.isStopped = true;
        HideAttackRange(); // ���� ���� ǥ�� ��Ȱ��ȭ

        white?.Play();
        brown?.Play();
        green?.Play();

        // �÷��̾���� �浹 ����
        Collider[] hitColliders = Physics.OverlapSphere(targetPos, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                var player = hitCollider.GetComponent<PlayerCtrl>();
                player?.TakeDamage(_damage); // ������ ����
            }
        }
    }
    // �ִϸ��̼� �̺�Ʈ: ���� ����
    void OnJumpEnd()
    {
        b_Agent.isStopped = false;
        isJumping = false; // ���� ���� ����
    }

    protected override void Update()
    {
        base.Update();
        Attack();
        // ���� �̵� ó��
        if (isJumping)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }
}
