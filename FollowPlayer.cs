using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset = new Vector3(-2.369f, 0.90f, -8.21f);


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }



}
