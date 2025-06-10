using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> quests = new List<QuestData>(); // 전체 의뢰 목록
    public QuestData currentQuest; // 현재 진행 중인 의뢰

    // 의뢰 수락
    public void AcceptQuest(QuestData quest)
    {
        quest.status = QuestStatus.InProgress;
        currentQuest = quest;
    }

    // 대화 조건 달성 처리 (선택지를 맞췄을 때 호출)
    public void CompleteCondition()
    {
        if (currentQuest != null && currentQuest.status == QuestStatus.InProgress)
        {
            currentQuest.status = QuestStatus.ConditionMet;
        }
    }

    // 의뢰 최종 완료 및 보상 지급
    public void FinalizeQuest()
    {
        if (currentQuest != null && currentQuest.status == QuestStatus.ConditionMet)
        {
            currentQuest.status = QuestStatus.Completed;

            GameManager.Instance.currentDollar += currentQuest.rewardDollar;
            GameManager.Instance.currentWon += currentQuest.rewardWon;

            Debug.Log($"의뢰 완료! 보상 지급: ${currentQuest.rewardDollar}, ₩{currentQuest.rewardWon}");
        }
    }

    void Start()
    {
        // 테스트용 의뢰 1
        quests.Add(new QuestData
        {
            title = "보급 물자 전달",
            description = "C 구역에 보급 물자 전달 요청 의뢰.",
            status = QuestStatus.NotAccepted,
            correctChoiceIndex = 0,
            rewardDollar = 500,
            rewardWon = 10000
        });

        // 테스트용 의뢰 2
        quests.Add(new QuestData
        {
            title = "기계 수리 도움",
            description = "B 구역의 고장난 기계 수리 요청 의뢰.",
            status = QuestStatus.NotAccepted,
            correctChoiceIndex = 1,
            rewardDollar = 750,
            rewardWon = 15000
        });
    }
}
