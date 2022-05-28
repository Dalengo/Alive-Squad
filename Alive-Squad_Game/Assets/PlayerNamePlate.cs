using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNamePlate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI usernameText;

    [SerializeField]
    private Player player;

    void Update()
    {
        usernameText.text = player.username;
    }
}
