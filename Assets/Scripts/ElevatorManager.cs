using System.Collections.Generic;

public class ElevatorManager
{
    private Elevator elevator;
    private BasePanel elevatorPanel;
    private BasePanel floorsPanel;
    private List<ElevatorTask> elevatorTasks = new List<ElevatorTask>();
    private ElevatorTask currentTask;
    private int currentFloor = 1;
    private static ElevatorManager g_instance;

    public static ElevatorManager GetInstance()
    {
        if (g_instance == null)
        {
            g_instance = new ElevatorManager();
        }

        return g_instance;
    }

    public void SetManagedElevator(Elevator elevator)
    {
        this.elevator = elevator;
    }

    public void SetFloorsPanel(BasePanel panel_floors)
    {
        floorsPanel = panel_floors;
    }

    public void SetElevatorPanel(BasePanel panel_elevator)
    {
        elevatorPanel = panel_elevator;
    }

    public void SetCurrentFloor(int floor)
    {
        currentFloor = floor;
    }

    public void SetTaskComplete(ElevatorTask task)
    {
        elevatorPanel.UnlockButton(task.TargetFloor);
        floorsPanel.UnlockButton(task.TargetFloor);

        elevatorTasks.Remove(task);
        currentTask = null;
        ChooseCurrentTask();
        currentTask = SetDirectionForTask(currentTask);
        elevator.ExecuteTask(currentTask == null ? new ElevatorTask(ElevatorTask.STOP_TASK_TYPE) : currentTask);
    }

    public void AddTaskToQueue(ElevatorTask task)
    {
        if (task.TaskType == ElevatorTask.STOP_TASK_TYPE)
        {
            floorsPanel.ResetPanel();
            elevatorPanel.ResetPanel();

            elevator.ExecuteTask(task);
            elevatorTasks.Clear();

            return;
        }

        task = SetDirectionForTask(task);

        elevatorTasks.Add(task);
        
        ChooseCurrentTask();
        elevator.ExecuteTask(currentTask);
    }

    private ElevatorTask SetDirectionForTask(ElevatorTask task)
    {
        if (task == null)
        {
            return null;
        }

        Elevator.Direction task_dir;
        if (task.TargetFloor > currentFloor)
        {
            task_dir = Elevator.Direction.Up;
        }
        else if (task.TargetFloor < currentFloor)
        {
            task_dir = Elevator.Direction.Down;
        }
        else
        {
            task_dir = Elevator.Direction.Stop;
            task.TaskType = ElevatorTask.MOVE_TASK_TYPE;
        }
        task.TargetDirection = task_dir;
        if (task.TaskType == ElevatorTask.MOVE_TASK_TYPE)
        {
            task.DesiredDirection = task.TargetDirection;
        }
        return task;
    }

    private void ChooseCurrentTask()
    {
        if (elevatorTasks.Count > 0)
        {
            currentTask = elevatorTasks[0];
        }

        if (currentTask == null)
        {
            return;
        }

        CheckForTaskOnTheWay();
    }

    private void CheckForTaskOnTheWay()
    {
        if (currentTask.TargetDirection == Elevator.Direction.Up)
        {
            for (int i = 0; i < elevatorTasks.Count; i++)
            {
                if (elevatorTasks[i].TargetFloor <= currentTask.TargetFloor
                    && elevatorTasks[i].TargetFloor >= currentFloor
                    && elevatorTasks[i].TargetDirection == currentTask.TargetDirection
                    && elevatorTasks[i].DesiredDirection == currentTask.TargetDirection)
                {
                    SetTaskFirst(elevatorTasks[i]);
                }
                else if (elevatorTasks[i].TargetFloor == currentFloor 
                         && elevatorTasks[i].TargetDirection == Elevator.Direction.Stop)
                {
                    SetTaskFirst(elevatorTasks[i]);
                }
            }
        }
        else if (currentTask.TargetDirection == Elevator.Direction.Down)
        {
            for (int i = 0; i < elevatorTasks.Count; i++)
            {
                if (elevatorTasks[i].TargetFloor >= currentTask.TargetFloor
                    && elevatorTasks[i].TargetFloor <= currentFloor
                    && elevatorTasks[i].TargetDirection == currentTask.TargetDirection
                    && elevatorTasks[i].DesiredDirection == currentTask.TargetDirection)
                {
                    SetTaskFirst(elevatorTasks[i]);
                }
                else if (elevatorTasks[i].TargetFloor == currentFloor
                         && elevatorTasks[i].TargetDirection == Elevator.Direction.Stop)
                {
                    SetTaskFirst(elevatorTasks[i]);
                }
            }
        }
    }

    private void SetTaskFirst(ElevatorTask task)
    {
        elevatorTasks.Remove(task);
        elevatorTasks.Insert(0, task);
        currentTask = task;
    }
}