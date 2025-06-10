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

                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            if (targetCover != null && (targetCover.OccupiedBy == null || targetCover.OccupiedBy == gameObject) &&
                Vector3.Distance(transform.position, targetPos) <= 0.05f)
            {
                targetCover.Reserve(gameObject);   // occupy
                unitController.SetSit();

                // Sit_Idle 상태가 될 때까지 대기한 뒤 조준 상태로 전환
                yield return new WaitUntil(() =>
                    unitController.animator.GetCurrentAnimatorStateInfo(0).IsName("Sit_Idle"));

                var target = AimAtNearestOpponent();
                if (target != null)
                    Debug.Log($"{gameObject.name} -> {target.name} 조준");

                unitController.SetAimOn();
                yield break;
            }

            yield return null; // re-evaluate target in next loop
        }
    }

    private CoverObject FindNearestAvailableCover()
    {
        var covers = FindObjectsOfType<CoverObject>();
        if (covers.Length == 0)
            return null;

        return covers
            .Where(c => c.IsAvailable || c.OccupiedBy == gameObject)
            .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
            .FirstOrDefault(c => c.IsAvailable || c.OccupiedBy == gameObject);
    }

    private GameObject AimAtNearestOpponent()
    {
        string opponentTag = gameObject.CompareTag("Enemy") ? "Merc" : "Enemy";
        var opponents = GameObject.FindGameObjectsWithTag(opponentTag);
        if (opponents.Length == 0)
            return null;

        GameObject nearest = null;
        float nearestDist = float.MaxValue;
        foreach (var opp in opponents)
        {
            float dist = Vector3.Distance(transform.position, opp.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = opp;
            }
        }

        if (nearest != null)
        {
            Vector3 dir = nearest.transform.position - transform.position;
            if (dir.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        return nearest;
    }

    void OnDestroy()
    {
        if (targetCover != null)
            targetCover.Release(gameObject);
    }
}
