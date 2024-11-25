using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.Examples;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelselectscript : MonoBehaviour
{
    // Start is called before the first frame update\
    public GameObject slotGrid;
    public Image backgroundImage;
    public TextMeshProUGUI backgroundNameText;
    public int maxSlots;
    public int p1_Current_Slot = 0;
    public GameObject backgroundCharacter;
    public AudioSource bgmusic;
    public bool isP1Selected = false;
    public UnityEvent P1Selected_Event;
    public UnityEvent P1Unselected_Event;
    


    [Header("Audio")]
    public AudioClip switch_AC;

    public List<GameObject> slots;
    void Start()
    {
        maxSlots = slotGrid.transform.childCount;

        foreach (Transform child in slotGrid.gameObject.transform)
        {
            slots.Add(child.gameObject);
        }

        GameManager.gameManager.background_prefab = null;
        GameManager.gameManager.musicSource = null;
    }

    void Update()
    {
        if (isP1Selected)
        {
            //isP1Selected = false;
            GameManager.gameManager.background_prefab = backgroundCharacter;
            GameManager.gameManager.musicSource = bgmusic;
            MusicManager.musicManager.PlayMusic();
            Invoke("LoadStageScene", 0);
            print("LoadNewScene");
        }

        HandleInput();
        HandleSlot();
        HandleCharacterDisplay();

    }

    public void LoadStageScene(){
        isP1Selected = false;
        SceneManager.LoadScene("SampleScene");
    }

    public void HandleInput()
    {
        // Player 1 Controls
        if (Input.GetKeyDown(KeyCode.A) && !isP1Selected)
        {
            p1_Current_Slot = math.clamp(p1_Current_Slot - 1, 0, maxSlots - 1);
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
            
        }
        if (Input.GetKeyDown(KeyCode.D) && !isP1Selected)
        {
            p1_Current_Slot = math.clamp(p1_Current_Slot + 1, 0, maxSlots - 1);
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
            
        }
        if (Input.GetKeyDown(KeyCode.W) && !isP1Selected)
        {
            if (p1_Current_Slot - 8 < 0) { return; }
            p1_Current_Slot = p1_Current_Slot - 8;
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
            
        }
        if (Input.GetKeyDown(KeyCode.S) && !isP1Selected)
        {
            if (p1_Current_Slot + 8 > maxSlots - 1) { return; }
            p1_Current_Slot = p1_Current_Slot + 8;
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isP1Selected)
            {
                isP1Selected = false;
                backgroundCharacter = null;
                P1Unselected_Event.Invoke();
            }
            else
            {
                backgroundCharacter = slots[p1_Current_Slot].GetComponent<SlotCharacterData>().characterPrefab;
                isP1Selected = true;
                P1Selected_Event.Invoke();
            }
        }
    }
        public void HandleSlot()
    {
        // Change Slot Color
        foreach (GameObject slot in slots)
        {
            if (slots.IndexOf(slot) != p1_Current_Slot)
            {
                slots[slots.IndexOf(slot)].GetComponent<Image>().color = Color.black;
            }
            else if (slots.IndexOf(slot) == p1_Current_Slot)
            {
                slots[p1_Current_Slot].GetComponent<Image>().color = Color.red;
            }

        }
    }

    public void HandleCharacterDisplay()
    {
        backgroundImage.sprite = slots[p1_Current_Slot].GetComponent<SlotCharacterData>().characterIcon;


        backgroundNameText.text = slots[p1_Current_Slot].GetComponent<SlotCharacterData>().characterName;

    }

}
