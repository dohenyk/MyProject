using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MercenaryDeploymentUIController : MonoBehaviour
{
    public GridCreator gridCreator;
    public Transform mercListParent;
    public HiredMercenarySlotUI hiredSlotPrefab;

    private MercenaryData selectedMercenary;
    private Dictionary<Vector2Int, HiredMercenarySlotUI> cellIndicators = new Dictionary<Vector2Int, HiredMercenarySlotUI>();

    void OnEnable()
    {
        PopulateMercenaryList();
    }

    void PopulateMercenaryList()
    {
        foreach (Transform child in mercListParent)
        {
            Destroy(child.gameObject);
        }

        var hiredList = MercenaryHireManager.Instance.GetHiredMercenaries();
        foreach (var data in hiredList)
        {
            var obj = Instantiate(hiredSlotPrefab.gameObject, mercListParent);
            var slot = obj.GetComponent<HiredMercenarySlotUI>();
            slot.SetData(data);
            var button = obj.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => SelectMercenary(data));
            }
        }
    }

    public void SelectMercenary(MercenaryData data)
    {
        selectedMercenary = data;
    }

    public void AssignSelectedToCell(Vector2Int cell)
    {
        if (selectedMercenary == null)
            return;

        MercenaryHireManager.Instance.SetMercenaryPosition(selectedMercenary, cell);
        selectedMercenary = null;
    }
}
