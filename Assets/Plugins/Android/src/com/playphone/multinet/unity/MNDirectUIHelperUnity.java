package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNDirectUIHelperUnity {
    public static void setDashboardStyle(final int newStyle) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectUIHelper.setDashboardStyle(newStyle);
            }
        });
    }

    public static int getDashboardStyle() {
        MNUnity.MARK();

        return MNDirectUIHelper.getDashboardStyle();
    }

    public static void showDashboard() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectUIHelper.showDashboard();
            }
        });
    }

    public static void hideDashboard() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectUIHelper.hideDashboard();
            }
        });
    }

    public static boolean isDashboardHidden() {
        MNUnity.MARK();

        return MNDirectUIHelper.isDashboardHidden();
    }

    public static boolean isDashboardVisible() {
        MNUnity.MARK();

        return MNDirectUIHelper.isDashboardVisible();
    }

    public static void setHostActivity(final boolean setFlag) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                if (setFlag) {
                    MNDirectUIHelper.setHostActivity(UnityPlayer.currentActivity);
                }
                else {
                    MNDirectUIHelper.setHostActivity(null);
                }
            }
        });
    }

    protected static class MNDirectUIHelperEventHandler extends com.playphone.multinet.MNDirectUIHelper.EventHandlerAbstract {

        @Override
        public void onShowDashboard() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onShowDashboard");
                }
            });
        }

        @Override
        public void onHideDashboard() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onHideDashboard");
                }
            });
        }
    }

    private static MNDirectUIHelperEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNDirectUIHelperUnity.MNDirectUIHelperEventHandler();
            MNDirectUIHelper.addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (registeredEventHandler != null) {
            MNDirectUIHelper.removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

