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
            DontDestroyOnLoad(gameObject);  // 씬 전환해도 이 오브젝트 유지
        }
        else
        {
            Destroy(gameObject);  // 두 번째 생긴 건 삭제
        }
    }

    public void AdvanceTurn()
    {
        currentTurn++;
        Debug.Log($"현재 턴: {currentTurn}");

        // 앞으로 여기서 턴 관련 이벤트들을 호출하게 될 거야
        // 예: CheckExpiredContracts();
        // 예: RefreshAvailableMercenaries();
        // 예: ApplyMaintenanceCosts();
    }

}
