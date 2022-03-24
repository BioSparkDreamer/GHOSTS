using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Actor
{
    // class used by DialogueReader
    // helps make script writing and parsing easier

    public char id;         // key var; used to identify speaker
    public string name;     // appears in dialogue box
    public Sprite portrait;
}
