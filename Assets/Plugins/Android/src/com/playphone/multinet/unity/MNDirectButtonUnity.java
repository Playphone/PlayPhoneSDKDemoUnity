package com.playphone.multinet.unity;

import com.playphone.multinet.*;
import com.unity3d.player.UnityPlayer;

public class MNDirectButtonUnity {
    public static boolean isVisible() {
        MNUnity.MARK();

        return MNDirectButton.isVisible();
    }

    public static boolean isHidden() {
        MNUnity.MARK();

        return MNDirectButton.isHidden();
    }

    public static void initWithLocation(final int param1_int) {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectButton.initWithLocation(param1_int);
            }
        });
    }

    public static void show() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectButton.show();
            }
        });
    }

    public static void hide() {
        MNUnity.MARK();

        UnityPlayer.currentActivity.runOnUiThread(new Runnable()
        {
            public void run()
            {
                MNDirectButton.hide();
            }
        });
    }

}

