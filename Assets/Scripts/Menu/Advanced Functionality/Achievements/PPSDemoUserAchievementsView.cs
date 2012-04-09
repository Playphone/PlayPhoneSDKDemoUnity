using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoUserAchievementsView : PPSDemoViewAbstract 
{
  public PPSDemoUserAchievementsView()
  {
    viewName = "User Achievements";
    
    MNDirect.GetAchievementsProvider().PlayerAchievementUnlocked  += new MNAchievementsProvider.PlayerAchievementUnlockedEventHandler(OnPlayerAchievementUnlocked);
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo != null)
   {
    if (!playerAchArrayUpdated)
    {
      playerAchInfoArray = MNDirect.GetAchievementsProvider().GetPlayerAchievementsList();
      playerAchArrayUpdated= true;
    }

    if(GUILayout.Button("Unlock achievements"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoUnlockAchievementsView());         
    }
        
    if (playerAchArrayUpdated) 
    {
      if (playerAchInfoArray != null)
      {
        for (int index = 0;index < playerAchInfoArray.Length;index++) 
        {
            if(GUILayout.Button("Achievment Id: "+playerAchInfoArray[index].Id))
            {
              tagedAchInf = MNDirect.GetAchievementsProvider().FindGameAchievementById(playerAchInfoArray[index].Id);
              tagetAchImageUrl = MNDirect.GetAchievementsProvider().GetAchievementImageURL(tagedAchInf.Id);
              PPSDemoMain.stackView.Push(new PPSDemoTagedAchievementView(tagedAchInf,tagetAchImageUrl));         
            }
        }
      }
      else 
      {
        GUILayout.Label(PPSDemoCommonInfo.noAchievementsMessage);        
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
   }
   else
   {
     GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
   }
  }

  public override void OnClose ()
  {
    MNDirect.GetAchievementsProvider().PlayerAchievementUnlocked  -= new MNAchievementsProvider.PlayerAchievementUnlockedEventHandler(OnPlayerAchievementUnlocked);
  }
  
  public override void UserLoggedIn()
  {
    playerAchArrayUpdated = false;
  }
  
  private void OnPlayerAchievementUnlocked (int achievementId) 
  {
    playerAchArrayUpdated = false;
    MNTools.DLog("MNDirect_PlayerAchievementUnlocked");
  }

  protected bool playerAchArrayUpdated = false;
  protected MNAchievementsProvider.PlayerAchievementInfo[] playerAchInfoArray = null;
  protected MNAchievementsProvider.GameAchievementInfo tagedAchInf = null;
  protected string tagetAchImageUrl = "";
}
