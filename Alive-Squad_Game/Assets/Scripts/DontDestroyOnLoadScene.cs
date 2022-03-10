using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;
    private void Awake()
    {
        foreach (GameObject element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }
}
