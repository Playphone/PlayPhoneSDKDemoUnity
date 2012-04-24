//
//  MNSessionUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNSession.h"

#import "MNSessionUnity.h"

@interface MNSessionUnity : NSObject <MNSessionDelegate>
+ (id)shared;
- (void)mnSessionStatusChangedTo:(int)newStatus from:(int)oldStatus;
- (void)mnSessionUserChangedTo:(long long)userId;
- (void)mnSessionRoomUserJoin:(MNUserInfo*)userInfo;
- (void)mnSessionRoomUserLeave:(MNUserInfo*)userInfo;
- (void)mnSessionGameMessageReceived:(NSString*)message from:(MNUserInfo*)sender;
- (void)mnSessionErrorOccurred:(MNErrorInfo*)errorInfo;
- (void)mnSessionExecAppCommandReceived:(NSString*)cmdName withParam:(NSString*)cmdParam;
- (void)mnSessionExecUICommandReceived:(NSString*)cmdName withParam:(NSString*)cmdParam;
- (void)mnSessionJoinRoomInvitationReceived:(MNJoinRoomInvitationParams*)params;
@end

static MNSessionUnity *MNSessionUnityInstance = nil;

@implementation MNSessionUnity

+(id)shared {
    if (MNSessionUnityInstance == nil) {
        MNSessionUnityInstance = [[MNSessionUnity alloc]init];
    }
    
    return MNSessionUnityInstance;
}

- (void)mnSessionStatusChangedTo:(int)newStatus from:(int)oldStatus {
    [MNUnity callUnityFunction:@"MNUM_mnSessionStatusChanged" withParams:[NSNumber numberWithInt:newStatus], [NSNumber numberWithInt:oldStatus], nil];
}
- (void)mnSessionUserChangedTo:(long long)userId {
    [MNUnity callUnityFunction:@"MNUM_mnSessionUserChanged" withParams:[NSNumber numberWithLongLong:userId], nil];
}
- (void)mnSessionRoomUserJoin:(MNUserInfo*)userInfo {
    [MNUnity callUnityFunction:@"MNUM_mnSessionRoomUserJoin" withParams:(userInfo == nil ? [NSNull null] : userInfo), nil];
}
- (void)mnSessionRoomUserLeave:(MNUserInfo*)userInfo {
    [MNUnity callUnityFunction:@"MNUM_mnSessionRoomUserLeave" withParams:(userInfo == nil ? [NSNull null] : userInfo), nil];
}
- (void)mnSessionGameMessageReceived:(NSString*)message from:(MNUserInfo*)sender {
    [MNUnity callUnityFunction:@"MNUM_mnSessionGameMessageReceived" withParams:(message == nil ? [NSNull null] : message), (sender == nil ? [NSNull null] : sender), nil];
}
- (void)mnSessionErrorOccurred:(MNErrorInfo*)errorInfo {
    [MNUnity callUnityFunction:@"MNUM_mnSessionErrorOccurred" withParams:(errorInfo == nil ? [NSNull null] : errorInfo), nil];
}
- (void)mnSessionExecAppCommandReceived:(NSString*)cmdName withParam:(NSString*)cmdParam {
    [MNUnity callUnityFunction:@"MNUM_mnSessionExecAppCommandReceived" withParams:(cmdName == nil ? [NSNull null] : cmdName), (cmdParam == nil ? [NSNull null] : cmdParam), nil];
}
- (void)mnSessionExecUICommandReceived:(NSString*)cmdName withParam:(NSString*)cmdParam {
    [MNUnity callUnityFunction:@"MNUM_mnSessionExecUICommandReceived" withParams:(cmdName == nil ? [NSNull null] : cmdName), (cmdParam == nil ? [NSNull null] : cmdParam), nil];
}
- (void)mnSessionJoinRoomInvitationReceived:(MNJoinRoomInvitationParams*)params {
    [MNUnity callUnityFunction:@"MNUM_mnSessionJoinRoomInvitationReceived" withParams:(params == nil ? [NSNull null] : params), nil];
}
@end

EXTERN_C
const char* _MNSession_GetMyUserInfo() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect getSession] getMyUserInfo]] UTF8String]);
}

EXTERN_C
void _MNSession_ReqJoinRandomRoom(const char* gameSetId) {
    MARK;

    [[MNDirect getSession] reqJoinRandomRoom:NSStringWithUTFStringSafe(gameSetId)];
}

EXTERN_C
const char* _MNSession_GetMyUserName() {
    MARK;

    return MakeStringCopy([[[MNDirect getSession] getMyUserName] UTF8String]);
}

EXTERN_C
void _MNSession_ExecUICommand(const char* name, const char* param) {
    MARK;

    [[MNDirect getSession] execUICommand:NSStringWithUTFStringSafe(name) withParam:NSStringWithUTFStringSafe(param)];
}

EXTERN_C
int _MNSession_GetStatus() {
    MARK;

    return [[MNDirect getSession] getStatus];
}

EXTERN_C
bool _MNSession_IsInGameRoom() {
    MARK;

    return [[MNDirect getSession] isInGameRoom];
}

EXTERN_C
void _MNSession_LeaveRoom() {
    MARK;

    [[MNDirect getSession] leaveRoom];
}

EXTERN_C
int _MNSession_GetRoomUserStatus() {
    MARK;

    return [[MNDirect getSession] getRoomUserStatus];
}

EXTERN_C
int _MNSession_GetCurrentRoomId() {
    MARK;

    return [[MNDirect getSession] getCurrentRoomId];
}

EXTERN_C
void _MNSession_ReqCreateBuddyRoom(const char* buddyRoomParams) {
    MARK;

    [[MNDirect getSession] reqCreateBuddyRoom:[[MNUnity serializer] deserialize:[MNBuddyRoomParams class] fromJson:[NSString stringWithUTF8String:buddyRoomParams]]];
}

EXTERN_C
void _MNSession_ReqJoinBuddyRoom(int roomSFId) {
    MARK;

    [[MNDirect getSession] reqJoinBuddyRoom:roomSFId];
}

EXTERN_C
void _MNSession_SendJoinRoomInvitationResponse(const char* invitationParams, bool accept) {
    MARK;

    [[MNDirect getSession] sendJoinRoomInvitationResponse:[[MNUnity serializer] deserialize:[MNJoinRoomInvitationParams class] fromJson:[NSString stringWithUTF8String:invitationParams]] accept:accept];
}

EXTERN_C
bool _MNSession_LoginAuto() {
    MARK;

    return [[MNDirect getSession] loginAuto];
}

EXTERN_C
const char* _MNSession_GetRoomUserList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect getSession] getRoomUserList]] UTF8String]);
}

EXTERN_C
bool _MNSession_RegisterEventHandler(){
    MARK;

    if ([MNDirect getSession] == nil) {
        return false;
    }

    [[MNDirect getSession] addDelegate:[MNSessionUnity shared]];

    return true;
}

EXTERN_C
bool _MNSession_UnregisterEventHandler(){
    MARK;

    if ([MNDirect getSession] == nil) {
        return false;
    }

    [[MNDirect getSession] removeDelegate:[MNSessionUnity shared]];

    return true;
}


