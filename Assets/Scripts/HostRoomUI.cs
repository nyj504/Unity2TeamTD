using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Fusion;
using UnityEngine.SceneManagement;

public class HostRoomUI : MonoBehaviour
{
    [SerializeField] 
    private TMP_InputField _roomNameInput;
    [SerializeField]
    private TMP_InputField _nickNameInput;
    [SerializeField] 
    private Toggle _soloToggle;
    [SerializeField] 
    private Toggle _coopToggle;
    [SerializeField] 
    private Button _createButton;
    [SerializeField]
    private GameObject _roomButtonPrefab;
    [SerializeField] 
    private Transform _roomListParent;
    
    [SerializeField]
    private GameObject _waitingRoom;
    [SerializeField]
    private GameObject _joinRoom;
   

    private void Awake()
    {
        _createButton.onClick.AddListener(OnClickCreateRoom);
    }
    private void OnClickCreateRoom()
    {
        string roomName = _roomNameInput.text;
        string nickName = _nickNameInput.text;
       
        int maxPlayers = _soloToggle.isOn ? 1 : 2;

        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("방 이름이 비어 있습니다.");
            return;
        }

        Debug.Log($"[HostRoom] Creating room: {roomName} / MaxPlayers: {maxPlayers}");

        NetworkRunner runner = RunnerManager.Instance.Runner;
        RunnerManager.Instance.SetPlayerName(nickName);
     
        runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Host,
            SessionName = roomName,
            PlayerCount = maxPlayers,
            SceneManager = runner.GetComponent<NetworkSceneManagerDefault>()
        });

        _waitingRoom.SetActive(true);
    }
}
