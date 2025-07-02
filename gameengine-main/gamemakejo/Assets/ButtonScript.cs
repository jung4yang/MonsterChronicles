using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public AudioSource audioSource;  // Ŭ�� �Ҹ��� ����� AudioSource
    public AudioClip clickSound;     // Ŭ�� ���� Ŭ��

    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound); // Ŭ�� �Ҹ� ���
        }
        else
        {
            Debug.LogWarning("AudioSource or ClickSound is missing!");
        }
    }
}

