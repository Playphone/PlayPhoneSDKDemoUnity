using System;

namespace PlayPhone.MultiNet.Core
{
  public class MNBuddyRoomParams
  {
    public string RoomName      {get;set;}
    public int    GameSetId     {get;set;}
    public string ToUserIdList  {get;set;}
    public string ToUserSFIdList{get;set;}
    public string InviteText    {get;set;}
  
    public MNBuddyRoomParams () {}
    
    public MNBuddyRoomParams (string  roomName,
                              int     gameSetId,
                              string  toUserIdList,
                              string  toUserSFIdList,
                              string  inviteText)
    {
      this.RoomName       = roomName;
      this.GameSetId      = gameSetId;
      this.ToUserIdList   = toUserIdList;
      this.ToUserSFIdList = toUserSFIdList;
      this.InviteText     = inviteText;
    }

    public override string ToString ()
    {
      return string.Format ("MNBuddyRoomParams[RoomName=\"{0}\", GameSetId={1}, ToUserIdList=\"{2}\", ToUserSFIdList=\"{3}\", InviteText=\"{4}\"]",
        (RoomName == null) ? "null" : "\"" + RoomName + "\"",
        GameSetId,
        (ToUserIdList == null) ? "null" : "\"" + ToUserIdList + "\"",
        (ToUserSFIdList == null) ? "null" : "\"" + ToUserSFIdList + "\"",
        (InviteText == null) ? "null" : "\"" + InviteText + "\"");
    }
  }
}

