using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            RPC_SendMyName(RunnerManager.Instance.PlayerName);
            RPC_RequestAllNames();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SendMyName(string name, RpcInfo info = default)
    {
        Debug.Log($"[Host] Received name '{name}' from player {Object.InputAuthority}");

        // 저장하고 전체에게 브로드캐스트
        RunnerManager.Instance.SaveName(Object.InputAuthority, name);
        RPC_BroadcastName(Object.InputAuthority, name);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_RequestAllNames(RpcInfo info = default)
    {
        var namePairs = new List<KeyValuePair<PlayerRef, string>>(RunnerManager.Instance.GetAllNamePairs());

        foreach (var pair in namePairs)
        {
            RPC_BroadcastName(pair.Key, pair.Value);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_BroadcastName(PlayerRef player, string name)
    {
        Debug.Log($"[Client] Registering player {player} as '{name}'");

        RunnerManager.Instance.SaveName(player, name);
        UIManager.Instance.AddPlayer();
    }
}
