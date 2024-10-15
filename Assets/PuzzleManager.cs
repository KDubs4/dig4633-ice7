using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // For TextMeshPro

public class PuzzleManager : MonoBehaviour
{
    public XRSocketInteractor socket1; // Socket for the first music disk
    public XRSocketInteractor socket2; // Socket for the second music disk
    public GameObject fireEffect; // Fire particles on the furnace
    public Light spotlight; // Spotlight to turn on
    public TextMeshProUGUI puzzleSolvedText; // TMP for solved message

    private bool isRecord1Placed = false;
    private bool isRecord2Placed = false;

    private void Start()
    {
        // Ensure all visuals are disabled at the start
        fireEffect.SetActive(false);
        spotlight.enabled = false;
        puzzleSolvedText.gameObject.SetActive(false);

        // Subscribe to the socket events
        socket1.selectEntered.AddListener(OnRecord1Placed);
        socket2.selectEntered.AddListener(OnRecord2Placed);

        // Optionally, handle when objects are removed from the sockets
        socket1.selectExited.AddListener(OnRecord1Removed);
        socket2.selectExited.AddListener(OnRecord2Removed);
    }

    private void OnRecord1Placed(SelectEnterEventArgs args)
    {
        isRecord1Placed = true;
        Debug.Log("Record 1 placed");
        CheckPuzzleCompletion();
    }

    private void OnRecord2Placed(SelectEnterEventArgs args)
    {
        isRecord2Placed = true;
        Debug.Log("Record 2 placed");
        CheckPuzzleCompletion();
    }

    private void OnRecord1Removed(SelectExitEventArgs args)
    {
        isRecord1Placed = false;
        Debug.Log("Record 1 removed");
        CheckPuzzleCompletion();
    }

    private void OnRecord2Removed(SelectExitEventArgs args)
    {
        isRecord2Placed = false;
        Debug.Log("Record 2 removed");
        CheckPuzzleCompletion();
    }

    private void CheckPuzzleCompletion()
    {
        if (isRecord1Placed && isRecord2Placed)
        {
            ActivateFireAndLight();
        }
        else
        {
            DeactivateFireAndLight(); // Turn off the effects if one record is removed
        }
    }

    private void ActivateFireAndLight()
    {
        fireEffect.SetActive(true);
        spotlight.enabled = true;
        puzzleSolvedText.gameObject.SetActive(true);
        puzzleSolvedText.text = "You've lit the fire!"; // Show message
        Debug.Log("Puzzle Solved: Fire activated, spotlight turned on");
    }

    private void DeactivateFireAndLight()
    {
        fireEffect.SetActive(false);
        spotlight.enabled = false;
        puzzleSolvedText.gameObject.SetActive(false);
        Debug.Log("Puzzle Incomplete: Fire deactivated, spotlight turned off");
    }
}
