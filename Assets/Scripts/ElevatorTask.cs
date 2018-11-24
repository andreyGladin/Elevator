using UnityEngine;
using System.Collections;

public class ElevatorTask
{
    public const string STOP_TASK_TYPE = "STOP_TASK";
    public const string MOVE_TASK_TYPE = "MOVE_TASK_TYPE";
    public const string STOP_FLOOR_TASK_TYPE = "STOP_FLOOR_TASK_TYPE";

    public string TaskType { get; set; }
    public Elevator.Direction TargetDirection { get; set; }
    public Elevator.Direction DesiredDirection { get; set; }
    public int TargetFloor { get; set; }

    public ElevatorTask(string type, int floor = 0, Elevator.Direction dir = Elevator.Direction.Stop)
    {
        TaskType = type;
        TargetFloor = floor;
        TargetDirection = dir;
    }
}
