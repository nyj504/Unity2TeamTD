using Fusion;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] NetworkObject _playerPrefab;
    private NetworkRunner _runner;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();

    private async void Start()
    {
        await Task.Yield();

        NetworkRunner runner = RunnerManager.Instance.Runner;

        if (!runner.IsServer)
            return;

        foreach (PlayerRef player in runner.ActivePlayers)
        {
            if (!_spawnedCharacters.ContainsKey(player))
            {
                Vector3 spawnPosition = GetSpawnPoint(player);
                NetworkObject obj = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
                _spawnedCharacters[player] = obj;
            }
        }
    }
    private Vector3 GetSpawnPoint(PlayerRef player)
    {
        int index = 2;

        switch (index)
        {
            case 0: return new Vector3(-5f, 1f, -5f);
            case 1: return new Vector3(5f, 1f, -5f);
            case 2: return new Vector3(-5f, 0f, 5f);
            case 3: return new Vector3(5f, 0f, 5f);
            default: return Vector3.zero;
        }
    }
}
