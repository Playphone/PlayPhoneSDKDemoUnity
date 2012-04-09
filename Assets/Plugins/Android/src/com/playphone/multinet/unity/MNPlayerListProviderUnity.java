package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNPlayerListProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getPlayerListProvider().shutdown();
            }
        });
    }

    public static String getPlayerList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getPlayerListProvider().getPlayerList());
    }

    protected static class MNPlayerListProviderEventHandler implements com.playphone.multinet.providers.MNPlayerListProvider.IEventHandler {

        @Override
        public void onPlayerJoin(final MNUserInfo player) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onPlayerJoin",player);
                }
            });
        }

        @Override
        public void onPlayerLeft(final MNUserInfo player) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onPlayerLeft",player);
                }
            });
        }
    }

    private static MNPlayerListProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getPlayerListProvider() == null) {
            MNUnity.ELog("MNPlayerListProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNPlayerListProviderUnity.MNPlayerListProviderEventHandler();
            MNDirect.getPlayerListProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getPlayerListProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getPlayerListProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

