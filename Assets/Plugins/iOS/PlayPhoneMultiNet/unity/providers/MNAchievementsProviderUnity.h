//
//  MNAchievementsProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
const char* _MNAchievementsProvider_GetGameAchievementsList();

EXTERN_C
const char* _MNAchievementsProvider_FindGameAchievementById(int achievementId);

EXTERN_C
bool _MNAchievementsProvider_IsGameAchievementListNeedUpdate();

EXTERN_C
void _MNAchievementsProvider_DoGameAchievementListUpdate();

EXTERN_C
bool _MNAchievementsProvider_IsPlayerAchievementUnlocked(int achievementId);

EXTERN_C
void _MNAchievementsProvider_UnlockPlayerAchievement(int achievementId);

EXTERN_C
const char* _MNAchievementsProvider_GetPlayerAchievementsList();

EXTERN_C
const char* _MNAchievementsProvider_GetAchievementImageURL(int achievementId);

EXTERN_C
bool _MNAchievementsProvider_RegisterEventHandler();

EXTERN_C
bool _MNAchievementsProvider_UnregisterEventHandler();


