using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour
{
    public static GameLauncher Instance;

    [SerializeField] private NetworkRunner _runnerPrefab;
    private NetworkRunner _runner;
    private void Awake()
    {
        Instance = this;
    }

    public async void StartGame()
    {
        if (_runner == null)
        {
            _runner = Instantiate(_runnerPrefab); // �����տ��� ����
        }

        _runner.ProvideInput = true;

        var result = await _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared, // �Ǵ� GameMode.Host
            SessionName = "MyGameRoom",
            //Scene = INetworkSceneManager.(SceneManager.GetActiveScene().buildIndex),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (result.Ok)
        {
            Debug.Log("���� ���� ����");
        }
        else
        {
            Debug.LogError($"���� ���� ����: {result.ShutdownReason}");
        }
    }
}
