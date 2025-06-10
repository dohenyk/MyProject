using UnityEngine;
using TMPro;

public class QuestProgressUI : MonoBehaviour
{
    public TextMeshProUGUI progressText;
    public QuestManager questManager;

    void Update()
    {
        var quest = questManager.currentQuest;

        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            progressText.text = $"���� ��: {quest.title}";
        }
        else
        {
            progressText.text = "";
        }
    }
}
