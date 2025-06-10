using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
public GameObject[] enemyPrefabs;
public EnemyData[] enemyDatas;
public BattleGridManager gridManager;

    void Start()
    {
        int index = 0;
        foreach (var prefab in enemyPrefabs)
        {
            int xFromRight = index / gridManager.gridHeight;
            int yFromTop = index % gridManager.gridHeight;
            int x = gridManager.gridWidth - 1 - xFromRight;
            int y = gridManager.gridHeight - 1 - yFromTop;

            if (x < 0)
            {
                Debug.LogWarning("그리드 공간이 부족하여 더 이상 스폰할 수 없습니다.");
                break;
            }

            Vector3 pos = gridManager.GetWorldPosition(x, y);
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

            // add movement behavior to seek nearest cover
            obj.AddComponent<UnitCoverMovement>();

            // EnemyUnitController에 EnemyData 연결
            var controller = obj.GetComponent<EnemyUnitController>();
            if (controller != null && enemyDatas != null && index < enemyDatas.Length)
            {
                controller.enemyData = enemyDatas[index];

                // Animator에 AnimatorController 연결
                var animator = obj.GetComponent<Animator>();
                var data = enemyDatas[index];
                if (data != null && data.animatorController != null)
                {
                    animator.runtimeAnimatorController = data.animatorController;
                }
            }
            index++;
        }
    }
}
