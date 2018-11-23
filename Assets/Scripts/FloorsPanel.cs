using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FloorsPanel : MonoBehaviour
{
    public ScrollRect scrollArea;
    public RectTransform content;
    public Elevator elevator;

    private int floorsCount = 0;
    private List<Button> upButtons = new List<Button>();
    private List<Button> downButtons = new List<Button>();

    public void InitData(int floor)
    {
        floorsCount = floor;
        CreateFloorsButtons();
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
            up_down_controller.downButton.onClick.AddListener(() => { OnElevatorUpButtonClick(floor_copy); });
            up_down_controller.upButton.onClick.AddListener(() => { OnElevatorDownButtonClick(floor_copy); });

            upButtons.Add(up_down_controller.upButton);
            downButtons.Add(up_down_controller.downButton);
        }

        upButtons.Reverse();
        downButtons.Reverse();
    }

    private void OnElevatorUpButtonClick(int floor)
    {
        //в очередь добавить надо сообщение с номером этажа
        upButtons[floor - 1].interactable = false;
    }

    private void OnElevatorDownButtonClick(int floor)
    {
        //в очередь добавить надо сообщение с номером этажа
        downButtons[floor - 1].interactable = false;
    }

    void Update()
    {

    }
}
