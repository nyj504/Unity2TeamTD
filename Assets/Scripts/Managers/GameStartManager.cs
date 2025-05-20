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
    //        // 추가 옵션: SetActiveOnLoad = true,
    //    };
    //
    //
    //    Debug.Log("모든 클라이언트가 게임 씬으로 전환 완료!");
    //}
}