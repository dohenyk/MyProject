using UnityEngine;

[CreateAssetMenu(fileName = "NewMercenaryData", menuName = "Game/Mercenary Data")]
public class MercenaryData : ScriptableObject
{
    public string mercenaryName;
   
    public float attack;
    public float attac_speed;
    public float hp;
    public float move_speed;

    public int price;

    public RuntimeAnimatorController animatorController;

    public Sprite portrait;              // 초상화 이미지
    public AnimationClip idleAnimation; // Idle 애니메이션 클립
}
