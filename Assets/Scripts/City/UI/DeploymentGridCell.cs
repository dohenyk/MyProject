using UnityEngine;
using UnityEngine.UI;

public class DeploymentGridCell : MonoBehaviour
{
    public Vector2Int cellCoordinate;
    public MercenaryDeploymentUIController controller;

    void Start()
    {
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnCellClicked);
        }
    }

    void OnCellClicked()
    {
        controller.AssignSelectedToCell(cellCoordinate);
    }
}
