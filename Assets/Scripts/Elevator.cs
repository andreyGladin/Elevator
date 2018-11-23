using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using UnityEngine.UI;
using System.Linq;

public enum Direction
{
    Up = 1,
    Down = 2
}

public struct ElevatorMessage
{
    public int floor;
    public bool isStopping;
}

public struct FloorMessage
{
    public int floor;
    public Direction direction;
}

public class Elevator : MonoBehaviour
{
    public Text currentFloor;
    public ScrollRect scrollArea;
    public RectTransform content;

    private int floorsCount = 0;
    private List<Button> buttons = new List<Button>();
    private List<ElevatorMessage> elevatorMessages = new List<ElevatorMessage>();
    private List<FloorMessage> floorsMessages = new List<FloorMessage>();
    private IState currentState;

    public void InitData(int floor)
    {
        floorsCount = floor;
        currentState = new WaitForCommandState();

        CreateFloorsButtons();
    }

    private void CreateFloorsButtons()
    {
        float panel_dist_y = 80f;

        for (int floor = floorsCount; floor > 0; floor--)
        {
            Vector3 panel_pos = new Vector3(0, 50f - panel_dist_y * floor, 0f);
            GameObject button_go = Instantiate(Resources.Load("Prefabs/button_elevator")) as GameObject;
            button_go.GetComponentInChildren<Text>().text = floor.ToString();

            button_go.transform.SetParent(content.transform);
            button_go.transform.localScale = Vector3.one;
            button_go.transform.localPosition = panel_pos;

            int floor_copy = floor;
            Button button = button_go.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => { OnFloorButtonClick(floor_copy); });
            buttons.Add(button);
        }
        
        buttons.Reverse();
    }

    private void OnFloorButtonClick(int floor)
    {
        buttons[floor - 1].interactable = false;
        
        AddElevatorMessageToQueue(new ElevatorMessage { floor = floor, isStopping = false });
    }

    public void AddElevatorMessageToQueue(ElevatorMessage msg)
    {
        elevatorMessages.Add(msg);

        OnElevatorMessageAdd();
    }

    public void AddFloorsMessageToQueue(FloorMessage msg)
    {
        floorsMessages.Add(msg);

        OnFloorMessageAdd();
    }

    private void OnElevatorMessageAdd()
    {

    }

    private void OnFloorMessageAdd()
    {

    }

    void Update ()
    {
		
	}
}
