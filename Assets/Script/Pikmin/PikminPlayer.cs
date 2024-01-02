using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikminPlayer : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotSpeed;
    [SerializeField] float _gravSpeed;

    public List<Pikmin> _followingPikmins;
    [SerializeField] GameObject _pikminPrefab;
    [SerializeField] Transform _playerGathPos;
    [SerializeField] Transform _onionPos;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Camera camera;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Pikmin pik = Instantiate(_pikminPrefab, _onionPos.position, Quaternion.identity).GetComponent<Pikmin>();
        }
        //ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆì¸óÕ
        float x = Input.GetAxis("Horizontal") * _speed;
        float z = Input.GetAxis("Vertical") * _speed;

        _rb.velocity = (transform.forward * z + Vector3.down * _gravSpeed);
        transform.Rotate(new Vector3(0, x, 0) * _rotSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        camera.transform.LookAt(transform.position);
    }
}
