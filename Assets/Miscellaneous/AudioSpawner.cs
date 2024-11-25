using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AudioSource bgmusic = Instantiate(GameManager.gameManager.musicSource, new Vector3(0, 0, 0), quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
