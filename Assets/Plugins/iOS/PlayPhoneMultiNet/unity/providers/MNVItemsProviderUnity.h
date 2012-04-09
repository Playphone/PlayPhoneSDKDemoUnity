//
//  MNVItemsProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
const char* _MNVItemsProvider_GetGameVItemsList();

EXTERN_C
const char* _MNVItemsProvider_FindGameVItemById(int vItemId);

EXTERN_C
bool _MNVItemsProvider_IsGameVItemsListNeedUpdate();

EXTERN_C
void _MNVItemsProvider_DoGameVItemsListUpdate();

EXTERN_C
void _MNVItemsProvider_ReqAddPlayerVItem(int vItemId, long long count, long long clientTransactionId);

EXTERN_C
void _MNVItemsProvider_ReqAddPlayerVItemTransaction(const char* transactionVItems, long long clientTransactionId);

EXTERN_C
void _MNVItemsProvider_ReqTransferPlayerVItem(int vItemId, long long count, long long toPlayerId, long long clientTransactionId);

EXTERN_C
void _MNVItemsProvider_ReqTransferPlayerVItemTransaction(const char* transactionVItems, long long toPlayerId, long long clientTransactionId);

EXTERN_C
const char* _MNVItemsProvider_GetPlayerVItemList();

EXTERN_C
long long _MNVItemsProvider_GetPlayerVItemCountById(int vItemId);

EXTERN_C
const char* _MNVItemsProvider_GetVItemImageURL(int vItemId);

EXTERN_C
long long _MNVItemsProvider_GetNewClientTransactionId();

EXTERN_C
bool _MNVItemsProvider_RegisterEventHandler();

EXTERN_C
bool _MNVItemsProvider_UnregisterEventHandler();


