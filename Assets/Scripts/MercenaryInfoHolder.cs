using UnityEngine;

public class MercenaryInfoHolder : MonoBehaviour
{
    public MercenaryData data;

    void Start()
    {
        if (data != null)
        {
            Debug.Log($"{gameObject.name}: {data.mercenaryName}, ���ݷ�: {data.attack}, HP: {data.hp}");
            // �ʿ��ϸ� ���⼭ Animator�� UI�� ����.
        }
    }
}
