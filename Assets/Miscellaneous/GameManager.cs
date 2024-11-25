
using UnityEngine;
using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public GameObject p1_character_prefab;
        public GameObject p2_character_prefab;
        public GameObject background_prefab;
        public AudioSource musicSource;


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

        public void RestartMatch(){
            SceneManager.LoadScene("SampleScene");
        }
      
    }