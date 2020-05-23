using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 0.1f;
    [SerializeField] private float _moveRotation = 1f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectilePosition;

    private void Start()
    {
        Material material = GetComponent<Renderer>().material;
        material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
        transform.Translate(0, 0, Input.GetAxis("Vertical") * _moveSpeed);
        transform.Rotate(0, Input.GetAxis("Horizontal") * _moveRotation, 0f);

        if (Input.GetKeyDown(KeyCode.Space))
            CmdShoot();
    }

    [Command]
    private void CmdShoot()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _projectilePosition.position, this.transform.rotation);
        NetworkServer.Spawn(projectile);
    }
}
