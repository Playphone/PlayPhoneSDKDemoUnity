//
//  MNJsonSerializer.h
//  Unity-iPhone
//
//  Created by Vladislav Ogol on 25.01.12.
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "JSON.h"

#import "MNUnity.h"

@interface MNJsonSerializer : NSObject <MNSerializer> 

- (NSString*)serialize:(id)object;
- (NSString*)serializeDicitionaryToArray:(NSDictionary*)dictionary;
- (id)deserialize:(Class)classType fromJson:(NSString*)jsonString;
- (NSArray*)deserializeArray:(Class)elementType fromJson:(NSString*)jsonString;

@end

