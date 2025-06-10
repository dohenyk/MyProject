using TMPro.Examples;
using UnityEngine;
using System.Collections;


public class UnitController : MonoBehaviour
{
    public float attackSpeed; // 초당 공격 횟수

    public Animator animator;
    public AnimationClip fireClip; // Fire 애니메이션 클립 (재생 길이를 구하기 위해)

    public MercenaryData mercenaryData;

    public float attackDamage = 1f;   // 공격력
    public float maxHP = 10f;         // 최대 HP
    public float currentHP;           // 현재 HP

    [HideInInspector]
    public GameObject currentTarget;  // 공격 대상

    private Coroutine attackCoroutine;

    void Start()
    {
        if (mercenaryData != null)
        {
            attackSpeed = mercenaryData.attac_speed;
            attackDamage = mercenaryData.attack;
            maxHP = mercenaryData.hp;
            Debug.Log($"[데이터 적용] {mercenaryData.mercenaryName}의 공격 속도: {attackSpeed}회/초, 공격력 {attackDamage}, HP {maxHP}");
        }
        else
        {
            Debug.LogWarning("MercenaryData가 연결되어 있지 않습니다. 기본 스탯 사용.");
        }

        currentHP = maxHP;

        // ––––– Clone 접미사 제거 –––––
        string baseName = gameObject.name;
        if (baseName.EndsWith("(Clone)"))
            baseName = baseName.Substring(0, baseName.Length - 7);
        // ––––– 아래부터 baseName 사용 –––––

        string fireClipName = baseName + "_Fire";
        var controller = animator.runtimeAnimatorController;
        foreach (var clip in controller.animationClips)
        {
            if (clip.name == fireClipName)
            {
                fireClip = clip;
                Debug.Log($"Fire 애니메이션 클립 자동 할당 완료! 재생 시간 : {fireClip.length} 초");
                break;
            }
        }

        if (fireClip == null)
        {
            //Debug.LogWarning("Fire 애니메이션 클립을 찾지 못했음!");
        }
    }

    public void SetIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAiming", false);
        animator.SetBool("isSitting", false);
        StopAttack();
        Debug.Log("상태: Idle");
    }

    public void SetWalk()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAiming", false);
        animator.SetBool("isSitting", false);
        StopAttack();
        Debug.Log("상태: Walk");
    }

    public void SetAimOn()
    {
        animator.SetBool("isAiming", true);
        animator.SetBool("isWalking", false);
        Debug.Log("상태: Aim On (조준 시작)");
    }

    public void SetAimOff()
    {
        animator.SetBool("isAiming", false);
        StopAttack();
        Debug.Log("상태: Aim Off (조준 해제)");
    }

    public void SetSit()
    {
        animator.SetBool("isSitting", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAiming", false);
        StopAttack();
        Debug.Log("상태: Sit (엄폐)");
    }

    public void SetUp()
    {
        animator.SetBool("isSitting", false);
        animator.SetBool("isAiming", false);
        StopAttack();
        Debug.Log("상태: Up (일어서기)");
    }

    public void StartAttack()
    {
        if (animator.GetBool("isAiming") == true)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackLoop());
                Debug.Log("자동 공격 시작!");
            }
        }
        else
        {
            Debug.Log("현재 조준 상태가 아니라 FireTrigger를 실행하지 않음.");
        }
    }

    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            Debug.Log("자동 공격 중지!");
        }
    }

    private IEnumerator AttackLoop()
    {
        float targetCycleTime = 1f / attackSpeed;        // 원래 계산된 주기 (초)
        float minCycleTime = fireClip.length;            // 최소 주기 (애니메이션 길이)
        float maxAllowedSpeed = 1f / minCycleTime;       // 최대 허용 공격 속도 (초당 몇 회)

        float cycleTime = Mathf.Max(targetCycleTime, minCycleTime); // 최종 사용할 주기
        float actualAppliedSpeed = 1f / cycleTime;                 // 최종 적용 속도 (초당 몇 회)

        if (cycleTime > targetCycleTime)
        {
            Debug.LogWarning(
                $"[공격 속도 제한] 입력된 attackSpeed: {attackSpeed:F2}회/초 → 최대 허용 속도: {maxAllowedSpeed:F2}회/초 → 실제 적용 속도: {actualAppliedSpeed:F2}회/초 (애니메이션 길이 {fireClip.length:F2}초 기준)"
            );
        }

        while (true)
        {
            if (currentTarget == null)
            {
                currentTarget = AcquireNearestOpponent();
                if (currentTarget == null)
                {
                    SetAimOff();
                    yield break;
                }

                Debug.Log($"{gameObject.name} -> {currentTarget.name} 새 타겟 조준");
            }

            animator.SetTrigger("FireTrigger");
            Debug.Log($"{gameObject.name} -> {currentTarget.name} FireTrigger 실행");

            yield return new WaitForSeconds(fireClip.length);

            if (currentTarget != null)
            {
                var targetCtrl = currentTarget.GetComponent<UnitController>();
                if (targetCtrl != null)
                {
                    targetCtrl.TakeDamage(attackDamage);
                    Debug.Log($"{gameObject.name}이(가) {currentTarget.name}에게 {attackDamage} 데미지 부여");

                    if (targetCtrl.currentHP <= 0)
                    {
                        currentTarget = null;
                    }
                }
                else
                {
                    currentTarget = null;
                }
            }
            else
            {
                currentTarget = null;
            }

            float waitTime = cycleTime - fireClip.length;

            if (waitTime > 0f)
            {
                Debug.Log($"AimHold 대기 {waitTime:F2}초");
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    public GameObject AcquireNearestOpponent()
    {
        string opponentTag = gameObject.CompareTag("Enemy") ? "Merc" : "Enemy";
        var opponents = GameObject.FindGameObjectsWithTag(opponentTag);
        if (opponents.Length == 0)
            return null;

        GameObject nearest = null;
        float nearestDist = float.MaxValue;
        foreach (var opp in opponents)
        {
            var ctrl = opp.GetComponent<UnitController>();
            if (ctrl != null && ctrl.currentHP > 0)
            {
                float dist = Vector3.Distance(transform.position, opp.transform.position);
                if (dist < nearestDist)
                {
                    nearest = opp;
                    nearestDist = dist;
                }
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

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log($"{gameObject.name} 피격: {amount}, 남은 HP {currentHP}");
        if (currentHP <= 0)
        {
            Debug.Log($"{gameObject.name} 사망!");
            Destroy(gameObject);
        }
    }
}
