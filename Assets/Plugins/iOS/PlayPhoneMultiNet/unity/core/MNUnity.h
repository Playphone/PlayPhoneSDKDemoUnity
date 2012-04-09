//
//  MNUnity.h
//  Unity
//
//  Created by Vladislav Ogol on 16.11.11.
//  Copyright (c) 2011 PlayPhone Inc. All rights reserved.
//

#import <Foundation/Foundation.h>

//#define MNUnity_DEBUG

#ifdef __cplusplus
#define EXTERN_C  extern "C"
#else
#define EXTERN_C  extern
#endif


#ifdef MNUnity_DEBUG
#define DLog(format,...) NSLog(@"MNUW:ObjC:[%s] " format, __PRETTY_FUNCTION__, ##__VA_ARGS__)
#else
#define DLog(format,...)
#endif

#define ELog(format,...) NSLog(@"MNUW:ObjC:[%s] " format, __PRETTY_FUNCTION__, ##__VA_ARGS__)

#define MARK DLog(@"")


@protocol MNSerializer <NSObject>

- (NSString*)serialize:(id)object;
- (NSString*)serializeDicitionaryToArray:(NSDictionary*)dictionary;
- (id)deserialize:(Class)classType fromJson:(NSString*)jsonString;
- (NSArray*)deserializeArray:(Class)elementType fromJson:(NSString*)jsonString;

@end

@interface MNUnity : NSObject

+ (void)callUnityFunction:(NSString *)functionName withParams:(id)firstParam, ...;

+ (id<MNSerializer>)serializer;

@end

EXTERN_C
char* MakeStringCopy (const char* string);

EXTERN_C
NSString* NSStringWithUTFStringSafe (const char* string);
//void d () 
//{
//    MNUnity *d = [MNUnity.serializer deserialize:[MNUnity class]];
//}
