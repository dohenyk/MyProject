using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public float attack;
    public float attac_speed;
    public float hp;
    public float move_speed;

    public RuntimeAnimatorController animatorController;
}
