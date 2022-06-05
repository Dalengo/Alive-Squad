using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMenu : MonoBehaviour
{
    [SerializeField]
    GameObject RespawnMenuItem;

    [SerializeField]
    Transform RespawnMenuList;


    private void OnEnable()
    {
        List<Player> AllPlayers = GameManager.AllPlayers;
        foreach(Player player in AllPlayers)
        {
            GameObject itemGO = Instantiate(RespawnMenuItem, RespawnMenuList);
            RespawnButton item = itemGO.GetComponent<RespawnButton>();
            if (item != null)
            {
                item.Setup(player);
            }
        }
    }
    private void OnDisable()
    {
        foreach (RespawnButton button in RespawnMenuList.GetComponentsInChildren<RespawnButton>())
        {
            Destroy(button.gameObject);
        }
    }
}
