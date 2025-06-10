using UnityEngine;

public class CoverObject : MonoBehaviour
{
    public int maxDurability = 3;
    public int CurrentDurability { get; private set; }

    // ���� ������ ��� ���� ����
    public GameObject OccupiedBy { get; private set; }

    public bool IsAvailable => OccupiedBy == null;

    void Awake()
    {
        CurrentDurability = maxDurability;
    }

    public void Reserve(GameObject unit)
    {
        OccupiedBy = unit;
    }

    public void Release(GameObject unit)
    {
        if (OccupiedBy == unit)
            OccupiedBy = null;
    }

    public void TakeDamage(int amount)
    {
        CurrentDurability -= amount;
        if (CurrentDurability <= 0)
        {
            Destroy(gameObject);        // cover destroyed
        }
    }
}