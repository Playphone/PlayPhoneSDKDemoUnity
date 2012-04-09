using UnityEngine;
using System.Collections;

public class PPSDemoAchievementsView : PPSDemoViewAbstract 
{
  public PPSDemoAchievementsView()
  {
    viewName = "Achievements";
  }
  
  public override void Draw()
  {
    if (GUILayout.Button("Achievements List"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoAchievementsListView());
    }

    if (PPSDemoInfoStorage.currentUserInfo == null)
    {
      GUILayout.Label (PPSDemoCommonInfo.NotLoggedInMessage);
      GUI.enabled =false;
    }
    else 
    {
      GUI.enabled =true;
    }
    if (GUILayout.Button("User Achievements"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoUserAchievementsView());
    }
  }
}
