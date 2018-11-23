using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpDownButtonsController : MonoBehaviour
{
    public Button upButton;
    public Button downButton;
    public Text floorLabel;

    public void InitData(int floor)
    {
        floorLabel.text = floor.ToString();
    }
}
