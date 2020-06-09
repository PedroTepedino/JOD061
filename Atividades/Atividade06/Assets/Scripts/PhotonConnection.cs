using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _button;
    [SerializeField] private string _gameVersion;
    [SerializeField] private string _roomName;
    [SerializeField] private byte _maxPlayers;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            _inputField.interactable = false;
            _button.interactable = false;
            PhotonNetwork.NickName = this._inputField.text;
            PhotonNetwork.GameVersion = this._gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            return;
        }

        if (PhotonNetwork.CountOfRooms == 0)
        {
            _button.interactable = false;
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = this._maxPlayers;
            PhotonNetwork.JoinOrCreateRoom(this._roomName, options, TypedLobby.Default);
            return;
        }

        PhotonNetwork.JoinRoom(this._roomName);
    }

    public override void OnConnectedToMaster()
    {
        _button.interactable = true;
        _button.GetComponentInChildren<TMP_Text>().text = "Jogar";
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene"); 
    }
}