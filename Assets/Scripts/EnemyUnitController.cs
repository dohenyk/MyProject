using UnityEngine;

public class EnemyUnitController : UnitController
{
    public EnemyData enemyData;

    new void Start()
    {
        ApplyEnemyData();
        FindFireClip();  // Fire 애니메이션 자동 할당
    }

    public void ApplyEnemyData()
    {
        if (mercenaryData != null)
        {
            attackSpeed = mercenaryData.attac_speed;
            attackDamage = mercenaryData.attack;
            maxHP = mercenaryData.hp;
            Debug.Log($"[데이터 적용] {mercenaryData.mercenaryName}의 공격 속도: {attackSpeed}회/초, 공격력 {attackDamage}, HP {maxHP}");
        }
        else if (enemyData != null)
        {
            attackSpeed = enemyData.attac_speed;
            attackDamage = enemyData.attack;
            maxHP = enemyData.hp;
            Debug.Log($"[Enemy 데이터 적용] {enemyData.enemyName}의 공격 속도: {attackSpeed}회/초, 공격력 {attackDamage}, HP {maxHP}");
        }
        else
        {
            Debug.LogWarning("MercenaryData와 EnemyData가 모두 연결되어 있지 않습니다. 기본 스탯 사용.");
        }

        currentHP = maxHP;
    }
}
