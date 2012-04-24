using UnityEngine;
using System;
using System.Collections.Generic;
using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoMain : MonoBehaviour
{
  void Start ()
  {
    MNTools.SetDebugLevel(MNTools.DEBUG_LEVEL_OFF);
    MNTools.DLog("Start");

    MNDirect.Init(PPSDemoCommonInfo.gameId,
                  MNDirect.MakeGameSecretByComponents(PPSDemoCommonInfo.gameSecretPart1,
                                                      PPSDemoCommonInfo.gameSecretPart2,
                                                      PPSDemoCommonInfo.gameSecretPart3,
                                                      PPSDemoCommonInfo.gameSecretPart4));

    MNDirectButton.InitWithLocation((int)MNDirectButton.MNDIRECTBUTTON_TOPRIGHT);
    MNDirectButton.Show();
    MNDirectPopup.Init((int)(MNDirectPopup.MNDIRECTPOPUP_ALL));

    MNDirectUIHelper.BindHostActivity();

    stackView.Push(new PPSDemoExitPageView());
    stackView.Push(new PPSDemoMainView());
    
    // set max title label size
    demoSkin.label.fixedWidth = Screen.width - topGap;

    // set title label style
    demoStyle.normal.textColor = Color.white;
    demoStyle.alignment = TextAnchor.MiddleCenter;
  }

  public GUISkin demoSkin;
  public GUIStyle demoStyle;
    
  public static PPSStackView stackView = new PPSStackView();

  void OnGUI ()
  {
    GUI.skin = demoSkin;
    
    GUI.enabled = true;
    
    GUILayout.BeginArea(new Rect(0,topGap,Screen.width,Screen.height - topGap));

    scrollPosition = GUILayout.BeginScrollView(scrollPosition,
                                               GUILayout.Width(Screen.width),
                                               GUILayout.Height(Screen.height - topGap));
    
    GUILayout.BeginVertical();

    GUILayout.BeginHorizontal();
    if (stackView.Count() > MainViewPosition)
    {
      if (GUILayout.Button("Back", GUILayout.Width((int)(Screen.width * 0.25))))
      {
        stackView.Pop();
      }
    }
    GUILayout.Label(stackView.getViewName(),demoStyle);

    GUILayout.EndHorizontal();

    stackView.Draw();

    GUILayout.EndVertical();

    GUILayout.EndScrollView();
    GUILayout.EndArea();
  }

  void Update()
  {
    if (stackView.Count() > 1)
    {
      if (Input.GetKeyUp(KeyCode.Escape))
      {
        stackView.Pop();
      }
    }
    else 
    {
      if (Input.GetKeyUp(KeyCode.Escape))
      {
        PPSDemoMain.stackView.Push(new PPSDemoMainView());
      }
    }
  }

  protected int topGap = 50;
  protected Vector2 scrollPosition;
  protected int MainViewPosition = 2;
}
