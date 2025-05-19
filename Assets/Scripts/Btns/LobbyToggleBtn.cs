using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyToggleBtn : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;
    [SerializeField]
    private TextMeshProUGUI _label;
    [SerializeField]
    private Color _selectedTextColor;
    [SerializeField]
    private Color _normalTextColor;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        if ((isOn))
        {
            Debug.Log("TrueToggleChanged");
        }
        else
        {
            Debug.Log("FalseOnToggleChanged");
        }
        _label.faceColor = isOn ? _selectedTextColor : _normalTextColor; 
    }

}
