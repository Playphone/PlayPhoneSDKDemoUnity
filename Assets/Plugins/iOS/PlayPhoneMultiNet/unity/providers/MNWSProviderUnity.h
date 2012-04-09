//
//  MNWSProviderUnity.h
//  Unity-iPhone
//
//  Created by Vladislav Ogol on 26.01.12.
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
const long long _MNWSProvider_Send(const char* request);

EXTERN_C
const void _MNWSProvider_CancelRequest(long long loaderId);
