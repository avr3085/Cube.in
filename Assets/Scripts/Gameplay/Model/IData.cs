/// <summary>
/// Simple entity interface for taking damage and adding score
/// </summary>
public interface IData
{
    public void TakeDamage(int amount);
    public void AddScore(int amount);
}