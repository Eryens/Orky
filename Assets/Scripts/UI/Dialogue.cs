using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    //public Sprite image; todo

    [TextArea(1,10)]
    public string[] sentences;

    [TextArea(1, 10)]
    public string[] sentencesEnglish;

}
