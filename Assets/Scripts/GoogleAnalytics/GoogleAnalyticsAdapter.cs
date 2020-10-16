using UnityEngine;

public class GoogleAnalyticsAdapter : MonoBehaviour
{
    [SerializeField]
     GoogleAnalyticsV4 googleAnalytics;

     private static GoogleAnalyticsAdapter sinstance;

     public static GoogleAnalyticsAdapter Instance
     {
         get
         {
             if(sinstance == null)
             {
                 sinstance = FindObjectOfType<GoogleAnalyticsAdapter>();
                 DontDestroyOnLoad(sinstance);
             }
            return sinstance;
         }
         
     }


     void Awake()
     {
          if(sinstance == null)
          {
              sinstance = this;
              DontDestroyOnLoad(this);
          }
          else if(this != sinstance)
          {
              Destroy(this.gameObject);
          }
     }

     public void googleAnalyticsLogEvent(string mEventName,string mParameter1 = " ", string mParameter2 = " ", int mParameter3 = 0)
     {
         googleAnalytics.LogEvent(mEventName,mParameter1,mParameter2,mParameter3);
     }
}
