using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IManager
{
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Data Manager initialized..";
        Debug.Log(_state);
    }
}
