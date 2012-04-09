package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNDirectPopupUnity {
    public static void init(final int actionsBitMask) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectPopup.init(actionsBitMask);
            }
        });
    }

    public static boolean isActive() {
        MNUnity.MARK();

        return MNDirectPopup.isActive();
    }

    public static void setActive(final boolean activeFlag) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectPopup.setActive(activeFlag);
            }
        });
    }

}

