using UnityEngine;

/// <summary>
/// Scene Loading Request Channel
/// </summary>
[CreateAssetMenu(fileName ="SceneRequestChannel", menuName = "EventSO/SceneRequestChannel")]
public class SceneRequestChannel : BaseEventSO<SceneSO, bool>{}