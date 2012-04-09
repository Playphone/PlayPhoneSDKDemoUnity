using System;

namespace PlayPhone.MultiNet.Core
{
  public class MNJoinRoomInvitationParams
  {
    public int    FromUserSFId  {get;set;}
    public string FromUserName  {get;set;}
    public int    RoomSFId      {get;set;}
    public string RoomName      {get;set;}
    public int    RoomGameId    {get;set;}
    public int    RoomGameSetId {get;set;}
    public string InviteText    {get;set;}

    public MNJoinRoomInvitationParams () {}
    
    public MNJoinRoomInvitationParams (int fromUserSFId, string fromUserName,
                                       int roomSFId, string roomName,
                                       int roomGameId, int roomGameSetId,
                                       string inviteText)
    {
      this.FromUserSFId  = fromUserSFId;
      this.FromUserName  = fromUserName;
      this.RoomSFId      = roomSFId;
      this.RoomName      = roomName;
      this.RoomGameId    = roomGameId;
      this.RoomGameSetId = roomGameSetId;
      this.InviteText    = inviteText;
    }

    public override string ToString ()
    {
      return string.Format ("MNJoinRoomInvitationParams[FromUserSFId={0}, FromUserName=\"{1}\", RoomSFId={2}, RoomName=\"{3}\", RoomGameId={4}, RoomGameSetId={5}, InviteText=\"{6}\"]",
        FromUserSFId,
        (FromUserName == null) ? "null" : "\"" + FromUserName + "\"",
        RoomSFId,
        (RoomName == null) ? "null" : "\"" + RoomName + "\"",
        RoomGameId,
        RoomGameSetId,
        (InviteText == null) ? "null" : "\"" + InviteText + "\"");
    }
  }
}

