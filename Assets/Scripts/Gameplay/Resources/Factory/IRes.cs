public interface IRes
{
    void Init();
    void AddItem();
    void RemoveItem(int hashKey);
    void DeInit();
}

public enum ResType
{
    Res, 
    MysteryBox,
    DeathCrate
}