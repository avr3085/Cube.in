using UnityEngine;
using System;

/// <summary>
/// File Management Abstract Class
/// </summary>
public abstract class FileHandler
{
    /// <summary>
    /// Saves File of Type T to the specified Location. Check FileUtils for location details and file type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileType"></param>
    /// <param name="fileName"></param>
    /// <param name="data"></param>
    public virtual void Save<T>(FileType fileType, string fileName, T data)
    {
        string filePath = FileUtils.GetFilePath(fileType, fileName);
        if(filePath.Equals("")){
            Debug.LogError("Invalid File Path. Please Check FileSystem.cs!");
            return;
        }

        FileUtils.WriteFile(filePath, data);
    }

    public virtual void Save<T>(FileType fileType, T data)
    {
        string filePath = FileUtils.GetFilePath(fileType);
        if(filePath.Equals("")){
            Debug.LogError("Invalid File Path. Please Check FileSystem.cs!");
            return;
        }

        FileUtils.WriteFile(filePath, data);
    }

    /// <summary>
    /// Load file from disk
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileType">File Category</param>
    /// <param name="args">add constructor to the class of type T</param>
    /// <returns>returns T if exit in disk, or returns new T(filetype, args)</returns>
    /// Ex = T fileClass = FileSystem.Instance.Load<T>(FileType.TestClass, args0,"args1",...);
    public virtual T Load<T>(FileType fileType, params object[] args) where T : new()
    {
        string filePath = FileUtils.GetFilePath(fileType);
        if(FileUtils.HasFile(filePath)){
            //save override the file with new data; 
            string val = FileUtils.ReadFile(filePath);
            return (T) Convert.ChangeType(JsonUtility.FromJson<T>(val), typeof(T));
        }else{
            T val = (T)Activator.CreateInstance(typeof(T), args);
            Save(fileType,val);
            return val;
        }
    }

    /// <summary>
    /// Loads file from disk
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileType">File Category</param>
    /// <param name="fileName">Name of the file without extention</param>
    /// <param name="args"></param>
    /// <returns>returns T if exit in disk, or returns new T(filetype, args)</returns>
    public virtual T Load<T>(FileType fileType, string fileName, params object[] args) where T : new()
    {
        string filePath = FileUtils.GetFilePath(fileType, fileName);
        if(FileUtils.HasFile(filePath)){
            //save override the file with new data; 
            string val = FileUtils.ReadFile(filePath);
            return (T) Convert.ChangeType(JsonUtility.FromJson<T>(val), typeof(T));
        }else{
            T val = (T)Activator.CreateInstance(typeof(T), args);
            Save(fileType, fileName, val);
            return val;
        }
    }
}
