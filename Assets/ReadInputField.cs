using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ReadInputField : MonoBehaviour
{
    public TMP_InputField FirstInputField;
    public TMP_InputField SecondInputField;
    public List<int> asciiCodeList = new List<int>();
    string[] LanguageNameArray = { "Text", "ASCII", "LowerCase", "UpperCase", "Binary" };
    public string OutputText;
    public string FirstLanguage;
    public string SecondLanguage;
    public bool AutomaticConversion;
    public Dropdown FirstDropdown;
    public Dropdown SecondDropdown;

    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;
    public string InputFieldSearchQuery;
    public string OutputFieldSearchQuery;

    public string SearchURL;

    public bool MustBeSearchedOnGoogle;
    public bool MustBeSearchedOnBing;
    public bool MustBeSearchedOnWikipedia;
    public bool MustBeSearchedOnYoutube;

    void Start()
    {
        MustBeSearchedOnGoogle = true;
        FirstLanguage = LanguageNameArray[0];
        SecondLanguage = LanguageNameArray[0];
    }

    void Update()
    {
        readInputField(FirstInputField.text);
        if (AutomaticConversion)
        {
            Convert();
        }
        if (FirstInputField.selectionAnchorPosition <= FirstInputField.text.Length && FirstInputField.selectionFocusPosition <= FirstInputField.text.Length)
        {
            InputFieldSearchQuery = FirstInputField.text.Substring(Mathf.Min(FirstInputField.selectionAnchorPosition, FirstInputField.selectionFocusPosition), Mathf.Abs(FirstInputField.selectionFocusPosition - FirstInputField.selectionAnchorPosition));
        }
        else
        {
            InputFieldSearchQuery = "";
        }
        if (SecondInputField.selectionAnchorPosition <= SecondInputField.text.Length && SecondInputField.selectionFocusPosition <= SecondInputField.text.Length)
        {
            OutputFieldSearchQuery = SecondInputField.text.Substring(Mathf.Min(SecondInputField.selectionAnchorPosition, SecondInputField.selectionFocusPosition), Mathf.Abs(SecondInputField.selectionFocusPosition - SecondInputField.selectionAnchorPosition));
        }
        else
        {
            OutputFieldSearchQuery = "";
        }
        print(InputFieldSearchQuery);
    }
    public void readInputField(string inputFieldText)
    {
        inputFieldText = FirstInputField.text;
        if (FirstLanguage == LanguageNameArray[0])
        {
            if (SecondLanguage == LanguageNameArray[0])
            {
                //Text to Text
                OutputText = inputFieldText;
            }
            else if (SecondLanguage == LanguageNameArray[1])
            {
                //Text to ASCII
                ConvertTextIntoASCII(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[2])
            {
                //Text to LowerCase
                ConvertUpperCaseToLowerCase(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[3])
            {
                //Text to UpperCase
                ConvertLowerCaseToUpperCase(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[4])
            {
                //Text to Binary
                ConvertTextToBinary(inputFieldText);
            }
        }
        else if (FirstLanguage == LanguageNameArray[1])
        {
            if (SecondLanguage == LanguageNameArray[0])
            {
                //ASCII to Text
                ConvertASCIIIntoText(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[1])
            {
                OutputText = inputFieldText;
            }
            else if (SecondLanguage == LanguageNameArray[2])
            {
                //ASCII to ASCII LowerCase
                ConvertASCIIIntoText(inputFieldText);
                ConvertUpperCaseToLowerCase(OutputText);
                ConvertTextIntoASCII(OutputText);
            }
            else if (SecondLanguage == LanguageNameArray[3])
            {
                //ASCII to ASCII UpperCase
                ConvertASCIIIntoText(inputFieldText);
                ConvertLowerCaseToUpperCase(OutputText);
                ConvertTextIntoASCII(OutputText);
            }
            else if (SecondLanguage == LanguageNameArray[4])
            {
                //ASCII to Binary
                ConvertASCIIIntoText(inputFieldText);
                ConvertTextToBinary(OutputText);
            }
        }
        else if (FirstLanguage == LanguageNameArray[2])
        {
            if (SecondLanguage == LanguageNameArray[0])
            {
                //LowerCase to Text
                OutputText = inputFieldText;
            }
            else if (SecondLanguage == LanguageNameArray[1])
            {
                //LowerCase to ASCII LowerCase = Text to ASCII
                ConvertUpperCaseToLowerCase(inputFieldText);
                ConvertTextIntoASCII(OutputText);
            }
            else if (SecondLanguage == LanguageNameArray[2])
            {
                //LowerCase to LowerCase
                ConvertUpperCaseToLowerCase(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[3])
            {
                //LowerCase to UpperCase
                ConvertLowerCaseToUpperCase(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[4])
            {
                //LowerCase to Binary = Text to Binary
                ConvertTextToBinary(inputFieldText);
            }
        }
        else if (FirstLanguage == LanguageNameArray[3])
        {
            if (SecondLanguage == LanguageNameArray[0])
            {
                //UpperCase to Text
                OutputText = inputFieldText;
            }
            else if (SecondLanguage == LanguageNameArray[1])
            {
                //UpperCase To ASCII UpperCase
                ConvertLowerCaseToUpperCase(inputFieldText);
                ConvertTextIntoASCII(OutputText);
            }
            else if (SecondLanguage == LanguageNameArray[2])
            {
                //UpperCase To LowerCase
                ConvertUpperCaseToLowerCase(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[3])
            {
                //UpperCase to UpperCase
                ConvertLowerCaseToUpperCase(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[4])
            {
                //UpperCase to Binary = Text to Binary
                ConvertTextToBinary(inputFieldText);
            }
        }
        else if (FirstLanguage == LanguageNameArray[4])
        {
            if (SecondLanguage == LanguageNameArray[0])
            {
                //Binary to Text
                ConvertBinaryToString(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[1])
            {
                //Binary to ASCII
                ConvertBinaryToString(inputFieldText);
                inputFieldText = OutputText;
                ConvertTextIntoASCII(inputFieldText);
            }
            else if (SecondLanguage == LanguageNameArray[2])
            {
                //Binary to Binary LowerCase
                ConvertBinaryToString(inputFieldText);
                ConvertUpperCaseToLowerCase(OutputText);
                ConvertTextToBinary(OutputText);
            }
            else if (SecondLanguage == LanguageNameArray[3])
            {
                //Binary to UpperCase
                ConvertBinaryToString(inputFieldText);
                ConvertLowerCaseToUpperCase(OutputText);
                ConvertTextToBinary(OutputText);
            }
            else if (SecondLanguage == LanguageNameArray[4])
            {
                //Binary to Binary
                OutputText = inputFieldText;
            }
        }
    }

    public void Convert()
    {
        if (FirstLanguage != null && SecondLanguage != null)
        {
            if (FirstLanguage == LanguageNameArray[0])
            {
                if (SecondLanguage == LanguageNameArray[0])
                {
                    //Text to Text
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[1])
                {
                    //Text to ASCII
                    WriteAsciiIntoSecondInputField();
                }
                else if (SecondLanguage == LanguageNameArray[2])
                {
                    //Text to LowerCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[3])
                {
                    //Text to UpperCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[4])
                {
                    //Text to Binary
                    WriteIntoSecondInputFieldFromOutputText();
                }
            }
            else if (FirstLanguage == LanguageNameArray[1])
            {
                if (SecondLanguage == LanguageNameArray[0])
                {
                    //ASCII to Text
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[1])
                {
                    //ASCII to ASCII
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[2])
                {
                    //ASCII to ASCII LowerCase 
                    WriteAsciiIntoSecondInputField();
                }
                else if (SecondLanguage == LanguageNameArray[3])
                {
                    //ASCII to ASCII UpperCase
                    WriteAsciiIntoSecondInputField();
                }
                else if (SecondLanguage == LanguageNameArray[4])
                {
                    //ASCII to Binary
                    WriteIntoSecondInputFieldFromOutputText();
                }
            }
            else if (FirstLanguage == LanguageNameArray[2])
            {
                if (SecondLanguage == LanguageNameArray[0])
                {
                    //LowerCase to Text
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[1])
                {
                    //LowerCase to ASCII LowerCase
                    WriteAsciiIntoSecondInputField();
                }
                else if (SecondLanguage == LanguageNameArray[2])
                {
                    //LowerCase to LowerCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[3])
                {
                    //LowerCase to UpperCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[4])
                {
                    //LowerCase to Binary = Text to Binary
                    WriteIntoSecondInputFieldFromOutputText();
                }
            }
            else if (FirstLanguage == LanguageNameArray[3])
            {
                if (SecondLanguage == LanguageNameArray[0])
                {
                    //UpperCase to Text
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[1])
                {
                    //UpperCase to ASCII = Text to ASCII
                    WriteAsciiIntoSecondInputField();
                }
                else if (SecondLanguage == LanguageNameArray[2])
                {
                    //UpperCase to LowerCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[3])
                {
                    //UpperCase to UpperCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[4])
                {
                    //UpperCase to Binary
                    WriteIntoSecondInputFieldFromOutputText();
                }
            }
            else if (FirstLanguage == LanguageNameArray[4])
            {
                if (SecondLanguage == LanguageNameArray[0])
                {
                    //Binary to Text
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[1])
                {
                    //Binary to ASCII
                    WriteAsciiIntoSecondInputField();
                }
                else if (SecondLanguage == LanguageNameArray[2])
                {
                    //Binary to Binary LowerCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[3])
                {
                    //Binary to Binary UpperCase
                    WriteIntoSecondInputFieldFromOutputText();
                }
                else if (SecondLanguage == LanguageNameArray[4])
                {
                    //Binary to Binary
                    WriteIntoSecondInputFieldFromOutputText();
                }
            }
        }
    }

    public void ConvertTextIntoASCII(string inputFieldText)
    {
        asciiCodeList.Clear();
        foreach (char c in inputFieldText)
        {
            int asciiCode = (int)c;
            asciiCodeList.Add(asciiCode);
        }
    }
    public void WriteAsciiIntoSecondInputField()
    {
        string asciiCodes = string.Join(" ", asciiCodeList);
        SecondInputField.text = asciiCodes;
    }

    public void ConvertASCIIIntoText(string asciiCodeString)
    {
        string[] asciiCodes = asciiCodeString.Split(new char[] { ' ', ',' });
        asciiCodeList.Clear();
        foreach (string code in asciiCodes)
        {
            if (int.TryParse(code, out int asciiCode))
            {
                asciiCodeList.Add(asciiCode);
            }
        }
        OutputText = ConvertAsciiToText(asciiCodeList);
    }
    string ConvertAsciiToText(List<int> asciiCodeList)
    {
        char[] chars = new char[asciiCodeList.Count];
        for (int i = 0; i < asciiCodeList.Count; i++)
        {
            chars[i] = (char)asciiCodeList[i];
        }
        return new string(chars);
    }

    public void ConvertLowerCaseToUpperCase(string inputFieldText)
    {
        OutputText = inputFieldText.ToUpper();
    }

    public void ConvertUpperCaseToLowerCase(string inputFieldText)
    {
        OutputText = inputFieldText.ToLower();
    }

    public void ConvertASCIIIntoUpperCase(string asciiCodeString)
    {
        ConvertASCIIIntoText(asciiCodeString);
        OutputText = OutputText.ToUpper();
    }

    public void ConvertTextToBinary(string inputFieldText)
    {
        List<string> binaryCodes = new List<string>();
        foreach (char c in inputFieldText)
        {
            binaryCodes.Add(System.Convert.ToString(c, 2).PadLeft(8, '0'));
        }
        OutputText = string.Join(" ", binaryCodes);
    }

    public string ConvertBinaryToString(string inputFieldText)
    {
        string[] binaryValues = inputFieldText.Split(' ');

        List<char> chars = new List<char>();

        foreach (string binaryValue in binaryValues)
        {
            if (binaryValue.Length > 0)
            {
                int asciiValue = System.Convert.ToInt32(binaryValue, 2);

                chars.Add((char)asciiValue);
            }
        }

        return OutputText = new string(chars.ToArray());
    }
    
    public void WriteIntoSecondInputFieldFromOutputText()
    {
        SecondInputField.text = OutputText;
    }
    public void GetFirstLanguage(int languageIndex)
    {
        FirstLanguage = LanguageNameArray[languageIndex];
    }
    public void GetSecondLanguage(int languageIndex)
    {
        SecondLanguage = LanguageNameArray[languageIndex];
    }
    public void ReverseLanguage()
    {
        int FirstDropDownIndex = FirstDropdown.value;
        int SecondDropDownIndex = SecondDropdown.value;
        
        FirstDropdown.value = SecondDropDownIndex;
        SecondDropdown.value = FirstDropDownIndex;

        FirstDropdown.onValueChanged.Invoke(SecondDropDownIndex);
        SecondDropdown.onValueChanged.Invoke(FirstDropDownIndex);
    }
    public void ReverseText()
    {
        string firstInputFieldText = FirstInputField.text;
        string secondInputFieldText = SecondInputField.text;
        FirstInputField.text = secondInputFieldText;
        SecondInputField.text = firstInputFieldText;
    }
    public void ReverseLanguageAndText()
    {
        ReverseLanguage();
        ReverseText();
    }
    public void AutomaticallyConverted(bool Toogle)
    {
        AutomaticConversion = !AutomaticConversion;
    }
    public void SearchOnGoogle(bool Toogle)
    {
        MustBeSearchedOnGoogle = Toogle;
    }
    public void SearchOnBing(bool Toogle)
    {
        MustBeSearchedOnBing = Toogle;
    }
    public void SearchOnWikipedia(bool Toogle)
    {
        MustBeSearchedOnWikipedia = Toogle;
    }
    public void SearchOnYoutube(bool Toogle)
    {
        MustBeSearchedOnYoutube = Toogle;
    }
    public void CopyAllTextFromInputField()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = FirstInputField.text;
        textEditor.SelectAll();
        textEditor.Copy();
    }
    public void CopySelectedTextFromInputField()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = InputFieldSearchQuery;
        textEditor.SelectAll();
        textEditor.Copy();
    }
    public void SearchAllTextFromInputField()
    {
        if (MustBeSearchedOnGoogle)
        {
            SearchURL = "https://www.google.com/search?q=" + FirstInputField.text;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnBing)
        {
            SearchURL = "https://www.bing.com/search?q=" + FirstInputField.text;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnWikipedia)
        {
            SearchURL = "https://en.wikipedia.org/wiki/Special:Search?search=" + FirstInputField.text;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnYoutube)
        {
            SearchURL = "https://www.youtube.com/results?search_query=" + FirstInputField.text;
            Application.OpenURL(SearchURL);
        }
    }
    public void SearchSelectedTextFromInputField()
    {
        if (MustBeSearchedOnGoogle)
        {
            SearchURL = "https://www.google.com/search?q=" + InputFieldSearchQuery;
            print(SearchURL);
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnBing)
        {
            SearchURL = "https://www.bing.com/search?q=" + InputFieldSearchQuery;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnWikipedia)
        {
            SearchURL = "https://en.wikipedia.org/wiki/Special:Search?search=" + InputFieldSearchQuery;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnYoutube)
        {
            SearchURL = "https://www.youtube.com/results?search_query=" + InputFieldSearchQuery;
            Application.OpenURL(SearchURL);
        }
    }

    public void CopyAllTextFromOutputField()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = FirstInputField.text;
        textEditor.SelectAll();
        textEditor.Copy();
    }
    public void CopySelectedTextFromOutputField()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = OutputFieldSearchQuery;
        textEditor.SelectAll();
        textEditor.Copy();
    }
    public void SearchAllTextFromOutputField()
    {
        if (MustBeSearchedOnGoogle)
        {
            SearchURL = "https://www.google.com/search?q=" + SecondInputField.text;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnBing)
        {
            SearchURL = "https://www.bing.com/search?q=" + SecondInputField.text;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnWikipedia)
        {
            SearchURL = "https://en.wikipedia.org/wiki/Special:Search?search=" + SecondInputField.text;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnYoutube)
        {
            SearchURL = "https://www.youtube.com/results?search_query=" + SecondInputField.text;
            Application.OpenURL(SearchURL);
        }
    }
    public void SearchSelectedTextFromOutputField()
    {
        if (MustBeSearchedOnGoogle)
        {
            SearchURL = "https://www.google.com/search?q=" + OutputFieldSearchQuery;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnBing)
        {
            SearchURL = "https://www.bing.com/search?q=" + OutputFieldSearchQuery;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnWikipedia)
        {
            SearchURL = "https://en.wikipedia.org/wiki/Special:Search?search=" + OutputFieldSearchQuery;
            Application.OpenURL(SearchURL);
        }
        if (MustBeSearchedOnYoutube)
        {
            SearchURL = "https://www.youtube.com/results?search_query=" + OutputFieldSearchQuery;
            Application.OpenURL(SearchURL);
        }
    }
}