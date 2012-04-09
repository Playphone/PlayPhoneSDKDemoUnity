using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoGameSettingsView : PPSDemoViewAbstract 
{
  public PPSDemoGameSettingsView()
  {
    viewName = "Game Settings";

    if (MNDirect.GetGameSettingsProvider().IsGameSettingListNeedUpdate())
    {
      MNDirect.GetGameSettingsProvider().GameSettingListUpdated += new MNGameSettingsProvider.GameSettingListUpdatedEventHandler(OnGameSettingListUpdated);
      MNDirect.GetGameSettingsProvider().DoGameSettingListUpdate();
      gameSettingListLoaded = false;
    }
    else
    {
      gameSettingListLoaded = true;
    }
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo == null)
    {
      GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
      GUI.enabled = false;
    }

    if (gameSettingListLoaded) 
    {
        if (gameSettingInfoArray == null)
        {
          gameSettingInfoArray = MNDirect.GetGameSettingsProvider().GetGameSettingList();
        }
        
        for (int index = 0;index < gameSettingInfoArray.Length;index++) 
        {
          string btnName = "id:"+gameSettingInfoArray[index].Id+" "+gameSettingInfoArray[index].Name;
          if(GUILayout.Button(btnName))
          {
            gameSettingID = gameSettingInfoArray[index].Id;
            PPSDemoMain.stackView.Push(new PPSDemoGameSettingsDetailView(gameSettingID));    
          }
        }
    }
    else 
    {
        GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
    
    GUI.enabled = true;
  }

  public override void OnClose ()
  {
    MNDirect.GetGameSettingsProvider().GameSettingListUpdated -= new MNGameSettingsProvider.GameSettingListUpdatedEventHandler(OnGameSettingListUpdated);
  }

  private void OnGameSettingListUpdated()
  {
    gameSettingListLoaded = true;
  }

  protected bool gameSettingListLoaded = false;
  protected MNGameSettingsProvider.GameSettingInfo[] gameSettingInfoArray = null;
  protected int gameSettingID = -100;
}
