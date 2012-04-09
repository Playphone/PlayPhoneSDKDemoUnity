//
//  MNGameRoomCookiesProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
void _MNGameRoomCookiesProvider_DownloadGameRoomCookie(int roomSFId, int key);

EXTERN_C
void _MNGameRoomCookiesProvider_SetCurrentGameRoomCookie(int key, const char* cookie);

EXTERN_C
const char* _MNGameRoomCookiesProvider_GetCurrentGameRoomCookie(int key);

EXTERN_C
bool _MNGameRoomCookiesProvider_RegisterEventHandler();

EXTERN_C
bool _MNGameRoomCookiesProvider_UnregisterEventHandler();


