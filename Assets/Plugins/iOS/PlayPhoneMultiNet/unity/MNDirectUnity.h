//
//  MNDirectUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
void _MNDirect_Init(int gameId, const char* gameSecret);

EXTERN_C
void _MNDirect_ShutdownSession();

EXTERN_C
bool _MNDirect_IsOnline();

EXTERN_C
bool _MNDirect_IsUserLoggedIn();

EXTERN_C
int _MNDirect_GetSessionStatus();

EXTERN_C
void _MNDirect_PostGameScore(long long score);

EXTERN_C
void _MNDirect_PostGameScorePending(long long score);

EXTERN_C
void _MNDirect_CancelGame();

EXTERN_C
void _MNDirect_SetDefaultGameSetId(int gameSetId);

EXTERN_C
int _MNDirect_GetDefaultGameSetId();

EXTERN_C
void _MNDirect_SendAppBeacon(const char* actionName, const char* beaconData);

EXTERN_C
void _MNDirect_ExecAppCommand(const char* name, const char* param);

EXTERN_C
void _MNDirect_SendGameMessage(const char* message);


