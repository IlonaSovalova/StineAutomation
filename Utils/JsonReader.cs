using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomationFramwork.Utils;

public static class JsonReader
{
    public static T Read<T>(string filePath)
    {
        var text= FileReader.Read(filePath);
        return JsonConvert.DeserializeObject<T>(text);
    }

    public static async Task<T> ReadAsync<T>(string filePath)
    {
        var text= await FileReader.ReadAsync(filePath); 
        return JsonConvert.DeserializeObject<T>(text);  
    }
    public static dynamic ReadJSONFromFile(string jsonPath)
    {
        var jsonFields = File.ReadAllText(jsonPath);
        return DeserializeJSONObject(jsonFields);
    }

    public static dynamic DeserializeJSONObject(dynamic jsonObj)
    {
        return JsonConvert.DeserializeObject<dynamic>(jsonObj);
    }

    public static string GetJSONValue(dynamic json, string key)
    {
        return json[key].ToString();
    }

    public static string GetFilePathFromInputData(string fileName)
    {
        var directoryPath = AppDomain.CurrentDomain.BaseDirectory.Split("\\bin");
        return directoryPath[0] + "\\InputData\\" + fileName;
    }
}
