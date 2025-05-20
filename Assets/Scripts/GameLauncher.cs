using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour
{
    public static GameLauncher Instance;

    private NetworkRunner _runner;
    private void Start()
    {
        StartGame();
    }

    public async void StartGame()
    {
        int lobbySceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/LobbyScene.unity");
        SceneRef lobbySceneRef = SceneRef.FromIndex(lobbySceneIndex);

        NetworkRunner networkRunner = RunnerManager.Instance.Runner;
        NetworkSceneManagerDefault sceneManager = networkRunner.GetComponent<NetworkSceneManagerDefault>();

        StartGameResult result = await networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Host,
            SessionName = "MyGameRoom",
            Scene = lobbySceneRef,
            SceneManager = sceneManager
        });
    }
}
