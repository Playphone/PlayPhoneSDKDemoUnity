package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;
import com.playphone.multinet.core.MNSession;

public class MNDirectUnity {
    public static void init(final int param1_int, final String param2_String) {
        MNUnity.MARK();

        MNUnity.DLog("UnityPlayer.currentActivity class: " + UnityPlayer.currentActivity.getClass().getName());
        
        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.init(param1_int, param2_String,new MNDirectUnity.MNDirectEventHandler(),UnityPlayer.currentActivity);
                MNDirect.handleApplicationIntent(UnityPlayer.currentActivity.getIntent());
            }
        });
    }

    public static void shutdownSession() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.shutdownSession();
            }
        });
    }

    public static boolean isOnline() {
        MNUnity.MARK();

        return MNDirect.isOnline();
    }

    public static boolean isUserLoggedIn() {
        MNUnity.MARK();

        return MNDirect.isUserLoggedIn();
    }

    public static int getSessionStatus() {
        MNUnity.MARK();

        return MNDirect.getSessionStatus();
    }

    public static void postGameScore(final long score) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.postGameScore(score);
            }
        });
    }

    public static void postGameScorePending(final long score) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.postGameScorePending(score);
            }
        });
    }

    public static void cancelGame() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.cancelGame();
            }
        });
    }

    public static void setDefaultGameSetId(final int gameSetId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.setDefaultGameSetId(gameSetId);
            }
        });
    }

    public static int getDefaultGameSetId() {
        MNUnity.MARK();

        return MNDirect.getDefaultGameSetId();
    }

    public static void sendAppBeacon(final String actionName, final String beaconData) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.sendAppBeacon(actionName, beaconData);
            }
        });
    }

    public static void execAppCommand(final String name, final String param) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.execAppCommand(name, param);
            }
        });
    }

    public static void sendGameMessage(final String message) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.sendGameMessage(message);
            }
        });
    }

    protected static class MNDirectEventHandler extends com.playphone.multinet.MNDirectEventHandlerAbstract {

        @Override
        public void mnDirectDoStartGameWithParams(final MNGameParams params) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectDoStartGameWithParams",params);
                }
            });
        }

        @Override
        public void mnDirectDoFinishGame() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectDoFinishGame");
                }
            });
        }

        @Override
        public void mnDirectDoCancelGame() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectDoCancelGame");
                }
            });
        }

        @Override
        public void mnDirectViewDoGoBack() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectViewDoGoBack");
                }
            });
        }

        @Override
        public void mnDirectDidReceiveGameMessage(final String message, final MNUserInfo sender) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectDidReceiveGameMessage",message, sender);
                }
            });
        }

        @Override
        public void mnDirectSessionStatusChanged(final int newStatus) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectSessionStatusChanged",newStatus);
                }
            });
        }

        @Override
        public void mnDirectErrorOccurred(final MNErrorInfo error) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectErrorOccurred",error);
                }
            });
        }

        @Override
        public void mnDirectSessionReady(MNSession session) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_mnDirectSessionReady");
                }
            });
        }
    }
}

