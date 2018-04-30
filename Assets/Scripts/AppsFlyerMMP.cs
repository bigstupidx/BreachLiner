using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppsFlyerMMP : MonoBehaviour {

    void Start()
    {
        // For detailed logging
        //AppsFlyer.setIsDebug (true);
        AppsFlyer.setAppsFlyerKey("aTYJZVwsYCTz8BbnbrDbxL");
#if UNITY_IOS
        //Mandatory - set your AppsFlyer’s Developer key.
        
        //Mandatory - set your apple app ID
        //NOTE: You should enter the number only and not the "ID" prefix
        AppsFlyer.setAppID ("1373841647");
        AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
        //Mandatory - set your Android package name
        AppsFlyer.setAppID("com.belizard.collider");
        //Mandatory - set your AppsFlyer’s Developer key.
        AppsFlyer.init("aTYJZVwsYCTz8BbnbrDbxL");

        //AppsFlyer.setCustomerUserID("659231");

        //For getting the conversion data in Android, you need to this listener.
        //AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");

#endif
    }
    
    public static void Score(int batch)
    {

        Dictionary<string, string> score = new Dictionary<string, string>();
        score.Add("score", batch.ToString());
        AppsFlyer.trackRichEvent("score", score);

    }

    public static void HighScore()
    {

        Dictionary<string, string> score = new Dictionary<string, string>();
        score.Add("high_score", "1");
        AppsFlyer.trackRichEvent("high_score", score);

    }

}
