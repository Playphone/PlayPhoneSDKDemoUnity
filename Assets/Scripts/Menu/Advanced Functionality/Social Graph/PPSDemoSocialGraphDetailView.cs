using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoSocialGraphDetailView : PPSDemoViewAbstract 
{
  public PPSDemoSocialGraphDetailView(MNWSBuddyListItem currUserBuddyList)
  {
    viewName = "Social Graph Detail";
    this.currUserBuddyList = currUserBuddyList;
  }
  
  public override void Draw()
  {
    GUI.enabled = false;
    GUILayout.TextArea(currUserBuddyList.ToString(),currUserBuddyList.ToString().Length);   
    GUI.enabled = true;
  }

  protected MNWSBuddyListItem currUserBuddyList = null;
}
