//
//  MNGameCookiesProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
void _MNGameCookiesProvider_DownloadUserCookie(int key);

EXTERN_C
void _MNGameCookiesProvider_UploadUserCookie(int key, const char* cookie);

EXTERN_C
bool _MNGameCookiesProvider_RegisterEventHandler();

EXTERN_C
bool _MNGameCookiesProvider_UnregisterEventHandler();


