//
//  MNDirectUIHelperUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirectUIHelper.h"

#import "MNDirectUIHelperUnity.h"

@interface MNDirectUIHelperUnity : NSObject <MNDirectUIHelperDelegate>
+ (id)shared;
- (void)mnUIHelperDashboardShown;
- (void)mnUIHelperDashboardHidden;
@end

static MNDirectUIHelperUnity *MNDirectUIHelperUnityInstance = nil;

@implementation MNDirectUIHelperUnity

+(id)shared {
    if (MNDirectUIHelperUnityInstance == nil) {
        MNDirectUIHelperUnityInstance = [[MNDirectUIHelperUnity alloc]init];
    }
    
    return MNDirectUIHelperUnityInstance;
}

- (void)mnUIHelperDashboardShown {
    [MNUnity callUnityFunction:@"MNUM_onShowDashboard" withParams:nil];
}
- (void)mnUIHelperDashboardHidden {
    [MNUnity callUnityFunction:@"MNUM_onHideDashboard" withParams:nil];
}
@end

EXTERN_C
void _MNDirectUIHelper_SetDashboardStyle(int newStyle) {
    MARK;

    [MNDirectUIHelper setDashboardStyle:newStyle];
}

EXTERN_C
int _MNDirectUIHelper_GetDashboardStyle() {
    MARK;

    return [MNDirectUIHelper getDashboardStyle];
}

EXTERN_C
void _MNDirectUIHelper_ShowDashboard() {
    MARK;

    [MNDirectUIHelper showDashboard];
}

EXTERN_C
void _MNDirectUIHelper_HideDashboard() {
    MARK;

    [MNDirectUIHelper hideDashboard];
}

EXTERN_C
bool _MNDirectUIHelper_IsDashboardHidden() {
    MARK;

    return [MNDirectUIHelper isDashboardHidden];
}

EXTERN_C
bool _MNDirectUIHelper_IsDashboardVisible() {
    MARK;

    return [MNDirectUIHelper isDashboardVisible];
}


EXTERN_C
bool _MNDirectUIHelper_RegisterEventHandler(){
    MARK;

    [MNDirectUIHelper addDelegate:[MNDirectUIHelperUnity shared]];

    return true;
}

EXTERN_C
bool _MNDirectUIHelper_UnregisterEventHandler(){
    MARK;

    [MNDirectUIHelper removeDelegate:[MNDirectUIHelperUnity shared]];

    return true;
}


