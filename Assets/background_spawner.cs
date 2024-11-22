using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class background_spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject background = Instantiate(GameManager.gameManager.background_prefab, new Vector3(0, 0, 0), quaternion.identity);
    }

    
}
