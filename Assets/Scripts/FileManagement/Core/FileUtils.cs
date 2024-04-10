using UnityEngine;
using System.IO;

/// <summary>
/// Utils Class for file management
/// </summary>
public static class FileUtils
{
    private const string FILE_EXTENTION = ".txt"; 
    public static bool HasFile(string pathName){
        return File.Exists(pathName);
    }

    public static string GetFilePath(string fileName){
        return Application.persistentDataPath + "/" + fileName;
    }

    public static string GetFilePath(string fileName, string parentFolderName){
        string dirPath = Application.persistentDataPath + "/" + parentFolderName;
        CreateCheckDirectory(dirPath);
        return  dirPath + "/" + fileName;
    }

    public static string GetFilePath(FileType fileType){
        string dirPath = Application.persistentDataPath + "/" + fileType.ToString();
        CreateCheckDirectory(dirPath);
        return dirPath + "/" + fileType.ToString() + FILE_EXTENTION;
    }

    public static string GetFilePath(FileType fileType, string fileName){
        string dirPath = Application.persistentDataPath + "/" + fileType.ToString();
        CreateCheckDirectory(dirPath);
        return dirPath + "/" + fileName + FILE_EXTENTION;
    }

    private static void CreateCheckDirectory(string dirName)
    {
        if(Directory.Exists(dirName))
        {
            return;
        }

        Directory.CreateDirectory(dirName);
    }

    public static void DeleteFile(string filePath)
    {
        if(HasFile(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static void WriteFile<T>(string filePath, T data){
        FileStream stream = new FileStream(filePath, FileMode.Create);
        string jsonData = JsonUtility.ToJson(data);
        
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(jsonData);
        writer.Close();
        stream.Close();
    }

    public static string ReadFile(string filePath){
        StreamReader reader = new StreamReader(filePath);
        string jsonData = reader.ReadToEnd();
        reader.Close();
        return jsonData;
    }
}
