using Fusion;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public static RunnerManager Instance { get; private set; }
    public NetworkRunner Runner { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Runner = gameObject.AddComponent<NetworkRunner>();
            Runner.ProvideInput = true;

            gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
