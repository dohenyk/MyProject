using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitController))]
public class UnitCoverMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    private UnitController unitController;
    private CoverObject targetCover;

    void Start()
    {
        unitController = GetComponent<UnitController>();

        // Prefer data-driven speed if available
        if (unitController.mercenaryData != null)
            moveSpeed = unitController.mercenaryData.move_speed;
        else if (unitController is EnemyUnitController enemy && enemy.enemyData != null)
            moveSpeed = enemy.enemyData.move_speed;

        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (targetCover == null || (targetCover.OccupiedBy != null && targetCover.OccupiedBy != gameObject))
            {
                targetCover = FindNearestAvailableCover();
                if (targetCover == null)
                {
                    unitController.SetIdle();
                    yield break;
                }
            }

            unitController.SetWalk();
            Vector3 targetPos = targetCover.transform.position;

            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                if (targetCover == null || (targetCover.OccupiedBy != null && targetCover.OccupiedBy != gameObject))
                    break;

                // 이동 방향에 맞춰 스프라이트가 목적지를 바라보게 조정
                Vector3 dir = targetPos - transform.position;
                if (dir.x < 0)
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                else if (dir.x > 0)
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            if (targetCover != null && (targetCover.OccupiedBy == null || targetCover.OccupiedBy == gameObject) &&
                Vector3.Distance(transform.position, targetPos) <= 0.05f)
            {
                targetCover.Reserve(gameObject);   // occupy
                unitController.SetSit();

                // Sit 애니메이션이 끝나고 Sit_Idle 상태(머쓱/적 모두)에 도달할 때까지 대기
                yield return new WaitUntil(IsSitIdleState);

                var target = unitController.AcquireNearestOpponent();
                if (target != null)
                    Debug.Log($"{gameObject.name} -> {target.name} 조준");

                unitController.currentTarget = target;
                unitController.SetAimOn();
                unitController.FindFireClip();
                unitController.StartAttack();
                yield break;
            }

            yield return null; // re-evaluate target in next loop
        }
    }

    private CoverObject FindNearestAvailableCover()
    {
        var covers = Object.FindObjectsByType<CoverObject>(FindObjectsSortMode.None);
        if (covers.Length == 0)
            return null;

        return covers
            .Where(c => c.IsAvailable || c.OccupiedBy == gameObject)
            .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
            .FirstOrDefault(c => c.IsAvailable || c.OccupiedBy == gameObject);
    }

    // Sit 애니메이션 완료 후 Idle 상태에 도달했는지 확인
    private bool IsSitIdleState()
    {
        var info = unitController.animator.GetCurrentAnimatorStateInfo(0);
        return info.IsName("Merc_Sit_Idle") || info.IsName("Enemy_Sit_Idle") || info.IsName("Sit_Idle");
    }

    void OnDestroy()
    {
        if (targetCover != null)
            targetCover.Release(gameObject);
    }
}
