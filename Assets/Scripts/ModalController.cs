﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModalController : MonoBehaviour
{
    public Button buttonGo;
    public Button buttonPlus;
    public Button buttonMinus;
    public Text floorsLabel;
    public GameObject game;
    public ElevatorPanel elevator;
    public FloorsPanel floorsPanel;

    private int floorsCount = 1;

    private void Start()
    {
        buttonPlus.onClick.AddListener(OnPlusClick);
        buttonMinus.onClick.AddListener(OnMinusClick);
        buttonGo.onClick.AddListener(OnGoClick);
    }

    private void OnPlusClick()
    {
        floorsCount++;
        floorsLabel.text = floorsCount.ToString();
    }

    private void OnMinusClick()
    {
        floorsCount = floorsCount-- >= 0? floorsCount : 0;
        floorsLabel.text = floorsCount.ToString();
    }

    private void OnGoClick()
    {
        int floors_count = int.Parse(floorsLabel.text);
        elevator.InitData(floors_count);
        floorsPanel.InitData(floors_count);

        buttonGo.onClick.RemoveListener(OnPlusClick);
        buttonGo.onClick.RemoveListener(OnMinusClick);
        buttonGo.onClick.RemoveListener(OnGoClick);

        game.SetActive(true);
        gameObject.SetActive(false);
    }
}