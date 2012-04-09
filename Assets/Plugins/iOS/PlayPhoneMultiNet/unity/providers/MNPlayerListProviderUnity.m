//
//  MNPlayerListProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNPlayerListProvider.h"

#import "MNPlayerListProviderUnity.h"

@interface MNPlayerListProviderUnity : NSObject <MNPlayerListProviderDelegate>
+ (id)shared;
- (void)onPlayerJoin:(MNUserInfo*)player;
- (void)onPlayerLeft:(MNUserInfo*)player;
@end

static MNPlayerListProviderUnity *MNPlayerListProviderUnityInstance = nil;

@implementation MNPlayerListProviderUnity

+(id)shared {
    if (MNPlayerListProviderUnityInstance == nil) {
        MNPlayerListProviderUnityInstance = [[MNPlayerListProviderUnity alloc]init];
    }
    
    return MNPlayerListProviderUnityInstance;
}

- (void)onPlayerJoin:(MNUserInfo*)player {
    [MNUnity callUnityFunction:@"MNUM_onPlayerJoin" withParams:(player == nil ? [NSNull null] : player), nil];
}
- (void)onPlayerLeft:(MNUserInfo*)player {
    [MNUnity callUnityFunction:@"MNUM_onPlayerLeft" withParams:(player == nil ? [NSNull null] : player), nil];
}
@end

EXTERN_C
const char* _MNPlayerListProvider_GetPlayerList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect playerListProvider] getPlayerList]] UTF8String]);
}

EXTERN_C
bool _MNPlayerListProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect playerListProvider] == nil) {
        return false;
    }

    [[MNDirect playerListProvider] addDelegate:[MNPlayerListProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNPlayerListProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect playerListProvider] == nil) {
        return false;
    }

    [[MNDirect playerListProvider] removeDelegate:[MNPlayerListProviderUnity shared]];

    return true;
}


