﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{


    private static GameObject _lastDiceClicked;

    private UnityEvent m_OnDiceStopped;
    private UnityEvent m_OnAttack;
    private UnityEvent m_OnStatusEffect; //*FIND A PLACE TO INVOKE STATUS EFFECTS!

    private bool _stopped;

    private static bool _hasMovedDown;
    private Vector3 _targetDicePos;

    private bool _resetDice;

    private static int _numDiceStopped;

    private bool _startMovingDiceDown;

    // Start is called before the first frame update
    void Start()
    {

        if (m_OnDiceStopped == null)
        {
            m_OnDiceStopped = new UnityEvent();
        }

        if (m_OnAttack == null)
        {
            m_OnAttack = new UnityEvent();
        }

        if (m_OnStatusEffect == null)
        {
            m_OnStatusEffect = new UnityEvent();
        }

        _stopped = false;
        this.GetComponent<Button>().enabled = false;
        _lastDiceClicked = null;

        _hasMovedDown = false;

        _targetDicePos = GameObject.Find("Dice1").GetComponent<RectTransform>().position;
        _targetDicePos.y -= 200f;

        _resetDice = false;

        _numDiceStopped = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_numDiceStopped == Utilities.GetActiveChildren(GameObject.Find("DiceCanvas")))
        {
            if (!DiceManager.CanReset())
            {
                DiceManager.SetCanReset(true);
                _numDiceStopped = 0;
            }
        }

        if (_resetDice)
        {
            Debug.Log("TRYING TO RESET DICE");
            MoveAndResetDice();
        }
    }

    public void ChangeDice(string diceType)
    {
        DiceType myDiceType = DiceManager.SearchDiceType(diceType);

        if (myDiceType != null)
        {
            this.gameObject.SetActive(true);
            this.GetComponent<Animator>().runtimeAnimatorController = myDiceType.GetAnimationController();
            m_OnDiceStopped = myDiceType.GetOnStoppedEvent();
            m_OnAttack = myDiceType.GetOnAttackEvent();
            m_OnStatusEffect = myDiceType.GetOnStatusEvent();
        } else
        {
            this.gameObject.SetActive(false);
        }
    }



    public void OnDiceClicked()
    {
        if (DiceManager.CurrCombatStage == DiceManager.CombatStage.DiceAndTargets)
        {
            _stopped = true;

            _lastDiceClicked = this.gameObject;

            this.GetComponent<ShakeObject>().StopShaking();

            _numDiceStopped++;

            string questionMarkName = "QuestionMark" + this.name.ToCharArray()[this.name.Length - 1];
            GameObject.Find(questionMarkName).GetComponent<Image>().enabled = false;

            if (!DiceManager.GetCurrCharacter().tag.Contains("Enemy"))
            {
                AudioManager.PlaySound(Resources.Load("Dice roll") as AudioClip);
                m_OnDiceStopped.Invoke();
            }

            this.GetComponent<Button>().enabled = false;
        }
    }


    public void InvokeOnAttackEvent()
    {
        m_OnAttack.Invoke();
    }

    public static GameObject LastDiceClicked()
    {
        return _lastDiceClicked;
    }


    public bool HasStopped()
    {
        return _stopped;
    }

    public void ManuallyClickDice()
    {
        if (DiceManager.CurrCombatStage == DiceManager.CombatStage.DiceAndTargets)
        {
            _stopped = true;

            _numDiceStopped++;

            _lastDiceClicked = this.gameObject;

            this.GetComponent<ShakeObject>().StopShaking();
            string questionMarkName = "QuestionMark" + this.name.ToCharArray()[this.name.Length - 1];
            GameObject.Find(questionMarkName).GetComponent<Image>().enabled = false;

            AudioManager.PlaySound(Resources.Load("Dice roll") as AudioClip);
            m_OnDiceStopped.Invoke();

            this.GetComponent<Button>().enabled = false;
        }

    }

    private void MoveAndResetDice()
    {
            if (!_hasMovedDown)
            {
            //Move dice until it has reached its target
            if (this.GetComponent<ShakeObject>().IsShaking())
            {
                this.GetComponent<ShakeObject>().StopShaking();
            }

            MoveDown();
            }
            else
            {

            //Reset roll values
            DiceManager.ResetRollValues();

            //Reset dice position and question marks and start shaking
            ResetDice();

            //Set necessary variables back to false

            _hasMovedDown = false;
            _resetDice = false;
        }
    }

    private void MoveDown()
    {
        float speed = 1000f;
        float step = speed * Time.deltaTime; // calculate distance to move

        _targetDicePos.x = this.transform.position.x;
        _targetDicePos.z = this.transform.position.z;

        if (Vector3.Distance(this.transform.position, _targetDicePos) > 0.1f)
        {
            this.transform.position = Vector3.MoveTowards(this.GetComponent<RectTransform>().position, _targetDicePos, step);
        }
        else
        {
            _hasMovedDown = true;
            _startMovingDiceDown = false;

            TurnManager.GetCurrTurnCharacter().GetComponent<Character>().ChangeDiceType();
        }
    }

    public void ResetDice()
    {
        //Reset position
        this.GetComponent<ShakeObject>().StopShaking();
        //Reset question marks
        string questionMarkName = "QuestionMark" + this.name.ToCharArray()[this.name.Length - 1];
        GameObject.Find(questionMarkName).GetComponent<Image>().enabled = true;

        //Start shaking
        this.GetComponent<ShakeObject>().Shake();
        _stopped = false;
        this.GetComponent<Animator>().enabled = true;

        //Re-enable buttons if the current character is not an enemy
        if (!TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
        {
            DiceManager.EnableAllButtons();
        }

    }

    public void StartDiceReset()
    {
        _resetDice = true;
    }

    public static void ResetNumDiceStopped()
    {
        _numDiceStopped = 0;
    }
}


//Old Methods


//public void ChangeBubbleText()
//{
//    if (DiceManager.GetCurrCharacter().tag != "Enemy")
//    {
//        string name;
//        string type;
//        string desc;

//        string[] allText;


//        if (this.gameObject.name.Equals("Ability1"))
//        {
//            allText = DiceManager.GetCurrCharacter().ability1Info.Split('#');

//            name = allText[0];
//            type = allText[1];
//            desc = allText[2];
//        }
//        else if (this.gameObject.name.Equals("Ability2"))
//        {
//            allText = DiceManager.GetCurrCharacter().ability2Info.Split('#');

//            name = allText[0];
//            type = allText[1];
//            desc = allText[2];

//        }
//        else
//        {
//            allText = DiceManager.GetCurrCharacter().ability3Info.Split('#');

//            name = allText[0];
//            type = allText[1];
//            desc = allText[2];
//        }
//    }
//}


