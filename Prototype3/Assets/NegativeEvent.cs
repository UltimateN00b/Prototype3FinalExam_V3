using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeEvent : MonoBehaviour
{

    public string eventName;
    private bool _triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        _triggered = false;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTriggered()
    {
        _triggered = true;
    }

    public string GetName()
    {
        return eventName;
    }

    public bool IsTriggered()
    {
        return _triggered;
    }
}
