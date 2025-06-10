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
            Debug.Log($"{data.mercenaryName} °í¿ëµÊ! ÇöÀç ÃÑ {hiredMercenaries.Count}¸í.");
        }
        else
        {
            Debug.Log($"{data.mercenaryName} ÀÌ¹Ì °í¿ëµÊ.");
        }
    }

    public List<MercenaryData> GetHiredMercenaries()
    {
        return hiredMercenaries;
    }
}
