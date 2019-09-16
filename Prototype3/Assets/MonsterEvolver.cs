using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvolver : MonoBehaviour
{
    public List<Sprite> evolutions;

    private int _evolutionIndex;

    // Start is called before the first frame update
    void Start()
    {
        _evolutionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Evolve()
    {
        this.GetComponent<SpriteRenderer>().sprite = evolutions[_evolutionIndex];
        AudioManager.PlaySound(Resources.Load("Heartbeat") as AudioClip);
        _evolutionIndex++;
    }
}
