//
//  MNServerInfoProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
void _MNServerInfoProvider_RequestServerInfoItem(int key);

EXTERN_C
bool _MNServerInfoProvider_RegisterEventHandler();

EXTERN_C
bool _MNServerInfoProvider_UnregisterEventHandler();


