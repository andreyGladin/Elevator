using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class Elevator : MonoBehaviour
{
    public enum Direction
    {
        Up = 1,
        Down = 2,
        Stop = 3
    }

    public Text doorsLabel;
    public Text currentFloorLabel;

    private ElevatorTask currentTask;
    private bool IsDoorsOpen = false;
    private bool IsMoving = false;
    private int currentFloor = 1;
    private const float MOVE_TIME = 0.7f;
    private int targetFloor = 1;
    private float timePassedMoving = 0f;

    public void ExecuteTask(ElevatorTask task)
    {
        currentTask = task;

        switch (task.TaskType)
        {
            case ElevatorTask.STOP_FLOOR_TASK_TYPE:
            case ElevatorTask.MOVE_TASK_TYPE:
                MoveElevatorOnFloor(task.TargetFloor);
                break;
            case ElevatorTask.STOP_TASK_TYPE:
                StopElevator();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        ElevatorManager.GetInstance().SetManagedElevator(this);
    }

    private void OpenDoors()
    {
        doorsLabel.text = "Doors Opened!";
        IsDoorsOpen = true;
    }

    private void CloseDoors()
    {
        doorsLabel.text = "Doors Closed!";
        IsDoorsOpen = false;
    }

    private void StopElevator()
    {
        OpenDoors();
    }

    private void Update()
    {
        if (IsMoving)
        {
            timePassedMoving += Time.deltaTime;
            if (timePassedMoving > MOVE_TIME)
            {
                if (currentTask.TargetDirection == Direction.Down)
                {
                    currentFloor--;
                }
                else if (currentTask.TargetDirection == Direction.Up)
                {
                    currentFloor++;
                }

                ElevatorManager.GetInstance().SetCurrentFloor(currentFloor);
                currentFloorLabel.text = currentFloor.ToString();
                timePassedMoving = 0;

                if (currentFloor == targetFloor)
                {
                    IsMoving = false;
                    OpenDoors();
                    ElevatorManager.GetInstance().SetTaskComplete(currentTask);
                }
            }
        }

        if (IsDoorsOpen)
        {
            timePassedMoving += Time.deltaTime;

            if (timePassedMoving > MOVE_TIME)
            {
                timePassedMoving = 0;
                CloseDoors();
            }
        }
    }

    private void MoveElevatorOnFloor(int floor)
    {
        if (IsDoorsOpen)
        {
            CloseDoors();
        }

        IsMoving = true;
        targetFloor = floor;
    }
}
