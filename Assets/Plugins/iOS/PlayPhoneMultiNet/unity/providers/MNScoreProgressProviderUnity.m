//
//  MNScoreProgressProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNScoreProgressProvider.h"

#import "MNScoreProgressProviderUnity.h"

#define MNScoreProgressProviderUnityComparatorMoreIsBetterId  (0)
#define MNScoreProgressProviderUnityComparatorLessIsBetterId  (1)


@interface MNScoreProgressProviderUnity : NSObject <MNScoreProgressProviderDelegate>
+ (id)shared;
- (void)scoresUpdated:(NSArray*)scoreBoard;
@end

static MNScoreProgressProviderUnity *MNScoreProgressProviderUnityInstance = nil;

@implementation MNScoreProgressProviderUnity

+(id)shared {
    if (MNScoreProgressProviderUnityInstance == nil) {
        MNScoreProgressProviderUnityInstance = [[MNScoreProgressProviderUnity alloc]init];
    }
    
    return MNScoreProgressProviderUnityInstance;
}

- (void)scoresUpdated:(NSArray*)scoreBoard {
    [MNUnity callUnityFunction:@"MNUM_onScoresUpdated" withParams:(scoreBoard == nil ? [NSNull null] : scoreBoard), nil];
}
@end

EXTERN_C
void _MNScoreProgressProvider_SetRefreshIntervalAndUpdateDelay(int refreshInterval, int updateDelay) {
    MARK;

    [[MNDirect scoreProgressProvider] setRefreshInterval:refreshInterval andUpdateDelay:updateDelay];
}

EXTERN_C
void _MNScoreProgressProvider_Start() {
    MARK;

    [[MNDirect scoreProgressProvider] start];
}

EXTERN_C
void _MNScoreProgressProvider_Stop() {
    MARK;

    [[MNDirect scoreProgressProvider] stop];
}

EXTERN_C
void _MNScoreProgressProvider_SetScoreComparator(int nativeComparatorId) {
    if (nativeComparatorId == MNScoreProgressProviderUnityComparatorMoreIsBetterId) {
        [[MNDirect scoreProgressProvider]setScoreCompareFunc:MNScoreProgressProviderScoreCompareFuncMoreIsBetter withContext:NULL];
    }
    else if (nativeComparatorId == MNScoreProgressProviderUnityComparatorLessIsBetterId) {
        [[MNDirect scoreProgressProvider]setScoreCompareFunc:MNScoreProgressProviderScoreCompareFuncLessIsBetter withContext:NULL];
    }
    else {
        ELog(@"Invalid score progress nativeComparatorId received: %d", nativeComparatorId);
        return;
    }
}

EXTERN_C
void _MNScoreProgressProvider_PostScore(long long score) {
    MARK;

    [[MNDirect scoreProgressProvider] postScore:score];
}

EXTERN_C
bool _MNScoreProgressProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect scoreProgressProvider] == nil) {
        return false;
    }

    [[MNDirect scoreProgressProvider] addDelegate:[MNScoreProgressProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNScoreProgressProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect scoreProgressProvider] == nil) {
        return false;
    }

    [[MNDirect scoreProgressProvider] removeDelegate:[MNScoreProgressProviderUnity shared]];

    return true;
}


