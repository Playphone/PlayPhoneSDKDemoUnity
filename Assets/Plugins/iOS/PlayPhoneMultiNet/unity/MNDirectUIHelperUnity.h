//
//  MNDirectUIHelperUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
void _MNDirectUIHelper_SetDashboardStyle(int newStyle);

EXTERN_C
int _MNDirectUIHelper_GetDashboardStyle();

EXTERN_C
void _MNDirectUIHelper_ShowDashboard();

EXTERN_C
void _MNDirectUIHelper_HideDashboard();

EXTERN_C
bool _MNDirectUIHelper_IsDashboardHidden();

EXTERN_C
bool _MNDirectUIHelper_IsDashboardVisible();

EXTERN_C
bool _MNDirectUIHelper_RegisterEventHandler();

EXTERN_C
bool _MNDirectUIHelper_UnregisterEventHandler();


