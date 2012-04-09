package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;
import com.playphone.multinet.providers.MNScoreProgressProvider;

public class MNScoreProgressProviderUnity {
    public static void setRefreshIntervalAndUpdateDelay(final int refreshInterval, final int updateDelay) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getScoreProgressProvider().setRefreshIntervalAndUpdateDelay(refreshInterval, updateDelay);
            }
        });
    }

    public static void start() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getScoreProgressProvider().start();
            }
        });
    }

    public static void stop() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getScoreProgressProvider().stop();
            }
        });
    }

    private static final int scoreComparatorMoreIsBetterId = 0;
    private static final int scoreComparatorLessIsBetterId = 1;
    
    public static void setScoreComparator(final int nativeComparatorId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                if (nativeComparatorId == scoreComparatorMoreIsBetterId) {
                    MNDirect.getScoreProgressProvider().setScoreComparator(new MNScoreProgressProvider.ScoreComparatorMoreIsBetter());
                }
                else if (nativeComparatorId == scoreComparatorLessIsBetterId) {
                    MNDirect.getScoreProgressProvider().setScoreComparator(new MNScoreProgressProvider.ScoreComparatorLessIsBetter());
                }
                else {
                    return;
                }
            }
        });
    }

    public static void postScore(final long score) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getScoreProgressProvider().postScore(score);
            }
        });
    }

    protected static class MNScoreProgressProviderEventHandler implements com.playphone.multinet.providers.MNScoreProgressProvider.IEventHandler {

        @Override
        public void onScoresUpdated(final MNScoreProgressProvider.ScoreItem[] scoreBoard) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onScoresUpdated",(Object)scoreBoard);
                }
            });
        }
    }

    private static MNScoreProgressProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getScoreProgressProvider() == null) {
            MNUnity.ELog("MNScoreProgressProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNScoreProgressProviderUnity.MNScoreProgressProviderEventHandler();
            MNDirect.getScoreProgressProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getScoreProgressProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getScoreProgressProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

