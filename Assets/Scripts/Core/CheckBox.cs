using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    private bool isCall = false;

    public System.Action onCheck;

    private void OnTriggerStay(Collider other)
    {
        if (!isCall && other.CompareTag("Player"))
        {
            isCall = true;
            onCheck?.Invoke();
        }
    }
}
