using UnityEngine;

public enum QuestStatus
{
    NotAccepted,   // 아직 수락하지 않음
    InProgress,    // 수락한 상태
    ConditionMet,  // 조건을 완료함
    Completed      // 최종 완료됨
}

[System.Serializable]
public class QuestData
{
    public string title;               // 의뢰 제목
    public string description;         // 의뢰 설명
    public QuestStatus status;         // 현재 상태
    public int correctChoiceIndex;     // 대화 선택지 중 정답 번호
    public int rewardDollar; // 달러 보상
    public int rewardWon;    // 원화 보상

    public bool isCompleted => status == QuestStatus.Completed;
    public bool isConditionMet => status == QuestStatus.ConditionMet;
}
