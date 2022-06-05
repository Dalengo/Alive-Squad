using UnityEngine;
using Mirror;

public class Spectator : NetworkBehaviour
{
    int i = 0;
    Camera cam;

    // Start is called before the first frame update
    private void OnEnable()
    {
        cam = GetCamera(ref i);
        if (cam != null)
        {
            cam.enabled = true;
        }
    }

    private void OnDisable()
    {
        cam.enabled = false;
    }

    private Camera GetCamera(ref int i)
    {
        if(i<0)
        {
            i += GameManager.AllPlayersAlive.Count;
        }
        Player player = GameManager.AllPlayersAlive[i%GameManager.AllPlayersAlive.Count];
        
        return player.camera;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CameraPrecedente();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            CameraSuivante();
        }
        
    }
    void CameraPrecedente()
    {
        i--;
        cam.enabled = false;
        cam = GetCamera(ref i);
        cam.enabled = true;
    }

    void CameraSuivante()
    {
        i++;
        cam.enabled = false;
        cam = GetCamera(ref i);
        cam.enabled = true;
    }
}
