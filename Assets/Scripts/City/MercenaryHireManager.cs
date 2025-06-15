using UnityEngine;
using System.Collections.Generic;

public class MercenaryHireManager : MonoBehaviour
{
    public static MercenaryHireManager Instance;

    private List<MercenaryData> hiredMercenaries = new List<MercenaryData>();
    private Dictionary<MercenaryData, Vector2Int> mercenaryPositions = new Dictionary<MercenaryData, Vector2Int>();

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
            Debug.Log($"{data.mercenaryName} 고용! 현재 수: {hiredMercenaries.Count}.");
        }
        else
        {
            Debug.Log($"{data.mercenaryName} 이미 고용됨.");
        }
    }

    public void SetMercenaryPosition(MercenaryData data, Vector2Int cell)
    {
        if (hiredMercenaries.Contains(data))
        {
            mercenaryPositions[data] = cell;
        }
        else
        {
            Debug.LogWarning($"{data.mercenaryName}은(는) 고용 상태가 아닙니다.");
        }
    }

    public bool TryGetMercenaryPosition(MercenaryData data, out Vector2Int cell)
    {
        return mercenaryPositions.TryGetValue(data, out cell);
    }

    public List<MercenaryData> GetHiredMercenaries()
    {
        return hiredMercenaries;
    }
}
