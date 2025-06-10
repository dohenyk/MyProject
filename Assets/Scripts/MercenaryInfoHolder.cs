using UnityEngine;

public class MercenaryInfoHolder : MonoBehaviour
{
    public MercenaryData data;

    void Start()
    {
        if (data != null)
        {
            Debug.Log($"{gameObject.name}: {data.mercenaryName}, 공격력: {data.attack}, HP: {data.hp}");
            // 필요하면 여기서 Animator나 UI에 연결.
        }
    }
}
