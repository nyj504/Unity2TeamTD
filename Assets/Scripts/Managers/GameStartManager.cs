using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameStartManager : MonoBehaviour
{
    public void OnClickStartGame()
    {
        if (RunnerManager.Instance.Runner.IsServer)
        {
            //StartCoroutine(LoadGameScene());
        }
    }
    //private IEnumerator LoadGameScene()
    //{
    //    int sceneIndex = SceneManager.GetSceneByName("Game").buildIndex;
    //    var sceneRef = SceneRef.FromIndex(sceneIndex);
    //
    //    var loadParams = new NetworkLoadSceneParameters
    //    {
    //        Scene = sceneRef,
    //        // �߰� �ɼ�: SetActiveOnLoad = true,
    //    };
    //
    //
    //    Debug.Log("��� Ŭ���̾�Ʈ�� ���� ������ ��ȯ �Ϸ�!");
    //}
}