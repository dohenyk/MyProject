using UnityEngine;

public enum QuestStatus
{
    NotAccepted,   // ���� �������� ����
    InProgress,    // ������ ����
    ConditionMet,  // ������ �Ϸ���
    Completed      // ���� �Ϸ��
}

[System.Serializable]
public class QuestData
{
    public string title;               // �Ƿ� ����
    public string description;         // �Ƿ� ����
    public QuestStatus status;         // ���� ����
    public int correctChoiceIndex;     // ��ȭ ������ �� ���� ��ȣ
    public int rewardDollar; // �޷� ����
    public int rewardWon;    // ��ȭ ����

    public bool isCompleted => status == QuestStatus.Completed;
    public bool isConditionMet => status == QuestStatus.ConditionMet;
}
