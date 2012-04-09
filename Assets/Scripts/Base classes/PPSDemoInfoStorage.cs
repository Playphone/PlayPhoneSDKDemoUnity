using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

class PPSDemoInfoStorage
{

  public delegate void UserLoggedInEventHandler();
  public UserLoggedInEventHandler UserLoggedIn;

  public delegate void UserLoggedOutEventHandler();
  public UserLoggedOutEventHandler UserLoggedOut;

  public static int curSessionStatus = MNConst.MN_OFFLINE;
  public static MNUserInfo currentUserInfo = null;

  public static PPSDemoInfoStorage GetInstance()
  {
    if (instance == null)
    {
      instance = new PPSDemoInfoStorage();
    }

    return instance;
  }

  void OnPPSSessionStatusChanged(int newStatus)
  {
    if (((curSessionStatus == MNConst.MN_OFFLINE) || (curSessionStatus == MNConst.MN_CONNECTING)) &&
        (newStatus == MNConst.MN_LOGGEDIN))
    {

      curSessionStatus = newStatus;
      currentUserInfo = MNDirect.GetSession().GetMyUserInfo();

      UserLoggedIn();
    }

    if (((curSessionStatus != MNConst.MN_OFFLINE) &&
         (curSessionStatus != MNConst.MN_CONNECTING)) &&
         ((newStatus == MNConst.MN_OFFLINE) ||
         (newStatus == MNConst.MN_CONNECTING)))
    {

      curSessionStatus = newStatus;
      currentUserInfo = null;

      UserLoggedOut();
    }

    curSessionStatus = newStatus;
  }

  private PPSDemoInfoStorage()
  {
    MNDirect.SessionStatusChanged += new MNDirect.SessionStatusChangedEventHandler(OnPPSSessionStatusChanged);

    MNSession session = MNDirect.GetSession();

    if (session != null) 
    {
      curSessionStatus = session.GetStatus();
      currentUserInfo = session.GetMyUserInfo();
    }
  }

  private static PPSDemoInfoStorage instance = null;
}

