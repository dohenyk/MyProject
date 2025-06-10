using UnityEngine;

public class BuildingOutline : MonoBehaviour
{
    public Sprite cityNormalSprite;        // �⺻ ��� ��������Ʈ
    public Sprite cityOutlineSprite;       // �ƿ����� ���� ��������Ʈ

    private SpriteRenderer cityRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �θ𿡼� ���� ��������Ʈ ��������
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
