//
//  MNMyHiScoresProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNMyHiScoresProvider.h"

#import "MNMyHiScoresProviderUnity.h"

@interface MNMyHiScoresProviderUnity : NSObject <MNMyHiScoresProviderDelegate>
+ (id)shared;
- (void)hiScoreUpdated:(long long)newScore gameSetId:(int)gameSetId periodMask:(int)periodMask;
@end

static MNMyHiScoresProviderUnity *MNMyHiScoresProviderUnityInstance = nil;

@implementation MNMyHiScoresProviderUnity

+(id)shared {
    if (MNMyHiScoresProviderUnityInstance == nil) {
        MNMyHiScoresProviderUnityInstance = [[MNMyHiScoresProviderUnity alloc]init];
    }
    
    return MNMyHiScoresProviderUnityInstance;
}

- (void)hiScoreUpdated:(long long)newScore gameSetId:(int)gameSetId periodMask:(int)periodMask {
    [MNUnity callUnityFunction:@"MNUM_onNewHiScore" withParams:[NSNumber numberWithLongLong:newScore], [NSNumber numberWithInt:gameSetId], [NSNumber numberWithInt:periodMask], nil];
}
@end

EXTERN_C
const char* _MNMyHiScoresProvider_GetMyHiScore(int gameSetId) {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect myHiScoresProvider] getMyHiScore:gameSetId]] UTF8String]);
}

EXTERN_C
const char* _MNMyHiScoresProvider_GetMyHiScores() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serializeDicitionaryToArray:[[MNDirect myHiScoresProvider] getMyHiScores]] UTF8String]);
}

EXTERN_C
bool _MNMyHiScoresProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect myHiScoresProvider] == nil) {
        return false;
    }

    [[MNDirect myHiScoresProvider] addDelegate:[MNMyHiScoresProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNMyHiScoresProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect myHiScoresProvider] == nil) {
        return false;
    }

    [[MNDirect myHiScoresProvider] removeDelegate:[MNMyHiScoresProviderUnity shared]];

    return true;
}


