using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoCurrentGameRoomCookiesView : PPSDemoViewAbstract 
{
  public PPSDemoCurrentGameRoomCookiesView()
  {
    viewName = "Current Game Room Cookies";
    MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrGameRoomList(MNWSInfoRequestCurrGameRoomListCompleted));
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo != null)
    {
      storeCookiesStr = GUILayout.TextField(storeCookiesStr,200);
    }
    else
    {
      GUILayout.TextField(PPSDemoCommonInfo.LoggedToManage,PPSDemoCommonInfo.LoggedToManage);
    }
    if (GUILayout.Button(" Store cookies "))
    {
      int key = UnityEngine.Random.Range(0,5);
      MNDirect.GetGameRoomCookiesProvider().SetCurrentGameRoomCookie(key,storeCookiesStr);    
      storedCookieStr = storeCookiesStr;
      storeCookies = true;
      cookiesStrArray = getCookiesArray();
    }
     
    if (GUILayout.Button(" Read cookies "))
    {
      cookiesStrArray = getCookiesArray();
      cookiesRead = true;
      storeCookies = false;
    }
     
    if (cookiesRead)
    {
      for (int index = 0; index < 5; index ++)
      {
        GUILayout.Label(cookiesStrArray[index]);
      }    
    }
      
    if (storeCookies)
    {
      GUILayout.Label("stored: " + storedCookieStr);    
    }
      
    GUI.enabled =  false;
    GUILayout.TextArea(PPSDemoCommonInfo.CurrenRoomMessage,PPSDemoCommonInfo.CurrenRoomMessage.Length);
  }
  
  private void MNWSInfoRequestCurrGameRoomListCompleted(MNWSInfoRequestCurrGameRoomList.RequestResult requestResult) 
  {
    if (!requestResult.HadError)
    {
      MNTools.DLog("MNWSInfoRequestCurrGameRoomListCompleted with result: " + requestResult.ToString());
      roomUserInfo = (MNWSRoomListItem[])requestResult.DataEntry;
    }
    else
    {
      MNTools.DLog(requestResult.ErrorMessage);
    }
  }
  
  private string[] getCookiesArray()
  {
    string[] tempStrArray = {"","","","",""};
    for (int index = 0; index < 5; index++)
    {
      string strToPrint = "Id:" + index + ", Value: ";
      string cookieStr = MNDirect.GetGameRoomCookiesProvider().GetCurrentGameRoomCookie(index);
      tempStrArray[index] = strToPrint + cookieStr;
    }    
    return tempStrArray;
  }

  protected int gameRoomKey = 1;
  protected MNWSRoomListItem[] roomUserInfo = null;
  protected bool gameRoomIDUpdated = false;
  protected string[] cookiesStrArray = new string[5];
  protected string storedCookieStr = "";
  protected string storeCookiesStr = "";
  protected bool storeCookies = false;
  protected bool cookiesRead = false;
}
