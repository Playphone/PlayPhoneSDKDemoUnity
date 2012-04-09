//
//  MNGameSettingsProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
const char* _MNGameSettingsProvider_GetGameSettingList();

EXTERN_C
const char* _MNGameSettingsProvider_FindGameSettingById(int gameSetId);

EXTERN_C
bool _MNGameSettingsProvider_IsGameSettingListNeedUpdate();

EXTERN_C
void _MNGameSettingsProvider_DoGameSettingListUpdate();

EXTERN_C
bool _MNGameSettingsProvider_RegisterEventHandler();

EXTERN_C
bool _MNGameSettingsProvider_UnregisterEventHandler();


