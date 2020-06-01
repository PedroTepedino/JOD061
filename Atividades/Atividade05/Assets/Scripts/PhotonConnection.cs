using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    private string _gameVersion = "0.0.1";
    private string _nickName = "Player";

    private string _roomName = "JOD061";

    private void Start()
    {
        Debug.Log("Conectando ao servidor .....", this);
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.NickName = _nickName + Random.Range(0, 9999);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Jogador " + PhotonNetwork.LocalPlayer.NickName + " esta conectado.", this);
        
        if (PhotonNetwork.CountOfRooms == 0)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            PhotonNetwork.JoinOrCreateRoom(_roomName, options, TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.JoinRoom(_roomName);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Sala criada : " + _roomName);
        Debug.Log("Jogador " + PhotonNetwork.LocalPlayer.NickName + " entrou na sala " + _roomName + " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Jogador " + newPlayer.NickName + " entrou na sala " + _roomName + " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Jogador " + otherPlayer.NickName + " saiu da sala " + _roomName + " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Join Room.\n Code: " + returnCode + "\n" + message);
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(_roomName, options, TypedLobby.Default);
        PhotonNetwork.JoinRoom(_roomName);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconectado: Motivo " + cause.ToString());
    }
}
