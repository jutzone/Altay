using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class UniversalConfigParser
{
    private static UniversalConfigParser instance;
    public static UniversalConfigParser Instance { get { return instance; } }
    public static UniversalConfigParser Init(string mainXMLPath)
    {
        if (instance == null)
        {
            instance = new UniversalConfigParser();
            instance.SetMainConfigXML(mainXMLPath);
            return instance;
        }
        else
            return instance;
    }
    /// <summary>
    /// main xml document
    /// </summary>
    /// <returns></returns>
    private XmlDocument xml;
    private void SetMainConfigXML(string path)
    {
        try
        {
            xml = new XmlDocument();
            xml.LoadXml(path);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
    public static XmlNodeList GetNodesByTag(string nodesTag)
    {
        var concreteXml = new XmlDocument();
        var path = Path.Combine(Application.persistentDataPath, $"{nodesTag}.xml");


        concreteXml.Load(path);
        return concreteXml.SelectNodes(nodesTag);
    }
    public static string GetStringParam(XmlNodeList nodes, string paramName)
    {
        string outParams = "";
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = 0; j < nodes.Count; j++)
                outParams += nodes[i].SelectNodes(paramName)[j].InnerText;
        }
        return outParams;
    }
    public static string[] GetArrayStringParam(XmlNodeList nodes, string paramName)
    {
        string[] outParams = new string[nodes.Count - 1];
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = 0; j < nodes.Count; j++)
                outParams[i] = nodes[i].SelectNodes(paramName)[j].InnerText;
        }
        return outParams;
    }

    public static float GetFloatParam(XmlNodeList nodes, string paramName)
    {
        float outParam = 0f;
        outParam = float.Parse(nodes[0].SelectNodes(paramName)[0].InnerText);
        return outParam;
    }

}
