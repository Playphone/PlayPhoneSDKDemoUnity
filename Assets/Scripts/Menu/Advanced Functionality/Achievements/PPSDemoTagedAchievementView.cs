using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoTagedAchievementView : PPSDemoViewAbstract 
{
  public PPSDemoTagedAchievementView(MNAchievementsProvider.GameAchievementInfo tagedAchInf,string tagetAchImageUrl)
  {
    viewName = "Taget Achievement";
    
    this.tagedAchInf = tagedAchInf;
    this.tagetAchImageUrl = tagetAchImageUrl;

    MNImageLoader.GetInstance().Load(tagetAchImageUrl, OnImageLoaded);    
  }
  
  public override void Draw()
  {
    if (tagedAchInf != null)
    {
      GUILayout.Label("name = " + tagedAchInf.Name);
      GUILayout.Label("Id = " + tagedAchInf.Id);        
      GUILayout.Label("Params = " + tagedAchInf.Params);
      GUILayout.Label("Points = " + tagedAchInf.Points);
      GUILayout.Label("Flags = " + tagedAchInf.Flags);
      GUILayout.Label("Description = " + tagedAchInf.Description);
      GUILayout.Label("");
      GUILayout.Label("Image url = " + tagetAchImageUrl);
      if (Image != null)
      {
        GUILayout.Box(Image);
      }
    }
    else
    {
      GUILayout.Label(PPSDemoCommonInfo.NoDataMessage);
    }
  }

  public void OnImageLoaded (Texture2D LoadedImage) 
  {
    Image = LoadedImage;
  }

  protected MNAchievementsProvider.GameAchievementInfo tagedAchInf = null;
  protected string tagetAchImageUrl = "";
  protected bool imgLoaded = false;
  protected Texture2D Image = null;
}
