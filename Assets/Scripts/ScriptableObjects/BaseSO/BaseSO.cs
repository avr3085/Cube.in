using UnityEngine;
/*
* Only Void Event Listener is inheriting from this class.
* Other Event Listener's are being inherited from Base Event SO
* This Class can be used in other data SO as well
*/

public class BaseSO : ScriptableObject
{
    [TextArea] public string description;
}
