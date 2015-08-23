using System;
using System.Xml;
using System.Collections.Generic;

public class DialogueLoader
{

    private static string dialogueProc(XmlNode textElem, Dictionary<string, string> chars)
    {
        string name = chars[textElem.Attributes["speaker"].Value];
        string outstr = (name.Length == 0 ? "" : name + "\n") + textElem.InnerText;
        foreach (var a in chars)
        {
            var r = "$(" + a.Key + ")";
            if (outstr.IndexOf(r) != -1)
            {
                outstr = outstr.Replace(r, a.Value);
            }
        }
        return outstr;
    }

    public static Dictionary<string, List<string>> LoadDialogue(string input)
    {
        var doc = new XmlDocument();
        doc.LoadXml(input);
        var charactersList = doc.GetElementsByTagName("character");
        var charactersMap = new Dictionary<string, string>();
        foreach (XmlNode charElem in charactersList)
        {
            charactersMap[charElem.Attributes["id"].Value] = charElem.Attributes["name"].Value;
        }
        var dialogueMap = new Dictionary<string, List<string>>();
        foreach (XmlNode dialogueElem in doc.GetElementsByTagName("dialogue"))
        {
            var dialogueStrs = new List<string>(dialogueElem.ChildNodes.Count);
            foreach (XmlNode textElem in dialogueElem)
            {
				if (!(textElem is XmlElement)) continue;
                dialogueStrs.Add(dialogueProc(textElem, charactersMap));
            }
            dialogueMap[dialogueElem.Attributes["id"].Value] = dialogueStrs;
        }
        return dialogueMap;
    }

    public static void Main(string[] args)
    {
        LoadDialogue("<scene><characters><character id=\"asdf\" name=\"DOGFISH\"/></characters><dialogue id=\"wat\"><text speaker=\"asdf\">POTATO $(asdf)</text></dialogue></scene>");
    }
}
