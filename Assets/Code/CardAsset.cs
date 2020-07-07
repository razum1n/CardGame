using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/CardAsset", order = 1)]
public class CardAsset : ScriptableObject
{
    [Header("Question")]
    [TextArea(2, 3)]
    public string Question;

    [Header("Options")]
    public string OptionOneText;
    public string OptionTwoText;
    public string OptionThreeText;
    public int CorrectAnswer;

    [Header("Info")]
    [TextArea(2, 3)]
    public string Reasoning;

}
