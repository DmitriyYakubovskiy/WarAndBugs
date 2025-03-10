using System;
using TMPro;
using UnityEngine;

public class CoefficientOfDifficulty : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private float step = 0.1f;

    private void Start()
    {
        text.text = SaveSystem.Data.coefficientOfDifficulty.ToString();
    }

    public void Up()
    {
        var value = SaveSystem.Data.coefficientOfDifficulty;
        var stepTmp=step;
        if (Input.GetKey(KeyCode.LeftShift)) stepTmp *= 10;
        value += stepTmp;
        if (value > 10) value = 10;
        text.text = Math.Round(value,1).ToString();
        var resultValue = (float)Math.Round(value, 1);
        if(resultValue<1) resultValue = 1;
        SaveSystem.Data.coefficientOfDifficulty = resultValue;
    }

    public void Down()
    {
        var value = SaveSystem.Data.coefficientOfDifficulty;
        var stepTmp = step;
        if (Input.GetKey(KeyCode.LeftShift)) stepTmp *= 10;
        value -= stepTmp;
        if (value < 1) value = 1;
        text.text = Math.Round(value, 1).ToString();
        SaveSystem.Data.coefficientOfDifficulty = (float)Math.Round(value, 1);
    }
}
