//#define PPS_UNIT_TESTS
//#define PPS_INTERNAL_TESTS
//#define PPS_WS

using UnityEngine;
using System;
using System.Collections;
using PlayPhone.MultiNet;

public class PPSDemoMainView : PPSDemoViewAbstract {
  
  public PPSDemoMainView()
  {
    viewName = "Main Menu";

    #if PPS_UNIT_TESTS
    try {
      allTests = new MNBatchUnitTest("All tests",true,true);

      MNBatchUnitTest serializationTestBatch = new MNBatchUnitTest("Serialization tests",true,true);
      /*
      serializationTestBatch.AddTest(new MNVShopProvider_CorrectSerializationTest(true));
      serializationTestBatch.AddTest(new MNVShopProvider_ThrowableSerializationTest(true));
      serializationTestBatch.AddTest(new MNVShopProvider_IncorrectSerializationTest(true));
      */

      serializationTestBatch.AddTest(new MNVShopProvider_VShopPackInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVShopProvider_VShopPackInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNVItemsProvider_TransactionInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVItemsProvider_TransactionInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNErrorInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNErrorInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNGameParamsSerializationTest(true));
      serializationTestBatch.AddTest(new MNGameParamsSerializationTest(true));

      serializationTestBatch.AddTest(new MNUserInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNUserInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNAchievementsProvider_GameAchievementInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNAchievementsProvider_GameAchievementInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNAchievementsProvider_PlayerAchievementInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNAchievementsProvider_PlayerAchievementInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNVItemsProvider_GameVItemInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVItemsProvider_GameVItemInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNVItemsProvider_PlayerVItemInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVItemsProvider_PlayerVItemInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNVItemsProvider_TransactionVItemInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVItemsProvider_TransactionVItemInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNVItemsProvider_TransactionErrorSerializationTest(true));
      serializationTestBatch.AddTest(new MNVItemsProvider_TransactionErrorSerializationTest(true));

      serializationTestBatch.AddTest(new MNVShopProvider_VShopDeliveryInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVShopProvider_VShopDeliveryInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNVShopProvider_VShopCategoryInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVShopProvider_VShopCategoryInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNScoreProgressProvider_ScoreItemSerializationTest(true));
      serializationTestBatch.AddTest(new MNScoreProgressProvider_ScoreItemSerializationTest(true));

      serializationTestBatch.AddTest(new MNVShopProvider_CheckoutVShopPackSuccessInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVShopProvider_CheckoutVShopPackSuccessInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNVShopProvider_CheckoutVShopPackFailInfoSerializationTest(true));
      serializationTestBatch.AddTest(new MNVShopProvider_CheckoutVShopPackFailInfoSerializationTest(true));

      serializationTestBatch.AddTest(new MNBuddyRoomParamsSerializationTest(true));
      serializationTestBatch.AddTest(new MNBuddyRoomParamsSerializationTest(true));

      serializationTestBatch.AddTest(new MNJoinRoomInvitationParamsSerializationTest(true));
      serializationTestBatch.AddTest(new MNJoinRoomInvitationParamsSerializationTest(true));

      allTests.AddTest(serializationTestBatch);
      allTests.Run();

      PPSDemoMain.consolePrintln(allTests.GetTestOutput());
    }
    catch (Exception e) {
      PPSDemoMain.consolePrintln(String.Format("Exception during runing UntiTests:\nException: {0}\nStack: {1}",e.Message,e.StackTrace));
    }
    #endif
  }
  
  public override void Draw()
  {
    GUILayout.Label("Required Integration");

    if (GUILayout.Button("Login User"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoLoginUserView());
    }
    if (GUILayout.Button("Dasboard"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoDasboardView());
    }
    if (GUILayout.Button("Virtual Economy"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoVirtualEconomyView());
    }

    GUILayout.Label("Advanced Functionality");

    if (GUILayout.Button("Current User Info"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoCurrentUserInfoView());
    }
    
    if (GUILayout.Button("Leaderbords"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoLeaderboardsView());
    }
    
    if (GUILayout.Button("Achievements"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoAchievementsView());
    }
    
    if (GUILayout.Button("Social Graph"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoSocialGraphView());
    }
    
    if (GUILayout.Button("Dashboard Control"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoDashboardControlView());
    }
    
    if (GUILayout.Button("Cloud Storage"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoCloudStorageView());
    }
    
    if (GUILayout.Button("Game Settings"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoGameSettingsView());
    }

    if (GUILayout.Button("Room cookies"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoRoomCookiesView());
    }
    
    if (GUILayout.Button("Multiplayer Basics"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoMultiplayerBasicsView());
    }

    if (GUILayout.Button("Server info"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoServerInfoView());
    }

    GUILayout.Label("System Information");
    
    if (GUILayout.Button("Application info"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoApplicationInfoView());
    }

    GUILayout.Label("");

    if (GUILayout.Button("Image Test"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoImageTestView());
    }

    #if PPS_INTERNAL_TESTS
    if (GUILayout.Button("Internal Tests"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoInternalTestsView());
    }
    #endif

    #if PPS_WS
    if (GUILayout.Button("WS Tests"))
    {
      PPSDemoMain.stackView.Push(new PPDemoWSTestView());
    }
    #endif
  }

  #if PPS_UNIT_TESTS
  protected MNBatchUnitTest allTests = null;
  #endif

}