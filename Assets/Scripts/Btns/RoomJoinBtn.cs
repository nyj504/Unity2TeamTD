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

    private string _playerName = "낯선 플레이어";

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

        _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Client,
            SessionName = _roomName,
            SceneManager = _runner.GetComponent<NetworkSceneManagerDefault>()
        });
  
        _JoinRoomPanel.SetActive(false);
        _waitingRoom.SetActive(true);
      
        _runner.name = _playerName;

        IEnumerable<PlayerRef> players = _runner.ActivePlayers;

        foreach (PlayerRef player in _runner.ActivePlayers)
        {
            GameObject playerObj = Instantiate(_waitingPlayerPrefab, _playerListParent);
            TextMeshProUGUI playerText = playerObj.GetComponentInChildren<TextMeshProUGUI>();
            playerText.text = _playerName;
        }
    }
}
