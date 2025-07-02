using UnityEngine;

public class SceneBGMSetter : MonoBehaviour
{
    public AudioClip bgmClip; // ���� ������ BGM

    private void Start()
    {
        if (BGMManager.Instance != null && bgmClip != null)
        {
            BGMManager.Instance.ChangeBGM(bgmClip);
        }
    }
}
