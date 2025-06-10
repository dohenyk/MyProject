using UnityEngine;
using System.Collections.Generic;

public class MercenaryUIController : MonoBehaviour
{
    public List<MercenarySlotUI> slotList;          // Inspector���� ���Ե� ����
    public List<MercenaryData> mercenaryDataList;   // Inspector���� �뺴 �����͵� ����

    void OnEnable()
    {
        FillSlotsWithRandomMercenaries();
    }

    void FillSlotsWithRandomMercenaries()
    {
        if (mercenaryDataList.Count == 0)
        {
            Debug.LogWarning("�뺴 �����Ͱ� �غ���� �ʾҽ��ϴ�.");
            return;
        }

        // ������ ���� �� ����
        List<MercenaryData> tempList = new List<MercenaryData>(mercenaryDataList);
        for (int i = 0; i < tempList.Count; i++)
        {
            int randIndex = Random.Range(i, tempList.Count);
            var temp = tempList[i];
            tempList[i] = tempList[randIndex];
            tempList[randIndex] = temp;
        }

        // ���� ä���
        for (int i = 0; i < slotList.Count; i++)
        {
            if (i < tempList.Count)
            {
                slotList[i].SetData(tempList[i]);
            }
            else
            {
                Debug.LogWarning($"���� {i}�� �Ҵ��� �뺴 �����Ͱ� �����մϴ�!");
            }
        }
    }
}
