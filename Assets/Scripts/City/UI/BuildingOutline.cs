using UnityEngine;

public class BuildingOutline : MonoBehaviour
{
    public Sprite cityNormalSprite;        // 기본 배경 스프라이트
    public Sprite cityOutlineSprite;       // 아웃라인 포함 스프라이트

    private SpriteRenderer cityRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 부모에서 도시 스프라이트 가져오기
        cityRenderer = transform.parent.GetComponent<SpriteRenderer>();

        if (cityRenderer != null)
            cityRenderer.sprite = cityNormalSprite;
    }

    void OnMouseEnter()
    {
        if (cityRenderer != null)
            cityRenderer.sprite = cityOutlineSprite;
    }

    void OnMouseExit()
    {
        if (cityRenderer != null)
            cityRenderer.sprite = cityNormalSprite;
    }
}
