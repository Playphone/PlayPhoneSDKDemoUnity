//
//  MNDirectPopupUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
void _MNDirectPopup_Init(int actionsBitMask);

EXTERN_C
bool _MNDirectPopup_IsActive();

EXTERN_C
void _MNDirectPopup_SetActive(bool activeFlag);


