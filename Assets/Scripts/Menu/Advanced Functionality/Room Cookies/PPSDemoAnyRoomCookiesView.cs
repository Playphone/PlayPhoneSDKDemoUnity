using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoAnyRoomCookiesView : PPSDemoViewAbstract 
{
  public PPSDemoAnyRoomCookiesView()
  {
    viewName = "Any Room Cookies";
    MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrGameRoomList(MNWSInfoRequestCurrGameRoomListCompleted));
  }
  
  public override void Draw()
  {
    GUILayout.Label("Room list:");
    if (GUILayout.Button("Reload list")) 
    {
      needReloadRoomList = true;
    }
    GUILayout.Label("Select room from list to load cookies");
    if (roomUserInfo != null)
    {
      GUILayout.Label("Total rooms = " + roomUserInfo.Length);

      for (int index = 0; index < roomUserInfo.Length; index++)
      {
        string btnCookieListName = "id(" + (int)roomUserInfo[index].GetRoomSFId() + ") : " + roomUserInfo[index].RoomName;
        if (GUILayout.Button(btnCookieListName)) 
        {
          cookiesRead = true;
          gameRoomID = (int)roomUserInfo[index].GetRoomSFId();
          getCookiesArray(gameRoomID);
        }
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
        
    if ((cookiesRead)&&(gameRoomID != 0))
    {
      GUILayout.Label("Room id:" + gameRoomID);
      for (int index = 0; index < cookiesStrArray.Length; index ++)
      {
        GUILayout.Label(cookiesStrArray[index]);
      }    
    }
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
    
  private void getCookiesArray(int RoomID)
  {
    MNDirect.GetGameRoomCookiesProvider().GameRoomCookieDownloadSucceeded += OnCookiesReaded;

    for (int index = 0; index < 5; index ++)
    {
      MNDirect.GetGameRoomCookiesProvider().DownloadGameRoomCookie(RoomID, index);
    }    
  }

  private void OnCookiesReaded(int roomSFId,int key,string cookie)
  {
    if (gameRoomID == roomSFId)
    {
      cookiesStrArray[key] = "Id:" + key + ", Value: " + cookie;
    }
  }

  public override void OnClose()
  {
    MNDirect.GetGameRoomCookiesProvider().GameRoomCookieDownloadSucceeded -= OnCookiesReaded;
  }

  protected bool needReloadRoomList = true;
  protected MNWSRoomListItem[] roomUserInfo = null;
  protected int gameRoomID = 0;
  protected bool cookiesRead = false;
  protected string[] cookiesStrArray = new string[5];
}
