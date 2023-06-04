using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DebugLogDisplay : MonoBehaviour
{

    Dictionary<string, string> logs = new Dictionary<string, string>();
    public TextMeshProUGUI text;

    private void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        if(type == LogType.Log || type == LogType.Error) {
           string[] split = logString.Split(char.Parse(":"));
           string key = split[0];
           string value = split.Length > 1 ? split[1] : "";

            if (logs.ContainsKey(key)) {
                // string strValue = logs[key];
                // if(strValue.Contains(")->* ")){
                //     string postStr = strValue.Split(char.Parse("*"))[1];
                //     string preStr = strValue.Split(char.Parse("*"))[0];
                //     string newValue = "(" + (int.Parse(preStr[1].ToString()) + 1).ToString() + "()->* " + postStr;
                // }else{
                //     string newValue = "(2)->* " + strValue;
                // }

                // string numAndValue = logs[key];
                logs[key] = value;
            } else {
                logs.Add(key, value);
            }
        }

        string textValue = "";
        foreach (KeyValuePair<string, string> log in logs) {
            if(log.Value == "")
                textValue += log.Key + "\n";
            else
                textValue += log.Key + ": " + log.Value + "\n";
        }
        
        if(type == LogType.Error)
            text.color = Color.red;

        text.text= textValue;        
    }

    public void CloseDebugLog() {
        gameObject.SetActive(false);
    }
}
