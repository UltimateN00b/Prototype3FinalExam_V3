using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCanvasDiceSelection : MonoBehaviour
{

    public GameObject buttonPrefab;
    public Sprite unlockImage;
    public float buttonGap;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupDiceCanvas(string characterName)
    {

        //Finding the relationship and character dice profile we are working with
        Relationship currRelationship = FindRelationship(characterName);
        CharacterDiceProfile currCDP = FindCharacterDiceProfile(characterName);

        //Getting starting position of button
        GameObject baseButton = this.transform.GetChild(0).gameObject;
        Vector3 buttonPos = baseButton.GetComponent<RectTransform>().position;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (!this.transform.GetChild(i).name.Contains("DontClear"))
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }

        //Get Level
        int level = currRelationship.GetCurrLevel();

        //For each unlock level, instantiate a new dice button
        for (int i = 0; i < currCDP.GetUnlockLevels().Count; i++)
        {
            
            if (level >= currCDP.GetUnlockLevels()[i])
            {
                DiceSkin newDiceSkin = FindDiceSkin(currCDP.GetDiceTypes()[i]);
                MakeButton(buttonPos, newDiceSkin);
            }
            else
            {
                MakeLockedButton(buttonPos, currCDP.GetUnlockLevels()[i]);
            }

            //Increase the gap between buttons
            buttonPos.y -= buttonGap;
        }

        //Change the character image

        GameObject dontClear = Utilities.SearchChild("DontClear", this.gameObject);
        Utilities.SearchChild("CharacterImage", dontClear).GetComponent<Image>().sprite = currCDP.GetCharacterImage();
    }

    public void MakeButton(Vector3 position, DiceSkin dS)
    {  
        GameObject newButton = Instantiate(buttonPrefab, this.GetComponent<RectTransform>(), false);
        newButton.GetComponent<RectTransform>().position = position;

        newButton.GetComponent<DiceButtonDiceSelection>().ChangeButton(dS.GetDiceImage(), dS.GetDiceName(), unlockImage, dS.GetDiceButtonColour());
    }

    public void MakeLockedButton(Vector3 position, int lockLevel)
    {
        GameObject newButton = Instantiate(buttonPrefab, this.GetComponent<RectTransform>(), false);
        newButton.GetComponent<RectTransform>().position = position;

        newButton.GetComponent<DiceButtonDiceSelection>().ChangeDiceName("Unlock At Level "+lockLevel);
    }

    private Relationship FindRelationship (string name)
    {
        Relationship returnRelationship = null;

        GameObject relationshipHolder = GameObject.Find("RelationshipHolder");

        foreach (Relationship r in relationshipHolder.GetComponents<Relationship>())
        {
            if (r.GetCharacterName().ToUpper().Equals(name.ToUpper()))
            {
                returnRelationship = r;
            }
        }

        return returnRelationship;
    }

    private CharacterDiceProfile FindCharacterDiceProfile(string name)
    {
        CharacterDiceProfile returnCDP = null;

        foreach (CharacterDiceProfile cD in GameObject.Find("CharacterDiceProfileHolder").GetComponents<CharacterDiceProfile>())
        {
            if (cD.GetCharacterName().ToUpper().Equals(name.ToUpper()))
            {
                returnCDP = cD;
            }
        }

        return returnCDP;
    }

    private DiceSkin FindDiceSkin(string name)
    {
        DiceSkin newDiceSkin = null;

        foreach (DiceSkin dS in GameObject.Find("DiceSkinHolder").GetComponents<DiceSkin>())
        {
            if (dS.GetDiceName().ToUpper().Contains(name.ToUpper()))
            {
                newDiceSkin = dS;
            }
        }

        return newDiceSkin;
    }

    public void Hide()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        GameObject playerDiceCanvas = GameObject.Find("PlayerDiceCanvas");

        for (int i = 0; i < playerDiceCanvas.transform.childCount; i++)
        {
            playerDiceCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    public void Show()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }

        GameObject playerDiceCanvas = GameObject.Find("PlayerDiceCanvas");

        for (int i = 0; i < playerDiceCanvas.transform.childCount; i++)
        {
            playerDiceCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
