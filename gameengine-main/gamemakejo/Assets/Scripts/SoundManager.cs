using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource shootAudioSource;  // �Ѿ� �߻� ���带 ����� AudioSource
    public AudioSource enemyDeadAudioSource;  // �� ��� ���带 ����� AudioSource
    public AudioSource expCollectAudioSource;  // ����ġ ȹ�� ���带 ����� AudioSource

    public AudioClip shootSound;  // �Ѿ� �߻� ���� Ŭ��
    public AudioClip enemyDeadSound;  // �� ��� ���� Ŭ��
    public AudioClip expCollectSound;  // ����ġ ȹ�� ���� Ŭ��

    // Start is called before the first frame update
    void Start()
    {
        if (shootAudioSource == null || shootSound == null)
        {
            Debug.LogWarning("ShootAudioSource or ShootSound is not set up!");
        }

        if (enemyDeadAudioSource == null || enemyDeadSound == null)
        {
            Debug.LogWarning("EnemyDeadAudioSource or EnemyDeadSound is not set up!");
        }

        if (expCollectAudioSource == null || expCollectSound == null)
        {
            Debug.LogWarning("ExpCollectAudioSource or ExpCollectSound is not set up!");
        }
    }

    // �Ѿ� �߻� ���� ���
    public void PlayShootSound()
    {
        if (shootAudioSource != null && shootSound != null)
        {
            shootAudioSource.PlayOneShot(shootSound);
        }
    }

    // �� ��� ���� ���
    public void PlayEnemyDeadSound()
    {
        if (enemyDeadAudioSource != null && enemyDeadSound != null)
        {
            enemyDeadAudioSource.PlayOneShot(enemyDeadSound);
        }
    }

    // ����ġ ȹ�� ���� ���
    public void PlayExpCollectSound()
    {
        if (expCollectAudioSource != null && expCollectSound != null)
        {
            expCollectAudioSource.PlayOneShot(expCollectSound);
        }
    }
}
