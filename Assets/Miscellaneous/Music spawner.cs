using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Music_spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource backgroundmusic = Instantiate(GameManager.gameManager.musicSource, new Vector3(0, 0, 0), quaternion.identity);
    }


}
