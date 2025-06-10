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
        // 대화 내용 한 번만 설정
        testDialogue = new DialogueData
        {
            dialogues = new System.Collections.Generic.List<string>
            {
                "안녕 플레이어. 이것은 테스트 대사야.",
                "테스트를 위해 잠깐 이야기 좀 할 수 있을까?"
            },
            choices = new System.Collections.Generic.List<ChoiceData>
            {
                new ChoiceData
                {
                    choiceText = "그래, 좋아.",
                    resultDialogues = new System.Collections.Generic.List<string> { "고마워." }
                },
                new ChoiceData
                {
                    choiceText = "싫어.",
                    resultDialogues = new System.Collections.Generic.List<string> { "싫다니 어쩔 수 없군." }
                }
            }
        };
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);

        // 여기서 대사를 새로 세팅
        dialogueUI.SetDialogue(testDialogue, -1, null, testDialogueSprite, testChoiceSprite);
    }
}
