using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public DialogueUIController dialogueUI;

    public Sprite testDialogueSprite;
    public Sprite testChoiceSprite;


    void Start()
    {
        DialogueData testDialogue = new DialogueData
        {
            dialogues = new List<string>
            {
                "안녕 플레이어. 이것은 테스트 대사야.",
                "테스트를 위해 잠깐 이야기 좀 할 수 있을까?"
            },
            choices = new List<ChoiceData>
            {
                new ChoiceData
                {
                    choiceText = "그래, 좋아.",
                    resultDialogues = new List<string> { "고마워." }
                },
                new ChoiceData
                {
                    choiceText = "싫어.",
                    resultDialogues = new List<string> { "싫다니 어쩔 수 없군." }
                }
            }
        };

        dialogueUI.SetDialogue(testDialogue, -1, null, testDialogueSprite, testChoiceSprite);

    }
}
