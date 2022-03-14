using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    private void Awake()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Name = "Push",
            Description = "Long term inactivity notification",
            Id = "push",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public static void SendNotification()
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "You have been inactive for 8 hours",
            Text = "Come play Knife Hit Clone!",
            FireTime = System.DateTime.Now.AddHours(8),
        };
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        AndroidNotificationCenter.SendNotification(notification, "push");
    }
}
