package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNGameRoomCookiesProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameRoomCookiesProvider().shutdown();
            }
        });
    }

    public static void downloadGameRoomCookie(final int roomSFId, final int key) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameRoomCookiesProvider().downloadGameRoomCookie(roomSFId, key);
            }
        });
    }

    public static void setCurrentGameRoomCookie(final int key, final String cookie) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameRoomCookiesProvider().setCurrentGameRoomCookie(key, cookie);
            }
        });
    }

    public static String getCurrentGameRoomCookie(final int key) {
        MNUnity.MARK();

        return MNDirect.getGameRoomCookiesProvider().getCurrentGameRoomCookie(key);
    }

    protected static class MNGameRoomCookiesProviderEventHandler implements com.playphone.multinet.providers.MNGameRoomCookiesProvider.IEventHandler {

        @Override
        public void onGameRoomCookieDownloadSucceeded(final int roomSFId, final int key, final String cookie) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameRoomCookieDownloadSucceeded",roomSFId, key, cookie);
                }
            });
        }

        @Override
        public void onGameRoomCookieDownloadFailedWithError(final int roomSFId, final int key, final String error) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameRoomCookieDownloadFailedWithError",roomSFId, key, error);
                }
            });
        }

        @Override
        public void onCurrentGameRoomCookieUpdated(final int key, final String newCookieValue) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onCurrentGameRoomCookieUpdated",key, newCookieValue);
                }
            });
        }
    }

    private static MNGameRoomCookiesProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getGameRoomCookiesProvider() == null) {
            MNUnity.ELog("MNGameRoomCookiesProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNGameRoomCookiesProviderUnity.MNGameRoomCookiesProviderEventHandler();
            MNDirect.getGameRoomCookiesProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getGameRoomCookiesProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getGameRoomCookiesProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

