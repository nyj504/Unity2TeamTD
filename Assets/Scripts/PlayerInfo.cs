using Fusion;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [Networked]
    public NetworkString<_32> Nickname { get; private set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority) //�ڱ� �ڽ��϶��� �г��� 
        {
            // Ŭ���̾�Ʈ ������ �г��� ����
            string chosenName = PlayerPrefs.GetString("nickname", "Player" + Random.Range(0, 999));
            RPC_SetNickname(chosenName);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickname(string name)
    {
        Nickname = name;

        // ������ UI�� �ݿ�
        UIManager.Instance.AddPlayer(Object.InputAuthority, name);
    }
}
