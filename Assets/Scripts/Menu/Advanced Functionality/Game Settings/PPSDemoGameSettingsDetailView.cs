using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoGameSettingsDetailView : PPSDemoViewAbstract 
{
  public PPSDemoGameSettingsDetailView(int gameSettingID)
  {
    viewName = "Game Settings Detail";
    this.gameSettingID = gameSettingID;
  }
  
  public override void Draw()
  {
    if (gameSettingID > -1)
    {
      if (gameSettingInfo == null)
      {
        gameSettingInfo = MNDirect.GetGameSettingsProvider().FindGameSettingById(gameSettingID);
      }
      
      GUI.enabled = false;
      GUILayout.TextField(gameSettingInfo.SysParams + " " + gameSettingInfo.Params,gameSettingInfoStringLength);
      GUI.enabled = true;
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
  }

  protected int gameSettingID;
  protected MNGameSettingsProvider.GameSettingInfo gameSettingInfo = null;
  protected int gameSettingInfoStringLength = 250;
}
