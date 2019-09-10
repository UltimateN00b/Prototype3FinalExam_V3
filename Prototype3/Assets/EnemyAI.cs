﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float timeBetweenDiceRolls;
    public int percentageChanceOfAttack;

    private bool _executeEnemyAI;

    private int _diceNum;

    private bool _startTimer;
    private float _timer;

    private bool _firstRollTaken;

    // Start is called before the first frame update
    void Start()
    {
        _executeEnemyAI = false;
        _diceNum = 0;
        _firstRollTaken = false;

        _timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_executeEnemyAI)
        {
            if (_startTimer)
            {
                _timer += Time.deltaTime;
                if (_timer >= timeBetweenDiceRolls)
                {
                    _timer = 0f;
                    _startTimer = false;
                }
            }
            else
            {
                if (_firstRollTaken)
                {
                    Random.InitState((int)System.DateTime.Now.Ticks);
                    float randomChance = Random.Range(1, 101);
                    if (randomChance <= percentageChanceOfAttack)
                    {
                        GameObject.Find("ConfirmAttackButton").GetComponent<ConfirmAttackButton>().ConfirmAttack();
                        _executeEnemyAI = false;
                        _firstRollTaken = false;
                    }

                }

                GameObject currDice = GameObject.Find("DiceCanvas").transform.GetChild(_diceNum).gameObject;
                currDice.GetComponent<Dice>().ManuallyClickDice();
                _firstRollTaken = true;

                if (_diceNum < GameObject.Find("DiceCanvas").transform.childCount-1)
                {
                    _diceNum++;
                } else
                {
                    _diceNum = 0;
                }
                _startTimer = true;
            }

        }
    }

    public void ExecuteEnemyAI()
    {
        DiceManager.AutoSetTargets();
        _firstRollTaken = false;
        _executeEnemyAI = true;
        _startTimer = true;

        Debug.Log("EXECUTE ENEMY AI CALLED");
    }

    public void CeaseEnemyAI()
    {
        _executeEnemyAI = false;
        _diceNum = 0;
        _timer = 0f;
        _startTimer = false;
        _firstRollTaken = false;
    }
}
