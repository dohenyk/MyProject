using UnityEngine;

public class MercenarySpawner : MonoBehaviour
{
    public GameObject unitPrefab;      // Instantiate할 용병 프리팹
    public BattleGridManager gridManager;


    void Start()
    {
        var hiredList = MercenaryHireManager.Instance.GetHiredMercenaries();

        int index = 0;
        foreach (var mercData in hiredList)
        {
            Vector2Int cell;
            if (!MercenaryHireManager.Instance.TryGetMercenaryPosition(mercData, out cell))
            {
                int tempX = index / gridManager.gridHeight;
                int yFromTop = index % gridManager.gridHeight;
                int tempY = gridManager.gridHeight - 1 - yFromTop;
                cell = new Vector2Int(tempX, tempY);
            }

            int x = cell.x;
            int y = cell.y;

            if (x >= gridManager.gridWidth)
            {
                Debug.LogWarning("그리드 공간이 부족하여 더 이상 스폰할 수 없습니다.");
                break;
            }

            Vector3 position = gridManager.GetWorldPosition(x, y);
            GameObject unitObj = Instantiate(unitPrefab, position, Quaternion.identity);
            unitObj.tag = "Merc"; // 태그 설정으로 적이 인식 가능하도록

            // add movement behavior to seek nearest cover
            unitObj.AddComponent<UnitCoverMovement>();

            // UnitController에 MercenaryData 연결 및 초기화
            var controller = unitObj.GetComponent<UnitController>();
            controller.mercenaryData = mercData;
            controller.ApplyData();

            // Animator에 AnimatorController 연결
            var animator = unitObj.GetComponent<Animator>();
            if (mercData.animatorController != null)
            {
                animator.runtimeAnimatorController = mercData.animatorController;
                controller.FindFireClip();
                Debug.Log($"{mercData.mercenaryName}에 AnimatorController 연결 완료!");
            }
            else
            {
                Debug.LogWarning($"{mercData.mercenaryName}의 AnimatorController가 비어 있습니다!");
            }

            Debug.Log($"{mercData.mercenaryName}가 {position} 위치에 배치됨");

            index++;
        }
    }

}
