using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Boidparam")]
public class Boidparam : ScriptableObject
{
    public float InitSpeed = 2f;
    public float MinSpeed = 2f;
    public float MaxSpeed = 5f;
    public float NeighborDistance = 1f;
    public float NeighborFov = 90f;
    public float SeparationWeight = 5f;
    public float WallScale = 5f;
    public float WallDistance = 3f;
    public float WallWeight = 1f;
    public float AlignmentWeight = 2f;
    public float CohesionWeight = 3f;
}
