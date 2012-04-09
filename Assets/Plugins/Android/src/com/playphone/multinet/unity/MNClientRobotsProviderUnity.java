package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNClientRobotsProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getClientRobotsProvider().shutdown();
            }
        });
    }

    public static boolean isRobot(final String userInfo) {
        MNUnity.MARK();

        return MNDirect.getClientRobotsProvider().isRobot(MNUnity.serializer.deserialize(userInfo,MNUserInfo.class));
    }

    public static void postRobotScore(final String userInfo, final long score) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getClientRobotsProvider().postRobotScore(MNUnity.serializer.deserialize(userInfo,MNUserInfo.class), score);
            }
        });
    }

    public static void setRoomRobotLimit(final int robotCount) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getClientRobotsProvider().setRoomRobotLimit(robotCount);
            }
        });
    }

    public static int getRoomRobotLimit() {
        MNUnity.MARK();

        return MNDirect.getClientRobotsProvider().getRoomRobotLimit();
    }

}

