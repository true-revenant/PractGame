using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal sealed class NotificationManager : MonoBehaviour
{
    [SerializeField] private Text notificationTextField;

    public void ShowNotification(string text)
    {
        notificationTextField.gameObject.SetActive(true);
        notificationTextField.text = text;
    }

    public void HideNotification()
    {
        notificationTextField.gameObject.SetActive(false);
    }
}
