using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    #region Attributes

    //public delegate void ClickAction();
    //public static event ClickAction OnClicked;
    
    private Stopwatch pressedTimer = new Stopwatch();
    private Stopwatch spinTimer = new Stopwatch();
    // duration in milliseconds
    private int duration = 0;
    private int number = 1;

    #endregion

    #region Methods

    // start counting time
    public void OnButtonPressed()
    {
        pressedTimer.Restart();
    }

    // conclude duration and speed of timer and start spin 
    public void OnButtonRealease()
    {
        // Get duration of button being held
        pressedTimer.Stop();
        duration = Convert.ToInt32(pressedTimer.Elapsed.TotalMilliseconds);
        duration = duration > 8000 ? 8000 : duration;

        // start the timer for spin
        spinTimer.Restart();
        StartCoroutine("ShowWonMessage");
    }

    IEnumerator ShowWonMessage()
    {
        yield return new WaitForSeconds(duration / 1000 + 1);

        var currentPos = GetComponent<RectTransform>().rotation.eulerAngles;
        number = FindNumber(currentPos.z);
        UnityEngine.Debug.Log(number);

        //var numbersWindow = FindObjectOfType(typeof(NumbersWindow)) as GameObject;

        //if (numbersWindow.GetComponent<NumbersWindow>().
        //{

        //}

        // @TODO: FINISH THIS
        //NumbersWindow.OnSpinCompleted += Show;
    }

    // convert angles to numbers
    int FindNumber(float angle)
    {
        for (int i = 0; i < 8; i++)
        {
            if (angle >= (i * 45f - 22.5f) && angle < ((i + 1) * 45f - 22.5f))
                return (8 - i) % 8 + 1;
        }

        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
        Time.fixedDeltaTime = 0.25f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Convert.ToInt32(spinTimer.Elapsed.TotalMilliseconds) < duration)
        {
            GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -45f));
        }

    }

    // Reset after finished game
    void Reset()
    {
        GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, 0f));
    }

    #endregion
}
