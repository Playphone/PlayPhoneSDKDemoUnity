using System;

namespace PlayPhone.MultiNet
{
  public class MNGameParams
  {
    public const int MN_GAMESET_ID_DEFAULT = 0;

    public const int MN_PLAYMODEL_SINGLEPLAY      = 0x0000;
    public const int MN_PLAYMODEL_SINGLEPLAY_NET  = 0x0100;
    public const int MN_PLAYMODEL_MULTIPLAY       = 0x1000;

    public int    GameSetId{get;set;}
    public string GameSetParams{get;set;}
    public string ScorePostLinkId{get;set;}
    public int    GameSeed{get;set;}
    public int    PlayModel{get;set;}

    public MNGameParams () {}

    public MNGameParams (int gameSetId,
                         string gameSetParams,
                         string scorePostLinkId,
                         int gameSeed,
                         int playModel) {
      this.GameSetId       = gameSetId;
      this.GameSetParams   = gameSetParams;
      this.ScorePostLinkId = scorePostLinkId;
      this.GameSeed        = gameSeed;
      this.PlayModel       = playModel;
    }

    public override string ToString() {
      return "MNGameParams["   +
        "gameSetId = "         + GameSetId       + ", "   +
        "gameSetParams = \""   + GameSetParams   + "\", " +
        "scorePostLinkId = \"" + ScorePostLinkId + "\", " +
        "gameSeed = "          + GameSeed        + ", "   +
        "playModel = "         + PlayModel       + "]";
    }
  }
}

