using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
        _roomNameText.text = roomName;
        _runner = runner;

        _waitingRoom = waitingRoom;
        _JoinRoomPanel = joinRoomPanel;
        _playerListParent = playerListParent;

        GetComponent<Button>().onClick.AddListener(JoinRoom);
    }

    private void JoinRoom()
    {
        _JoinRoomPanel.SetActive(false);
        _waitingRoom.SetActive(true);
        GameObject playerObj = Instantiate(_waitingPlayerPrefab, _playerListParent);

        //IReadOnlyList<PlayerRef> players = _runner.ActivePlayers;

        TextMeshProUGUI playerText = playerObj.GetComponentInChildren<TextMeshProUGUI>();
        if (playerText != null)
        {
            playerText.text = _playerName;
        }
        else
        {
            Debug.LogWarning("WaitingPlayer 프리팹에 TextMeshProUGUI 컴포넌트가 없습니다.");
        }
    }
}
