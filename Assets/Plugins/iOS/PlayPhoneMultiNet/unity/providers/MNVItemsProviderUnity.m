//
//  MNVItemsProviderUnity.m
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"
#import "MNVItemsProvider.h"

#import "MNVItemsProviderUnity.h"

@interface MNVItemsProviderUnity : NSObject <MNVItemsProviderDelegate>
+ (id)shared;
- (void)onVItemsListUpdated;
- (void)onVItemsTransactionCompleted:(MNVItemsTransactionInfo*)transaction;
- (void)onVItemsTransactionFailed:(MNVItemsTransactionError*)error;
@end

static MNVItemsProviderUnity *MNVItemsProviderUnityInstance = nil;

@implementation MNVItemsProviderUnity

+(id)shared {
    if (MNVItemsProviderUnityInstance == nil) {
        MNVItemsProviderUnityInstance = [[MNVItemsProviderUnity alloc]init];
    }
    
    return MNVItemsProviderUnityInstance;
}

- (void)onVItemsListUpdated {
    [MNUnity callUnityFunction:@"MNUM_onVItemsListUpdated" withParams:nil];
}
- (void)onVItemsTransactionCompleted:(MNVItemsTransactionInfo*)transaction {
    [MNUnity callUnityFunction:@"MNUM_onVItemsTransactionCompleted" withParams:(transaction == nil ? [NSNull null] : transaction), nil];
}
- (void)onVItemsTransactionFailed:(MNVItemsTransactionError*)error {
    [MNUnity callUnityFunction:@"MNUM_onVItemsTransactionFailed" withParams:(error == nil ? [NSNull null] : error), nil];
}
@end

EXTERN_C
const char* _MNVItemsProvider_GetGameVItemsList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect vItemsProvider] getGameVItemsList]] UTF8String]);
}

EXTERN_C
const char* _MNVItemsProvider_FindGameVItemById(int vItemId) {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect vItemsProvider] findGameVItemById:vItemId]] UTF8String]);
}

EXTERN_C
bool _MNVItemsProvider_IsGameVItemsListNeedUpdate() {
    MARK;

    return [[MNDirect vItemsProvider] isGameVItemsListNeedUpdate];
}

EXTERN_C
void _MNVItemsProvider_DoGameVItemsListUpdate() {
    MARK;

    [[MNDirect vItemsProvider] doGameVItemsListUpdate];
}

EXTERN_C
void _MNVItemsProvider_ReqAddPlayerVItem(int vItemId, long long count, long long clientTransactionId) {
    MARK;

    [[MNDirect vItemsProvider] reqAddPlayerVItem:vItemId count:count andClientTransactionId:clientTransactionId];
}

EXTERN_C
void _MNVItemsProvider_ReqAddPlayerVItemTransaction(const char* transactionVItems, long long clientTransactionId) {
    MARK;

    [[MNDirect vItemsProvider] reqAddPlayerVItemTransaction:[[MNUnity serializer] deserializeArray:[MNTransactionVItemInfo class] fromJson:[NSString stringWithUTF8String:transactionVItems]] andClientTransactionId:clientTransactionId];
}

EXTERN_C
void _MNVItemsProvider_ReqTransferPlayerVItem(int vItemId, long long count, long long toPlayerId, long long clientTransactionId) {
    MARK;

    [[MNDirect vItemsProvider] reqTransferPlayerVItem:vItemId count:count toPlayer:toPlayerId andClientTransactionId:clientTransactionId];
}

EXTERN_C
void _MNVItemsProvider_ReqTransferPlayerVItemTransaction(const char* transactionVItems, long long toPlayerId, long long clientTransactionId) {
    MARK;

    [[MNDirect vItemsProvider] reqTransferPlayerVItemTransaction:[[MNUnity serializer] deserializeArray:[MNTransactionVItemInfo class] fromJson:[NSString stringWithUTF8String:transactionVItems]] toPlayer:toPlayerId andClientTransactionId:clientTransactionId];
}

EXTERN_C
const char* _MNVItemsProvider_GetPlayerVItemList() {
    MARK;

    return MakeStringCopy([[[MNUnity serializer]serialize:[[MNDirect vItemsProvider] getPlayerVItemList]] UTF8String]);
}

EXTERN_C
long long _MNVItemsProvider_GetPlayerVItemCountById(int vItemId) {
    MARK;

    return [[MNDirect vItemsProvider] getPlayerVItemCountById:vItemId];
}

EXTERN_C
const char* _MNVItemsProvider_GetVItemImageURL(int vItemId) {
    MARK;

    NSURL *resultUrl = [[MNDirect vItemsProvider] getVItemImageURL:vItemId];

    if (resultUrl == nil) {
        return NULL;
    }

    return MakeStringCopy([[resultUrl absoluteString] UTF8String]);
}

EXTERN_C
long long _MNVItemsProvider_GetNewClientTransactionId() {
    MARK;

    return [[MNDirect vItemsProvider] getNewClientTransactionId];
}

EXTERN_C
bool _MNVItemsProvider_RegisterEventHandler(){
    MARK;

    if ([MNDirect vItemsProvider] == nil) {
        return false;
    }

    [[MNDirect vItemsProvider] addDelegate:[MNVItemsProviderUnity shared]];

    return true;
}

EXTERN_C
bool _MNVItemsProvider_UnregisterEventHandler(){
    MARK;

    if ([MNDirect vItemsProvider] == nil) {
        return false;
    }

    [[MNDirect vItemsProvider] removeDelegate:[MNVItemsProviderUnity shared]];

    return true;
}


