//
//  MNAchievementsProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNAchievementsProvider.h"

#import "MNAchievementsProviderUnity.h"

@interface MNAchievementsProviderUnity : NSObject <MNAchievementsProviderDelegate>
+ (id)shared;
- (void)onGameAchievementListUpdated;
- (void)onPlayerAchievementUnlocked:(int)achievementId;
@end

static MNAchievementsProviderUnity *MNAchievementsProviderUnityInstance = nil;

@implementation MNAchievementsProviderUnity

+(id)shared {
    if (MNAchievementsProviderUnityInstance == nil) {
        MNAchievementsProviderUnityInstance = [[MNAchievementsProviderUnity alloc]init];
    }
    
    return MNAchievementsProviderUnityInstance;
}

- (void)onGameAchievementListUpdated {
    [MNUnity callUnityFunction:@"MNUM_onGameAchievementListUpdated" withParams:nil];
}
- (void)onPlayerAchievementUnlocked:(int)achievementId {
    [MNUnity callUnityFunction:@"MNUM_onPlayerAchievementUnlocked" withParams:[NSNumber numberWithInt:achievementId], nil];
}
@end

EXTERN_C
const char* _MNAchievementsProvider_GetGameAchievementsList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect achievementsProvider] getGameAchievementList]] UTF8String]);
}

EXTERN_C
const char* _MNAchievementsProvider_FindGameAchievementById(int achievementId) {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect achievementsProvider] findGameAchievementById:achievementId]] UTF8String]);
}

EXTERN_C
bool _MNAchievementsProvider_IsGameAchievementListNeedUpdate() {
    MARK;

    return [[MNDirect achievementsProvider] isGameAchievementListNeedUpdate];
}

EXTERN_C
void _MNAchievementsProvider_DoGameAchievementListUpdate() {
    MARK;

    [[MNDirect achievementsProvider] doGameAchievementListUpdate];
}

EXTERN_C
bool _MNAchievementsProvider_IsPlayerAchievementUnlocked(int achievementId) {
    MARK;

    return [[MNDirect achievementsProvider] isPlayerAchievementUnlocked:achievementId];
}

EXTERN_C
void _MNAchievementsProvider_UnlockPlayerAchievement(int achievementId) {
    MARK;

    [[MNDirect achievementsProvider] unlockPlayerAchievement:achievementId];
}

EXTERN_C
const char* _MNAchievementsProvider_GetPlayerAchievementsList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect achievementsProvider] getPlayerAchievementList]] UTF8String]);
}

EXTERN_C
const char* _MNAchievementsProvider_GetAchievementImageURL(int achievementId) {
    MARK;

    NSURL *resultUrl = [[MNDirect achievementsProvider] getAchievementImageURL:achievementId];

    if (resultUrl == nil) {
        return NULL;
    }

    return MakeStringCopy([[resultUrl absoluteString] UTF8String]);
}

EXTERN_C
bool _MNAchievementsProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect achievementsProvider] == nil) {
        return false;
    }

    [[MNDirect achievementsProvider] addDelegate:[MNAchievementsProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNAchievementsProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect achievementsProvider] == nil) {
        return false;
    }

    [[MNDirect achievementsProvider] removeDelegate:[MNAchievementsProviderUnity shared]];

    return true;
}


