package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;
import com.playphone.multinet.providers.*;

public class MNVItemsProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVItemsProvider().shutdown();
            }
        });
    }

    public static String getGameVItemsList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getVItemsProvider().getGameVItemsList());
    }

    public static String findGameVItemById(final int vItemId) {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getVItemsProvider().findGameVItemById(vItemId));
    }

    public static boolean isGameVItemsListNeedUpdate() {
        MNUnity.MARK();

        return MNDirect.getVItemsProvider().isGameVItemsListNeedUpdate();
    }

    public static void doGameVItemsListUpdate() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVItemsProvider().doGameVItemsListUpdate();
            }
        });
    }

    public static void reqAddPlayerVItem(final int vItemId, final long count, final long clientTransactionId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVItemsProvider().reqAddPlayerVItem(vItemId, count, clientTransactionId);
            }
        });
    }

    public static void reqAddPlayerVItemTransaction(final String transactionVItems, final long clientTransactionId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVItemsProvider().reqAddPlayerVItemTransaction(MNUnity.serializer.deserialize(transactionVItems,MNVItemsProvider.TransactionVItemInfo[].class), clientTransactionId);
            }
        });
    }

    public static void reqTransferPlayerVItem(final int vItemId, final long count, final long toPlayerId, final long clientTransactionId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVItemsProvider().reqTransferPlayerVItem(vItemId, count, toPlayerId, clientTransactionId);
            }
        });
    }

    public static void reqTransferPlayerVItemTransaction(final String transactionVItems, final long toPlayerId, final long clientTransactionId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVItemsProvider().reqTransferPlayerVItemTransaction(MNUnity.serializer.deserialize(transactionVItems,MNVItemsProvider.TransactionVItemInfo[].class), toPlayerId, clientTransactionId);
            }
        });
    }

    public static String getPlayerVItemList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getVItemsProvider().getPlayerVItemList());
    }

    public static long getPlayerVItemCountById(final int vItemId) {
        MNUnity.MARK();

        return MNDirect.getVItemsProvider().getPlayerVItemCountById(vItemId);
    }

    public static String getVItemImageURL(final int vItemId) {
        MNUnity.MARK();

        return MNDirect.getVItemsProvider().getVItemImageURL(vItemId);
    }

    public static long getNewClientTransactionId() {
        MNUnity.MARK();

        return MNDirect.getVItemsProvider().getNewClientTransactionId();
    }

    protected static class MNVItemsProviderEventHandler implements com.playphone.multinet.providers.MNVItemsProvider.IEventHandler {

        @Override
        public void onVItemsListUpdated() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onVItemsListUpdated");
                }
            });
        }

        @Override
        public void onVItemsTransactionCompleted(final MNVItemsProvider.TransactionInfo transaction) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onVItemsTransactionCompleted",transaction);
                }
            });
        }

        @Override
        public void onVItemsTransactionFailed(final MNVItemsProvider.TransactionError error) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onVItemsTransactionFailed",error);
                }
            });
        }
    }

    private static MNVItemsProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getVItemsProvider() == null) {
            MNUnity.ELog("MNVItemsProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNVItemsProviderUnity.MNVItemsProviderEventHandler();
            MNDirect.getVItemsProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getVItemsProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getVItemsProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

