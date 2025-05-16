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

        // �г��� UI �����, ���� ����
        //gameObject.SetActive(false);

        //GameLauncher.Instance.StartGame(); // ���⿡ ��Ʈ��ũ ���� ���� ����
    }
}
