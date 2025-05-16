using Fusion;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [Networked]
    public NetworkString<_32> Nickname { get; private set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority) //자기 자신일때만 닉네임 
        {
            // 클라이언트 측에서 닉네임 설정
            string chosenName = PlayerPrefs.GetString("nickname", "Player" + Random.Range(0, 999));
            RPC_SetNickname(chosenName);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickname(string name)
    {
        Nickname = name;

        // 서버가 UI에 반영
        UIManager.Instance.AddPlayer(Object.InputAuthority, name);
    }
}
