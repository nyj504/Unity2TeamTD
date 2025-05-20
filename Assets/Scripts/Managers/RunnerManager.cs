using Fusion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerManager : MonoBehaviour
{
    public static RunnerManager Instance { get; private set; }
    public NetworkRunner Runner { get; private set; }

    public string PlayerName { get; private set; }

    private Dictionary<PlayerRef, string> _names = new();
    public void SaveName(PlayerRef player, string name) { _names[player] = name; }
    public string GetName(PlayerRef player) { return _names[player]; }
    public IEnumerable<KeyValuePair<PlayerRef, string>> GetAllNamePairs()
    {
        return _names.ToList();
    }
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

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }
}
