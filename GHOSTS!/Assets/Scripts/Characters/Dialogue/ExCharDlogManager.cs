using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharDlogManager : MonoBehaviour
{
    private DialogueReader dmgr;
    private void Start()
    {
        dmgr = GetComponent<DialogueReader>();
    }
    public void PressTestButton()
    {
        dmgr.ReadDialogue("Dialogue/ExampleCharacter/TestDlog");
    }
}
