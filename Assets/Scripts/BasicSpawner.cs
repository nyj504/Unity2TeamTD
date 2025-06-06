using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private GameObject _roomButtonPrefab;
    [SerializeField] private Transform _roomListParent;
    [SerializeField] private GameObject _waitingRoom;
    [SerializeField] private GameObject _JoinRoomPanel;
    [SerializeField] private Transform _playerListParent;

    [SerializeField] private NetworkObject _gameManagerPrefab;
    private GameManager _gameManager;

    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    private Dictionary<PlayerRef, string> _playerNames = new();

    private NetworkRunner _runner;
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
    {
        Debug.Log("OnPlayerJoined");

        if (runner.IsServer)
        {
            // 1. 플레이어 캐릭터 스폰
            var obj = runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, player);
            _spawnedCharacters[player] = obj;

            // 2. GameManager는 최초 한 번만 스폰
            if (_gameManager == null)
            {
                _gameManager = runner.Spawn(_gameManagerPrefab, Vector3.zero, Quaternion.identity, null)
                    .GetComponent<GameManager>();
                
            }
        }
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
            data.direction += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            data.direction += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            data.direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.direction += Vector3.right;

        input.Set(data);
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        foreach (Transform child in _roomListParent)
            Destroy(child.gameObject);

        foreach (SessionInfo info in sessionList)
        {
            GameObject btn = Instantiate(_roomButtonPrefab, _roomListParent);
            btn.GetComponent<RoomJoinBtn>().Init(info.Name, _runner, _waitingRoom, _JoinRoomPanel, _playerListParent);
        }
    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Create the NetworkSceneInfo from the current scene
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public void OnClickHostRoom()
    {
        UIManager.Instance.CreateHostRoom();
    }
    public void OnClickJoinRoom()
    {
        UIManager.Instance.JoinRoom();
    }
    public void StartGameIfReady()
    {
        if (_gameManager != null)
        {
            _gameManager.OnAllPlayersReady();
        }
        else
        {
            Debug.LogWarning("GameManager가 아직 스폰되지 않았습니다.");
        }
    }
}
