using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;  // Singleton �ν��Ͻ�
    private AudioSource audioSource;    // AudioSource ������Ʈ
    private bool isMuted = false;       // BGM Ȱ��/��Ȱ�� ����

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �������� ����
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // �ߺ��� BGMManager�� �������� �ʵ��� �ı�
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Ű���� M �Է� ����
        {
            ToggleBGM();
        }
    }

    // BGM�� �����ϴ� �޼���
    public void ChangeBGM(AudioClip bgmClip)
    {
        if (audioSource.clip == bgmClip) return; // �̹� ��� ���� BGM�̸� ����

        audioSource.clip = bgmClip;
        audioSource.Play();
    }

    // BGM �Ѱ� ���� ��� �޼���
    public void ToggleBGM()
    {
        isMuted = !isMuted; // ���� ����
        audioSource.mute = isMuted; // AudioSource�� ���Ұ� ���� ����
        Debug.Log("BGM " + (isMuted ? "Muted" : "Unmuted"));
    }
}
