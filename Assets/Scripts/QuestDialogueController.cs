using UnityEngine;

public class QuestDialogueController : MonoBehaviour
{
    public QuestManager questManager;
    public DialogueUIController dialogueUI;

    public GameObject dialoguePanel;
    public Sprite questDialogueSprite;  // 대사 진행 중에 보일 스프라이트
    public Sprite questChoiceSprite;    // 선택지 나올 때 보일 스프라이트


    void Start()
    {
        dialoguePanel.SetActive(false); // 처음엔 비활성화
    }

    public void StartQuestDialogue()
    {
        if (questManager.currentQuest == null) return;

        // 테스트용 대사 세팅
        var quest = questManager.currentQuest;

        DialogueData dialogue = new DialogueData
        {
            dialogues = new System.Collections.Generic.List<string>
            {
                $"{quest.title} 의뢰 관련자: 안녕하세요. 테스트 의뢰입니다. 무슨 일로 오셨나요?"
            },
            choices = new System.Collections.Generic.List<ChoiceData>
            {
                new ChoiceData
                {
                    choiceText = "전달하러 왔어요.",
                    resultDialogues = new System.Collections.Generic.List<string> { "고맙습니다. 잘 받았습니다." }
                },
                new ChoiceData
                {
                    choiceText = "전달하지 않기. 테스트 의뢰 진행 안 됨.",
                    resultDialogues = new System.Collections.Generic.List<string> { "그럼 잘 가세요." }
                }
            }
        };

        dialogueUI.SetDialogue(dialogue, quest.correctChoiceIndex, questManager, questDialogueSprite, questChoiceSprite);

        dialoguePanel.SetActive(true);
    }
}
