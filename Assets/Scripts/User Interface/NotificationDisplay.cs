using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject notification_prefab = default;
    [SerializeField]
    private GameObject list_layout = default;

    private List<string> notifications = new List<string>();
    private List<GameObject> notifications_objects = new List<GameObject>();

    public void SendNotification(Mood notification_type) {
        if (ExistNotification(notification_type)) { return; }

        var notification = Instantiate(notification_prefab, list_layout.transform);
        var notification_type_string = notification_type.ToString();

        switch (notification_type) {
            case Mood.Hungry:
                notification.GetComponent<Text>().text = "(￣q￣)";
                break;
            case Mood.Tired:
                notification.GetComponent<Text>().text = "(￣ヘ￣)";
                break;
            case Mood.Hungry_and_tired:
                notification.GetComponent<Text>().text = "(｡╯︵╰｡)";
                break;
            case Mood.Happy:
                notification.GetComponent<Text>().text = "(￣ω￣)";
                break;
            case Mood.Purry:
                notification.GetComponent<Text>().text = "(≧◡≦)♡";
                break;
            case Mood.Playing:
                notification.GetComponent<Text>().text = "ヽ(♡‿♡)ノ";
                break;
            case Mood.Sleeping:
                notification.GetComponent<Text>().text = "(￣ρ￣)..";
                break;
        }

        notifications.Add(notification_type_string);
        notifications_objects.Add(notification);

        Toggle();
        StartCoroutine(RemoveNotification(Mood.Purry, 3));
        //StartCoroutine(RemoveNotification(Mood.Sleeping, 3));
    }

    public bool ExistNotification(Mood notification_type) {
        return notifications.Contains(notification_type.ToString());
    }

    public IEnumerator RemoveNotification(Mood notification_type, int delay) {
        yield return new WaitForSeconds(delay);

        GameObject notification_to_remove = null;
        for (var i = 0; i < notifications_objects.Count; i++) {
            var notification_label = notifications_objects[i].GetComponent<Text>().text;
            var current_notification_type = Mood.Happy;

            if (notification_label == "(￣q￣)") { current_notification_type = Mood.Hungry; }
            else if (notification_label == "(￣ヘ￣)") { current_notification_type = Mood.Tired; }
            else if (notification_label == "(｡╯︵╰｡)") { current_notification_type = Mood.Hungry_and_tired; }
            else if (notification_label == "(￣ω￣)") { current_notification_type = Mood.Happy; }
            else if (notification_label == "(≧◡≦)♡") { current_notification_type = Mood.Purry; }
            else if (notification_label == "ヽ(♡‿♡)ノ") { current_notification_type = Mood.Playing; }
            else if (notification_label == "(￣ρ￣)..") { current_notification_type = Mood.Sleeping; }

            if (current_notification_type.ToString() == notification_type.ToString()) {
                notification_to_remove = notifications_objects[i];
                break;
            }
        }

        notifications.Remove(notification_type.ToString());
        notifications_objects.Remove(notification_to_remove);
        Destroy(notification_to_remove);

        Toggle();
    }

    private void Toggle() {
        if (notifications.Count == 0) { this.gameObject.SetActive(false); }
        else { this.gameObject.SetActive(true); }
    }
}
