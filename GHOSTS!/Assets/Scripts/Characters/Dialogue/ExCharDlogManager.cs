using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharDlogManager : MonoBehaviour
{
    private DialogueReader dmgr;
    private bool flip;
    private void Start()
    {
        dmgr = GetComponent<DialogueReader>();
        flip = false;
    }
    public void PressTestButton()
    {
        if (flip)
        {
            dmgr.ReadDialogue("Dialogue/ExampleCharacter/BingusBack");
        }
        else
        {
            dmgr.ReadDialogue("Dialogue/ExampleCharacter/TestDlog");
        }
        flip = !flip;
    }
}
