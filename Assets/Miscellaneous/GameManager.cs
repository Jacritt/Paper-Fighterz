
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public GameObject p1_character_prefab;
        public GameObject p2_character_prefab;
        public GameObject background_prefab;
        public int round = 1;

        public int p1WinsNum = 0;
        public int p2WinsNum = 0;
        public GameObject gamoverScreen;
        public GameObject gameplayScreen;
        
        //public AudioSource musicSource;


    public static GameManager gameManager;
        void Awake()
        {
            if(gameManager == null)
            {
                gameManager = this;
                DontDestroyOnLoad(this);
            }
            else if(gameManager != (this))
            {
                Destroy(gameObject);
            }
        }

        void Start() 
        {
            Application.targetFrameRate = 45;
        }

        void Update() 
        {
            
        }

        public void StartNextRound(){
            if (p1WinsNum >= 2)
            {
                print("Player 1 Wins");
                Invoke("BackToCharacterSelect", 3);
            }
            else if (p2WinsNum >= 2)
            {
                print("Player 2 Wins");
                Invoke("BackToCharacterSelect", 3);
            }
            else
            {
                SceneManager.LoadScene("SampleScene");
                round++;
            }
        }

        // public void GameOver(int winningPlayer){
        //     if (winningPlayer == 1){ // Player 1 Wins
            
        //     }
        //     else if (winningPlayer == 2){   // Player 2 Wins

        //     }
        // }


        public void BackToCharacterSelect(){
            SceneManager.LoadScene("Character Select");
            round = 1;
            p1WinsNum = 0;
            p2WinsNum = 0;
        }
      
    }