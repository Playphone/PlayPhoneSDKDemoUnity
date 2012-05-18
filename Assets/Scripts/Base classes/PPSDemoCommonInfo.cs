using UnityEngine;

public class PPSDemoCommonInfo{

  public static int gameId              = 10900;
  public static uint gameSecretPart1    = 0xae2b10f2;
  public static uint gameSecretPart2    = 0x248f58d9;
  public static uint gameSecretPart3    = 0xc9654f24;
  public static uint gameSecretPart4    = 0x37960337;

  public static string NotLoggedInMessage         = "User is not logged in";
  public static string NoDataMessage              = "<No Data>";
  public static string LeaderboardIsEmptyMessage  = "Leaderboard is empty";
  public static string NoFriendsMessage           = "You have no friends yet.";
  public static string InformationUpdatingMessage = "Information updating...";
  public static string noAchievementsMessage      = "There is no achievements.";
  public static string LoggedToManage             = "You should be logged in to manage this";
  public static string CurrenRoomMessage          = "Room Cookies saved data for current room only with an id form 0 to 99(each up to 1KB), this simple app only saves randomly to first five.";
  public static string CloudStorageMessage        = "Room Cookies saved data for current room only with an id form 0 to 99(each up to 1KB), this simple app only saves randomly to first five.";

  public static GameObject GetJSGameObject() {
    if (jsGameObject == null) {
      Debug.Log("jsGameObject = null");
      jsGameObject = new GameObject(jsGameObjectName);

      jsGameObject.AddComponent("PPSDemoBuyShopPackViewJS");
      jsGameObject.AddComponent("PPSDemoSocialGraphViewJS");
    }

    return jsGameObject;
  }

  private static GameObject jsGameObject = null;
  private const string jsGameObjectName = "PPSDemoJSGameObject";
}
