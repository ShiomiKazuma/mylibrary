using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Boidparam Boidparam;
    public Vector3 Pos { get; private set; }
    public Vector3 Velocity { get; private set; }
    Vector3 _accel = Vector3.zero;
    List<Boid> _neighbors = new List<Boid>();
    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position;
        Velocity = transform.forward * Boidparam.InitSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // 近隣の個体を探して neighbors リストを更新
        UpdateNeighbors();

        // 壁に当たりそうになったら向きを変える
        UpdateWalls();

        // 近隣の個体から離れる
        UpdateSeparation();

        // 近隣の個体と速度を合わせる
        UpdateAlignment();

        // 近隣の個体の中心に移動する
        UpdateCohesion();

        // 上記 4 つの結果更新された accel を velocity に反映して位置を動かす
        UpdateMove();
    }

    void UpdateNeighbors()
    {

    }

    void UpdateWalls()
    {

    }

    void UpdateSeparation()
    {

    }

    void UpdateAlignment()
    {

    }

    void UpdateCohesion()
    {

    }

    void UpdateMove()
    {
        var deltatime = Time.deltaTime;
        Velocity += _accel * deltatime;
        var dir = Velocity.normalized;
        var speed = Velocity.magnitude;
        Velocity = Mathf.Clamp(speed, Boidparam.MinSpeed, Boidparam.MaxSpeed) * dir;
        Pos += Velocity * deltatime;

        var rot = Quaternion.LookRotation(Velocity);
        transform.SetPositionAndRotation(Pos, rot);
        _accel = Vector3.zero;
    }
}
