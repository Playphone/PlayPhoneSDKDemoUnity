using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoDasboardView : PPSDemoViewAbstract 
{
  public PPSDemoDasboardView()
  {
    viewName = "Dashboard";
  }
  
  public override void Draw()
  {
    if (GUILayout.Button("Show Launcher Icon"))
    {
      MNDirectButton.Show();
    }

    GUILayout.Label("Tap on the P button to launch the dashboard");

    if (GUILayout.Button("Hide Launcher Icon"))
    {
      MNDirectButton.Hide();
    }

    GUILayout.Label("Dashboard Control");
    if (GUILayout.Button("Show dashboard"))
    {
      MNDirectUIHelper.ShowDashboard();
    }
  }
}
