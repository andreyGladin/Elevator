using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using UnityEngine.UI;
using System.Linq;

public class ElevatorPanel : BasePanel
{
    public ScrollRect scrollArea;
    public RectTransform content;
    public Button stopButton;

    private int floorsCount = 0;
    private List<Button> buttons = new List<Button>();

    public void InitData(int floor)
    {
        floorsCount = floor;
        stopButton.onClick.AddListener(OnStopButtonClick);

        ElevatorManager.GetInstance().SetElevatorPanel(this);
        CreateFloorsButtons();
    }
    
    public override void ResetPanel()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public override void UnlockButton(int floor)
    {
        buttons[floor - 1].interactable = true;
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

    private void OnStopButtonClick()
    {
        ElevatorManager.GetInstance().AddTaskToQueue(new ElevatorTask(ElevatorTask.STOP_TASK_TYPE));
    }

    private void OnFloorButtonClick(int floor)
    {
        buttons[floor - 1].interactable = false;
        
        ElevatorManager.GetInstance().AddTaskToQueue(new ElevatorTask(ElevatorTask.MOVE_TASK_TYPE, floor));
    }

    private void OnDestroy()
    {
        foreach(Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}