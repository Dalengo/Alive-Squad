using UnityEngine;
using Mirror;

public class Spectator : NetworkBehaviour
{
    int i = 0;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetCamera(i);
        if (cam != null)
        {
            cam.enabled = true;
        }
    }

    private Camera GetCamera(int i)
    {
        Player player = GameManager.AllPlayers[i];
        
        return player.camera;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            CameraSuivante();
        }
        
    }
    
    void CameraSuivante()
    {
        i++;
        cam.enabled = false;
        cam = GetCamera(i);
       
    }
}
