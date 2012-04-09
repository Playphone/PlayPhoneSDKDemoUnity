using System;

namespace PlayPhone.MultiNet
{
  public class MNUserInfo
  {
    public long   UserId        {get;set;}
    public int    UserSFId      {get;set;}
    public string UserName      {get;set;}
    public string UserAvatarUrl {get;set;}

    public MNUserInfo () {}
    public MNUserInfo (long   userId,
                       int    userSFId,
                       string userName,
                       string userAvatarUrl)
    {
      this.UserId        = userId;
      this.UserSFId      = userSFId;
      this.UserName      = userName;
      this.UserAvatarUrl = userAvatarUrl;
    }

    public override string ToString() {
      return "MNUserInfo["   +
        "UserId = "          + UserId        +", "   +
        "UserSFId = "        + UserSFId      +", "   +
        "UserName = \""      + UserName      + "\", " +
        "UserAvatarUrl = \"" + UserAvatarUrl + "\"]";
    }
  }
}

