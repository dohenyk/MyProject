using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class GridCreator : MonoBehaviour
{
    public GameObject cellPrefab;
    public int columns = 16;
    public int rows = 5;

    private void Start()
    {
        var grid = GetComponent<GridLayoutGroup>();
        var rt = GetComponent<RectTransform>();

        // 1. 셀 크기와 간격 읽기
        Vector2 cellSize = grid.cellSize;
        Vector2 spacing = grid.spacing;

        // 2. 전체 그리드 크기 계산
        float totalWidth = columns * (cellSize.x + spacing.x) - spacing.x;
        float totalHeight = rows * (cellSize.y + spacing.y) - spacing.y;

        // 3. GridPanel(RectTransform)의 크기를 정확히 설정
        rt.sizeDelta = new Vector2(totalWidth, totalHeight);

        // 4. GridPanel이 화면 중심에 오도록 설정
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;

        // 5. 셀 생성
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                cell.name = $"Cell_{x}_{y}";

                var cellComp = cell.GetComponent<DeploymentGridCell>();
                if (cellComp != null)
                {
                    cellComp.cellCoordinate = new Vector2Int(x, y);
                    cellComp.controller = GetComponentInParent<MercenaryDeploymentUIController>();
                }

                var text = cell.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = $"({x},{y})";
            }
        }
    }
}
