using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIElement : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI statusText;
    public Button questButton;

    private QuestData questData;
    private QuestManager questManager;

    public void Initialize(QuestData data, QuestManager manager)
    {
        questData = data;
        questManager = manager;

        titleText.text = questData.title;
        UpdateStatusText();

        questButton.onClick.RemoveAllListeners();
        questButton.onClick.AddListener(OnQuestButtonClicked);
    }

    void UpdateStatusText()
    {
        if (questData.status == QuestStatus.Completed)
        {
            statusText.text = "성공";
        }
        else if (questData.status == QuestStatus.ConditionMet)
        {
            statusText.text = "조건 완료";
        }
        else if (questData == questManager.currentQuest)
        {
            statusText.text = "진행 중";
        }
        else
        {
            statusText.text = "의뢰 수락 가능";
        }
    }

    void OnQuestButtonClicked()
    {
        if (questData.status == QuestStatus.Completed)
        {
            return; // 이미 완료된 의뢰는 무시
        }

        if (questData.status == QuestStatus.ConditionMet)
        {
            questData.status = QuestStatus.Completed;
            GameManager.Instance.currentDollar += questData.rewardDollar;
            GameManager.Instance.currentWon += questData.rewardWon;
            Debug.Log($"의뢰 보상 지급: ${questData.rewardDollar}, ₩{questData.rewardWon}");
        }
        else
        {
            questManager.currentQuest = questData;
            Debug.Log($"의뢰 수락됨: {questData.title}");
        }

        UpdateStatusText();
    }
}
