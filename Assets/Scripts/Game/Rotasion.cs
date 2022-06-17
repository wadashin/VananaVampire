using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotasion : MonoBehaviour
{
    public static float speed = 0.2f;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.Rotate(new Vector3(0, 0, speed));
    }
}
