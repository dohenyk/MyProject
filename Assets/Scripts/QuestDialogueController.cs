using UnityEngine;

public class QuestDialogueController : MonoBehaviour
{
    public QuestManager questManager;
    public DialogueUIController dialogueUI;

    public GameObject dialoguePanel;
    public Sprite questDialogueSprite;  // ��� ���� �߿� ���� ��������Ʈ
    public Sprite questChoiceSprite;    // ������ ���� �� ���� ��������Ʈ


    void Start()
    {
        dialoguePanel.SetActive(false); // ó���� ��Ȱ��ȭ
    }

    public void StartQuestDialogue()
    {
        if (questManager.currentQuest == null) return;

        // �׽�Ʈ�� ��� ����
        var quest = questManager.currentQuest;

        DialogueData dialogue = new DialogueData
        {
            dialogues = new System.Collections.Generic.List<string>
            {
                $"{quest.title} �Ƿ� ������: �ȳ��ϼ���. �׽�Ʈ �Ƿ��Դϴ�. ���� �Ϸ� ���̳���?"
            },
            choices = new System.Collections.Generic.List<ChoiceData>
            {
                new ChoiceData
                {
                    choiceText = "�����Ϸ� �Ծ��.",
                    resultDialogues = new System.Collections.Generic.List<string> { "�����ϴ�. �� �޾ҽ��ϴ�." }
                },
                new ChoiceData
                {
                    choiceText = "�������� �ʱ�. �׽�Ʈ �Ƿ� ���� �� ��.",
                    resultDialogues = new System.Collections.Generic.List<string> { "�׷� �� ������." }
                }
            }
        };

        dialogueUI.SetDialogue(dialogue, quest.correctChoiceIndex, questManager, questDialogueSprite, questChoiceSprite);

        dialoguePanel.SetActive(true);
    }
}
