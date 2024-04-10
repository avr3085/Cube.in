using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="VoidEventListener", menuName = "EventSO/BaseEvent/VoidEvent")]
public class VoidEventListener : BaseSO
{
    public UnityAction onEventRaised;
    public void Raise(){
        onEventRaised?.Invoke();
    }
}
