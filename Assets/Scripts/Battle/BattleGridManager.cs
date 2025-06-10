using UnityEngine;

public class BattleGridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 16;
    public int gridHeight = 5;
    public float cellSize = 1f;
    public Vector2 gridCenterOffset = Vector2.zero;
    public bool drawGrid = true;

    public GameObject coverPrefab;

    public Vector3 GetWorldPosition(int x, int y)
    {
        return GridOrigin + new Vector3(x * cellSize, y * cellSize, 0f);
    }

    private Vector3 GridOrigin
    {
        get
        {
            float originX = -(gridWidth - 1) * cellSize * 0.5f + gridCenterOffset.x;
            float originY = -(gridHeight - 1) * cellSize * 0.5f + gridCenterOffset.y;
            return new Vector3(originX, originY, 0f);
        }
    }

    void OnDrawGizmos()
    {
        if (!drawGrid)
            return;

        Gizmos.color = Color.white;
        Vector3 origin = GridOrigin;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 center = origin + new Vector3(x * cellSize, y * cellSize, 0f);
                Gizmos.DrawWireCube(center, new Vector3(cellSize, cellSize, 0f));
            }
        }
    }

    public GameObject PlaceCover(int x, int y)
    {
        Vector3 pos = GetWorldPosition(x, y);
        return Instantiate(coverPrefab, pos, Quaternion.identity);
    }
}
