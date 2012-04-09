package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNGameCookiesProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameCookiesProvider().shutdown();
            }
        });
    }

    public static void downloadUserCookie(final int key) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameCookiesProvider().downloadUserCookie(key);
            }
        });
    }

    public static void uploadUserCookie(final int key, final String cookie) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameCookiesProvider().uploadUserCookie(key, cookie);
            }
        });
    }

    protected static class MNGameCookiesProviderEventHandler implements com.playphone.multinet.providers.MNGameCookiesProvider.IEventHandler {

        @Override
        public void onGameCookieDownloadSucceeded(final int key, final String cookie) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameCookieDownloadSucceeded",key, cookie);
                }
            });
        }

        @Override
        public void onGameCookieDownloadFailedWithError(final int key, final String error) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameCookieDownloadFailedWithError",key, error);
                }
            });
        }

        @Override
        public void onGameCookieUploadSucceeded(final int key) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameCookieUploadSucceeded",key);
                }
            });
        }

        @Override
        public void onGameCookieUploadFailedWithError(final int key, final String error) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameCookieUploadFailedWithError",key, error);
                }
            });
        }
    }

    private static MNGameCookiesProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getGameCookiesProvider() == null) {
            MNUnity.ELog("MNGameCookiesProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNGameCookiesProviderUnity.MNGameCookiesProviderEventHandler();
            MNDirect.getGameCookiesProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getGameCookiesProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getGameCookiesProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

