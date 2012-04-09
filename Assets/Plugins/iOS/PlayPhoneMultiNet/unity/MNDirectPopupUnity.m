//
//  MNDirectPopupUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirectPopup.h"

#import "MNDirectPopupUnity.h"

EXTERN_C
void _MNDirectPopup_Init(int actionsBitMask) {
    MARK;

    [MNDirectPopup init:actionsBitMask];
}

EXTERN_C
bool _MNDirectPopup_IsActive() {
    MARK;

    return [MNDirectPopup isActive];
}

EXTERN_C
void _MNDirectPopup_SetActive(bool activeFlag) {
    MARK;

    [MNDirectPopup setActive:activeFlag];
}


