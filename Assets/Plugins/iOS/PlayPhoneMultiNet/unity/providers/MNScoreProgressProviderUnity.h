//
//  MNScoreProgressProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
void _MNScoreProgressProvider_SetRefreshIntervalAndUpdateDelay(int refreshInterval, int updateDelay);

EXTERN_C
void _MNScoreProgressProvider_Start();

EXTERN_C
void _MNScoreProgressProvider_Stop();

EXTERN_C
void _MNScoreProgressProvider_SetScoreComparator(int nativeComparatorId);

EXTERN_C
void _MNScoreProgressProvider_PostScore(long long score);

EXTERN_C
bool _MNScoreProgressProvider_RegisterEventHandler();

EXTERN_C
bool _MNScoreProgressProvider_UnregisterEventHandler();


