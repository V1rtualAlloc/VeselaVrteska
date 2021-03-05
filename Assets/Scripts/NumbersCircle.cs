using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NumbersCircle : MonoBehaviour
{
    private Stopwatch pressedTimer = new Stopwatch();
    private Stopwatch spinTimer = new Stopwatch();
    private float duration = 0f;
    //private bool isObjectSpinning = false;

    public GameObject spinningObject;

    public delegate void OnSpinningFinishedEvent(int value);
    public OnSpinningFinishedEvent OnSpinningFinished;

    public SpinnerManager spinnerManager;

    #region Button Events
    // start counting time
    public void OnButtonPress()
    {
        pressedTimer.Start();
    }

    // conclude duration and speed of timer and start spin 
    public void OnButtonRealease()
    {
        // Get duration of button being held
        pressedTimer.Stop();
        var milliseconds = System.Convert.ToInt32(pressedTimer.Elapsed.TotalMilliseconds);
        duration = milliseconds > 8000 ? 8f : milliseconds / 1000f;

        // start the timer for spin
        spinTimer.Start();

        //isObjectSpinning = true;
        
        StartCoroutine(SpinFinished(duration));
    }

    IEnumerator SpinFinished(float duration)
    {
        //yield return new WaitForSeconds(duration);

        //isObjectSpinning = false;
        float time = 0f;
        while (time <= duration)
        {
            spinningObject.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -45f));
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            time += Time.fixedDeltaTime;
        }
        var currentPos = spinningObject.GetComponent<RectTransform>().rotation.eulerAngles;
        var number = FindNumber((int)currentPos.z);
        UnityEngine.Debug.Log(number);

        yield return new WaitForSeconds(1f);

        // @TODO: Need to call SpinnerManager here to handle window presentation
        spinnerManager.ShowNumbersMessageObject(number);
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

    #endregion

    void Start()
    {
        //isObjectSpinning = false;
        Time.fixedDeltaTime = 0.25f;
    }

    void FixedUpdate()
    {
        //if (isObjectSpinning)
        //{
        //    spinningObject.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -45f));
        //}
    }

    void Reset()
    {
        spinningObject.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 1f);
        duration = 0f;
        //isObjectSpinning = false;
        pressedTimer.Reset();
        spinTimer.Reset();
    }
}
