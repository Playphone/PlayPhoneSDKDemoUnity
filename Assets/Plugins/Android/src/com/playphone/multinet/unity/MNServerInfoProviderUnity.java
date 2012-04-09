package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNServerInfoProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getServerInfoProvider().shutdown();
            }
        });
    }

    public static void requestServerInfoItem(final int key) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getServerInfoProvider().requestServerInfoItem(key);
            }
        });
    }

    protected static class MNServerInfoProviderEventHandler implements com.playphone.multinet.providers.MNServerInfoProvider.IEventHandler {

        @Override
        public void onServerInfoItemReceived(final int key, final String value) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onServerInfoItemReceived",key, value);
                }
            });
        }

        @Override
        public void onServerInfoItemRequestFailedWithError(final int key, final String error) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onServerInfoItemRequestFailedWithError",key, error);
                }
            });
        }
    }

    private static MNServerInfoProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getServerInfoProvider() == null) {
            MNUnity.ELog("MNServerInfoProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNServerInfoProviderUnity.MNServerInfoProviderEventHandler();
            MNDirect.getServerInfoProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getServerInfoProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getServerInfoProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

