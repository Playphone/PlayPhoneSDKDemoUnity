package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;
import com.playphone.multinet.providers.*;

public class MNVShopProviderUnity {
    public static void shutdown() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVShopProvider().shutdown();
            }
        });
    }

    public static String getVShopPackList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getVShopProvider().getVShopPackList());
    }

    public static String getVShopCategoryList() {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getVShopProvider().getVShopCategoryList());
    }

    public static String findVShopPackById(final int packId) {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getVShopProvider().findVShopPackById(packId));
    }

    public static String findVShopCategoryById(final int categoryId) {
        MNUnity.MARK();

        return MNUnity.serializer.serialize(MNDirect.getVShopProvider().findVShopCategoryById(categoryId));
    }

    public static boolean isVShopInfoNeedUpdate() {
        MNUnity.MARK();

        return MNDirect.getVShopProvider().isVShopInfoNeedUpdate();
    }

    public static void doVShopInfoUpdate() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVShopProvider().doVShopInfoUpdate();
            }
        });
    }

    public static String getVShopPackImageURL(final int packId) {
        MNUnity.MARK();

        return MNDirect.getVShopProvider().getVShopPackImageURL(packId);
    }

    public static boolean isVShopReady() {
        MNUnity.MARK();

        return MNDirect.getVShopProvider().isVShopReady();
    }

    public static void execCheckoutVShopPacks(final String packIdArray, final String packCountArray, final long clientTransactionId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVShopProvider().execCheckoutVShopPacks(MNUnity.serializer.deserialize(packIdArray,int[].class), MNUnity.serializer.deserialize(packCountArray,int[].class), clientTransactionId);
            }
        });
    }

    public static void procCheckoutVShopPacksSilent(final String packIdArray, final String packCountArray, final long clientTransactionId) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirect.getVShopProvider().procCheckoutVShopPacksSilent(MNUnity.serializer.deserialize(packIdArray,int[].class), MNUnity.serializer.deserialize(packCountArray,int[].class), clientTransactionId);
            }
        });
    }

    protected static class MNVShopProviderEventHandler implements com.playphone.multinet.providers.MNVShopProvider.IEventHandler {

        @Override
        public void onVShopInfoUpdated() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onVShopInfoUpdated");
                }
            });
        }

        @Override
        public void showDashboard() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_showDashboard");
                }
            });
        }

        @Override
        public void hideDashboard() {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_hideDashboard");
                }
            });
        }

        @Override
        public void onCheckoutVShopPackSuccess(final MNVShopProvider.IEventHandler.CheckoutVShopPackSuccessInfo result) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onCheckoutVShopPackSuccess",result);
                }
            });
        }

        @Override
        public void onCheckoutVShopPackFail(final MNVShopProvider.IEventHandler.CheckoutVShopPackFailInfo result) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onCheckoutVShopPackFail",result);
                }
            });
        }

        @Override
        public void onVShopReadyStatusChanged(final boolean isVShopReady) {
            MNUnity.MARK();

            UnityPlayer.currentActivity.runOnUiThread(new Runnable()
            {
                public void run()
                {
                  MNUnity.callUnityFunction("MNUM_onVShopReadyStatusChanged",isVShopReady);
                }
            });
        }
    }

    private static MNVShopProviderEventHandler registeredEventHandler = null;
    public static synchronized boolean registerEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getVShopProvider() == null) {
            MNUnity.ELog("MNVShopProvider is not ready. Use MNDirect\'s sessionReady event for adding your delegates.");
            return false;
        }

        if (registeredEventHandler == null) {
            registeredEventHandler = new MNVShopProviderUnity.MNVShopProviderEventHandler();
            MNDirect.getVShopProvider().addEventHandler(registeredEventHandler);
        }

        return true;
    }
    
    public static synchronized boolean unregisterEventHandler() {
        MNUnity.MARK();

        if (MNDirect.getVShopProvider() == null) {
            return false;
        }

        if (registeredEventHandler != null) {
            MNDirect.getVShopProvider().removeEventHandler(registeredEventHandler);
            registeredEventHandler = null;
        }

        return true;
    }
}

