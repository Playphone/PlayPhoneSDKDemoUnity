package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNGameSettingsProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameSettingsProvider().shutdown();
            }
        });
    }

    public static String getGameSettingList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getGameSettingsProvider().getGameSettingList());
    }

    public static String findGameSettingById(final int gameSetId) {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getGameSettingsProvider().findGameSettingById(gameSetId));
    }

    public static boolean isGameSettingListNeedUpdate() {
        MNUnity.MARK();

        return MNDirect.getGameSettingsProvider().isGameSettingListNeedUpdate();
    }

    public static void doGameSettingListUpdate() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getGameSettingsProvider().doGameSettingListUpdate();
            }
        });
    }

    protected static class MNGameSettingsProviderEventHandler implements com.playphone.multinet.providers.MNGameSettingsProvider.IEventHandler {

        @Override
        public void onGameSettingListUpdated() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameSettingListUpdated");
                }
            });
        }
    }

    private static MNGameSettingsProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getGameSettingsProvider() == null) {
            MNUnity.ELog("MNGameSettingsProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNGameSettingsProviderUnity.MNGameSettingsProviderEventHandler();
            MNDirect.getGameSettingsProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getGameSettingsProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getGameSettingsProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

