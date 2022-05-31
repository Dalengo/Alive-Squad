using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerNamePlate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI usernameText;

    [SerializeField]
    private Player player;

    


    // Update is called once per frame
    void Update()
    {
        usernameText.text = player.username;
    }
}
