using UnityEngine;

public class BuildingClickHandler : MonoBehaviour
{
    public GameObject uiPanel;  // ��� UI �г� (�����Ϳ��� ����)

    void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} Ŭ����!");
        if (uiPanel != null)
            uiPanel.SetActive(true);
    }
}
