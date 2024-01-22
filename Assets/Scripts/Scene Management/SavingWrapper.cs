using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{
    private const string defaultSaveFile = "save";

    //private IEnumerator Start()
    //{
    //    yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            Save();
        }
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }
}
