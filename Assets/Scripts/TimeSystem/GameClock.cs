using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClock : MonoBehaviour
{
    [SerializeField] private Text timeText = null;
    [SerializeField] private Text dateText = null;
    [SerializeField] private Text seasonText = null;
    [SerializeField] private Text yearText = null;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        EventHandler.AdvanceGameMinuteEvent += GameTimeChanged;
    }

    private void OnDisable()
    {
        EventHandler.AdvanceGameMinuteEvent -= GameTimeChanged;
    }

    public void GameTimeChanged(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        string timeArea;
        if(gameHour < 12 && gameHour >= 0)
        {
            timeArea = " am";
        }
        else
        {
            timeArea = " pm";
            gameHour -= 12;
        }
        timeText.text = gameHour + ":" + gameMinute + timeArea;
        dateText.text = gameDayOfWeek + ". " + gameDay;
        seasonText.text = gameSeason.ToString();
        yearText.text = "年: " + gameYear;
    }
}
