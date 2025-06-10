using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class HiredMercenarySlotUI : MonoBehaviour
{
    public Image mercenaryImage;
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statsText;

    private AnimatorOverrideController overrideController;

    public void SetData(MercenaryData data)
    {
        StartCoroutine(SetupAnimatorNextFrame(data));
    }

    private IEnumerator SetupAnimatorNextFrame(MercenaryData data)
    {
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
    }
}
