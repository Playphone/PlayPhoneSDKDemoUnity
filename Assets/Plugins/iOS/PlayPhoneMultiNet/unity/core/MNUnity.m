//
//  MNUnity.m
//  Unity
//
//  Created by Vladislav Ogol on 16.11.11.
//  Copyright (c) 2011 PlayPhone Inc. All rights reserved.
//

#import "MNJsonSerializer.h"
#import "MNUnity.h"

#pragma mark -

EXTERN_C
void UnitySendMessage(const char *objectName,const char *methodName,const char *messageToSend);

static id<MNSerializer> sharedSerializer = nil;

@implementation MNUnity
/*
+ (NSString*)paramToString:(id)param {
    NSString *paramString;
    
    if ([param isKindOfClass:[NSString class]]) {
        paramString = param;
    }
    else if ([param isKindOfClass:[NSNumber class]]) {
        paramString = [((NSNumber*)param) stringValue];
    }
    else {
        paramString = [[[[SBJSON alloc]init]autorelease]stringWithObject:param];

        if (paramString == nil) {
            ELog(@"Can not convert param of type [%@] to string", NSStringFromClass([param class]));
            assert(paramString);
        }
    }
    
    return paramString;
}
*/
+ (void)callUnityFunction:(NSString *)functionName withParams:(id)firstParam, ... {
    id object;
    va_list argumentList;
    NSMutableArray *paramsArray = [NSMutableArray arrayWithCapacity:10];
    
    va_start(argumentList, firstParam);
    object = firstParam;
    
    while (object != nil) {
        [paramsArray addObject:object];

        object = va_arg(argumentList, id);
    }
    va_end(argumentList);
    
    NSString *paramPassString;

    //All params passed to Unity functions as JsonArray.
    //If only one param - JsonArray with one elememt passed.
    //If no params - Empty String passed.
    if ([paramsArray count] == 0) {
        paramPassString = @"";
    }
    else {
        paramPassString = [[MNUnity serializer]serialize:paramsArray];
    }

    //Assert if unsupported data type should be passed to Unity function.
    if (paramPassString == nil) {
        ELog(@"paramPassString == nil");
        assert(paramPassString);
    }
    else {
        DLog(@"UnitySendMessage(\"%@\",\"%@\",\"%@\")",
             @"MNUnityMessanger",
             functionName,
             paramPassString);
        
        UnitySendMessage([[NSString stringWithString:@"MNUnityCommunicator"]UTF8String],
                         [functionName UTF8String],
                         [paramPassString UTF8String]);
    }
}

+ (id<MNSerializer>)serializer {
    if (sharedSerializer == nil) {
        sharedSerializer = [[MNJsonSerializer alloc]init];
    }
    
    return sharedSerializer;
}

@end

EXTERN_C
NSString* NSStringWithUTFStringSafe (const char* string) {
    if (string != NULL) {
        return [NSString stringWithUTF8String: string];
    }
    else {
        return [NSString stringWithUTF8String: ""];
    }
}


EXTERN_C
char* MakeStringCopy (const char* string) {
    if (string == NULL) {
        return NULL;
    }
    
    char* res = (char*)malloc((strlen(string) + 1) * sizeof(char));
    
    if (res == NULL) {
        NSLog(@"res == NULL");
        return NULL;
    }
    
    strcpy(res,string);
    
    return res;
}
