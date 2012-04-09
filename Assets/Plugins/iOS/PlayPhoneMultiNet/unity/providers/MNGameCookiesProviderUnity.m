//
//  MNGameCookiesProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNGameCookiesProvider.h"

#import "MNGameCookiesProviderUnity.h"

@interface MNGameCookiesProviderUnity : NSObject <MNGameCookiesProviderDelegate>
+ (id)shared;
- (void)gameCookie:(int)key downloadSucceeded:(NSString*)cookie;
- (void)gameCookie:(int)key downloadFailedWithError:(NSString*)error;
- (void)gameCookieUploadSucceeded:(int)key;
- (void)gameCookie:(int)key uploadFailedWithError:(NSString*)error;
@end

static MNGameCookiesProviderUnity *MNGameCookiesProviderUnityInstance = nil;

@implementation MNGameCookiesProviderUnity

+(id)shared {
    if (MNGameCookiesProviderUnityInstance == nil) {
        MNGameCookiesProviderUnityInstance = [[MNGameCookiesProviderUnity alloc]init];
    }
    
    return MNGameCookiesProviderUnityInstance;
}

- (void)gameCookie:(int)key downloadSucceeded:(NSString*)cookie {
    [MNUnity callUnityFunction:@"MNUM_onGameCookieDownloadSucceeded" withParams:[NSNumber numberWithInt:key], (cookie == nil ? [NSNull null] : cookie), nil];
}
- (void)gameCookie:(int)key downloadFailedWithError:(NSString*)error {
    [MNUnity callUnityFunction:@"MNUM_onGameCookieDownloadFailedWithError" withParams:[NSNumber numberWithInt:key], (error == nil ? [NSNull null] : error), nil];
}
- (void)gameCookieUploadSucceeded:(int)key {
    [MNUnity callUnityFunction:@"MNUM_onGameCookieUploadSucceeded" withParams:[NSNumber numberWithInt:key], nil];
}
- (void)gameCookie:(int)key uploadFailedWithError:(NSString*)error {
    [MNUnity callUnityFunction:@"MNUM_onGameCookieUploadFailedWithError" withParams:[NSNumber numberWithInt:key], (error == nil ? [NSNull null] : error), nil];
}
@end

EXTERN_C
void _MNGameCookiesProvider_DownloadUserCookie(int key) {
    MARK;

    [[MNDirect gameCookiesProvider] downloadUserCookie:key];
}

EXTERN_C
void _MNGameCookiesProvider_UploadUserCookie(int key, const char* cookie) {
    MARK;

    [[MNDirect gameCookiesProvider] uploadUserCookieWithKey:key andCookie:NSStringWithUTFStringSafe(cookie)];
}

EXTERN_C
bool _MNGameCookiesProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect gameCookiesProvider] == nil) {
        return false;
    }

    [[MNDirect gameCookiesProvider] addDelegate:[MNGameCookiesProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNGameCookiesProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect gameCookiesProvider] == nil) {
        return false;
    }

    [[MNDirect gameCookiesProvider] removeDelegate:[MNGameCookiesProviderUnity shared]];

    return true;
}


