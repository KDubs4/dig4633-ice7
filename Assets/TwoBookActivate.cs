using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SocketManager : MonoBehaviour
{
    public UnityEvent onBothBooksInserted;

    private bool isBook1Inserted = false;
    private bool isBook2Inserted = false;

    public void InsertBook1()
    {
        isBook1Inserted = true;
        CheckBothBooksInserted();
    }

    public void InsertBook2()
    {
        isBook2Inserted = true;
        CheckBothBooksInserted();
    }

    private void CheckBothBooksInserted()
    {
        if (isBook1Inserted && isBook2Inserted)
        {
            onBothBooksInserted.Invoke();
        }
    }
}
