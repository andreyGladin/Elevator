using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FloorsPanel : BasePanel
{
    public ScrollRect scrollArea;
    public RectTransform content;

    private int floorsCount = 0;
    private List<Button> upButtons = new List<Button>();
    private List<Button> downButtons = new List<Button>();

    public void InitData(int floor)
    {
        floorsCount = floor;
        ElevatorManager.GetInstance().SetFloorsPanel(this);
        CreateFloorsButtons();
    }
    
    public override void ResetPanel()
    {
        foreach (Button button in downButtons)
        {
            button.interactable = true;
        }

        foreach (Button button in upButtons)
        {
            button.interactable = true;
        }
    }

    public override void UnlockButton(int floor)
    {
        upButtons[floor - 1].interactable = true;
        downButtons[floor - 1].interactable = true;
    }

    private void CreateFloorsButtons()
    {
        float panel_dist_y = 80f;

        for (int floor = floorsCount; floor > 0; floor--)
        {
            Vector3 panel_pos = new Vector3(0, 50f - panel_dist_y * floor, 0f);
            GameObject panel_go = Instantiate(Resources.Load("Prefabs/floor_panel")) as GameObject;

            panel_go.transform.SetParent(content.transform);
            panel_go.transform.localScale = Vector3.one;
            panel_go.transform.localPosition = panel_pos;

            UpDownButtonsController up_down_controller = panel_go.GetComponent<UpDownButtonsController>();
            up_down_controller.InitData(floor);

            int floor_copy = floor;
            up_down_controller.downButton.onClick.AddListener(() => { OnElevatorDownButtonClick(floor_copy); });
            up_down_controller.upButton.onClick.AddListener(() => { OnElevatorUpButtonClick(floor_copy); });

            upButtons.Add(up_down_controller.upButton);
            downButtons.Add(up_down_controller.downButton);
        }

        upButtons.Reverse();
        downButtons.Reverse();
    }

    private void OnElevatorUpButtonClick(int floor)
    {
        upButtons[floor - 1].interactable = false;
        ElevatorTask task = new ElevatorTask(ElevatorTask.STOP_FLOOR_TASK_TYPE, floor);
        task.DesiredDirection = Elevator.Direction.Up;
        ElevatorManager.GetInstance().AddTaskToQueue(task);
    }

    private void OnElevatorDownButtonClick(int floor)
    {
        downButtons[floor - 1].interactable = false;
        ElevatorTask task = new ElevatorTask(ElevatorTask.STOP_FLOOR_TASK_TYPE, floor);
        task.DesiredDirection = Elevator.Direction.Down;
        ElevatorManager.GetInstance().AddTaskToQueue(task);
    }

    private void OnDestroy()
    {
        foreach (Button button in upButtons)
        {
            button.onClick.RemoveAllListeners();
        }

        foreach (Button button in downButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
