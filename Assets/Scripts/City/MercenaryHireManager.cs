using UnityEngine;
using System.Collections.Generic;

public class MercenaryHireManager : MonoBehaviour
{
    public static MercenaryHireManager Instance;

    private List<MercenaryData> hiredMercenaries = new List<MercenaryData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddHiredMercenary(MercenaryData data)
    {
        if (!hiredMercenaries.Contains(data))
        {
            hiredMercenaries.Add(data);
            Debug.Log($"{data.mercenaryName} ����! ���� �� {hiredMercenaries.Count}��.");
        }
        else
        {
            Debug.Log($"{data.mercenaryName} �̹� ����.");
        }
    }

    public List<MercenaryData> GetHiredMercenaries()
    {
        return hiredMercenaries;
    }
}
