using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public GameObject dialoguePanel;
    public DialogueUIController dialogueUI;

    public Sprite testDialogueSprite;
    public Sprite testChoiceSprite;

    private DialogueData testDialogue;

    void Awake()
    {
        // ��ȭ ���� �� ���� ����
        testDialogue = new DialogueData
        {
            dialogues = new System.Collections.Generic.List<string>
            {
                "�ȳ� �÷��̾�. �̰��� �׽�Ʈ ����.",
                "�׽�Ʈ�� ���� ��� �̾߱� �� �� �� ������?"
            },
            choices = new System.Collections.Generic.List<ChoiceData>
            {
                new ChoiceData
                {
                    choiceText = "�׷�, ����.",
                    resultDialogues = new System.Collections.Generic.List<string> { "����." }
                },
                new ChoiceData
                {
                    choiceText = "�Ⱦ�.",
                    resultDialogues = new System.Collections.Generic.List<string> { "�ȴٴ� ��¿ �� ����." }
                }
            }
        };
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);

        // ���⼭ ��縦 ���� ����
        dialogueUI.SetDialogue(testDialogue, -1, null, testDialogueSprite, testChoiceSprite);
    }
}
