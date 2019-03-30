using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class SettingPage : MonoBehaviour { 


    public GameObject Language;
    public GameObject Sound;
    public GameObject Light;
    /**   public GameObject SoundLeft;
      public GameObject SoundRight;
      public GameObject LightLeft;
      public GameObject LightRight;
      public GameObject LanguageLeft;
      public GameObject LanguageRight;**/


    private ArrayList languageList = new ArrayList { "CHINESE", "ENGLISH", "FRENCH" };
    private ArrayList soundList = new ArrayList { "0%", "25%", "50%", "75%", "100%" };
    private ArrayList lightList = new ArrayList { "0%", "25%", "50%", "75%", "100%" };
    public void LanguageLeft() {

        var current = Language.GetComponent<Text>().text;
        print(current);
        int i =1;
        foreach (string s in languageList) {

            if (s.Equals(current)) {
               i = languageList.IndexOf(s);
                break;
            }

        }

        if (i != 0)
        {

            Language.GetComponent<Text>().text = (string)languageList[i - 1];
        }



    }

    public void LanguageRight()
    {

        var current = Language.GetComponent<Text>().text;
        int i = languageList.IndexOf(current);
        if (i != languageList.Count - 1)
        {

            Language.GetComponent<Text>().text = (string)languageList[i + 1];
        }



    }

    public void SoundLeft() {

        var current = Sound.GetComponent<Text>().text;
        int i = soundList.IndexOf(current);
        if (i != 0) {

            Sound.GetComponent<Text>().text = (string)soundList[i - 1];
        }



    }
    public void SoundRight()
    {

        var current = Sound.GetComponent<Text>().text;
        int i = soundList.IndexOf(current);
        if (i != soundList.Count-1)
        {

            Sound.GetComponent<Text>().text = (string)soundList[i + 1];
        }



    }

    public void LightLeft()
    {

        var current = Light.GetComponent<Text>().text;
        int i = lightList.IndexOf(current);
        if (i != 0)
        {

            Light.GetComponent<Text>().text = (string)lightList[i - 1];
        }




    }
    public void LightRight()
    {

        var current = Light.GetComponent<Text>().text;
        int i = lightList.IndexOf(current);
        if (i != lightList.Count - 1)
        {

            Light.GetComponent<Text>().text = (string)lightList[i + 1];
        }



    }


}
