using System;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0.2f;
    [SerializeField] private float _moveRotation = 20f;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = this.GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!_photonView.IsMine)
            return;
        
        transform.Translate(0f, 0f, Input.GetAxis("Vertical") * _moveSpeed);
        transform.Rotate(0f, Input.GetAxis("Horizontal") * _moveRotation, 0f);
    }
}