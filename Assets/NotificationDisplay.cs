using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationPrefab;

    private List<string> notifications = new List<string>();
    private List<GameObject> notifications_objects = new List<GameObject>();

    public void SendNotification(string notification_type) {
        if (ExistNotification(notification_type)) { return; }

        var notification = Instantiate(notificationPrefab, this.transform);

        switch (notification_type) {
            case "Hungry":
                notification.GetComponent<TextMeshProUGUI>().text = "Hungry";
                notifications.Add("Hungry");
                break;
            case "Tired":
                notification.GetComponent<TextMeshProUGUI>().text = "Tired";
                notifications.Add("Tired");
                break;
            case "Happy":
                notification.GetComponent<TextMeshProUGUI>().text = "Happy";
                notifications.Add("Happy");
                break;
        }

        notifications_objects.Add(notification);
    }

    public bool ExistNotification(string notification_type) {
        return notifications.Contains(notification_type);
    }

    public void RemoveNotification(string notification_type) {
        GameObject notification_to_remove = null;
        for (int i = 0; i < notifications_objects.Count; i++) {
            var notification_label = notifications_objects[i].GetComponent<TextMeshProUGUI>().text;
            if (notification_label == notification_type) {
                notification_to_remove = notifications_objects[i];
                break;
            }
        }

        notifications.Remove(notification_type);
        notifications_objects.Remove(notification_to_remove);
        Destroy(notification_to_remove);
    }
}
