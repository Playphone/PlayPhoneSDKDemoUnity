#pragma strict

private var socialGraphUpdated : boolean = false;
private var currUserBuddyListArray : PlayPhone.MultiNet.Providers.MNWSBuddyListItem[] = null;

function PPSDemoJsSocialGraphConstruct() {
  PlayPhone.MultiNet.MNDirect.GetWSProvider().Send(new PlayPhone.MultiNet.Providers.MNWSInfoRequestCurrUserBuddyList(PPSDemoJsInfoRequestCurrUserBuddyListComplited));
}

function PPSDemoJsSocialGraphDraw() {
  if (!socialGraphUpdated) {
    GUILayout.Label("InformationUpdatingMessage");
  }
  else {
    if (currUserBuddyListArray != null) {
      if (currUserBuddyListArray.Length == 0) {
        GUILayout.Label("NoFriendsMessage");
      }
      else {

        var index:int = 0;

        while (index < currUserBuddyListArray.Length) {
          var postfix = " offline";

          if (currUserBuddyListArray[index].GetFriendUserOnlineNow().Value) {
            postfix = " online now";
          }

          var btnName = currUserBuddyListArray[index].FriendUserNickName + postfix;

          if (GUILayout.Button(btnName)) {
          }

          index++;
        }
      }
    }
    else {
      if (!PlayPhone.MultiNet.MNDirect.IsUserLoggedIn())
      {
        GUILayout.Label("NotLoggedInMessage");
      }
      else
      {
        GUILayout.Label("NoDataMessage");
      }
    }
  }
}

function PPSDemoJsSocialGraphUserLoggedIn() {
  PlayPhone.MultiNet.MNDirect.GetWSProvider().Send(new PlayPhone.MultiNet.Providers.MNWSInfoRequestCurrUserBuddyList(PPSDemoJsInfoRequestCurrUserBuddyListComplited));
}

function PPSDemoJsSocialGraphUserLoggedOut() {
  currUserBuddyListArray = null;
}

function PPSDemoJsInfoRequestCurrUserBuddyListComplited(result : PlayPhone.MultiNet.Providers.MNWSInfoRequestCurrUserBuddyList.RequestResult) {
  socialGraphUpdated = true;
  currUserBuddyListArray = result.DataEntry;
}
