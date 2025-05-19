using Fusion;
using TMPro;
using UnityEngine;

public class JoinRoomUI : MonoBehaviour
{
    private NetworkRunner _runner;

    private void Awake()
    {
        _runner = RunnerManager.Instance.Runner;
    }

    public void RefreshSessionList()
    {
        _runner.JoinSessionLobby(SessionLobby.ClientServer);
    }
}
