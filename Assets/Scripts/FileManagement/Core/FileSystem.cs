/// <summary>
/// Implements File Handler
/// </summary>
public class FileSystem : FileHandler
{
    // [DllImport("__Internal")]
    // private static extern void SyncFiles();

    private static readonly FileSystem instace = new FileSystem();
    public static FileSystem Instance{
        get{
            return instace;
        }
    }

    // public override void Save<T>(FileType fileType, T data)
    // {
    //     base.Save(fileType, data);
    //     if (Application.platform == RuntimePlatform.WebGLPlayer)
    //     {
    //         SyncFiles();
    //     }
    // }
}
