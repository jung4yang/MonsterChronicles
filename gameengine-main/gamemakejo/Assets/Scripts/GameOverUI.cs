using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;  // ���� ���� UI �г�
    private CameraCtrl cameraCtrl;    // CameraCtrl ��ũ��Ʈ ����

    void Start()
    {
        gameOverPanel.SetActive(false);  // ���� �� UI ��Ȱ��ȭ
        cameraCtrl = FindObjectOfType<CameraCtrl>();
    }

    public void ShowGameOverUI()
    {
        Time.timeScale = 0;  // ���� �Ͻ�����
        gameOverPanel.SetActive(true);  // ���� ���� UI Ȱ��ȭ
        if (cameraCtrl != null)
        {
            cameraCtrl.UnlockCursor();  // Ŀ���� �����Ͽ� ��ư Ŭ�� ����
        }
    }

    public void OnRestartButtonClicked()
    {
        // �÷��̾� ���� �ʱ�ȭ
        PlayerCtrl player = FindObjectOfType<PlayerCtrl>();
        if (player != null)
        {
            player.ResetPlayerState();  // �÷��̾� ���� �ʱ�ȭ
        }

        // Stage1 �� ��ε�
        SceneManager.LoadScene("Stage1");

        if (cameraCtrl != null)
        {
            cameraCtrl.LockCursor();  // ����� �� Ŀ�� ���
        }
        Time.timeScale = 1f;
    }

    public void OnExitButtonClicked()
    {
        // Lobby �� �ε�
        SceneManager.LoadScene("Lobby");

        if (cameraCtrl != null)
        {
            cameraCtrl.LockCursor();  // �κ�� ���ư��� Ŀ�� ���
        }
        Time.timeScale = 1f;
    }
}
