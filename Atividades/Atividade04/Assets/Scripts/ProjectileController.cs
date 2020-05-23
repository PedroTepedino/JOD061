using System;
using Mirror;
using UnityEngine;

public class ProjectileController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 20f;
    
    private void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * _moveSpeed);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        NetworkServer.Destroy(gameObject);
        NetworkServer.Destroy(other.gameObject);
    }
}
