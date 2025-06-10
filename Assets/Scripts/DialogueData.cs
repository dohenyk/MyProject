using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public List<string> dialogues; // 처음 나오는 대사들
    public List<ChoiceData> choices; // 선택지들
}

[System.Serializable]
public class ChoiceData
{
    public string choiceText; // 선택지에 보여질 문장
    public List<string> resultDialogues; // 이 선택지를 고른 뒤 보여줄 대사들
}
