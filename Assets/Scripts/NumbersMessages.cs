using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersMessages : MonoBehaviour
{
    public SpinnerManager spinnerManager;
    public void OnExitButtonPressed()
    {
        spinnerManager.ShowNumbersSpinnerObject();
    }
}
