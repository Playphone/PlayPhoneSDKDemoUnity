//
//  MNVShopProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
const char* _MNVShopProvider_GetVShopPackList();

EXTERN_C
const char* _MNVShopProvider_GetVShopCategoryList();

EXTERN_C
const char* _MNVShopProvider_FindVShopPackById(int packId);

EXTERN_C
const char* _MNVShopProvider_FindVShopCategoryById(int categoryId);

EXTERN_C
bool _MNVShopProvider_IsVShopInfoNeedUpdate();

EXTERN_C
void _MNVShopProvider_DoVShopInfoUpdate();

EXTERN_C
const char* _MNVShopProvider_GetVShopPackImageURL(int packId);

EXTERN_C
bool _MNVShopProvider_IsVShopReady();

EXTERN_C
void _MNVShopProvider_ExecCheckoutVShopPacks(const char* packIdArray, const char* packCountArray, long long clientTransactionId);

EXTERN_C
void _MNVShopProvider_ProcCheckoutVShopPacksSilent(const char* packIdArray, const char* packCountArray, long long clientTransactionId);

EXTERN_C
bool _MNVShopProvider_RegisterEventHandler();

EXTERN_C
bool _MNVShopProvider_UnregisterEventHandler();


