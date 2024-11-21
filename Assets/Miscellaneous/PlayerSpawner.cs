using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public bool isP1;
    public GameObject characterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (isP1)
        {
            characterPrefab = GameManager.gameManager.p1_character_prefab;
            GameObject player = Instantiate(characterPrefab, new Vector3(transform.position.x, transform.position.y, 0), quaternion.identity);
            player.GetComponent<BaseCharacter>().playerType = "Player1";
            player.GetComponent<BaseCharacter>().otherPlayerTransform = GameManager.gameManager.p2_character_prefab.transform;
            player.tag = "Player1";
        }
        else{
            characterPrefab = GameManager.gameManager.p2_character_prefab;
            GameObject player = Instantiate(characterPrefab, new Vector3(transform.position.x, transform.position.y, 0), quaternion.identity);
            player.GetComponent<BaseCharacter>().playerType = "Player2";
            player.GetComponent<BaseCharacter>().otherPlayerTransform = GameManager.gameManager.p1_character_prefab.transform;
            player.tag = "Player2";
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
