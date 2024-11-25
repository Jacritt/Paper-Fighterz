using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversallevelManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    //public Image background;
    // Start is called before the first frame update
    private void Start()
    {
        int level = Dialogscript.selectedLevel;
        Debug.Log(level);
        //background.sprite = backgrounds[level+1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
