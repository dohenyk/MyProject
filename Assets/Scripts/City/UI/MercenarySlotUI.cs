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
        // Animator�� ������ �ʱ�ȭ�ǵ��� �� ������ ���
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
            statsText.text = $"����: {data.attack}\n���� �ӵ�: {data.attac_speed}\nHP: {data.hp}";

        if (priceText != null)
            priceText.text = $"{data.price} ��";
    }

    public void OnHireButtonClicked()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager �ν��Ͻ��� ã�� �� �����ϴ�!");
            return;
        }

        int price = assignedData.price;

        if (GameManager.Instance.currentWon >= price)
        {
            GameManager.Instance.currentWon -= price;
            Debug.Log($"{assignedData.mercenaryName} ��� ����! ���� ��: {GameManager.Instance.currentWon} ��");

            MercenaryHireManager.Instance.AddHiredMercenary(assignedData);
        }
        else
        {
            Debug.Log($"���� �����մϴ�! {price} �� �ʿ�, ���� {GameManager.Instance.currentWon} �� ����.");
        }
    }


}
