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
        questPanel.SetActive(false); // 시작 시 닫혀 있음
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
        // 기존 목록 제거
        foreach (Transform child in contentRoot)
            Destroy(child.gameObject);

        // 새로 생성
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
            questManager.currentQuest = quest;   // 먼저 현재 의뢰 설정
            questManager.FinalizeQuest();        // 매개변수 없는 메서드 호출
        }

        RefreshQuestList(); // 상태 반영
    }
}
