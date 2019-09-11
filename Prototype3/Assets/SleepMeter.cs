using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SleepMeter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {  
        DontDestroyOnLoad(this.transform.parent.gameObject);


    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name.Contains("Narrative"))
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSleepValue()
    {
        return this.GetComponent<Slider>().value;
    }

    public void Hide()
    {
       for (int i = 0; i < this.transform.parent.gameObject.transform.childCount; i++)
        {
            this.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        for (int i = 0; i < this.transform.parent.gameObject.transform.childCount; i++)
        {
            this.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
