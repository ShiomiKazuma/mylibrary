using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AStarTest : MonoBehaviour
{
    private const int _columnCount = 10;
    private const int _rowCount = 10;
    private const float _cellSize = 1.0f;
    [SerializeField] private AStarGrid _grid = null;
    private AStar _aStar;
    private List<AStarGrid.Cell> _shortestWay = new List<AStarGrid.Cell>();

    private void OnEnable()
    {
        if(_grid == null)
        {
            _grid = new AStarGrid(_columnCount, _rowCount);
            _grid.StartCell = _grid.GetCell(0, 0);
            _grid.GoalCell = _grid.GetCell(_columnCount - 1, _rowCount - 1);
        }
        _aStar = new AStar(_grid);
    }

    private void OnDrawGizmos()
    {
        Tools.current = Tool.None;
        var preColor = Gizmos.color;
        var preMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * _cellSize);

        // クリック判定
        if (Event.current != null && Event.current.type == EventType.MouseUp)
        {
            // レイを取得する
            var clickedPosition = Event.current.mousePosition;
            clickedPosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - clickedPosition.y;
            var clickRay = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(clickedPosition);

            // レイとy=0の平面との交点を求める
            var scale = Mathf.Abs(clickRay.origin.y / clickRay.direction.y);
            var intersection = clickRay.origin + clickRay.direction * scale;

            // 選択されたセルを編集するメニューを描画
            intersection /= _cellSize;
            var selectedColumn = Mathf.FloorToInt(intersection.x);
            var selectedRow = Mathf.FloorToInt(intersection.z);
            if (selectedColumn >= 0 && selectedColumn < _columnCount && selectedRow >= 0 && selectedRow < _rowCount)
            {
                GenericMenu menu = new GenericMenu();

                // CellType変更メニュー
                var cell = _grid.GetCell(selectedColumn, selectedRow);
                menu.AddItem(new GUIContent("Set Type > Not Road"), cell.GroundType == AStarGrid.GroundType.Wall, () => {
                    Undo.RecordObject(this, "Set Type > Not Road");
                    cell.GroundType = AStarGrid.GroundType.Wall;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Set Type > Road"), cell.GroundType == AStarGrid.GroundType.Road, () => {
                    Undo.RecordObject(this, "Set Type > Road");
                    cell.GroundType = AStarGrid.GroundType.Road;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Set Type > Start"), _grid.StartCell == cell, () => {
                    Undo.RecordObject(this, "Set Type > Start");
                    _grid.StartCell = cell;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Set Type > Goal"), _grid.GoalCell == cell, () => {
                    Undo.RecordObject(this, "Set Type > Goal");
                    _grid.GoalCell = cell;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Show Shortest Way"), false, () => {
                    _shortestWay = _aStar.GetShortestWay(_grid.StartCell, _grid.GoalCell);
                });

                menu.ShowAsContext();
            }
        }

        // セルを描画
        System.Action<AStarGrid.Cell> drawCell = cell => {
            Gizmos.DrawWireCube(new Vector3(cell.Coord.X + 0.5f, 0.0f, cell.Coord.Y + 0.5f), new Vector3(1.0f, 0.0f, 1.0f));
        };
        Gizmos.color = Color.green;
        foreach (var item in _grid.Cells.Where(cell => cell.GroundType == AStarGrid.GroundType.Wall))
        {
            drawCell(item);
        }
        Gizmos.color = Color.yellow;
        foreach (var item in _grid.Cells.Where(cell => cell.GroundType == AStarGrid.GroundType.Road))
        {
            drawCell(item);
        }
        Gizmos.color = Color.blue;
        if (_grid.StartCell != null)
        {
            drawCell(_grid.StartCell);
        }
        Gizmos.color = Color.red;
        if (_grid.GoalCell != null)
        {
            drawCell(_grid.GoalCell);
        }
        Gizmos.color = Color.red;
        foreach (var item in _shortestWay)
        {
            Gizmos.DrawSphere(new Vector3(item.Coord.X + 0.5f, 0.0f, item.Coord.Y + 0.5f), 0.2f);
        }

        Gizmos.color = preColor;
        Gizmos.matrix = preMatrix;
    }
}
