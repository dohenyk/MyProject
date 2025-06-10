using UnityEngine;
using System.Collections.Generic;

public class MercenaryUIController : MonoBehaviour
{
    public List<MercenarySlotUI> slotList;          // Inspector에서 슬롯들 연결
    public List<MercenaryData> mercenaryDataList;   // Inspector에서 용병 데이터들 연결

    void OnEnable()
    {
        FillSlotsWithRandomMercenaries();
    }

    void FillSlotsWithRandomMercenaries()
    {
        if (mercenaryDataList.Count == 0)
        {
            Debug.LogWarning("용병 데이터가 준비되지 않았습니다.");
            return;
        }

        // 데이터 복사 및 셔플
        List<MercenaryData> tempList = new List<MercenaryData>(mercenaryDataList);
        for (int i = 0; i < tempList.Count; i++)
        {
            int randIndex = Random.Range(i, tempList.Count);
            var temp = tempList[i];
            tempList[i] = tempList[randIndex];
            tempList[randIndex] = temp;
        }

        // 슬롯 채우기
        for (int i = 0; i < slotList.Count; i++)
        {
            if (i < tempList.Count)
            {
                slotList[i].SetData(tempList[i]);
            }
            else
            {
                Debug.LogWarning($"슬롯 {i}에 할당할 용병 데이터가 부족합니다!");
            }
        }
    }
}
