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
                "�ȳ� �÷��̾�. �̰��� �׽�Ʈ ����.",
                "�׽�Ʈ�� ���� ��� �̾߱� �� �� �� ������?"
            },
            choices = new List<ChoiceData>
            {
                new ChoiceData
                {
                    choiceText = "�׷�, ����.",
                    resultDialogues = new List<string> { "����." }
                },
                new ChoiceData
                {
                    choiceText = "�Ⱦ�.",
                    resultDialogues = new List<string> { "�ȴٴ� ��¿ �� ����." }
                }
            }
        };

        dialogueUI.SetDialogue(testDialogue, -1, null, testDialogueSprite, testChoiceSprite);

    }
}
