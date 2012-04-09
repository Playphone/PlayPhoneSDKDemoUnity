using UnityEngine;
using System.Collections;
using System;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoServerInfoView : PPSDemoViewAbstract 
{
  public PPSDemoServerInfoView()
  {
    viewName = "Server Info";
    MNDirect.GetServerInfoProvider().ServerInfoItemReceived += new MNServerInfoProvider.ServerInfoItemReceivedEventHandler(OnServerInfoRequestMessage);
    MNDirect.GetServerInfoProvider().ServerInfoItemRequestFailedWithError += new MNServerInfoProvider.ServerInfoItemRequestFailedWithErrorEventHandler(OnServerInfoItemRequestFailedWithError);
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo == null)
    {
      GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage); 
      GUILayout.Button("Get server info");
    }
    else
    {
      if (GUILayout.Button("Get server info"))
      {

        MNDirect.GetServerInfoProvider().RequestServerInfoItem(MNServerInfoProvider.SERVER_TIME_INFO_KEY);
      }
            
      if (showServerInfo)
      {
        GUILayout.Label("Server Info:");
        GUILayout.Label("");
        DateTime d = ConvertFromUnixTimestamp(long.Parse(serverRequest));
        string strDate = "Server time: " + serverRequest;
        GUILayout.Label(strDate);
        GUILayout.Label("");
        strDate = "(" + String.Format("{0:F}", d) + ")";
        GUILayout.Label(strDate);
      }
            
      if (showServerError)
      {
        GUILayout.Label("Server send some error message:");
        GUILayout.Label(serverRequest);
      }
    }
  }

  public override void OnClose()
  {
    MNDirect.GetServerInfoProvider().ServerInfoItemReceived -= new MNServerInfoProvider.ServerInfoItemReceivedEventHandler(OnServerInfoRequestMessage);
    MNDirect.GetServerInfoProvider().ServerInfoItemRequestFailedWithError -= new MNServerInfoProvider.ServerInfoItemRequestFailedWithErrorEventHandler(OnServerInfoItemRequestFailedWithError);
  }
  
  private DateTime ConvertFromUnixTimestamp(double timestamp)
  {
    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    return origin.AddSeconds(timestamp);
  }


  private void OnServerInfoRequestMessage(int key, string message)
  {
    showServerInfo = true;
    serverRequest = message;
  }

  private void OnServerInfoItemRequestFailedWithError(int key, string error)
  {
    showServerError = true;
    serverRequest = error;    
  }

  protected string serverRequest = "";
  protected bool showServerInfo = false;
  protected bool showServerError = false;
}
