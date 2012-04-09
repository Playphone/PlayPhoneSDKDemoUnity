//
//  MNDirectButtonUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirectButton.h"

#import "MNDirectButtonUnity.h"

EXTERN_C
bool _MNDirectButton_IsVisible() {
    MARK;

    return [MNDirectButton isVisible];
}

EXTERN_C
bool _MNDirectButton_IsHidden() {
    MARK;

    return [MNDirectButton isHidden];
}

EXTERN_C
void _MNDirectButton_InitWithLocation(int location) {
    MARK;

    [MNDirectButton initWithLocation:location];
}

EXTERN_C
void _MNDirectButton_Show() {
    MARK;

    [MNDirectButton show];
}

EXTERN_C
void _MNDirectButton_Hide() {
    MARK;

    [MNDirectButton hide];
}


