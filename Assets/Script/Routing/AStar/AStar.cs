using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class AStar
{
    private class AStarInfo
    {
        public AStarGrid.Cell cell;
        public AStarInfo previous;
        public float step;
        public float distance;
        public float Weight { get { return step + distance; } }
    }

    private AStarGrid _grid;
    public AStar(AStarGrid grid)
    { 
        _grid = grid; 
    }

    /// <summary>
    /// 最短経路を取得する
    /// </summary>
    public List<AStarGrid.Cell> GetShortestWay(AStarGrid.Cell startCell, AStarGrid.Cell goalCell)
    {
        //最短経路
        var root = new List<AStarGrid.Cell>();
        //通った道
        var passedCells = new List<AStarGrid.Cell>();
        //現在起点になっているセル
        var recentTargets = new List<AStarInfo>();
        passedCells.Add(startCell);
        recentTargets.Add(GetAStarInfo(startCell, goalCell, null));
        AStarInfo goalInfo = null;

        while(true)
        {
            // recentTargetsのうちweightが最も低いものを計算対象とする
            var currentTarget = recentTargets
                .OrderBy(info => info.Weight)
                .FirstOrDefault();

            // ターゲットの隣接セルのAStarInfoを取得する
            var adjacentInfos = _grid.GetAdjacences(currentTarget.cell.Coord.X, currentTarget.cell.Coord.Y)
                .Where(cell => {
                    // タイプが道でもなくゴールのセルでもない場合は対象外
                    if (cell.GroundType != AStarGrid.GroundType.Road && cell != goalCell)
                    {
                        return false;
                    }
                    // 計算済みのセルは対象外
                    if (passedCells.Contains(cell))
                    {
                        return false;
                    }
                    return true;
                })
                .Select(cell => GetAStarInfo(cell, goalCell, currentTarget))
                .ToList();

            // recentTargetsとpassedCellsを更新
            recentTargets.Remove(currentTarget);
            recentTargets.AddRange(adjacentInfos);
            passedCells.Add(currentTarget.cell);

            // ゴールが含まれていたらそこで終了
            goalInfo = adjacentInfos.FirstOrDefault(info => info.cell == goalCell);
            if (goalInfo != null)
            {
                break;
            }
            // recentTargetsがゼロだったら行き止まりなので終了
            if (recentTargets.Count == 0)
            {
                break;
            }
        }

        // ゴールが結果に含まれていない場合は最短経路が見つからなかった
        if (goalInfo == null)
        {
            return root;
        }

        // Previousを辿ってセルのリストを作成する
        root.Add(goalInfo.cell);
        AStarInfo current = goalInfo;
        while (true)
        {
            if (current.previous != null)
            {
                root.Add(current.previous.cell);
                current = current.previous;
            }
            else
            {
                break;
            }
        }
        return root;
    }

    private AStarInfo GetAStarInfo(AStarGrid.Cell targetCell, AStarGrid.Cell goalCell, AStarInfo previousInfo)
    {
        var result = new AStarInfo();
        result.cell = targetCell;
        result.previous = previousInfo;
        result.step = previousInfo == null ? 0 : previousInfo.step + 1;
        result.distance = Mathf.Abs(goalCell.Coord.X - targetCell.Coord.X) + Mathf.Abs(goalCell.Coord.Y - targetCell.Coord.Y);
        return result;
    }
}
