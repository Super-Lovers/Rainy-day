public interface IInteractable
{
    bool IsObjectLocked { get; set; }
    void ToggleObject();
}