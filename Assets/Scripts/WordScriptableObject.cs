using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "ScriptableObjects/Letter", order = 1)]
public class WordScriptableObject : ScriptableObject
{
    public List<string> _words;
}
