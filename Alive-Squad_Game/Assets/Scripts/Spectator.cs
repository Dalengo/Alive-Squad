using UnityEngine;

public class Spectator : MonoBehaviour
{
    int i = 0;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.enabled = true;
        cam = GetCamera(i);
        cam.enabled = true;
    }

    private Camera GetCamera(int i)
    {
        Player player = GameManager.AllPlayers[i];
        Camera cam = player.GetComponent<Camera>();
        return cam;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            {
            CameraSuivante();
            }
        
    }
    
    void CameraSuivante()
    {
        i++;
        cam.enabled = false;
        cam = GetCamera(i);
        
        cam.enabled = true;
        
    }
}
