//
//  MNSessionUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
bool _MNSession_LoginAuto();

EXTERN_C
void _MNSession_Logout();

EXTERN_C
int _MNSession_GetStatus();

EXTERN_C
const char* _MNSession_GetMyUserName();

EXTERN_C
const char* _MNSession_GetMyUserInfo();

EXTERN_C
const char* _MNSession_GetRoomUserList();

EXTERN_C
int _MNSession_GetRoomUserStatus();

EXTERN_C
int _MNSession_GetCurrentRoomId();

EXTERN_C
int _MNSession_GetRoomGameSetId();

EXTERN_C
void _MNSession_ReqJoinBuddyRoom(int roomSFId);

EXTERN_C
void _MNSession_SendJoinRoomInvitationResponse(const char* invitationParams, bool accept);

EXTERN_C
void _MNSession_ReqJoinRandomRoom(const char* gameSetId);

EXTERN_C
void _MNSession_ReqCreateBuddyRoom(const char* buddyRoomParams);

EXTERN_C
void _MNSession_ReqSetUserStatus(int userStatus);

EXTERN_C
void _MNSession_LeaveRoom();

EXTERN_C
void _MNSession_ExecUICommand(const char* name, const char* param);

EXTERN_C
bool _MNSession_IsInGameRoom();

EXTERN_C
bool _MNSession_RegisterEventHandler();

EXTERN_C
bool _MNSession_UnregisterEventHandler();


