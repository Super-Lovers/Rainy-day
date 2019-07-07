using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public void ToggleWindow(Object window) {
        GameObject obj = (GameObject)window;
        obj.SetActive(!obj.activeSelf);
        AudioManager.Instance.AudioController.PlaySound("Press");
    }
}
