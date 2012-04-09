//
//  MNClientRobotsProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
bool _MNClientRobotsProvider_IsRobot(const char* userInfo);

EXTERN_C
void _MNClientRobotsProvider_PostRobotScore(const char* userInfo, long long score);

EXTERN_C
void _MNClientRobotsProvider_SetRoomRobotLimit(int robotCount);

EXTERN_C
int _MNClientRobotsProvider_GetRoomRobotLimit();


