using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MercenarySlotUI : MonoBehaviour
{
    public Image mercenaryImage;
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI priceText;

    private AnimatorOverrideController overrideController;
    private MercenaryData assignedData;

    public void SetData(MercenaryData data)
    {
        assignedData = data;
        StartCoroutine(SetupAnimatorNextFrame(data));
    }

    private IEnumerator SetupAnimatorNextFrame(MercenaryData data)
    {
        // Animator가 완전히 초기화되도록 한 프레임 대기
        yield return null;

        if (animator != null && animator.runtimeAnimatorController != null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = overrideController;

            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            overrideController.GetOverrides(overrides);
            if (overrides.Count > 0)
            {
                string baseClipName = overrides[0].Key.name;
                overrideController[baseClipName] = data.idleAnimation;
            }
        }

        if (nameText != null)
            nameText.text = data.mercenaryName;

        if (statsText != null)
            statsText.text = $"공격: {data.attack}\n공격 속도: {data.attac_speed}\nHP: {data.hp}";

        if (priceText != null)
            priceText.text = $"{data.price} 원";
    }

    public void OnHireButtonClicked()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager 인스턴스를 찾을 수 없습니다!");
            return;
        }

        int price = assignedData.price;

        if (GameManager.Instance.currentWon >= price)
        {
            GameManager.Instance.currentWon -= price;
            Debug.Log($"{assignedData.mercenaryName} 고용 성공! 남은 돈: {GameManager.Instance.currentWon} 원");

            MercenaryHireManager.Instance.AddHiredMercenary(assignedData);
        }
        else
        {
            Debug.Log($"돈이 부족합니다! {price} 원 필요, 현재 {GameManager.Instance.currentWon} 원 보유.");
        }
    }


}
