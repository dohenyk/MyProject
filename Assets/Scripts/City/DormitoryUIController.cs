using UnityEngine;
using System.Collections.Generic;

public class DormitoryUIController : MonoBehaviour
{
    public Transform contentParent;                   // ScrollView¿« Content
    public GameObject hiredSlotPrefab;                // HiredMercenarySlot «¡∏Æ∆’

    void OnEnable()
    {
        PopulateHiredMercenaries();
    }

    void PopulateHiredMercenaries()
    {
        // ±‚¡∏ ΩΩ∑‘ ¡§∏Æ
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        List<MercenaryData> hiredList = MercenaryHireManager.Instance.GetHiredMercenaries();

        foreach (var data in hiredList)
        {
            GameObject slotObj = Instantiate(hiredSlotPrefab, contentParent);
            HiredMercenarySlotUI slotUI = slotObj.GetComponent<HiredMercenarySlotUI>();
            slotUI.SetData(data);
        }
    }
}
