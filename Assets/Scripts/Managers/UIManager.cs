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
    [SerializeField]
    private GameObject _waitingRoom;
    [SerializeField]
    private GameObject _waitingPlayerPrefab;
    [SerializeField]
    private Transform _playerListParent;

    private Dictionary<PlayerRef, GameObject> _playerEntries = new();

    private void Awake()
    {
        _instance = this;
    }
    public void AddPlayer()
    {
        RefreshWaitingRoomUI();
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
    public void JoinRoom()
    {
        _joinRoomCanvas.SetActive(true);
    }
    private void RefreshWaitingRoomUI()
    {
        Debug.Log("RefreshWaitingRoom");
        foreach (Transform child in _playerListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<PlayerRef, string> pair in RunnerManager.Instance.GetAllNamePairs())
        {
            GameObject playerObj = Instantiate(_waitingPlayerPrefab, _playerListParent);

            TextMeshProUGUI text = playerObj.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = pair.Value;
            }
        }
    }
}
