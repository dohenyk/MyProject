using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public List<string> dialogues; // ó�� ������ ����
    public List<ChoiceData> choices; // ��������
}

[System.Serializable]
public class ChoiceData
{
    public string choiceText; // �������� ������ ����
    public List<string> resultDialogues; // �� �������� �� �� ������ ����
}
