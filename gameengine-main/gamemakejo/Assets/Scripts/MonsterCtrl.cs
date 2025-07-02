using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MonsterCtrl : MonoBehaviour
{
    public SoundManager soundManager; // SoundManager�� Inspector���� �Ҵ�

    private NavMeshAgent m_Agent = null;
    private GameObject _target = null;

    private Animator m_Animator = null;

    public GameObject expGem;

    public float HP = 5f;
    public float currentHp;
    public float damage = 1f;
    private float attackRange = 2f;

    public delegate void MonsterDeathEventHandler();
    public static event MonsterDeathEventHandler OnMonsterDeath;
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            var playerCtrl = coll.gameObject.GetComponent<PlayerCtrl>();
            playerCtrl.TakeDamage(damage);
        }
    }

    void Start()
    {
        _target = GameObject.FindWithTag("Player");
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        HP = MonsterData.monsterHP;
        currentHp = HP;
        damage = MonsterData.monsterAttack;
        if (soundManager == null)
        {
            soundManager = FindObjectOfType<SoundManager>(); // �ڵ����� SoundManager ã��
        }
    }
    public void takeDamage(float damage)
    {
        currentHp -= damage;
        m_Animator.SetTrigger("GetHit");
    }
    // Update is called once per frame
    void Update()
    {

        // �÷��̾���� �Ÿ� ���
        float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);

        if (distanceToTarget <= attackRange)
        {
            // ���� �ִϸ��̼� ����
            m_Animator.SetTrigger("Attack");
        }
        else
        {
            // �÷��̾ ���� �̵�
            m_Agent.isStopped = false; // �̵� ����
            m_Agent.SetDestination(_target.transform.position);
        }

        // ��� ó��
        if (currentHp <= 0)
        {
            Die();
            if (soundManager != null) // soundManager�� null���� Ȯ��
            {
                soundManager.PlayEnemyDeadSound();
            }
            else
            {
                Debug.LogWarning("SoundManager is not assigned!");
            }
        }
    }

    void Die()
    {
        Instantiate(expGem, transform.position, Quaternion.identity);

        OnMonsterDeath?.Invoke();

        Destroy(gameObject);  // ���� ������Ʈ �ı�
    }
    void OnAttackStart()
    {
        m_Agent.isStopped = true; // �̵� ����
        Debug.Log("���� ����!");
    }

    void OnAttackEnd()
    {
        m_Agent.isStopped = false; // �̵� �簳
        Debug.Log("���� ����!");
    }
}
