//
//  MNClientRobotsProvider.java
//  MultiNet client
//
//  Copyright 2010 PlayPhone. All rights reserved.
//

package com.playphone.multinet.providers;

import java.util.HashSet;
import java.util.HashMap;

import it.gotoandplay.smartfoxclient.SmartFoxClient;
import it.gotoandplay.smartfoxclient.data.Room;
import it.gotoandplay.smartfoxclient.data.SFSVariable;
import it.gotoandplay.smartfoxclient.data.RoomVariableRequest;

import com.playphone.multinet.core.MNSession;
import com.playphone.multinet.core.MNSessionEventHandlerAbstract;
import com.playphone.multinet.MNUserInfo;

/**
 * A class representing "client-side robots" MultiNet provider.
 * "client-side robot" provider provides information on client-side robots
 * and allows to post robots scores.
 */
public class MNClientRobotsProvider
 {
  public static final String PROVIDER_NAME = "com.playphone.mn.robotscore";

  /**
   * Constructs a new <code>MNClientRobotsProvider</code> object.
   *
   * @param session         MultiNet session instance
   */
  public MNClientRobotsProvider (MNSession      session)
   {
    sessionEventHandler = new SessionEventHandler(session);
   }

  /**
   * Stops provider and frees all allocated resources.
   */
  public void shutdown ()
   {
    sessionEventHandler.shutdown();
    sessionEventHandler = null;
   }

  /**
   * Checks if passed user information belongs to client-side robot
   *
   * @param userInfo player information
   * @return <code>true</code> if player identified by userInfo parameter is
   * client-side robot and <code>false</code> otherwise.
   */
  public boolean isRobot (MNUserInfo userInfo)
   {
    return sessionEventHandler.isRobot(userInfo);
   }

  /**
   * Sends client-side robot's score to MultiNet server
   *
   * @param userInfo robot information
   * @param score game score
   */
  public void postRobotScore (MNUserInfo userInfo, long score)
   {
    sessionEventHandler.session.sendPluginMessage
     (PROVIDER_NAME,String.format("robotScore:%d:%d",userInfo.userSFId,score));
   }

  /**
   * Requests a server to maintain the particular number of robots in
   * current game room
   * @param robotCount number of robots server should maintain
   */
  public void setRoomRobotLimit (int robotCount)
   {
    SmartFoxClient smartFox = sessionEventHandler.session.getSmartFox();

    HashMap<String,RoomVariableRequest> vars = new HashMap<String,RoomVariableRequest>();

    RoomVariableRequest varRequest =
     new RoomVariableRequest(Integer.toString(robotCount),
                             SFSVariable.TYPE_STRING,
                             false,true);

    vars.put(ROBOT_ROOM_LIMIT_VARNAME,varRequest);

    smartFox.setRoomVariables(vars,smartFox.activeRoomId);
   }

  /**
   * Returns a number of robots server was asked to maintain in current
   * game room
   * @return number of robots server should maintain
   */
  public int getRoomRobotLimit ()
   {
    int  result     = 0;
    Room activeRoom = sessionEventHandler.session.getSmartFox().getActiveRoom();

    if (activeRoom != null)
     {
      SFSVariable var = activeRoom.getVariable(ROBOT_ROOM_LIMIT_VARNAME);

      if (var != null && var.getType().equals(SFSVariable.TYPE_STRING))
       {
        try
         {
          result = Integer.parseInt(var.getValue());
         }
        catch (NumberFormatException e)
         {
         }
       }
     }

    return result;
   }

  private static class SessionEventHandler extends MNSessionEventHandlerAbstract
   {
    public HashSet<Integer> robots;
    public MNSession        session;

    public SessionEventHandler (MNSession      session)
     {
      this.session = session;

      robots = new HashSet<Integer>();

      session.addEventHandler(this);
     }

    public synchronized void shutdown ()
     {
      session.removeEventHandler(this);

      robots = null;
     }

    public synchronized boolean isRobot (MNUserInfo userInfo)
     {
      return robots.contains(new Integer(userInfo.userSFId));
     }

    public synchronized void mnSessionRoomUserLeave (MNUserInfo userInfo)
     {
      robots.remove(new Integer(userInfo.userSFId));
     }

    public synchronized void mnSessionPluginMessageReceived
                                               (String     pluginName,
                                                String     message,
                                                MNUserInfo sender)
     {
      if (pluginName.equals(PROVIDER_NAME))
       {
        if (message.startsWith(IROBOT_MESSAGE_PREFIX))
         {
          String dataStr  = message.substring(IROBOT_MESSAGE_PREFIX_LEN);
          int    colonPos = dataStr.indexOf(':');

          if (colonPos >= 0)
           {
            dataStr = dataStr.substring(0,colonPos);
           }

          try
           {
            int sfid = Integer.parseInt(dataStr);

            robots.add(new Integer(sfid));
           }
          catch (NumberFormatException e)
           {
           }
         }
       }
     }

    public synchronized void mnSessionStatusChanged (int newStatus, int oldStatus)
     {
      if (!session.isInGameRoom())
       {
        robots.clear();
       }
     }

    private static final String IROBOT_MESSAGE_PREFIX = "irobot:";
    private static final int    IROBOT_MESSAGE_PREFIX_LEN = IROBOT_MESSAGE_PREFIX.length();
   }

  private static final String ROBOT_ROOM_LIMIT_VARNAME = "MN_robot_limit";

  private SessionEventHandler sessionEventHandler;
 }

