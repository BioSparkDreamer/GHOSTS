using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Actor
{
    // class used by DialogueReader
    // helps make script writing and parsing easier

    public char id;         // key var; used to identify speaker
    public string name;     // appears in dialogue box
    public Sprite portrait;
}

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
    private string line;

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
        // reset i
        i = -1;
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
            ReadNextLine();
        }

        // textBox.text = lines[0].Substring(3);

        box.SetActive(true);
        inDialogue = true;

    }

    void ReadNextLine()
    {
        i++;
        if (i == actors.Length - 1)
        {
            CloseDialogue();
            return;
        }

        if (lines[i][0] == '/')
        {
            ReadNextLine();
        }

        // second check on i for when function return from comment at EOF
        if (i == actors.Length)
        {
            CloseDialogue();
            return;
        }

        if (lines[i][1] == ':')
        {
            Actor a = System.Array.Find(actors, Actor => Actor.id == lines[i][0]);
            nameBox.text = a.name;
            portrait.sprite = a.portrait;
            line = lines[i].Substring(3);
        }
        else
        {
            line = lines[i].Substring(2);
        }

        StartCoroutine("PrintLine");

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
                    textBox.text = lines[i].Substring(3);
                    printingLine = false;
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
        textBox.text = "";
        for (int j=0; j < line.Length; j++)
        {
            // print tags all at once
            if (line[j] == '<')
            {
                // compiles text of tag into a string
                // prints string in full, not by character
                // full tag will disappear, 
                // but printing by char will reveal tag until it is complete
                string tag = "";
                while (line[j] != '>')
                {
                    tag += line[j];
                    j++;
                }
                tag += line[j];
                textBox.text += tag;
            }
            else
            { 
                textBox.text += line[j];
            }
            yield return new WaitForSeconds(0.03f);
        }
        printingLine = false;
    }
}
