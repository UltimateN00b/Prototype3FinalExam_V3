using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiceHolder : MonoBehaviour
{

    private static string dice1;
    private static string dice2;
    private static string dice3;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        dice1 = "Common";
        dice2 = "Common";
        dice3 = "Common";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<string> GetDiceList()
    {
        List<string> diceList = new List<string>();

        diceList.Add(dice1);
        diceList.Add(dice2);
        diceList.Add(dice3);

        return diceList;
    }

    public static void ChangeDice(int diceNum, string name)
    {
        switch (diceNum)
        {
            case 1:
                dice1 = name;
                break;
            case 2:
                dice2 = name;
                break;
            case 3:
                dice3 = name;
                break;
            default:
                dice1 = name;
                break;
        }
    }
}
