using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet.Core;

public class PPSDemoImageTestView : PPSDemoViewAbstract
{
  public PPSDemoImageTestView()
  {
    viewName = "testing images";
    
    MNImageLoader imgLoader = MNImageLoader.GetInstance();
    Images = new Texture2D[10];

    imgLoader.Load(img1, PPSDemo_ImageLoaded0);
    imgLoader.Load(img2, PPSDemo_ImageLoaded1);
    imgLoader.Load(img3, PPSDemo_ImageLoaded2);
    imgLoader.Load(img4, PPSDemo_ImageLoaded3);
  }
  
  public override void Draw()
  {
    GUILayout.Label("testing");
    
    for(int i = 0; i< 10; i++)
    {
      if (Images[i] != null)
      {
        GUILayout.Label("img = " + i);
        GUILayout.Box(Images[i]);
        GUILayout.Label("width = " + Images[i].width);
        GUILayout.Label("heigth = " + Images[i].height);
      }
    }
  }
  
  public void PPSDemo_ImageLoaded0 (Texture2D LoadedImage) 
  {
    Images[0] = LoadedImage;
  }
  public void PPSDemo_ImageLoaded1 (Texture2D LoadedImage) 
  {
    Images[1] = LoadedImage;
  }  
  public void PPSDemo_ImageLoaded2 (Texture2D LoadedImage) 
  {
    Images[2] = LoadedImage;
  }  
  public void PPSDemo_ImageLoaded3 (Texture2D LoadedImage) 
  {
    Images[3] = LoadedImage;
  }

  private Texture2D[] Images = null;

  private string img1 = "http://playphone.com/Content/images/header_logo2.jpg";
  private string img2 = "https://www.google.com.ua/logos/2012/juan_gris-2012-hp11.jpg";  // incorect URL
  private string img3 = "http://developer.playphone.com/sites/all/themes/portal/images/header_logo.png";
  private string img4 = "https://www.google.com.ua/images/srpr/logo3w.png";
}
