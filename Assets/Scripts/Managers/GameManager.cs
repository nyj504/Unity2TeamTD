using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public enum GameState
{
    Lobby, InGame
}

public class GameManager : NetworkBehaviour
{
    public enum GameState { Lobby, InGame }
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [Networked] public GameState CurrentState { get; set; }

    public override void Spawned()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Runner.Despawn(Object); // 중복 생성 방지
        }

        if (Object.HasStateAuthority)
        {
            CurrentState = GameState.Lobby;
        }
    }

    public async void OnAllPlayersReady()
    {
        if (!Object.HasStateAuthority)
            return;

        Debug.Log("All players are ready. Loading game scene...");

        NetworkRunner networkRunner = RunnerManager.Instance.Runner;

        networkRunner.SessionInfo.IsOpen = false;
        networkRunner.SessionInfo.IsVisible = false;

        await LoadGameScene();
    }

    private async Task LoadGameScene()
    {
        SceneRef sceneRef = SceneRef.FromIndex(1);

        await RunnerManager.Instance.Runner.LoadScene(sceneRef, 
            new LoadSceneParameters(LoadSceneMode.Single), true);

        CurrentState = GameState.InGame;
    }
}