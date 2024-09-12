public interface IRes
{
    void Init();
    void AddRes(int amount);
    void RemoveRes(int hashKey, RNode node);
    void DeInit();
}

public enum ResType
{
    DCrate,
    Edible, 
    MBox
}