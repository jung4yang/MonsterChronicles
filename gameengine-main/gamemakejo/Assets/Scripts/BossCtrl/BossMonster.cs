using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossMonster : MonoBehaviour
{
    protected NavMeshAgent b_Agent;
    protected GameObject _target;
    protected Animator animator;

    public float _damage = 5;
    public float HP = 30;
    public float currentHp;

    // ���� Ÿ���� �� �������� �����ϰ� �ϹǷ� �⺻ �޼���� abstract�� ó��
    public abstract void Attack1();

    private void Start()
    {
        b_Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _target = GameObject.FindWithTag("Player");
        currentHp = HP;
    }

    protected virtual void Update()
    {
        b_Agent.SetDestination(_target.transform.position);

        if (currentHp <= 0)
        {
            currentHp = 0;
            OnBossDefeated();
        }
        // ������ ���� �޼��带 ȣ�� (�� ������ �´� ������ �������̵�)
        Attack1();
    }
    
    public void takeDamage(float damage)
    {
        currentHp -= damage;
        animator.SetTrigger("GetHit");
    }

    private void OnBossDefeated()
    {
        // ���� ���� ��Ȳ�� �°� ���� ���� �ε�
        GameManager.Instance.LoadNextScene();
        Destroy(gameObject);
    }
}
