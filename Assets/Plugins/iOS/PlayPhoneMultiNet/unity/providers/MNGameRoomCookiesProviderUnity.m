//
//  MNGameRoomCookiesProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNGameRoomCookiesProvider.h"

#import "MNGameRoomCookiesProviderUnity.h"

@interface MNGameRoomCookiesProviderUnity : NSObject <MNGameRoomCookiesProviderDelegate>
+ (id)shared;
- (void)gameRoomCookieForRoom:(int)roomSFId withKey:(int)key downloadSucceeded:(NSString*)cookie;
- (void)gameRoomCookieForRoom:(int)roomSFId withKey:(int)key downloadFailedWithError:(NSString*)error;
//- (void)onCurrentGameRoomCookieUpdated:(int)key ;
@end

static MNGameRoomCookiesProviderUnity *MNGameRoomCookiesProviderUnityInstance = nil;

@implementation MNGameRoomCookiesProviderUnity

+(id)shared {
    if (MNGameRoomCookiesProviderUnityInstance == nil) {
        MNGameRoomCookiesProviderUnityInstance = [[MNGameRoomCookiesProviderUnity alloc]init];
    }
    
    return MNGameRoomCookiesProviderUnityInstance;
}

- (void)gameRoomCookieForRoom:(int)roomSFId withKey:(int)key downloadSucceeded:(NSString*)cookie {
    [MNUnity callUnityFunction:@"MNUM_onGameRoomCookieDownloadSucceeded" withParams:[NSNumber numberWithInt:roomSFId], [NSNumber numberWithInt:key], (cookie == nil ? [NSNull null] : cookie), nil];
}
- (void)gameRoomCookieForRoom:(int)roomSFId withKey:(int)key downloadFailedWithError:(NSString*)error {
    [MNUnity callUnityFunction:@"MNUM_onGameRoomCookieDownloadFailedWithError" withParams:[NSNumber numberWithInt:roomSFId], [NSNumber numberWithInt:key], (error == nil ? [NSNull null] : error), nil];
}
/*
- (void)onCurrentGameRoomCookieUpdated:(int)key  {
    [MNUnity callUnityFunction:@"MNUM_onCurrentGameRoomCookieUpdated" withParams:[NSNumber numberWithInt:key], (newCookieValue == nil ? [NSNull null] : newCookieValue), nil];
}
*/
@end

EXTERN_C
void _MNGameRoomCookiesProvider_DownloadGameRoomCookie(int roomSFId, int key) {
    MARK;

    [[MNDirect gameRoomCookiesProvider] downloadGameRoomCookieForRoom:roomSFId withKey:key];
}

EXTERN_C
void _MNGameRoomCookiesProvider_SetCurrentGameRoomCookie(int key, const char* cookie) {
    MARK;

    [[MNDirect gameRoomCookiesProvider] setCurrentGameRoomCookieWithKey:key andCookie:NSStringWithUTFStringSafe(cookie)];
}

EXTERN_C
const char* _MNGameRoomCookiesProvider_GetCurrentGameRoomCookie(int key) {
    MARK;

    return MakeStringCopy([[[MNDirect gameRoomCookiesProvider] currentGameRoomCookieWithKey:key] UTF8String]);
}

EXTERN_C
bool _MNGameRoomCookiesProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect gameRoomCookiesProvider] == nil) {
        return false;
    }

    [[MNDirect gameRoomCookiesProvider] addDelegate:[MNGameRoomCookiesProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNGameRoomCookiesProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect gameRoomCookiesProvider] == nil) {
        return false;
    }

    [[MNDirect gameRoomCookiesProvider] removeDelegate:[MNGameRoomCookiesProviderUnity shared]];

    return true;
}


