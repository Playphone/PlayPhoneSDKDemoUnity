package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNMyHiScoresProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getMyHiScoresProvider().shutdown();
            }
        });
    }

    public static String getMyHiScore(final int gameSetId) {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getMyHiScoresProvider().getMyHiScore(gameSetId));
    }

    public static String getMyHiScores() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getMyHiScoresProvider().getMyHiScores());
    }

    protected static class MNMyHiScoresProviderEventHandler implements com.playphone.multinet.providers.MNMyHiScoresProvider.IEventHandler {

        @Override
        public void onNewHiScore(final long newScore, final int gameSetId, final int periodMask) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onNewHiScore",newScore, gameSetId, periodMask);
                }
            });
        }
    }

    private static MNMyHiScoresProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getMyHiScoresProvider() == null) {
            MNUnity.ELog("MNMyHiScoresProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNMyHiScoresProviderUnity.MNMyHiScoresProviderEventHandler();
            MNDirect.getMyHiScoresProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getMyHiScoresProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getMyHiScoresProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

