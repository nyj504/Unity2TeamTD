using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    { get { return _instance; } }

    public Transform playerListParent;
    public GameObject playerEntryPrefab;
    [SerializeField]
    private GameObject _hostRoomCanvas;
    [SerializeField]
    private GameObject _joinRoomCanvas;

    private Dictionary<PlayerRef, GameObject> _playerEntries = new();

    private void Awake()
    {
        _instance = this;
    }
    public void AddPlayer(PlayerRef player, string nickname)
    {
        if (_playerEntries.ContainsKey(player)) return;

        Debug.Log("플레이어 접속");

        GameObject entry = Instantiate(playerEntryPrefab, playerListParent);
        entry.GetComponentInChildren<TextMeshProUGUI>().text = nickname;

        _playerEntries[player] = entry;
    }

    public void RemovePlayer(PlayerRef player)
    {
        if (_playerEntries.TryGetValue(player, out GameObject entry))
        {
            Destroy(entry);
            _playerEntries.Remove(player);
        }
    }

    public void CreateHostRoom()
    {
        _hostRoomCanvas.SetActive(true);
    }
    public void ShowGameRoom()
    {
        _joinRoomCanvas.SetActive(true);
    }

    public void OnNicknameInputChanged(string input)
    {
        PlayerPrefs.SetString("nickname", input);
    }
}
