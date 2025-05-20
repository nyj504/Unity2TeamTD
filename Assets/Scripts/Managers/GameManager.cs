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

    [Networked] public GameState CurrentState { get; set; }

    public override void Spawned()
    {
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