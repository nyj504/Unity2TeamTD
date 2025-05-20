using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Unity.Collections.Unicode;

public class RoomJoinBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject _waitingPlayerPrefab;
    [SerializeField]
    private TextMeshProUGUI _roomNameText;

    private GameObject _waitingRoom;
    private GameObject _JoinRoomPanel;
    private Transform _playerListParent;

    private string _playerName = "���� �÷��̾�";

    private string _roomName;
    private NetworkRunner _runner;

    public void Init(string roomName, NetworkRunner runner,
        GameObject waitingRoom, GameObject joinRoomPanel, Transform playerListParent)
    {
        _roomName = roomName;
        _roomNameText.text = roomName;
        _runner = runner;

        _waitingRoom = waitingRoom;
        _JoinRoomPanel = joinRoomPanel;
        _playerListParent = playerListParent;

        GetComponent<Button>().onClick.AddListener(JoinRoom);
    }

    private void JoinRoom()
    {
        if(!_runner)
        {
            _runner = RunnerManager.Instance.Runner;
        }

        RunnerManager.Instance.SetPlayerName(_playerName);

        _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Client,
            SessionName = _roomName,
            SceneManager = _runner.GetComponent<NetworkSceneManagerDefault>()
        });

        _JoinRoomPanel.SetActive(false);
        _waitingRoom.SetActive(true);
    }
}
