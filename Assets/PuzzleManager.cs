using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    public XRSocketInteractor socket1; // Socket for the first record player
    public XRSocketInteractor socket2; // Socket for the second record player
    public GameObject fireEffect; // The fire effect GameObject (e.g., particle system)
    public Text puzzleSolvedText; // Text component to display the solved message
    public UnityEvent onPuzzleSolved; // Unity Event to trigger additional actions

    private bool isRecord1Placed = false;
    private bool isRecord2Placed = false;

    private void Start()
    {
        fireEffect.SetActive(false); // Make sure fire is off at start
        puzzleSolvedText.gameObject.SetActive(false); // Hide text at start

        // Subscribe to the select entered event to know when a record is placed in the socket
        socket1.selectEntered.AddListener(OnRecord1Placed);
        socket2.selectEntered.AddListener(OnRecord2Placed);
    }

    // Callback for when the first record is placed
    private void OnRecord1Placed(SelectEnterEventArgs args)
    {
        isRecord1Placed = true;
        CheckPuzzleCompletion();
    }

    // Callback for when the second record is placed
    private void OnRecord2Placed(SelectEnterEventArgs args)
    {
        isRecord2Placed = true;
        CheckPuzzleCompletion();
    }

    // Check if both records are placed to complete the puzzle
    private void CheckPuzzleCompletion()
    {
        if (isRecord1Placed && isRecord2Placed)
        {
            ActivateFire();
            onPuzzleSolved.Invoke(); // Invoke the Unity Event when puzzle is solved
        }
    }

    // Activate the fire and show the puzzle solved message
    private void ActivateFire()
    {
        fireEffect.SetActive(true); // Activate fire
        puzzleSolvedText.gameObject.SetActive(true); // Show "puzzle solved" text
    }
}
