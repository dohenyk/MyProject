using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestListUI : MonoBehaviour
{
    public GameObject questItemPrefab;
    public Transform contentRoot;
    public GameObject questPanel;

    public QuestManager questManager;

    void Start()
    {
        questPanel.SetActive(false); // ���� �� ���� ����
    }

    public void ToggleQuestPanel()
    {
        if (!questPanel.activeSelf)
        {
            OpenQuestPanel();
        }
        else
        {
            CloseQuestPanel();
        }
    }

    void OpenQuestPanel()
    {
        questPanel.SetActive(true);
        RefreshQuestList();
    }

    void CloseQuestPanel()
    {
        questPanel.SetActive(false);
    }

    void RefreshQuestList()
    {
        // ���� ��� ����
        foreach (Transform child in contentRoot)
            Destroy(child.gameObject);

        // ���� ����
        foreach (var quest in questManager.quests)
        {
            GameObject item = Instantiate(questItemPrefab, contentRoot);
            item.GetComponentInChildren<TextMeshProUGUI>().text =
                $"{quest.title} ({quest.status})";

            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnQuestClicked(quest);
            });
        }
    }

    void OnQuestClicked(QuestData quest)
    {
        if (quest.status == QuestStatus.NotAccepted)
        {
            questManager.AcceptQuest(quest);
        }
        else if (quest.status == QuestStatus.ConditionMet)
        {
            questManager.currentQuest = quest;   // ���� ���� �Ƿ� ����
            questManager.FinalizeQuest();        // �Ű����� ���� �޼��� ȣ��
        }

        RefreshQuestList(); // ���� �ݿ�
    }
}
