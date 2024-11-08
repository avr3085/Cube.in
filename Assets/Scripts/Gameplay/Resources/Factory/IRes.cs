/// <summary>
/// Resource Interface
/// </summary>
public interface IRes
{
    void Init();
    void RemoveRes(RNode node);
    void DeInit();
}

/// <summary>
/// Type of the resource being generated
/// </summary>
public enum ResType
{
    DCrate,
    Edible, 
    MBox
}