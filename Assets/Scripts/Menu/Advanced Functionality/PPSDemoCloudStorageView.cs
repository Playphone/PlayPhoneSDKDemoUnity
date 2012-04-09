using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoCloudStorageView : PPSDemoViewAbstract 
{
  public PPSDemoCloudStorageView()
  {
    viewName = "Cloud storage";
    MNDirect.GetGameCookiesProvider().GameCookieDownloadSucceeded += new MNGameCookiesProvider.GameCookieDownloadSucceededEventHandler(OnGameCookieDownloadSucceeded);
    MNDirect.GetGameCookiesProvider().GameCookieUploadSucceeded += new MNGameCookiesProvider.GameCookieUploadSucceededEventHandler(OnGameCookieUploadSucceeded); 
    MNDirect.GetGameCookiesProvider().GameCookieUploadFailedWithError += new MNGameCookiesProvider.GameCookieUploadFailedWithErrorEventHandler(OnGameCookieUploadFailedWithError);
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo != null)
    {
      if (PPSDemoInfoStorage.currentUserInfo != null)
      {
        storeCookiesStr = GUILayout.TextField(storeCookiesStr,200);
      }
      else
      {
        GUILayout.TextField(PPSDemoCommonInfo.LoggedToManage,PPSDemoCommonInfo.LoggedToManage);
      }

      if (GUILayout.Button(" Store cookies ") && (PPSDemoInfoStorage.currentUserInfo != null))
      {
        int key = UnityEngine.Random.Range(0,5);
        MNDirect.GetGameCookiesProvider().UploadUserCookie(key,storeCookiesStr);
        storeCookies = true;
        ReadCookies();
      }

      if (GUILayout.Button(" Read cookies ") && (PPSDemoInfoStorage.currentUserInfo != null))
      {
        ReadCookies();
        cookiesRead = true;
        storeCookies = false;
      }
      
      if (cookiesRead)
      {
        for (int index = 0; index < 5; index ++)
        {
          GUILayout.Label("cookie id " + index + " : " + cookiesStrArray[index]);
        }    
      }
      
      if (storeCookies)
      {
        GUILayout.Label("stored: " + storeCookiesInt+ " " + cookiesStrArray[storeCookiesInt]);    
      }
      
      GUI.enabled =  false;
      GUILayout.TextArea(PPSDemoCommonInfo.CurrenRoomMessage,PPSDemoCommonInfo.CurrenRoomMessage.Length);
    }
    else
    {
      GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
    }
  }

  public override void UserLoggedOut()
  {
    cookiesRead = false;
    storeCookies = false;
  }
  
  private void ReadCookies()
  {
    for (int i = 0; i<5; i++)
    {
      MNDirect.GetGameCookiesProvider().DownloadUserCookie(i);
    }
  }

  private void OnGameCookieDownloadSucceeded(int key, string cookie)
  {
    MNTools.DLog("MNGameCookiesProvider_GameCookieDownloadSucceeded with result: key-" + key.ToString() + ", cookie -" + cookie);
    cookiesStrArray[key] = cookie;  
  }
  private void OnGameCookieUploadSucceeded(int key)
  {
    MNTools.DLog("MNGameCookiesProvider_GameCookieUploadSucceeded with result: key-" + key.ToString());
    storeCookies = true;
    storeCookiesInt = key;
  }
  private void OnGameCookieUploadFailedWithError(int key, string error)
  {
    MNTools.DLog("MNGameCookiesProvider_GameCookieUploadFailedWithError with result: key-" + key.ToString()+ ", error -" + error);
  }

  protected int gameRoomID = 0;
  protected MNWSRoomListItem[] roomUserInfo = null;
  protected bool gameRoomIDUpdated = false;
  protected string[] cookiesStrArray = new string[5];
  protected string storedCookieStr = "";
  protected string storeCookiesStr = "";
  protected bool storeCookies = false;
  protected int storeCookiesInt;
  protected bool cookiesRead = false;
}
