using UnityEngine;

public class BuildingClickHandler : MonoBehaviour
{
    public GameObject uiPanel;  // 띄울 UI 패널 (에디터에서 연결)

    void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} 클릭됨!");
        if (uiPanel != null)
            uiPanel.SetActive(true);
    }
}
