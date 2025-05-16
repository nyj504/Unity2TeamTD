using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateSession : MonoBehaviour
{
    [SerializeField] private TMP_InputField _sessionNameInput;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private GameObject _sessionPanel;


    private void Start()
    {
        _confirmButton.onClick.AddListener(OnConfirm);
        _sessionNameInput.onValueChanged.AddListener(x =>
        {
            ServerInfo.LobbyName = x;
            _confirmButton.interactable = !string.IsNullOrEmpty(x);
        });
        _sessionNameInput.text = ServerInfo.LobbyName = "Session" + Random.Range(0, 1000);
    }


    private void OnConfirm()
    {
        _sessionPanel.SetActive(true);
        //string nickname = _sessionNameInput.text;
        //if (string.IsNullOrWhiteSpace(nickname))
        //{
        //    nickname = "Player" + Random.Range(0, 999);
        //}

        //PlayerPrefs.SetString("nickname", nickname);

        // 닉네임 UI 숨기고, 게임 시작
        //gameObject.SetActive(false);

        //GameLauncher.Instance.StartGame(); // 여기에 네트워크 시작 로직 연결
    }
}
