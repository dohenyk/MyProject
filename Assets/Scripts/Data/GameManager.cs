using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentDollar = 1500;
    public int currentWon = 32000;

    public int currentTurn = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ�ص� �� ������Ʈ ����
        }
        else
        {
            Destroy(gameObject);  // �� ��° ���� �� ����
        }
    }

    public void AdvanceTurn()
    {
        currentTurn++;
        Debug.Log($"���� ��: {currentTurn}");

        // ������ ���⼭ �� ���� �̺�Ʈ���� ȣ���ϰ� �� �ž�
        // ��: CheckExpiredContracts();
        // ��: RefreshAvailableMercenaries();
        // ��: ApplyMaintenanceCosts();
    }

}
