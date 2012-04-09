using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoVirtualItemsInfoView : PPSDemoViewAbstract 
{
  public PPSDemoVirtualItemsInfoView(MNVItemsProvider.GameVItemInfo gameVItemInfo)
  {
    viewName = "Virtual Item info";
    this.gameVItemInfo = gameVItemInfo;
  }
  
  public override void Draw()
  {
    if (needUpdateImage)
    {
      vItemImageUrl = MNDirect.GetVItemsProvider().GetVItemImageURL(gameVItemInfo.Id);
      needUpdateImage = false;

      MNImageLoader.GetInstance().Load(vItemImageUrl,OnImageLoaded);
    }

    GUILayout.Label("Id:"+gameVItemInfo.Id);
    
    GUILayout.Label("Name: "+gameVItemInfo.Name);
    
    GUILayout.Label("Description:");
    GUILayout.Label(""+gameVItemInfo.Description);
        
    GUILayout.Label("Model:");
    GUILayout.Label(""+gameVItemInfo.Model);
    
    GUILayout.Label("Image:");
    GUILayout.Label(vItemImageUrl);
    if (Image != null)
    {
      GUILayout.Box(Image);
    }
    
    GUILayout.Label("Aplication parameters:");
    GUI.enabled = false;
    GUILayout.TextArea(gameVItemInfo.Params,200);
    GUI.enabled = true;
  }
  
  public void OnImageLoaded (Texture2D LoadedImage) 
  {
    Image = LoadedImage;
  }

  protected MNVItemsProvider.GameVItemInfo gameVItemInfo;
  protected bool needUpdateImage = true;
  protected string vItemImageUrl;

  protected Texture2D Image = null;

}
