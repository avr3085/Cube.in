public interface IRes
{
    void Init();
    void AddItem(int amount);
    void RemoveItem(int hashKey, RNode node);
    void DeInit();
}

public enum ResType
{
    DCrate,
    Edible, 
    MBox
}