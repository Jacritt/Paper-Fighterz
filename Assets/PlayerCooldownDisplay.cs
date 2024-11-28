using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldownDisplay : MonoBehaviour
{
    public GameObject up_pb;
    public GameObject down_pb;
    public GameObject normal_pb;
    public GameObject dash_pb;
    public bool isPlayer1;

    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer1){
            player = GameObject.FindGameObjectWithTag("Player1");
        }
        else{
            player = GameObject.FindGameObjectWithTag("Player2");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
