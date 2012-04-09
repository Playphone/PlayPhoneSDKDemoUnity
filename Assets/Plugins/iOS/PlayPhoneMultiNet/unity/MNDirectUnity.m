//
//  MNDirectUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"

#import "MNDirectUnity.h"

@interface MNDirectUnity : NSObject <MNDirectDelegate>
+ (id)shared;
- (void)mnDirectDoStartGameWithParams:(MNGameParams*)params;
- (void)mnDirectDoFinishGame;
- (void)mnDirectDoCancelGame;
- (void)mnDirectViewDoGoBack;
- (void)mnDirectDidReceiveGameMessage:(NSString*)message from:(MNUserInfo*)sender;
- (void)mnDirectSessionStatusChangedTo:(int)newStatus;
- (void)mnDirectErrorOccurred:(MNErrorInfo*)error;
- (void)mnDirectSessionReady:(MNSession*) session;

@end

static MNDirectUnity *MNDirectUnityInstance = nil;

@implementation MNDirectUnity

+(id)shared {
    if (MNDirectUnityInstance == nil) {
        MNDirectUnityInstance = [[MNDirectUnity alloc]init];
    }
    
    return MNDirectUnityInstance;
}

- (void)mnDirectDoStartGameWithParams:(MNGameParams*)params {
    [MNUnity callUnityFunction:@"MNUM_mnDirectDoStartGameWithParams" withParams:(params == nil ? [NSNull null] : params), nil];
}
- (void)mnDirectDoFinishGame {
    [MNUnity callUnityFunction:@"MNUM_mnDirectDoFinishGame" withParams:nil];
}
- (void)mnDirectDoCancelGame {
    [MNUnity callUnityFunction:@"MNUM_mnDirectDoCancelGame" withParams:nil];
}
- (void)mnDirectViewDoGoBack {
    [MNUnity callUnityFunction:@"MNUM_mnDirectViewDoGoBack" withParams:nil];
}
- (void)mnDirectDidReceiveGameMessage:(NSString*)message from:(MNUserInfo*)sender {
    [MNUnity callUnityFunction:@"MNUM_mnDirectDidReceiveGameMessage" withParams:(message == nil ? [NSNull null] : message), (sender == nil ? [NSNull null] : sender), nil];
}
- (void)mnDirectSessionStatusChangedTo:(int)newStatus {
    [MNUnity callUnityFunction:@"MNUM_mnDirectSessionStatusChanged" withParams:[NSNumber numberWithInt:newStatus], nil];
}
- (void)mnDirectErrorOccurred:(MNErrorInfo*)error {
    [MNUnity callUnityFunction:@"MNUM_mnDirectErrorOccurred" withParams:(error == nil ? [NSNull null] : error), nil];
}
- (void)mnDirectSessionReady:(MNSession*) session {
    [MNUnity callUnityFunction:@"MNUM_mnDirectSessionReady" withParams:nil];
}

@end

EXTERN_C
void _MNDirect_Init(int gameId, const char* gameSecret) {
  MARK;

  [MNDirect prepareSessionWithGameId:gameId gameSecret:NSStringWithUTFStringSafe(gameSecret) andDelegate:[MNDirectUnity shared]];
}

EXTERN_C
void _MNDirect_ShutdownSession() {
    MARK;

    [MNDirect shutdownSession];
}

EXTERN_C
bool _MNDirect_IsOnline() {
    MARK;

    return [MNDirect isOnline];
}

EXTERN_C
bool _MNDirect_IsUserLoggedIn() {
    MARK;

    return [MNDirect isUserLoggedIn];
}

EXTERN_C
int _MNDirect_GetSessionStatus() {
    MARK;

    return [MNDirect getSessionStatus];
}

EXTERN_C
void _MNDirect_PostGameScore(long long score) {
    MARK;

    [MNDirect postGameScore:score];
}

EXTERN_C
void _MNDirect_PostGameScorePending(long long score) {
    MARK;

    [MNDirect postGameScorePending:score];
}

EXTERN_C
void _MNDirect_CancelGame() {
    MARK;

    [MNDirect cancelGame];
}

EXTERN_C
void _MNDirect_SetDefaultGameSetId(int gameSetId) {
    MARK;

    [MNDirect setDefaultGameSetId:gameSetId];
}

EXTERN_C
int _MNDirect_GetDefaultGameSetId() {
    MARK;

    return [MNDirect getDefaultGameSetId];
}

EXTERN_C
void _MNDirect_SendAppBeacon(const char* actionName, const char* beaconData) {
    MARK;

    [MNDirect sendAppBeacon:NSStringWithUTFStringSafe(actionName) beaconData:NSStringWithUTFStringSafe(beaconData)];
}

EXTERN_C
void _MNDirect_ExecAppCommand(const char* name, const char* param) {
    MARK;

    [MNDirect execAppCommand:NSStringWithUTFStringSafe(name) withParam:NSStringWithUTFStringSafe(param)];
}

EXTERN_C
void _MNDirect_SendGameMessage(const char* message) {
    MARK;

    [MNDirect sendGameMessage:NSStringWithUTFStringSafe(message)];
}


