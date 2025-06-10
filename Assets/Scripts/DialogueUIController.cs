using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueUIController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button choiceButton1;
    public Button choiceButton2;
    public TextMeshProUGUI choiceText1;
    public TextMeshProUGUI choiceText2;
    public GameObject endText;
    public Image characterImage;
    public Sprite spriteA;
    public Sprite spriteB;
    public GameObject logPanel;
    public TextMeshProUGUI logText;
    public ScrollRect logScrollRect;

    public Sprite dialogueSprite;
    public Sprite choiceSprite;

    private List<string> logHistory = new List<string>();
    private int dialogueIndex = 0;
    private bool isInResultPhase = false;
    private List<string> currentDialogues;
    private int correctChoiceIndex = -1;
    private QuestManager questManagerRef = null;
    private DialogueData currentDialogueData;

    void Start()
    {
        choiceButton1.onClick.AddListener(() => OnChoiceSelected(0));
        choiceButton2.onClick.AddListener(() => OnChoiceSelected(1));

        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        endText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (endText.activeSelf)
            {
                gameObject.SetActive(false);
            }
            else
            {
                ShowNextDialogue();
            }
        }
    }

    void ShowNextDialogue()
    {
        if (dialogueIndex < currentDialogues.Count)
        {
            characterImage.sprite = dialogueSprite;
            dialogueText.text = currentDialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            if (!isInResultPhase)
            {
                characterImage.sprite = choiceSprite;

                dialogueText.text = "";
                choiceButton1.gameObject.SetActive(true);
                choiceButton2.gameObject.SetActive(true);

                choiceText1.text = currentDialogueData.choices[0].choiceText;
                choiceText2.text = currentDialogueData.choices[1].choiceText;
            }
            else
            {
                characterImage.gameObject.SetActive(false);
                dialogueText.text = "";
                endText.SetActive(true);
            }
        }
    }

    void OnChoiceSelected(int index)
    {
        if (index == correctChoiceIndex && questManagerRef != null)
        {
            questManagerRef.CompleteCondition();
        }

        // 로그 기록 추가
        string selectedText = currentDialogueData.choices[index].choiceText;
        logHistory.Add($"[{logHistory.Count + 1}] 선택지 {index + 1}: {selectedText}");

        var choice = currentDialogueData.choices[index];
        currentDialogues = choice.resultDialogues;
        dialogueIndex = 0;
        isInResultPhase = true;

        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);

        ShowNextDialogue();
    }

    public void RestartDialogue()
    {
        dialogueIndex = 0;
        isInResultPhase = false;

        dialogueText.text = "";
        endText.SetActive(false);
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(true);

        ShowNextDialogue();
    }

    public void ToggleLog()
    {
        bool isActive = logPanel.activeSelf;
        logPanel.SetActive(!isActive);

        if (!isActive)
        {
            logText.text = string.Join("\n", logHistory);
            Canvas.ForceUpdateCanvases();
            logScrollRect.verticalNormalizedPosition = 0f;
        }
    }

    public void SetDialogue(
    DialogueData data,
    int correctIndex = -1,
    QuestManager questMgr = null,
    Sprite dialogueImg = null,
    Sprite choiceImg = null)
    {
        // 상태 완전 초기화
        dialogueIndex = 0;
        isInResultPhase = false;
        currentDialogueData = null;
        currentDialogues = null;
        correctChoiceIndex = -1;
        questManagerRef = null;

        // 새 데이터 적용
        currentDialogueData = data;
        currentDialogues = data.dialogues;
        correctChoiceIndex = correctIndex;
        questManagerRef = questMgr;

        dialogueSprite = dialogueImg != null ? dialogueImg : spriteB;
        choiceSprite = choiceImg != null ? choiceImg : spriteA;

        // UI 리셋
        dialogueText.text = "";
        endText.SetActive(false);
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(true);
        characterImage.sprite = dialogueSprite;

        ShowNextDialogue();
    }


}
