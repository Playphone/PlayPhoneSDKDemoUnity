using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoCurrentUserInfoView : PPSDemoViewAbstract 
{
  public PPSDemoCurrentUserInfoView()
  {
    
    MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrentUserInfo(CurrentUserInfoRequestCompleted));
    
    viewName = "Current User Info";
  }
  
  public override void Draw()
  {
    if ((currentUserInfoExt == null) || (PPSDemoInfoStorage.currentUserInfo == null))
    {
      GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
    }
    else 
    {
      GUILayout.Label("Id: " + currentUserInfoExt.UserId);
      GUILayout.Label("NickName: " + currentUserInfoExt.UserNickName);
      GUILayout.Label("E-mail: " + currentUserInfoExt.UserEmail);
      GUILayout.Label("AvatarExists: " + currentUserInfoExt.UserAvatarExists);
      GUILayout.Label("AvatarUrl: " + currentUserInfoExt.UserAvatarUrl);
      if (Image != null)
      {
        GUILayout.Box(Image);
      }
      GUILayout.Label("GamePoints: " + currentUserInfoExt.UserGamePoints);
      GUILayout.Label("OnlineNow: " + currentUserInfoExt.UserOnlineNow);
    }
  }
  
  private void CurrentUserInfoRequestCompleted (MNWSInfoRequestCurrentUserInfo.RequestResult result) 
  {
    if (!result.HadError)
    {
      currentUserInfoExt = result.DataEntry;

      MNImageLoader.GetInstance().Load(currentUserInfoExt.UserAvatarUrl, OnImageLoaded);
    }
    else 
    {
      MNTools.DLog(result.ErrorMessage);
    }
  }
  
  private void OnImageLoaded (Texture2D LoadedImage) 
  {
    Image = LoadedImage;
  }

  protected MNWSCurrentUserInfo currentUserInfoExt = null;

  protected Texture2D Image = null;
}
