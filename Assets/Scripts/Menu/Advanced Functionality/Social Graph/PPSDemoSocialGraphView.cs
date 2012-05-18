#define USE_JS

using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoSocialGraphView : PPSDemoViewAbstract 
{
  #if USE_JS
  public PPSDemoSocialGraphView()
  {
    viewName = "Social Graph";

    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsSocialGraphConstruct");
  }

  public override void Draw()
  {
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsSocialGraphDraw");
  }

  public override void UserLoggedIn()
  {
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsSocialGraphUserLoggedIn");
  }
  
  public override void UserLoggedOut()
  {
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsSocialGraphUserLoggedOut");
  }
  #else
  public PPSDemoSocialGraphView()
  {
    viewName = "Social Graph";
    MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrUserBuddyList(MNWSInfoRequestCurrUserBuddyListComplited));
  }
  
  public override void Draw()
  {
    if(!socialGraphUpdated)
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
    else
    {
      if (currUserBuddyListArray != null)
      {
        if (currUserBuddyListArray.Length == 0)
        {
          GUILayout.Label(PPSDemoCommonInfo.NoFriendsMessage);
        }
        else
        {
          for (int index = 0; index < currUserBuddyListArray.Length; index++)
          {
            string btnName = currUserBuddyListArray[index].FriendUserNickName + " "
                             + ((bool)currUserBuddyListArray[index].GetFriendUserOnlineNow() ? "online now" : "offline");

            if (GUILayout.Button(btnName))
            {
              currUserBuddyList = currUserBuddyListArray[index];
              PPSDemoMain.stackView.Push(new PPSDemoSocialGraphDetailView(currUserBuddyList));
            }
          }
        }
      }
      else
      {
        if (PPSDemoInfoStorage.currentUserInfo == null)
        {
          GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
        }
        else
        {
          GUILayout.Label(PPSDemoCommonInfo.NoDataMessage);
        }
      }
    }
  }
  
  public override void UserLoggedIn()
  {
    MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrUserBuddyList(MNWSInfoRequestCurrUserBuddyListComplited));
  }
  
  public override void UserLoggedOut()
  {
    currUserBuddyListArray = null;
  }
  
  private void MNWSInfoRequestCurrUserBuddyListComplited(MNWSInfoRequestCurrUserBuddyList.RequestResult requestResult) 
  {
      MNTools.DLog("MNWSInfoRequestCurrGameRoomListCompleted with result: " + requestResult.ToString());
      currUserBuddyListArray = (MNWSBuddyListItem[])requestResult.DataEntry;
      socialGraphUpdated = true;      
  }

  protected MNWSBuddyListItem[] currUserBuddyListArray = null;
  protected MNWSBuddyListItem currUserBuddyList = null;
  protected bool socialGraphUpdated = false;
  #endif
}
