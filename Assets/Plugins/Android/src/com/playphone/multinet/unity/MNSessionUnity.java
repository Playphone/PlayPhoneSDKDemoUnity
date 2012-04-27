package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;
import com.playphone.multinet.core.MNBuddyRoomParams;
import com.playphone.multinet.core.MNJoinRoomInvitationParams;

public class MNSessionUnity {
    public static boolean loginAuto() {
        MNUnity.MARK();

        return MNDirect.getSession().loginAuto();
    }

    public static void logout() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getSession().logout();
            }
        });
    }

    public static int getStatus() {
        MNUnity.MARK();

        return MNDirect.getSession().getStatus();
    }

    public static String getMyUserName() {
        MNUnity.MARK();

        return MNDirect.getSession().getMyUserName();
    }

    public static String getMyUserInfo() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getSession().getMyUserInfo());
    }

    public static String getRoomUserList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getSession().getRoomUserList());
    }

    public static int getRoomUserStatus() {
        MNUnity.MARK();

        return MNDirect.getSession().getRoomUserStatus();
    }

    public static int getCurrentRoomId() {
        MNUnity.MARK();

        return MNDirect.getSession().getCurrentRoomId();
    }

    public static int getRoomGameSetId() {
        MNUnity.MARK();

        return MNDirect.getSession().getRoomGameSetId();
    }

    public static void reqJoinBuddyRoom(final int roomSFId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            @SuppressWarnings("deprecation")
            public void run()
            {
                MNDirect.getSession().reqJoinBuddyRoom(roomSFId);
            }
        });
    }

    public static void sendJoinRoomInvitationResponse(final String invitationParams, final boolean accept) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getSession().sendJoinRoomInvitationResponse(MNUnity.serializer.deserialize(invitationParams,MNJoinRoomInvitationParams.class), accept);
            }
        });
    }

    public static void reqJoinRandomRoom(final String gameSetId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getSession().reqJoinRandomRoom(gameSetId);
            }
        });
    }

    public static void reqCreateBuddyRoom(final String buddyRoomParams) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getSession().reqCreateBuddyRoom(MNUnity.serializer.deserialize(buddyRoomParams,MNBuddyRoomParams.class));
            }
        });
    }

    public static void reqSetUserStatus(final int userStatus) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getSession().reqSetUserStatus(userStatus);
            }
        });
    }

    public static void leaveRoom() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getSession().leaveRoom();
            }
        });
    }

    public static void execUICommand(final String name, final String param) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getSession().execUICommand(name, param);
            }
        });
    }

    public static boolean isInGameRoom() {
        MNUnity.MARK();

        return MNDirect.getSession().isInGameRoom();
    }

    protected static class MNSessionEventHandler extends com.playphone.multinet.core.MNSessionEventHandlerAbstract {

        @Override
        public void mnSessionStatusChanged(final int newStatus, final int oldStatus) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionStatusChanged",newStatus, oldStatus);
                }
            });
        }

        @Override
        public void mnSessionUserChanged(final long userId) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionUserChanged",userId);
                }
            });
        }

        @Override
        public void mnSessionRoomUserJoin(final MNUserInfo userInfo) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionRoomUserJoin",userInfo);
                }
            });
        }

        @Override
        public void mnSessionRoomUserLeave(final MNUserInfo userInfo) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionRoomUserLeave",userInfo);
                }
            });
        }

        @Override
        public void mnSessionGameMessageReceived(final String message, final MNUserInfo sender) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionGameMessageReceived",message, sender);
                }
            });
        }

        @Override
        public void mnSessionErrorOccurred(final MNErrorInfo errorInfo) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionErrorOccurred",errorInfo);
                }
            });
        }

        @Override
        public void mnSessionExecAppCommandReceived(final String cmdName, final String cmdParam) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionExecAppCommandReceived",cmdName, cmdParam);
                }
            });
        }

        @Override
        public void mnSessionExecUICommandReceived(final String cmdName, final String cmdParam) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionExecUICommandReceived",cmdName, cmdParam);
                }
            });
        }

        @Override
        public void mnSessionJoinRoomInvitationReceived(final MNJoinRoomInvitationParams params) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnSessionJoinRoomInvitationReceived",params);
                }
            });
        }
    }

    private static MNSessionEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getSession() == null) {
            MNUnity.ELog("MNSession is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNSessionUnity.MNSessionEventHandler();
            MNDirect.getSession().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getSession() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getSession().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

