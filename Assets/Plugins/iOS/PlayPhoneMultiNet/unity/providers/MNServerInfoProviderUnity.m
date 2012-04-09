//
//  MNServerInfoProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNServerInfoProvider.h"

#import "MNServerInfoProviderUnity.h"

@interface MNServerInfoProviderUnity : NSObject <MNServerInfoProviderDelegate>
+ (id)shared;
- (void)serverInfoWithKey:(int)key received:(NSString*)value;
- (void)serverInfoWithKey:(int)key requestFailedWithError:(NSString*)error;
@end

static MNServerInfoProviderUnity *MNServerInfoProviderUnityInstance = nil;

@implementation MNServerInfoProviderUnity

+(id)shared {
    if (MNServerInfoProviderUnityInstance == nil) {
        MNServerInfoProviderUnityInstance = [[MNServerInfoProviderUnity alloc]init];
    }
    
    return MNServerInfoProviderUnityInstance;
}

- (void)serverInfoWithKey:(int)key received:(NSString*)value {
    [MNUnity callUnityFunction:@"MNUM_onServerInfoItemReceived" withParams:[NSNumber numberWithInt:key], (value == nil ? [NSNull null] : value), nil];
}
- (void)serverInfoWithKey:(int)key requestFailedWithError:(NSString*)error {
    [MNUnity callUnityFunction:@"MNUM_onServerInfoItemRequestFailedWithError" withParams:[NSNumber numberWithInt:key], (error == nil ? [NSNull null] : error), nil];
}
@end

EXTERN_C
void _MNServerInfoProvider_RequestServerInfoItem(int key) {
    MARK;

    [[MNDirect serverInfoProvider] requestServerInfoItem:key];
}

EXTERN_C
bool _MNServerInfoProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect serverInfoProvider] == nil) {
        return false;
    }

    [[MNDirect serverInfoProvider] addDelegate:[MNServerInfoProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNServerInfoProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect serverInfoProvider] == nil) {
        return false;
    }

    [[MNDirect serverInfoProvider] removeDelegate:[MNServerInfoProviderUnity shared]];

    return true;
}


