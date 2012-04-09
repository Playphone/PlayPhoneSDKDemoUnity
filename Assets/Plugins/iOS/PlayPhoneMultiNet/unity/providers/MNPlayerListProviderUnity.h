//
//  MNPlayerListProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
const char* _MNPlayerListProvider_GetPlayerList();

EXTERN_C
bool _MNPlayerListProvider_RegisterEventHandler();

EXTERN_C
bool _MNPlayerListProvider_UnregisterEventHandler();


