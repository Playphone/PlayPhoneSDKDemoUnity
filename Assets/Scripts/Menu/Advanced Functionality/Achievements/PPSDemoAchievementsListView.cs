using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoAchievementsListView : PPSDemoViewAbstract 
{
  public PPSDemoAchievementsListView()
  {
    viewName = "Achievements List";

    if (MNDirect.GetAchievementsProvider().IsGameAchievementListNeedUpdate())
    {
      MNDirect.GetAchievementsProvider().GameAchievementListUpdated += new MNAchievementsProvider.GameAchievementListUpdatedEventHandler(OnPlayerAchievementUnlocked);
      MNDirect.GetAchievementsProvider().DoGameAchievementListUpdate();
      gameAchievementListLoaded = false;
    }
    else
    {
      gameAchievementListLoaded = true;
    }
  }
  
  public override void Draw()
  {
    if (gameAchievementListLoaded) 
    {
      if (achInfoArray == null) 
      {
        achInfoArray = MNDirect.GetAchievementsProvider().GetGameAchievementsList();
      }

      for (int index = 0;index < achInfoArray.Length;index++) 
      {
        if(GUILayout.Button(achInfoArray[index].Name))
        {
          tagedAchInf = achInfoArray[index];
          tagetAchImageUrl = MNDirect.GetAchievementsProvider().GetAchievementImageURL(tagedAchInf.Id);
          PPSDemoMain.stackView.Push(new PPSDemoTagedAchievementView(tagedAchInf,tagetAchImageUrl));        
        }
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
  }

  public override void OnClose ()
  {
    MNDirect.GetAchievementsProvider().GameAchievementListUpdated -= new MNAchievementsProvider.GameAchievementListUpdatedEventHandler(OnPlayerAchievementUnlocked);
  }

  private void OnPlayerAchievementUnlocked()
  {
    gameAchievementListLoaded = true;
  }

  protected bool gameAchievementListLoaded = false;
  protected MNAchievementsProvider.GameAchievementInfo[] achInfoArray = null;
  protected MNAchievementsProvider.GameAchievementInfo tagedAchInf = null;
  protected string tagetAchImageUrl = "";
}
