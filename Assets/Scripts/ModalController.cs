using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModalController : MonoBehaviour
{
    public Button buttonGo;
    public Button buttonPlus;
    public Button buttonMinus;
    public Text floorsLabel;
    public GameObject game;
    public Elevator elevator;
    public FloorsPanel floorsPanel;

    private int floorsCount = 1;

    void Start()
    {
        buttonPlus.onClick.AddListener(OnPlusClick);
        buttonMinus.onClick.AddListener(OnMinusClick);
        buttonGo.onClick.AddListener(OnGoClick);
    }

    void OnPlusClick()
    {
        floorsCount++;
        floorsLabel.text = floorsCount.ToString();
    }

    void OnMinusClick()
    {
        floorsCount = floorsCount-- >= 0? floorsCount : 0;
        floorsLabel.text = floorsCount.ToString();
    }

    void OnGoClick()
    {
        elevator.floorsCount = int.Parse(floorsLabel.text);
        floorsPanel.floorsCount = int.Parse(floorsLabel.text);

        buttonGo.onClick.RemoveListener(OnPlusClick);
        buttonGo.onClick.RemoveListener(OnMinusClick);
        buttonGo.onClick.RemoveListener(OnGoClick);

        game.SetActive(true);
        gameObject.SetActive(false);
    }
}
