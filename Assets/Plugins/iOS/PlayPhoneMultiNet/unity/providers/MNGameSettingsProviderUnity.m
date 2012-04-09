//
//  MNGameSettingsProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNGameSettingsProvider.h"

#import "MNGameSettingsProviderUnity.h"

@interface MNGameSettingsProviderUnity : NSObject <MNGameSettingsProviderDelegate>
+ (id)shared;
- (void)onGameSettingListUpdated;
@end

static MNGameSettingsProviderUnity *MNGameSettingsProviderUnityInstance = nil;

@implementation MNGameSettingsProviderUnity

+(id)shared {
    if (MNGameSettingsProviderUnityInstance == nil) {
        MNGameSettingsProviderUnityInstance = [[MNGameSettingsProviderUnity alloc]init];
    }
    
    return MNGameSettingsProviderUnityInstance;
}

- (void)onGameSettingListUpdated {
    [MNUnity callUnityFunction:@"MNUM_onGameSettingListUpdated" withParams:nil];
}
@end

EXTERN_C
const char* _MNGameSettingsProvider_GetGameSettingList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect gameSettingsProvider] getGameSettingList]] UTF8String]);
}

EXTERN_C
const char* _MNGameSettingsProvider_FindGameSettingById(int gameSetId) {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect gameSettingsProvider] findGameSettingById:gameSetId]] UTF8String]);
}

EXTERN_C
bool _MNGameSettingsProvider_IsGameSettingListNeedUpdate() {
    MARK;

    return [[MNDirect gameSettingsProvider] isGameSettingListNeedUpdate];
}

EXTERN_C
void _MNGameSettingsProvider_DoGameSettingListUpdate() {
    MARK;

    [[MNDirect gameSettingsProvider] doGameSettingListUpdate];
}

EXTERN_C
bool _MNGameSettingsProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect gameSettingsProvider] == nil) {
        return false;
    }

    [[MNDirect gameSettingsProvider] addDelegate:[MNGameSettingsProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNGameSettingsProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect gameSettingsProvider] == nil) {
        return false;
    }

    [[MNDirect gameSettingsProvider] removeDelegate:[MNGameSettingsProviderUnity shared]];

    return true;
}


