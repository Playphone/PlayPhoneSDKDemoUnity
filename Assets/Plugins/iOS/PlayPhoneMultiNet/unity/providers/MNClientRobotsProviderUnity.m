//
//  MNClientRobotsProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNClientRobotsProvider.h"

#import "MNClientRobotsProviderUnity.h"

EXTERN_C
bool _MNClientRobotsProvider_IsRobot(const char* userInfo) {
    MARK;

    return [[MNDirect clientRobotsProvider] isRobot:[[MNUnity serializer] deserialize:[MNUserInfo class] fromJson:[NSString stringWithUTF8String:userInfo]]];
}

EXTERN_C
void _MNClientRobotsProvider_PostRobotScore(const char* userInfo, long long score) {
    MARK;

    [[MNDirect clientRobotsProvider] postRobot:[[MNUnity serializer] deserialize:[MNUserInfo class] fromJson:[NSString stringWithUTF8String:userInfo]] score:score];
}

EXTERN_C
void _MNClientRobotsProvider_SetRoomRobotLimit(int robotCount) {
    MARK;

    [[MNDirect clientRobotsProvider] setRoomRobotLimit:robotCount];
}

EXTERN_C
int _MNClientRobotsProvider_GetRoomRobotLimit() {
    MARK;

    return [[MNDirect clientRobotsProvider] getRoomRobotLimit];
}


