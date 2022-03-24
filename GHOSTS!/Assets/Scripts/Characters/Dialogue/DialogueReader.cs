using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueReader : MonoBehaviour
{
    public Actor[] actors;      // array of all actors that appear in any dialogue with this actor (character)
    public Transform canvas;        // parent of dialoguebox; box not visible without it
    public GameObject canvasPrefab; // canvas prefab; instantiated if a canvas is not present
    public GameObject boxPrefab;    // dialogue box prefab
    private GameObject box;         // instance of dialogue box; remains in scene disabled after creation
    private TextMeshProUGUI nameBox, textBox; 
    private Image portrait;
    private string[] lines;         // contents of document
    private bool inDialogue, printingLine;
    private int i;                  // index in file; line number

    private void Awake ()
    {
        // check for a canvas to host the dialogue box
        // if missing, make a new one
        if (canvas == null)
        {
            try
            {
                canvas = GameObject.FindObjectOfType<Canvas>().transform;
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Canvas not found. instantiating new");
                var newCan = Instantiate(canvasPrefab);
                canvas = newCan.transform;
            }
        }
        else
        {
            Debug.Log("Canvas set!");
        }

        // create and hide box
        box = Instantiate(boxPrefab, canvas);
        box.SetActive(false);

        portrait = box.transform.GetChild(1).GetComponent<Image>();
        nameBox = box.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        textBox = box.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();


    }

    public void ReadDialogue(string filePath)
    {
        

        // read file into array
        lines = Resources.Load<TextAsset>(filePath).text.Split("\n"[0]);
        
        if (lines[0] == null)
        {
            Debug.Log("File read error. Exiting dialogue...");
            return;
        }

        inDialogue = true;

        // set up dialogue box

        if (lines[0][1] != ':')
        {
            Debug.Log("File read error. Exiting dialogue...");
            return;
        }
        else
        {
            Actor a = System.Array.Find(actors, Actor => Actor.id == lines[0][0]);
            nameBox.text = a.name;
            portrait.sprite = a.portrait;
        }

        textBox.text = lines[0].Substring(2);

        box.SetActive(true);
        inDialogue = true;

    }

    void ReadNextLine()
    {
        i++;
        if (lines[i] == null)
        {
            CloseDialogue();
        }

        if (lines[0][1] == ':')
        {
            Actor a = System.Array.Find(actors, Actor => Actor.id == lines[i][0]);
            nameBox.text = a.name;
            portrait.sprite = a.portrait;
        }
        textBox.text = lines[i].Substring(2);

    }

    void CloseDialogue()
    {
        box.SetActive(false);
        inDialogue = false;
        lines = null;
    }

    private void Start()
    {
        inDialogue = false;
        printingLine = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))    // on left click
        {
            if (inDialogue)
            {
                if (printingLine)
                {
                    StopCoroutine("PrintLine");
                }
                else
                {
                    ReadNextLine();
                }
            }
        }
    }

    IEnumerator PrintLine()
    {
        if (lines[i] == null)
        {
            yield break;
        }
        printingLine = true;
        foreach (char letter in lines[i].ToCharArray())
        {
            // text += letter;
            yield return null;
        }
    }
}
