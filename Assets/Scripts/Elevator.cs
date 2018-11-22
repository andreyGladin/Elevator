using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    public int floorsCount = 0;
    public Text currentFloor;

    List<Button> buttons;
    Queue<Message> messages = new Queue<Message>();
    IState currentState;

    void Start ()
    {
        
    }

	void Update ()
    {
		
	}
}
