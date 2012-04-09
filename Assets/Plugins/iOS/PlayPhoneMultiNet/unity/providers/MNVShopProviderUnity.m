//
//  MNVShopProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNVShopProvider.h"

#import "MNVShopProviderUnity.h"

@interface MNVShopProviderUnity : NSObject <MNVShopProviderDelegate>
+ (id)shared;
- (void)onVShopInfoUpdated;
- (void)showDashboard;
- (void)hideDashboard;
- (void)onCheckoutVShopPackSuccess:(MNVShopProviderCheckoutVShopPackSuccessInfo*)result;
- (void)onCheckoutVShopPackFail:(MNVShopProviderCheckoutVShopPackFailInfo*)result;
- (void)onVShopReadyStatusChanged:(bool)isVShopReady;
@end

static MNVShopProviderUnity *MNVShopProviderUnityInstance = nil;

@implementation MNVShopProviderUnity

+(id)shared {
    if (MNVShopProviderUnityInstance == nil) {
        MNVShopProviderUnityInstance = [[MNVShopProviderUnity alloc]init];
    }
    
    return MNVShopProviderUnityInstance;
}

- (void)onVShopInfoUpdated {
    [MNUnity callUnityFunction:@"MNUM_onVShopInfoUpdated" withParams:nil];
}
- (void)showDashboard {
    [MNUnity callUnityFunction:@"MNUM_showDashboard" withParams:nil];
}
- (void)hideDashboard {
    [MNUnity callUnityFunction:@"MNUM_hideDashboard" withParams:nil];
}
- (void)onCheckoutVShopPackSuccess:(MNVShopProviderCheckoutVShopPackSuccessInfo*)result {
    [MNUnity callUnityFunction:@"MNUM_onCheckoutVShopPackSuccess" withParams:(result == nil ? [NSNull null] : result), nil];
}
- (void)onCheckoutVShopPackFail:(MNVShopProviderCheckoutVShopPackFailInfo*)result {
    [MNUnity callUnityFunction:@"MNUM_onCheckoutVShopPackFail" withParams:(result == nil ? [NSNull null] : result), nil];
}
- (void)onVShopReadyStatusChanged:(bool)isVShopReady {
    [MNUnity callUnityFunction:@"MNUM_onVShopReadyStatusChanged" withParams:[NSNumber numberWithBool:isVShopReady], nil];
}
@end

EXTERN_C
const char* _MNVShopProvider_GetVShopPackList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect vShopProvider] getVShopPackList]] UTF8String]);
}

EXTERN_C
const char* _MNVShopProvider_GetVShopCategoryList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect vShopProvider] getVShopCategoryList]] UTF8String]);
}

EXTERN_C
const char* _MNVShopProvider_FindVShopPackById(int packId) {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect vShopProvider] findVShopPackById:packId]] UTF8String]);
}

EXTERN_C
const char* _MNVShopProvider_FindVShopCategoryById(int categoryId) {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect vShopProvider] findVShopCategoryById:categoryId]] UTF8String]);
}

EXTERN_C
bool _MNVShopProvider_IsVShopInfoNeedUpdate() {
    MARK;

    return [[MNDirect vShopProvider] isVShopInfoNeedUpdate];
}

EXTERN_C
void _MNVShopProvider_DoVShopInfoUpdate() {
    MARK;

    [[MNDirect vShopProvider] doVShopInfoUpdate];
}

EXTERN_C
const char* _MNVShopProvider_GetVShopPackImageURL(int packId) {
    MARK;

    NSURL *resultUrl = [[MNDirect vShopProvider] getVShopPackImageURL:packId];

    if (resultUrl == nil) {
        return NULL;
    }

    return MakeStringCopy([[resultUrl absoluteString] UTF8String]);
}

EXTERN_C
bool _MNVShopProvider_IsVShopReady() {
    MARK;

    return [[MNDirect vShopProvider] isVShopReady];
}

EXTERN_C
void _MNVShopProvider_ExecCheckoutVShopPacks(const char* packIdArray, const char* packCountArray, long long clientTransactionId) {
    MARK;

    [[MNDirect vShopProvider] execCheckoutVShopPacks:[[MNUnity serializer] deserializeArray:[NSNumber class] fromJson:[NSString stringWithUTF8String:packIdArray]] packCount:[[MNUnity serializer] deserializeArray:[NSNumber class] fromJson:[NSString stringWithUTF8String:packCountArray]] clientTransactionId:clientTransactionId];
}

EXTERN_C
void _MNVShopProvider_ProcCheckoutVShopPacksSilent(const char* packIdArray, const char* packCountArray, long long clientTransactionId) {
    MARK;

    [[MNDirect vShopProvider] procCheckoutVShopPacksSilent:[[MNUnity serializer] deserializeArray:[NSNumber class] fromJson:[NSString stringWithUTF8String:packIdArray]] packCount:[[MNUnity serializer] deserializeArray:[NSNumber class] fromJson:[NSString stringWithUTF8String:packCountArray]] clientTransactionId:clientTransactionId];
}

EXTERN_C
bool _MNVShopProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect vShopProvider] == nil) {
        return false;
    }

    [[MNDirect vShopProvider] addDelegate:[MNVShopProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNVShopProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect vShopProvider] == nil) {
        return false;
    }

    [[MNDirect vShopProvider] removeDelegate:[MNVShopProviderUnity shared]];

    return true;
}


