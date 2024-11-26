using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slotGrid;
    public Image p1_CharacterImage;
    public TextMeshProUGUI p1_CharacterNameText;
    public Image p2_CharacterImage;
    public TextMeshProUGUI p2_CharacterNameText;
    public int maxSlots;
    public int p1_Current_Slot = 0;
    public int p2_Current_Slot = 1;
    public GameObject p1_selectedCharacter;
    public GameObject p2_selectedCharacter;
    public bool isP1Selected = false;
    public bool isP2Selected = false;
    public UnityEvent P1Selected_Event;
    public UnityEvent P1Unselected_Event;
    public UnityEvent P2Selected_Event;
    public UnityEvent P2Unselected_Event;

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

        GameManager.gameManager.p1_character_prefab = null;
        GameManager.gameManager.p2_character_prefab = null;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleSlot();
        HandleCharacterDisplay();

        if(isP1Selected && isP2Selected){
            isP1Selected = false;
            isP2Selected = false;
            GameManager.gameManager.p1_character_prefab = p1_selectedCharacter;
            GameManager.gameManager.p2_character_prefab = p2_selectedCharacter;
            SceneManager.LoadScene("LevelSelect");
            print("LoadNewScene");
        }
    }

    [Obsolete]
    public void HandleInput(){
        // Player 1 Controls
        if(Input.GetKeyDown(KeyCode.A) && !isP1Selected){
            p1_Current_Slot = math.clamp(p1_Current_Slot-1, 0, maxSlots-1);
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.D) && !isP1Selected){
            p1_Current_Slot = math.clamp(p1_Current_Slot+1, 0, maxSlots-1);
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.W) && !isP1Selected){
            if (p1_Current_Slot-5 < 0){return;}
            p1_Current_Slot = p1_Current_Slot-5;
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.S) && !isP1Selected){
            if (p1_Current_Slot+5 > maxSlots-1){return;}
            p1_Current_Slot = p1_Current_Slot+5;
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            if (isP1Selected){
                isP1Selected= false;
                p1_selectedCharacter = null;
                P1Unselected_Event.Invoke();
            }
            else{
                p1_selectedCharacter = slots[p1_Current_Slot].GetComponent<SlotCharacterData>().characterPrefab;
                if (p1_selectedCharacter == null){

                    //GetRandomCharacter
                    int randNum = UnityEngine.Random.Range(0,slots.Count);
                    p1_selectedCharacter = slots[randNum].GetComponent<SlotCharacterData>().characterPrefab;
                }
                isP1Selected = true;
                P1Selected_Event.Invoke();
            }
        }

        // Player 2 Controls
        if(Input.GetKeyDown(KeyCode.LeftArrow) && !isP2Selected){
            p2_Current_Slot = math.clamp(p2_Current_Slot-1, 0, maxSlots-1);
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) && !isP2Selected){
            p2_Current_Slot = math.clamp(p2_Current_Slot+1, 0, maxSlots-1);;
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && !isP2Selected){
            if (p2_Current_Slot-5 < 0){return;}
            p2_Current_Slot = p2_Current_Slot-5;
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && !isP2Selected){
            if (p2_Current_Slot+5 > maxSlots-1){return;}
            p2_Current_Slot = p2_Current_Slot+5;
            SoundFXManager.soundFXManager.PlaySoundEffect(switch_AC);
        }
        if(Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.RightShift)){
           if (isP2Selected){
                isP2Selected = false;
                p2_selectedCharacter = null;
                P2Unselected_Event.Invoke();
            }
            else{
                p2_selectedCharacter = slots[p2_Current_Slot].GetComponent<SlotCharacterData>().characterPrefab;
                isP2Selected = true;
                P2Selected_Event.Invoke();
            }
        }
    }

    public void HandleSlot(){
        // Change Slot Color
        foreach (GameObject slot in slots)
        {
            if (slots.IndexOf(slot) != p1_Current_Slot && slots.IndexOf(slot) != p2_Current_Slot){
                slots[slots.IndexOf(slot)].GetComponent<Image>().color = Color.black;
            }
            else if(p2_Current_Slot == p1_Current_Slot){
                slots[p1_Current_Slot].GetComponent<Image>().color = Color.magenta;
                slots[p2_Current_Slot].GetComponent<Image>().color = Color.magenta;
            }
            else if(slots.IndexOf(slot) == p1_Current_Slot){
                slots[p1_Current_Slot].GetComponent<Image>().color = new Color(0, 216, 230, 255); //Color.blue;
            }
            else if(slots.IndexOf(slot) == p2_Current_Slot){
                slots[p2_Current_Slot].GetComponent<Image>().color =  Color.red;
            }
        
        }
    }

    public void HandleCharacterDisplay(){
        p1_CharacterImage.sprite = slots[p1_Current_Slot].GetComponent<SlotCharacterData>().characterIcon;
        p2_CharacterImage.sprite = slots[p2_Current_Slot].GetComponent<SlotCharacterData>().characterIcon;

        p1_CharacterNameText.text = slots[p1_Current_Slot].GetComponent<SlotCharacterData>().characterName;
        p2_CharacterNameText.text = slots[p2_Current_Slot].GetComponent<SlotCharacterData>().characterName;

        // Random Select Display
        if (p2_CharacterNameText.text == "Random" && p2_CharacterImage.transform.localScale != new Vector3(1,1,1)){
             p2_CharacterImage.transform.localScale = new Vector3(1,1,1);
        }
        else if (p2_CharacterNameText.text != "Random" && p2_CharacterImage.transform.localScale == new Vector3(1,1,1)){
            p2_CharacterImage.transform.localScale = new Vector3(-1,1,1);
        }

    }

}
