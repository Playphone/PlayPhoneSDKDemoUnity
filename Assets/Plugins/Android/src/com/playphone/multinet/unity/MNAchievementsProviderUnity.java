package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNAchievementsProviderUnity {
    public static String getGameAchievementsList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getAchievementsProvider().getGameAchievementsList());
    }

    public static String findGameAchievementById(final int achievementId) {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getAchievementsProvider().findGameAchievementById(achievementId));
    }

    public static boolean isGameAchievementListNeedUpdate() {
        MNUnity.MARK();

        return MNDirect.getAchievementsProvider().isGameAchievementListNeedUpdate();
    }

    public static void doGameAchievementListUpdate() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getAchievementsProvider().doGameAchievementListUpdate();
            }
        });
    }

    public static boolean isPlayerAchievementUnlocked(final int achievementId) {
        MNUnity.MARK();

        return MNDirect.getAchievementsProvider().isPlayerAchievementUnlocked(achievementId);
    }

    public static void unlockPlayerAchievement(final int achievementId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getAchievementsProvider().unlockPlayerAchievement(achievementId);
            }
        });
    }

    public static String getPlayerAchievementsList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getAchievementsProvider().getPlayerAchievementsList());
    }

    public static String getAchievementImageURL(final int achievementId) {
        MNUnity.MARK();

        return MNDirect.getAchievementsProvider().getAchievementImageURL(achievementId);
    }

    protected static class MNAchievementsProviderEventHandler implements com.playphone.multinet.providers.MNAchievementsProvider.IEventHandler {

        @Override
        public void onGameAchievementListUpdated() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onGameAchievementListUpdated");
                }
            });
        }

        @Override
        public void onPlayerAchievementUnlocked(final int achievementId) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onPlayerAchievementUnlocked",achievementId);
                }
            });
        }
    }

    private static MNAchievementsProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getAchievementsProvider() == null) {
            MNUnity.ELog("MNAchievementsProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNAchievementsProviderUnity.MNAchievementsProviderEventHandler();
            MNDirect.getAchievementsProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getAchievementsProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getAchievementsProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

