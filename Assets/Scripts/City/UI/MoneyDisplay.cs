using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public TextMeshProUGUI dollarText;
    public TextMeshProUGUI wonText;

    void Start()
    {
        UpdateMoneyUI();
    }

    void Update()
    {
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        if (dollarText != null)
        {
            dollarText.text = GameManager.Instance.currentDollar.ToString();
        }

        if (wonText != null)
        {
            wonText.text = GameManager.Instance.currentWon.ToString();
        }
    }
}