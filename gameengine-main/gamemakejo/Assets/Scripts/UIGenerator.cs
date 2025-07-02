using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGenerator : MonoBehaviour
{
    public Canvas canvas;
    public GameObject textPrefab;
    private TextMeshProUGUI timeText;
    private float elapsedTime = 0f;

    private GameObject specialMessage;
    public GameObject specialMessagePrefab;

    private bool isSpecialMessageActivated = false; // Ư�� �޽��� Ȱ��ȭ ���� üũ

    void Start()
    {
        CreateText(new Vector2(0, -50), "00:00");

        // Ư�� �޽��� UI �ʱ�ȭ
        if (specialMessagePrefab != null && canvas != null)
        {
            specialMessage = Instantiate(specialMessagePrefab, canvas.transform);
            specialMessage.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        }
        else
        {
            Debug.LogWarning("SpecialMessagePrefab or Canvas is not assigned.");
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        float seconds = elapsedTime % 60;
        timeText.text = string.Format("Time: {0:D2}:{1:D2}", minutes, Mathf.FloorToInt(seconds));

        // 150�� �Ŀ� Ư�� �޽��� Ȱ��ȭ
        if (!isSpecialMessageActivated && elapsedTime >= 60f)
        {
            ActivateSpecialMessage();
        }
    }

    void CreateText(Vector2 position, string initialText)
    {
        GameObject textObj = Instantiate(textPrefab, canvas.transform);

        // RectTransform�� �����ͼ� ��ġ�� ȭ�� ��� �߾ӿ� ����
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;  // (0, -50)���� ���� ��ġ ����

        // Anchor ���� (��� �߾�)
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 1f);

        // �ؽ�Ʈ ���� ����
        timeText = textObj.GetComponent<TextMeshProUGUI>();
        timeText.text = initialText;
    }

    void ActivateSpecialMessage()
    {
        if (specialMessage != null)
        {
            specialMessage.SetActive(true); // Ư�� �޽��� Ȱ��ȭ
            Debug.Log("Special message activated!");
        }
        isSpecialMessageActivated = true; // ���� ����
    }
}