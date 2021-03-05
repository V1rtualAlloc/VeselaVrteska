using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerManager : MonoBehaviour
{
    public GameObject numbersSpinner;

    public GameObject numbersMessages;

    public List<GameObject> numbersWindows;

    public GameObject spinner;

    private int indexActive = -1;

    GameManager GM;

    void Awake()
    {
        GM = GameManager.Instance;
        GM.OnStateChange += HandleOnEnter;

        Debug.Log("Awake state: " + GM.gameState);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var element in numbersWindows)
        {
            element.SetActive(false);
        }

        Debug.Log("Start state: " + GM.gameState);
    }

    public void HandleOnEnter()
    {
        GM.SetGameState(GameState.NUMBERS);
        Debug.Log("HandleOnEnter: " + GM.gameState);
    }

    public void ShowNumbersMessageObject(int number)
    {
        numbersSpinner.SetActive(false);
        numbersMessages.SetActive(true);

        indexActive = number - 1;
        numbersWindows[indexActive].SetActive(true);
        numbersWindows[indexActive].transform.parent.gameObject.SetActive(true);
    }

    public void ShowNumbersSpinnerObject()
    {
        numbersSpinner.SetActive(true);
        numbersMessages.SetActive(false);

        numbersWindows[indexActive].SetActive(false);
        numbersWindows[indexActive].transform.parent.gameObject.SetActive(false);
        indexActive = -1;

        spinner.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
    }

    /// <summary>
    /// Reverse update for all children in the hierarchy of root object
    /// </summary>
    /// <param name="root">root of the target hierarchy to traverse</param>
    /// <param name="enable">true to enable, false to disable</param>
    private void ChildrenReverseUpdate(GameObject root, bool enable)
    {
        for (int i = 0; i < root.transform.childCount; i++)
        {
            var obj = root.transform.GetChild(i).gameObject;
            ChildrenReverseUpdate(obj, enable);
            obj.SetActive(enable);
        }
    }


    //public GameObject FindFirstChildInHierarchy(string name, GameObject root = this)
    //{
    //    for (int i = 0; i < root.transform.childCount; i++)
    //    {
    //        var obj = root.transform.GetChild(i).gameObject;
    //        if (obj.name == name)
    //        {
    //            return obj;
    //        }
    //        ChildrenReverseUpdate(obj, enable);
    //        obj.SetActive(enable);
    //    }

    //    return null;
    //}
}
