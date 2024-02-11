using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Param")]
public class BoidParam : MonoBehaviour
{
    [Header("初速")]
    public float InitSpeed = 2f;
    [Header("最低速度")]
    public float MinSpeed = 2f;
    [Header("最高速度")]
    public float MaxSpeed = 5f;
    [Header("隣との距離")]
    public float NeighborDistance = 1f;
    [Header("視野角")]
    public float NeighborFov = 90f;
    [Header("分離の重み")]
    public float SeparationWeight = 5f;
    [Header("壁の大きさ")]
    public float WallScale = 5f;
    [Header("壁までの距離感")]
    public float WallDistance = 3f;
    [Header("壁の重さ")]
    public float WallWeight = 1f;
    [Header("整列の重み")]
    public float AlignmentWeight = 2f;
    [Header("結合の重み")]
    public float CohesionWeight = 3f;
}
