//
//  MNDirectButtonUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
bool _MNDirectButton_IsVisible();

EXTERN_C
bool _MNDirectButton_IsHidden();

EXTERN_C
void _MNDirectButton_InitWithLocation(int location);

EXTERN_C
void _MNDirectButton_Show();

EXTERN_C
void _MNDirectButton_Hide();

EXTERN_C
void _MNDirectButton_SetVShopEventAutoHandleEnabled(bool isEnabled);


