using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("LobbyScene");
            return;
        }

        PhotonNetwork.Instantiate(this._playerPrefab.name, new Vector3(Random.Range(-5f, 5f), 0f, 0f),
            Quaternion.identity, 0);
    }

    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
