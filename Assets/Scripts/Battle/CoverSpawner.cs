// Assets/Scripts/Cover/CoverSpawner.cs
using UnityEngine;

public class CoverSpawner : MonoBehaviour
{
    public BattleGridManager gridManager;
    public Vector2Int[] coverCells;        // grid coordinates to place covers

    void Start()
    {
        foreach (var cell in coverCells)
        {
            gridManager.PlaceCover(cell.x, cell.y);
        }
    }
}
