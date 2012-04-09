//
//  MNJsonSerializer.m
//  Unity-iPhone
//
//  Created by Vladislav Ogol on 25.01.12.
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import <objc/runtime.h>

#import "MNDirect.h"
#import "MNErrorInfo.h"
#import "MNGameParams.h"
#import "MNUserInfo.h"

#import "MNAchievementsProvider.h"
#import "MNVItemsProvider.h"
#import "MNVShopProvider.h"
#import "MNGameSettingsProvider.h"
#import "MNScoreProgressProvider.h"

#import "MNSession.h"

#import "MNWSProvider.h"
#import "MNWSAnyGameItem.h"
#import "MNWSAnyUserItem.h"
#import "MNWSUserGameCookie.h"
#import "MNWSCurrentUserInfo.h"
#import "MNWSRoomListItem.h"
#import "MNWSRoomUserInfoItem.h"
#import "MNWSBuddyListItem.h"
#import "MNWSCurrUserSubscriptionStatus.h"
#import "MNWSSessionSignedClientToken.h"
#import "MNWSSystemGameNetStats.h"
#import "MNWSLeaderboardListItem.h"

#import "MNBuddyRoomParams.h"
#import "MNJoinRoomInvitationParams.h"

#import "MNJsonSerializer.h"

@interface MNJsonSerializer()

@property (retain, nonatomic) SBJSON *jsonSerializer;

@end

@implementation MNJsonSerializer

@synthesize jsonSerializer = _jsonSerializer;

- (id)init {
    if (self = [super init]) {
        self.jsonSerializer = [[[SBJSON alloc]init]autorelease];
    }
    
    return self;
}

- (void)dealloc {
    self.jsonSerializer = nil;
    
    [super dealloc];
}

- (NSString*)serialize:(id)object {
    NSError *_lastError = nil;
    
    if (object == nil) {
        object = [NSNull null];
    }
    
    NSString *jsonString = [self.jsonSerializer stringWithObject:object error:&_lastError];
    
    if ([_lastError code] == EFRAGMENT) {
        DLog(@"Serialization Error = EFRAGMENT, try to pack value in array");
        // May be it's a boxed primitive type or null. If so, serialize it as array of one element.
        jsonString = [self.jsonSerializer stringWithObject:[NSArray arrayWithObject:object] error:&_lastError];
    }

    DLog(@"Serialization result: %@",jsonString == nil?@"<nil>":jsonString);
    
    return jsonString;
}

- (NSString*)serializeDicitionaryToArray:(NSDictionary*)dictionary {
    NSMutableArray *dictArray = [NSMutableArray arrayWithCapacity:[dictionary count]];
    NSEnumerator *enumerator = [dictionary keyEnumerator];
    id key;
    
    while ((key = [enumerator nextObject])) {
        [dictArray addObject:key];
        [dictArray addObject:[dictionary objectForKey:key]];
    }

    return [self serialize:dictArray];
}

- (id)deserialize:(Class)classType fromJson:(NSString*)jsonString {
    if ([classType respondsToSelector:@selector(fromJson:)]) {
        return [classType fromJson:jsonString];
    }
    else {
        ELog(@"Deserialization error. Class: %s",class_getName(classType));
    }
    
    return nil;
}

- (NSArray*)deserializeArray:(Class)elementType fromJson:(NSString*)jsonString {
    NSError *_lastError = nil;

    DLog(@"Deserialize Array of %s",class_getName(elementType));
    
    NSMutableArray *resultArray = nil;
    
    if (([elementType isSubclassOfClass:[NSNumber class]]) || ([elementType isSubclassOfClass:[NSString class]])) {
        resultArray = [self.jsonSerializer objectWithString:jsonString error:&_lastError];
    }
    else {
        NSArray *rawArray = [self.jsonSerializer objectWithString:jsonString error:&_lastError];
        resultArray = [NSMutableArray arrayWithCapacity:[rawArray count]];
        
        for (NSDictionary *dict in rawArray) {
            if ([elementType respondsToSelector:@selector(fromDictionary:)]) {
                [resultArray addObject:[elementType fromDictionary:dict]];
            }
            else {
                ELog(@"Array Deserialization error. Elemets type: %s",class_getName(elementType));
                break;
            }
        }
    }
    
    return resultArray;
}

@end

#pragma mark - JSON Serializers

@implementation MNErrorInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:2];
    
    id localErrorMessage = self.errorMessage;
    [fieldsDict setObject:(localErrorMessage == nil) ? [NSNull null] : localErrorMessage forKey:@"ErrorMessage"];
    
    id localActionCode = [NSNumber numberWithInt:self.actionCode];
    [fieldsDict setObject:(localActionCode == nil) ? [NSNull null] : localActionCode forKey:@"ActionCode"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNErrorInfo *object = [[[MNErrorInfo alloc]initWithActionCode:((NSNumber*)[fieldsDict objectForKey:@"ActionCode"]).intValue
                                                  andErrorMessage:[fieldsDict objectForKey:@"ErrorMessage"]]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNErrorInfo fromDictionary:fieldsDict];
}

@end

@implementation MNGameParams(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:6];
    
    id localPlayModel = [NSNumber numberWithInt:self.playModel];
    [fieldsDict setObject:(localPlayModel == nil) ? [NSNull null] : localPlayModel forKey:@"PlayModel"];
    
    id localGameSeed = [NSNumber numberWithInt:self.gameSeed];
    [fieldsDict setObject:(localGameSeed == nil) ? [NSNull null] : localGameSeed forKey:@"GameSeed"];
    
    id localGameSetParams = self.gameSetParams;
    [fieldsDict setObject:(localGameSetParams == nil) ? [NSNull null] : localGameSetParams forKey:@"GameSetParams"];
    
    id localScorePostLinkId = self.scorePostLinkId;
    [fieldsDict setObject:(localScorePostLinkId == nil) ? [NSNull null] : localScorePostLinkId forKey:@"ScorePostLinkId"];
    
    id localGameSetId = [NSNumber numberWithInt:self.gameSetId];
    [fieldsDict setObject:(localGameSetId == nil) ? [NSNull null] : localGameSetId forKey:@"GameSetId"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNGameParams *object = [[[MNGameParams alloc]initWithGameSetId:((NSNumber*)[fieldsDict objectForKey:@"GameSetId"]).intValue
                                                     gameSetParams:[fieldsDict objectForKey:@"GameSetParams"]
                                                   scorePostLinkId:[fieldsDict objectForKey:@"ScorePostLinkId"]
                                                          gameSeed:((NSNumber*)[fieldsDict objectForKey:@"GameSeed"]).intValue
                                                         playModel:((NSNumber*)[fieldsDict objectForKey:@"PlayModel"]).intValue]autorelease];

    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNGameParams fromDictionary:fieldsDict];
}

@end

@implementation MNUserInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:3];
    
    id localUserId = [NSNumber numberWithLongLong:self.userId];
    [fieldsDict setObject:(localUserId == nil) ? [NSNull null] : localUserId forKey:@"UserId"];
    
    id localUserName = self.userName;
    [fieldsDict setObject:(localUserName == nil) ? [NSNull null] : localUserName forKey:@"UserName"];
    
    id localUserSFId = [NSNumber numberWithInt:self.userSFId];
    [fieldsDict setObject:(localUserSFId == nil) ? [NSNull null] : localUserSFId forKey:@"UserSFId"];

    id localUserAvatarUrl = [self getAvatarUrl];
    [fieldsDict setObject:(localUserAvatarUrl == nil) ? [NSNull null] : localUserAvatarUrl forKey:@"UserAvatarUrl"];
        
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNUserInfo *userInfo = [[[MNUserInfo alloc]initWithUserId:((NSNumber*)[fieldsDict objectForKey:@"UserId"  ]).longLongValue 
                                                     userSFId:((NSNumber*)[fieldsDict objectForKey:@"UserSFId"]).intValue
                                                     userName:[fieldsDict objectForKey:@"UserName"]
                                                   webBaseUrl:[[MNDirect getSession]getWebServerURL]]autorelease];
    
    return userInfo;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNUserInfo fromDictionary:fieldsDict];
}

@end

@implementation MNGameAchievementInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:6];
    
    id localFlags = [NSNumber numberWithInt:self.flags];
    [fieldsDict setObject:(localFlags == nil) ? [NSNull null] : localFlags forKey:@"Flags"];
    
    id localName = self.name;
    [fieldsDict setObject:(localName == nil) ? [NSNull null] : localName forKey:@"Name"];
    
    id localPoints = [NSNumber numberWithInt:self.points];
    [fieldsDict setObject:(localPoints == nil) ? [NSNull null] : localPoints forKey:@"Points"];
    
    id localDescription = self.description;
    [fieldsDict setObject:(localDescription == nil) ? [NSNull null] : localDescription forKey:@"Description"];
    
    id localParams = self.params;
    [fieldsDict setObject:(localParams == nil) ? [NSNull null] : localParams forKey:@"Params"];
    
    id localId = [NSNumber numberWithInt:self.achievementId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNGameAchievementInfo *object = [[[MNGameAchievementInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue
                                                                         name:[fieldsDict objectForKey:@"Name"]
                                                                     andFlags:((NSNumber*)[fieldsDict objectForKey:@"Flags"]).intValue]autorelease];
    
    object.points = ((NSNumber*)[fieldsDict objectForKey:@"Points"]).intValue;
    object.description = [fieldsDict objectForKey:@"Description"];
    object.params = [fieldsDict objectForKey:@"Params"];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNGameAchievementInfo fromDictionary:fieldsDict];
}

@end

@implementation MNPlayerAchievementInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:1];
    
    id localId = [NSNumber numberWithInt:self.achievementId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNPlayerAchievementInfo *object = [[[MNPlayerAchievementInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNPlayerAchievementInfo fromDictionary:fieldsDict];
}

@end

@implementation MNGameVItemInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:5];
    
    id localModel = [NSNumber numberWithInt:self.model];
    [fieldsDict setObject:(localModel == nil) ? [NSNull null] : localModel forKey:@"Model"];
    
    id localName = self.name;
    [fieldsDict setObject:(localName == nil) ? [NSNull null] : localName forKey:@"Name"];
    
    id localDescription = self.description;
    [fieldsDict setObject:(localDescription == nil) ? [NSNull null] : localDescription forKey:@"Description"];
    
    id localParams = self.params;
    [fieldsDict setObject:(localParams == nil) ? [NSNull null] : localParams forKey:@"Params"];
    
    id localId = [NSNumber numberWithInt:self.vItemId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNGameVItemInfo *object = [[[MNGameVItemInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue
                                                             name:[fieldsDict objectForKey:@"Name"]
                                                         andModel:((NSNumber*)[fieldsDict objectForKey:@"Model"]).intValue]autorelease];
    
    object.description = [fieldsDict objectForKey:@"Description"];
    object.params = [fieldsDict objectForKey:@"Params"];

    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNGameVItemInfo fromDictionary:fieldsDict];
}

@end

@implementation MNPlayerVItemInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:2];
    
    id localCount = [NSNumber numberWithLongLong:self.count];
    [fieldsDict setObject:(localCount == nil) ? [NSNull null] : localCount forKey:@"Count"];
    
    id localId = [NSNumber numberWithInt:self.vItemId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNPlayerVItemInfo *object = [[[MNPlayerVItemInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue
                                                             andCount:((NSNumber*)[fieldsDict objectForKey:@"Count"]).longLongValue]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNPlayerVItemInfo fromDictionary:fieldsDict];
}

@end

@implementation MNTransactionVItemInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:2];
    
    id localDelta = [NSNumber numberWithLongLong:self.delta];
    [fieldsDict setObject:(localDelta == nil) ? [NSNull null] : localDelta forKey:@"Delta"];
    
    id localId = [NSNumber numberWithInt:self.vItemId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNTransactionVItemInfo *object = [[[MNTransactionVItemInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue
                                                                       andDelta:((NSNumber*)[fieldsDict objectForKey:@"Delta"]).longLongValue]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNTransactionVItemInfo fromDictionary:fieldsDict];
}

@end

@implementation MNVItemsTransactionInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:4];
    
    id localCorrUserId = [NSNumber numberWithLongLong:self.corrUserId];
    [fieldsDict setObject:(localCorrUserId == nil) ? [NSNull null] : localCorrUserId forKey:@"CorrUserId"];
    
    id localVItems = self.transactionVItems;
    [fieldsDict setObject:(localVItems == nil) ? [NSNull null] : localVItems forKey:@"VItems"];
    
    id localServerTransactionId = [NSNumber numberWithLongLong:self.serverTransactionId];
    [fieldsDict setObject:(localServerTransactionId == nil) ? [NSNull null] : localServerTransactionId forKey:@"ServerTransactionId"];
    
    id localClientTransactionId = [NSNumber numberWithLongLong:self.clientTransactionId];
    [fieldsDict setObject:(localClientTransactionId == nil) ? [NSNull null] : localClientTransactionId forKey:@"ClientTransactionId"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    NSArray *vItemsJsonArray = [fieldsDict objectForKey:@"VItems"];
    NSMutableArray *vItems = [NSMutableArray arrayWithCapacity:[vItemsJsonArray count]];
    
    for (NSDictionary *vItemDictionary in vItemsJsonArray) {
        MNTransactionVItemInfo *vItemInfo = [[[MNTransactionVItemInfo alloc]initWithId:((NSNumber*)[vItemDictionary objectForKey:@"Id"]).intValue
                                                                             andDelta:((NSNumber*)[vItemDictionary objectForKey:@"Delta"]).longLongValue]autorelease];

        [vItems addObject:vItemInfo];
    }
    
    MNVItemsTransactionInfo *object = [[[MNVItemsTransactionInfo alloc]initWithClientTransactionId:((NSNumber*)[fieldsDict objectForKey:@"ClientTransactionId"]).longLongValue
                                                                               serverTransactionId:((NSNumber*)[fieldsDict objectForKey:@"ServerTransactionId"]).longLongValue
                                                                                        corrUserId:((NSNumber*)[fieldsDict objectForKey:@"CorrUserId"]).longLongValue
                                                                                         andVItems:vItems]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNVItemsTransactionInfo fromDictionary:fieldsDict];
}

@end

@implementation MNVItemsTransactionError(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:5];
    
    id localCorrUserId = [NSNumber numberWithLongLong:self.corrUserId];
    [fieldsDict setObject:(localCorrUserId == nil) ? [NSNull null] : localCorrUserId forKey:@"CorrUserId"];
    
    id localErrorMessage = self.errorMessage;
    [fieldsDict setObject:(localErrorMessage == nil) ? [NSNull null] : localErrorMessage forKey:@"ErrorMessage"];
    
    id localFailReasonCode = [NSNumber numberWithInt:self.failReasonCode];
    [fieldsDict setObject:(localFailReasonCode == nil) ? [NSNull null] : localFailReasonCode forKey:@"FailReasonCode"];
    
    id localServerTransactionId = [NSNumber numberWithLongLong:self.serverTransactionId];
    [fieldsDict setObject:(localServerTransactionId == nil) ? [NSNull null] : localServerTransactionId forKey:@"ServerTransactionId"];
    
    id localClientTransactionId = [NSNumber numberWithLongLong:self.clientTransactionId];
    [fieldsDict setObject:(localClientTransactionId == nil) ? [NSNull null] : localClientTransactionId forKey:@"ClientTransactionId"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNVItemsTransactionError *object = [[[MNVItemsTransactionError alloc]initWithClientTransactionId:((NSNumber*)[fieldsDict objectForKey:@"ClientTransactionId"]).longLongValue
                                                                                 serverTransactionId:((NSNumber*)[fieldsDict objectForKey:@"ServerTransactionId"]).longLongValue
                                                                                          corrUserId:((NSNumber*)[fieldsDict objectForKey:@"CorrUserId"]).longLongValue
                                                                                      failReasonCode:((NSNumber*)[fieldsDict objectForKey:@"FailReasonCode"]).intValue
                                                                                     andErrorMessage:[fieldsDict objectForKey:@"ErrorMessage"]]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNVItemsTransactionError fromDictionary:fieldsDict];
}

@end

@implementation MNVShopDeliveryInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:2];
    
    id localAmount = [NSNumber numberWithLongLong:self.amount];
    [fieldsDict setObject:(localAmount == nil) ? [NSNull null] : localAmount forKey:@"Amount"];
    
    id localVItemId = [NSNumber numberWithInt:self.vItemId];
    [fieldsDict setObject:(localVItemId == nil) ? [NSNull null] : localVItemId forKey:@"VItemId"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNVShopDeliveryInfo *object = [[[MNVShopDeliveryInfo alloc]initWithVItemId:((NSNumber*)[fieldsDict objectForKey:@"VItemId"]).intValue
                                                                     andAmount:((NSNumber*)[fieldsDict objectForKey:@"Amount"]).longLongValue]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNVShopDeliveryInfo fromDictionary:fieldsDict];
}

@end

@implementation MNVShopPackInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:10];
    
    id localAppParams = self.appParams;
    [fieldsDict setObject:(localAppParams == nil) ? [NSNull null] : localAppParams forKey:@"AppParams"];
    
    id localName = self.name;
    [fieldsDict setObject:(localName == nil) ? [NSNull null] : localName forKey:@"Name"];
    
    id localId = [NSNumber numberWithInt:self.packId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    id localDelivery = self.delivery;
    [fieldsDict setObject:(localDelivery == nil) ? [NSNull null] : localDelivery forKey:@"Delivery"];
    
    id localSortPos = [NSNumber numberWithInt:self.sortPos];
    [fieldsDict setObject:(localSortPos == nil) ? [NSNull null] : localSortPos forKey:@"SortPos"];
    
    id localCategoryId = [NSNumber numberWithInt:self.categoryId];
    [fieldsDict setObject:(localCategoryId == nil) ? [NSNull null] : localCategoryId forKey:@"CategoryId"];
    
    id localModel = [NSNumber numberWithInt:self.model];
    [fieldsDict setObject:(localModel == nil) ? [NSNull null] : localModel forKey:@"Model"];
    
    id localPriceValue = [NSNumber numberWithLongLong:self.priceValue];
    [fieldsDict setObject:(localPriceValue == nil) ? [NSNull null] : localPriceValue forKey:@"PriceValue"];
    
    id localPriceItemId = [NSNumber numberWithInt:self.priceItemId];
    [fieldsDict setObject:(localPriceItemId == nil) ? [NSNull null] : localPriceItemId forKey:@"PriceItemId"];
    
    id localDescription = self.description;
    [fieldsDict setObject:(localDescription == nil) ? [NSNull null] : localDescription forKey:@"Description"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNVShopPackInfo *object = [[[MNVShopPackInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue
                                                         andName:[fieldsDict objectForKey:@"Name"]]autorelease];
    
    object.appParams = [fieldsDict objectForKey:@"AppParams"];
    object.delivery = [fieldsDict objectForKey:@"Delivery"];
    object.sortPos = ((NSNumber*)[fieldsDict objectForKey:@"SortPos"]).intValue;
    object.categoryId = ((NSNumber*)[fieldsDict objectForKey:@"CategoryId"]).intValue;
    object.model = ((NSNumber*)[fieldsDict objectForKey:@"Model"]).intValue;
    object.priceValue = ((NSNumber*)[fieldsDict objectForKey:@"PriceValue"]).longLongValue;
    object.priceItemId = ((NSNumber*)[fieldsDict objectForKey:@"PriceItemId"]).intValue;
    object.description = [fieldsDict objectForKey:@"Description"];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNVShopPackInfo fromDictionary:fieldsDict];
}

@end

@implementation MNVShopCategoryInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:3];
    
    id localName = self.name;
    [fieldsDict setObject:(localName == nil) ? [NSNull null] : localName forKey:@"Name"];
    
    id localSortPos = [NSNumber numberWithInt:self.sortPos];
    [fieldsDict setObject:(localSortPos == nil) ? [NSNull null] : localSortPos forKey:@"SortPos"];
    
    id localId = [NSNumber numberWithInt:self.categoryId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNVShopCategoryInfo *object = [[[MNVShopCategoryInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue
                                                                  andName:[fieldsDict objectForKey:@"Name"]]autorelease];

    object.sortPos = ((NSNumber*)[fieldsDict objectForKey:@"SortPos"]).intValue;
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNVShopCategoryInfo fromDictionary:fieldsDict];
}

@end

@implementation MNVShopProviderCheckoutVShopPackSuccessInfo(MNUnity)

- (id)proxyForJson {
 NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:1];
 
 id localTransaction = self.transaction;
 [fieldsDict setObject:(localTransaction == nil) ? [NSNull null] : localTransaction forKey:@"Transaction"];
 
 
 return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
 MNVShopProviderCheckoutVShopPackSuccessInfo *object = [[[MNVShopProviderCheckoutVShopPackSuccessInfo alloc]initWithTransactionInfo:[MNVItemsTransactionInfo fromDictionary:[fieldsDict objectForKey:@"Transaction"]]]autorelease];
 
 return object;
}
+ (id)fromJson:(NSString*)jsonString {
 NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
 
 return [MNVShopProviderCheckoutVShopPackSuccessInfo fromDictionary:fieldsDict];
}

@end

@implementation MNVShopProviderCheckoutVShopPackFailInfo(MNUnity)

- (id)proxyForJson {
 NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:3];
 
 id localErrorMessage = self.errorMessage;
 [fieldsDict setObject:(localErrorMessage == nil) ? [NSNull null] : localErrorMessage forKey:@"ErrorMessage"];
 
 id localClientTransactionId = [NSNumber numberWithLongLong:self.clientTransactionId];
 [fieldsDict setObject:(localClientTransactionId == nil) ? [NSNull null] : localClientTransactionId forKey:@"ClientTransactionId"];
 
 id localErrorCode = [NSNumber numberWithInt:self.errorCode];
 [fieldsDict setObject:(localErrorCode == nil) ? [NSNull null] : localErrorCode forKey:@"ErrorCode"];
 
 
 return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
 MNVShopProviderCheckoutVShopPackFailInfo *object = [[[MNVShopProviderCheckoutVShopPackFailInfo alloc]initWithErrorCode:((NSNumber*)[fieldsDict objectForKey:@"ErrorCode"]).intValue
                                                                                                           errorMessage:[fieldsDict objectForKey:@"ErrorMessage"]
                                                                                                 andClientTransactionId:((NSNumber*)[fieldsDict objectForKey:@"ClientTransactionId"]).longLongValue]autorelease];
 
 return object;
}
+ (id)fromJson:(NSString*)jsonString {
 NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
 
 return [MNVShopProviderCheckoutVShopPackFailInfo fromDictionary:fieldsDict];
}

@end

@implementation MNGameSettingInfo(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:4];
    
    id localSysParams = self.sysParams;
    [fieldsDict setObject:(localSysParams == nil) ? [NSNull null] : localSysParams forKey:@"SysParams"];
    
    id localName = self.name;
    [fieldsDict setObject:(localName == nil) ? [NSNull null] : localName forKey:@"Name"];
    
    id localParams = self.params;
    [fieldsDict setObject:(localParams == nil) ? [NSNull null] : localParams forKey:@"Params"];
    
    id localId = [NSNumber numberWithInt:self.gameSetId];
    [fieldsDict setObject:(localId == nil) ? [NSNull null] : localId forKey:@"Id"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNGameSettingInfo *object = [[[MNGameSettingInfo alloc]initWithId:((NSNumber*)[fieldsDict objectForKey:@"Id"]).intValue
                                                              andName:[fieldsDict objectForKey:@"Name"]]autorelease];

    object.sysParams = [fieldsDict objectForKey:@"SysParams"];
    object.params = [fieldsDict objectForKey:@"Params"];
    object.isMultiplayerEnabled = ((NSNumber*)[fieldsDict objectForKey:@"MultiplayerEnabled"]).boolValue;
    object.isLeaderboardVisible = ((NSNumber*)[fieldsDict objectForKey:@"LeaderboardVisible"]).boolValue;
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNGameSettingInfo fromDictionary:fieldsDict];
}

@end

@implementation MNWSInfoRequestResult(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:3];
    
    [fieldsDict setObject:[NSNumber numberWithBool:[self hadError]]  forKey:@"HadError"];
    
    id localErrorMessage = [self getErrorMessage];
    [fieldsDict setObject:(localErrorMessage == nil) ? [NSNull null] : localErrorMessage forKey:@"ErrorMessage"];
    
    id localDataEntry = [(id)self getDataEntry];
    [fieldsDict setObject:(localDataEntry == nil) ? [NSNull null] : localDataEntry forKey:@"DataEntry"];
    
    return fieldsDict;
}
@end

@implementation MNWSAnyGameItem(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:9];
    
    id localGameStatus = [self getGameStatus];
    [fieldsDict setObject:(localGameStatus == nil) ? [NSNull null] : localGameStatus forKey:@"GameStatus"];
    
    id localGameGenreId = [self getGameGenreId];
    [fieldsDict setObject:(localGameGenreId == nil) ? [NSNull null] : localGameGenreId forKey:@"GameGenreId"];
    
    id localGameFlags = [self getGameFlags];
    [fieldsDict setObject:(localGameFlags == nil) ? [NSNull null] : localGameFlags forKey:@"GameFlags"];
    
    id localGameName = [self getGameName];
    [fieldsDict setObject:(localGameName == nil) ? [NSNull null] : localGameName forKey:@"GameName"];
    
    id localGameIconUrl = [self getGameIconUrl];
    [fieldsDict setObject:(localGameIconUrl == nil) ? [NSNull null] : localGameIconUrl forKey:@"GameIconUrl"];
    
    id localGamePlayModel = [self getGamePlayModel];
    [fieldsDict setObject:(localGamePlayModel == nil) ? [NSNull null] : localGamePlayModel forKey:@"GamePlayModel"];
    
    id localDeveloperId = [self getDeveloperId];
    [fieldsDict setObject:(localDeveloperId == nil) ? [NSNull null] : localDeveloperId forKey:@"DeveloperId"];
    
    id localGameDesc = [self getGameDesc];
    [fieldsDict setObject:(localGameDesc == nil) ? [NSNull null] : localGameDesc forKey:@"GameDesc"];
    
    id localGameId = [self getGameId];
    [fieldsDict setObject:(localGameId == nil) ? [NSNull null] : localGameId forKey:@"GameId"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSAnyUserItem(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:7];
    
    id localMyFriendLinkStatus = [self getMyFriendLinkStatus];
    [fieldsDict setObject:(localMyFriendLinkStatus == nil) ? [NSNull null] : localMyFriendLinkStatus forKey:@"MyFriendLinkStatus"];
    
    id localUserGamePoints = [self getUserGamePoints];
    [fieldsDict setObject:(localUserGamePoints == nil) ? [NSNull null] : localUserGamePoints forKey:@"UserGamePoints"];
    
    id localUserId = [self getUserId];
    [fieldsDict setObject:(localUserId == nil) ? [NSNull null] : localUserId forKey:@"UserId"];
    
    id localUserAvatarExists = [self getUserAvatarExists];
    [fieldsDict setObject:(localUserAvatarExists == nil) ? [NSNull null] : localUserAvatarExists forKey:@"UserAvatarExists"];
    
    id localUserOnlineNow = [self getUserOnlineNow];
    [fieldsDict setObject:(localUserOnlineNow == nil) ? [NSNull null] : localUserOnlineNow forKey:@"UserOnlineNow"];
    
    id localUserAvatarUrl = [self getUserAvatarUrl];
    [fieldsDict setObject:(localUserAvatarUrl == nil) ? [NSNull null] : localUserAvatarUrl forKey:@"UserAvatarUrl"];
    
    id localUserNickName = [self getUserNickName];
    [fieldsDict setObject:(localUserNickName == nil) ? [NSNull null] : localUserNickName forKey:@"UserNickName"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSUserGameCookie(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:3];
    
    id localUserId = [self getUserId];
    [fieldsDict setObject:(localUserId == nil) ? [NSNull null] : localUserId forKey:@"UserId"];
    
    id localCookieValue = [self getCookieValue];
    [fieldsDict setObject:(localCookieValue == nil) ? [NSNull null] : localCookieValue forKey:@"CookieValue"];
    
    id localCookieKey = [self getCookieKey];
    //DLog(@"localCookieKey = %@", localCookieKey == nil ? @"nil" : [localCookieKey stringValue]);
    [fieldsDict setObject:(localCookieKey == nil) ? [NSNull null] : localCookieKey forKey:@"CookieKey"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSCurrentUserInfo(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:10];
    
    id localUserAvatarHasExternalUrl = [self getUserAvatarHasExternalUrl];
    [fieldsDict setObject:(localUserAvatarHasExternalUrl == nil) ? [NSNull null] : localUserAvatarHasExternalUrl forKey:@"UserAvatarHasExternalUrl"];
    
    id localUserAvatarUrl = [self getUserAvatarUrl];
    [fieldsDict setObject:(localUserAvatarUrl == nil) ? [NSNull null] : localUserAvatarUrl forKey:@"UserAvatarUrl"];
    
    id localUserStatus = [self getUserStatus];
    [fieldsDict setObject:(localUserStatus == nil) ? [NSNull null] : localUserStatus forKey:@"UserStatus"];
    
    id localUserGamePoints = [self getUserGamePoints];
    [fieldsDict setObject:(localUserGamePoints == nil) ? [NSNull null] : localUserGamePoints forKey:@"UserGamePoints"];
    
    id localUserEmail = [self getUserEmail];
    [fieldsDict setObject:(localUserEmail == nil) ? [NSNull null] : localUserEmail forKey:@"UserEmail"];
    
    id localUserOnlineNow = [self getUserOnlineNow];
    [fieldsDict setObject:(localUserOnlineNow == nil) ? [NSNull null] : localUserOnlineNow forKey:@"UserOnlineNow"];
    
    id localUserAvatarExists = [self getUserAvatarExists];
    [fieldsDict setObject:(localUserAvatarExists == nil) ? [NSNull null] : localUserAvatarExists forKey:@"UserAvatarExists"];
    
    id localUserId = [self getUserId];
    [fieldsDict setObject:(localUserId == nil) ? [NSNull null] : localUserId forKey:@"UserId"];
    
    id localUserNickName = [self getUserNickName];
    [fieldsDict setObject:(localUserNickName == nil) ? [NSNull null] : localUserNickName forKey:@"UserNickName"];
    
    id localUserAvatarHasCustomImg = [self getUserAvatarHasCustomImg];
    [fieldsDict setObject:(localUserAvatarHasCustomImg == nil) ? [NSNull null] : localUserAvatarHasCustomImg forKey:@"UserAvatarHasCustomImg"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSRoomListItem(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:6];
    
    id localRoomName = [self getRoomName];
    [fieldsDict setObject:(localRoomName == nil) ? [NSNull null] : localRoomName forKey:@"RoomName"];
    
    id localRoomSFId = [self getRoomSFId];
    [fieldsDict setObject:(localRoomSFId == nil) ? [NSNull null] : localRoomSFId forKey:@"RoomSFId"];
    
    id localGameId = [self getGameId];
    [fieldsDict setObject:(localGameId == nil) ? [NSNull null] : localGameId forKey:@"GameId"];
    
    id localRoomUserCount = [self getRoomUserCount];
    [fieldsDict setObject:(localRoomUserCount == nil) ? [NSNull null] : localRoomUserCount forKey:@"RoomUserCount"];
    
    id localGameSetId = [self getGameSetId];
    [fieldsDict setObject:(localGameSetId == nil) ? [NSNull null] : localGameSetId forKey:@"GameSetId"];
    
    id localRoomIsLobby = [self getRoomIsLobby];
    [fieldsDict setObject:(localRoomIsLobby == nil) ? [NSNull null] : localRoomIsLobby forKey:@"RoomIsLobby"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSRoomUserInfoItem(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:6];
    
    id localRoomSFId = [self getRoomSFId];
    [fieldsDict setObject:(localRoomSFId == nil) ? [NSNull null] : localRoomSFId forKey:@"RoomSFId"];
    
    id localUserId = [self getUserId];
    [fieldsDict setObject:(localUserId == nil) ? [NSNull null] : localUserId forKey:@"UserId"];
    
    id localUserAvatarExists = [self getUserAvatarExists];
    [fieldsDict setObject:(localUserAvatarExists == nil) ? [NSNull null] : localUserAvatarExists forKey:@"UserAvatarExists"];
    
    id localUserOnlineNow = [self getUserOnlineNow];
    [fieldsDict setObject:(localUserOnlineNow == nil) ? [NSNull null] : localUserOnlineNow forKey:@"UserOnlineNow"];
    
    id localUserAvatarUrl = [self getUserAvatarUrl];
    [fieldsDict setObject:(localUserAvatarUrl == nil) ? [NSNull null] : localUserAvatarUrl forKey:@"UserAvatarUrl"];
    
    id localUserNickName = [self getUserNickName];
    [fieldsDict setObject:(localUserNickName == nil) ? [NSNull null] : localUserNickName forKey:@"UserNickName"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSBuddyListItem(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:19];
    
    id localFriendSnId = [self getFriendSnId];
    [fieldsDict setObject:(localFriendSnId == nil) ? [NSNull null] : localFriendSnId forKey:@"FriendSnId"];
    
    id localFriendHasCurrentGame = [self getFriendHasCurrentGame];
    [fieldsDict setObject:(localFriendHasCurrentGame == nil) ? [NSNull null] : localFriendHasCurrentGame forKey:@"FriendHasCurrentGame"];
    
    id localFriendSnUserAsnId = [self getFriendSnUserAsnId];
    [fieldsDict setObject:(localFriendSnUserAsnId == nil) ? [NSNull null] : localFriendSnUserAsnId forKey:@"FriendSnUserAsnId"];
    
    id localFriendInGameId = [self getFriendInGameId];
    [fieldsDict setObject:(localFriendInGameId == nil) ? [NSNull null] : localFriendInGameId forKey:@"FriendInGameId"];
    
    id localFriendSnUserAsnIdList = [self getFriendSnUserAsnIdList];
    [fieldsDict setObject:(localFriendSnUserAsnIdList == nil) ? [NSNull null] : localFriendSnUserAsnIdList forKey:@"FriendSnUserAsnIdList"];
    
    id localFriendUserAvatarUrl = [self getFriendUserAvatarUrl];
    [fieldsDict setObject:(localFriendUserAvatarUrl == nil) ? [NSNull null] : localFriendUserAvatarUrl forKey:@"FriendUserAvatarUrl"];
    
    id localFriendUserOnlineNow = [self getFriendUserOnlineNow];
    [fieldsDict setObject:(localFriendUserOnlineNow == nil) ? [NSNull null] : localFriendUserOnlineNow forKey:@"FriendUserOnlineNow"];
    
    id localFriendIsIgnored = [self getFriendIsIgnored];
    [fieldsDict setObject:(localFriendIsIgnored == nil) ? [NSNull null] : localFriendIsIgnored forKey:@"FriendIsIgnored"];
    
    id localFriendFlags = [self getFriendFlags];
    [fieldsDict setObject:(localFriendFlags == nil) ? [NSNull null] : localFriendFlags forKey:@"FriendFlags"];
    
    id localFriendCurrGameAchievementsList = [self getFriendCurrGameAchievementsList];
    [fieldsDict setObject:(localFriendCurrGameAchievementsList == nil) ? [NSNull null] : localFriendCurrGameAchievementsList forKey:@"FriendCurrGameAchievementsList"];
    
    id localFriendInGameName = [self getFriendInGameName];
    [fieldsDict setObject:(localFriendInGameName == nil) ? [NSNull null] : localFriendInGameName forKey:@"FriendInGameName"];
    
    id localFriendUserLocale = [self getFriendUserLocale];
    [fieldsDict setObject:(localFriendUserLocale == nil) ? [NSNull null] : localFriendUserLocale forKey:@"FriendUserLocale"];
    
    id localFriendSnIdList = [self getFriendSnIdList];
    [fieldsDict setObject:(localFriendSnIdList == nil) ? [NSNull null] : localFriendSnIdList forKey:@"FriendSnIdList"];
    
    id localFriendUserId = [self getFriendUserId];
    [fieldsDict setObject:(localFriendUserId == nil) ? [NSNull null] : localFriendUserId forKey:@"FriendUserId"];
    
    id localFriendInRoomIsLobby = [self getFriendInRoomIsLobby];
    [fieldsDict setObject:(localFriendInRoomIsLobby == nil) ? [NSNull null] : localFriendInRoomIsLobby forKey:@"FriendInRoomIsLobby"];
    
    id localFriendUserSfid = [self getFriendUserSfid];
    [fieldsDict setObject:(localFriendUserSfid == nil) ? [NSNull null] : localFriendUserSfid forKey:@"FriendUserSfid"];
    
    id localFriendUserNickName = [self getFriendUserNickName];
    [fieldsDict setObject:(localFriendUserNickName == nil) ? [NSNull null] : localFriendUserNickName forKey:@"FriendUserNickName"];
    
    id localFriendInGameIconUrl = [self getFriendInGameIconUrl];
    [fieldsDict setObject:(localFriendInGameIconUrl == nil) ? [NSNull null] : localFriendInGameIconUrl forKey:@"FriendInGameIconUrl"];
    
    id localFriendInRoomSfid = [self getFriendInRoomSfid];
    [fieldsDict setObject:(localFriendInRoomSfid == nil) ? [NSNull null] : localFriendInRoomSfid forKey:@"FriendInRoomSfid"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSCurrUserSubscriptionStatus(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:3];
    
    id localOffersAvailable = [self getOffersAvailable];
    [fieldsDict setObject:(localOffersAvailable == nil) ? [NSNull null] : localOffersAvailable forKey:@"OffersAvailable"];
    
    id localHasSubscription = [self getHasSubscription];
    [fieldsDict setObject:(localHasSubscription == nil) ? [NSNull null] : localHasSubscription forKey:@"HasSubscription"];
    
    id localIsSubscriptionAvailable = [self getIsSubscriptionAvailable];
    [fieldsDict setObject:(localIsSubscriptionAvailable == nil) ? [NSNull null] : localIsSubscriptionAvailable forKey:@"IsSubscriptionAvailable"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSSessionSignedClientToken(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:2];
    
    id localClientTokenBody = [self getClientTokenBody];
    [fieldsDict setObject:(localClientTokenBody == nil) ? [NSNull null] : localClientTokenBody forKey:@"ClientTokenBody"];
    
    id localClientTokenSign = [self getClientTokenSign];
    [fieldsDict setObject:(localClientTokenSign == nil) ? [NSNull null] : localClientTokenSign forKey:@"ClientTokenSign"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSSystemGameNetStats(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:7];
    
    id localGameOnlineRooms = [self getGameOnlineRooms];
    [fieldsDict setObject:(localGameOnlineRooms == nil) ? [NSNull null] : localGameOnlineRooms forKey:@"GameOnlineRooms"];
    
    id localGameOnlineUsers = [self getGameOnlineUsers];
    [fieldsDict setObject:(localGameOnlineUsers == nil) ? [NSNull null] : localGameOnlineUsers forKey:@"GameOnlineUsers"];
    
    id localServTotalGames = [self getServTotalGames];
    [fieldsDict setObject:(localServTotalGames == nil) ? [NSNull null] : localServTotalGames forKey:@"ServTotalGames"];
    
    id localServTotalUsers = [self getServTotalUsers];
    [fieldsDict setObject:(localServTotalUsers == nil) ? [NSNull null] : localServTotalUsers forKey:@"ServTotalUsers"];
    
    id localServOnlineRooms = [self getServOnlineRooms];
    [fieldsDict setObject:(localServOnlineRooms == nil) ? [NSNull null] : localServOnlineRooms forKey:@"ServOnlineRooms"];
    
    id localServOnlineGames = [self getServOnlineGames];
    [fieldsDict setObject:(localServOnlineGames == nil) ? [NSNull null] : localServOnlineGames forKey:@"ServOnlineGames"];
    
    id localServOnlineUsers = [self getServOnlineUsers];
    [fieldsDict setObject:(localServOnlineUsers == nil) ? [NSNull null] : localServOnlineUsers forKey:@"ServOnlineUsers"];
    
    
    return fieldsDict;
}
@end

@implementation MNWSLeaderboardListItem(MNUnity)
- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:16];
    
    id localUserAchievementsList = [self getUserAchievementsList];
    [fieldsDict setObject:(localUserAchievementsList == nil) ? [NSNull null] : localUserAchievementsList forKey:@"UserAchievementsList"];
    
    id localUserAvatarUrl = [self getUserAvatarUrl];
    [fieldsDict setObject:(localUserAvatarUrl == nil) ? [NSNull null] : localUserAvatarUrl forKey:@"UserAvatarUrl"];
    
    id localOutHiDateTimeDiff = [self getOutHiDateTimeDiff];
    [fieldsDict setObject:(localOutHiDateTimeDiff == nil) ? [NSNull null] : localOutHiDateTimeDiff forKey:@"OutHiDateTimeDiff"];
    
    id localUserIsFriend = [self getUserIsFriend];
    [fieldsDict setObject:(localUserIsFriend == nil) ? [NSNull null] : localUserIsFriend forKey:@"UserIsFriend"];
    
    id localOutHiDateTime = [self getOutHiDateTime];
    [fieldsDict setObject:(localOutHiDateTime == nil) ? [NSNull null] : localOutHiDateTime forKey:@"OutHiDateTime"];
    
    id localGamesetId = [self getGamesetId];
    [fieldsDict setObject:(localGamesetId == nil) ? [NSNull null] : localGamesetId forKey:@"GamesetId"];
    
    id localOutUserPlace = [self getOutUserPlace];
    [fieldsDict setObject:(localOutUserPlace == nil) ? [NSNull null] : localOutUserPlace forKey:@"OutUserPlace"];
    
    id localUserOnlineNow = [self getUserOnlineNow];
    [fieldsDict setObject:(localUserOnlineNow == nil) ? [NSNull null] : localUserOnlineNow forKey:@"UserOnlineNow"];
    
    id localUserLocale = [self getUserLocale];
    [fieldsDict setObject:(localUserLocale == nil) ? [NSNull null] : localUserLocale forKey:@"UserLocale"];
    
    id localOutHiScoreText = [self getOutHiScoreText];
    [fieldsDict setObject:(localOutHiScoreText == nil) ? [NSNull null] : localOutHiScoreText forKey:@"OutHiScoreText"];
    
    id localUserId = [self getUserId];
    [fieldsDict setObject:(localUserId == nil) ? [NSNull null] : localUserId forKey:@"UserId"];
    
    id localOutHiScore = [self getOutHiScore];
    [fieldsDict setObject:(localOutHiScore == nil) ? [NSNull null] : localOutHiScore forKey:@"OutHiScore"];
    
    id localUserNickName = [self getUserNickName];
    [fieldsDict setObject:(localUserNickName == nil) ? [NSNull null] : localUserNickName forKey:@"UserNickName"];
    
    id localUserIsIgnored = [self getUserIsIgnored];
    [fieldsDict setObject:(localUserIsIgnored == nil) ? [NSNull null] : localUserIsIgnored forKey:@"UserIsIgnored"];
    
    id localGameId = [self getGameId];
    [fieldsDict setObject:(localGameId == nil) ? [NSNull null] : localGameId forKey:@"GameId"];
    
    id localUserSfid = [self getUserSfid];
    [fieldsDict setObject:(localUserSfid == nil) ? [NSNull null] : localUserSfid forKey:@"UserSfid"];
    
    
    return fieldsDict;
}
@end

@implementation MNScoreProgressProviderItem(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:3];
    
    id localPlace = [NSNumber numberWithInt:self.place];
    [fieldsDict setObject:(localPlace == nil) ? [NSNull null] : localPlace forKey:@"Place"];
    
    id localUserInfo = self.userInfo;
    [fieldsDict setObject:(localUserInfo == nil) ? [NSNull null] : localUserInfo forKey:@"UserInfo"];
    
    id localScore = [NSNumber numberWithLongLong:self.score];
    [fieldsDict setObject:(localScore == nil) ? [NSNull null] : localScore forKey:@"Score"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNScoreProgressProviderItem *object = [[[MNScoreProgressProviderItem alloc]initWithUserInfo:[fieldsDict objectForKey:@"UserInfo"]
                                                                                          score:((NSNumber*)[fieldsDict objectForKey:@"Score"]).longLongValue
                                                                                       andPlace:((NSNumber*)[fieldsDict objectForKey:@"Place"]).intValue]autorelease];
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNScoreProgressProviderItem fromDictionary:fieldsDict];
}

@end


@implementation MNBuddyRoomParams(MNUnity)

- (id)proxyForJson {
    NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:5];
    
    id localRoomName = self.roomName;
    [fieldsDict setObject:(localRoomName == nil) ? [NSNull null] : localRoomName forKey:@"RoomName"];
    
    id localInviteText = self.inviteText;
    [fieldsDict setObject:(localInviteText == nil) ? [NSNull null] : localInviteText forKey:@"InviteText"];
    
    id localToUserIdList = self.toUserIdList;
    [fieldsDict setObject:(localToUserIdList == nil) ? [NSNull null] : localToUserIdList forKey:@"ToUserIdList"];
    
    id localToUserSFIdList = self.toUserSFIdList;
    [fieldsDict setObject:(localToUserSFIdList == nil) ? [NSNull null] : localToUserSFIdList forKey:@"ToUserSFIdList"];
    
    id localGameSetId = [NSNumber numberWithInteger:self.gameSetId];
    [fieldsDict setObject:(localGameSetId == nil) ? [NSNull null] : localGameSetId forKey:@"GameSetId"];
    
    
    return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNBuddyRoomParams *object = [[[MNBuddyRoomParams alloc]init]autorelease];
    
    object.roomName = [fieldsDict objectForKey:@"RoomName"];
    object.gameSetId = ((NSNumber*)[fieldsDict objectForKey:@"GameSetId"]).integerValue;
    object.toUserIdList = [fieldsDict objectForKey:@"ToUserIdList"];
    object.toUserSFIdList = [fieldsDict objectForKey:@"ToUserSFIdList"];
    object.inviteText = [fieldsDict objectForKey:@"InviteText"];
    
    
    return object;
}
+ (id)fromJson:(NSString*)jsonString {
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNBuddyRoomParams fromDictionary:fieldsDict];
}

@end

@implementation MNJoinRoomInvitationParams(MNUnity)

- (id)proxyForJson {
 NSMutableDictionary *fieldsDict = [NSMutableDictionary dictionaryWithCapacity:7];
 
 id localRoomGameId = [NSNumber numberWithInt:self.roomGameId];
 [fieldsDict setObject:(localRoomGameId == nil) ? [NSNull null] : localRoomGameId forKey:@"RoomGameId"];
 
 id localRoomName = self.roomName;
 [fieldsDict setObject:(localRoomName == nil) ? [NSNull null] : localRoomName forKey:@"RoomName"];
 
 id localRoomSFId = [NSNumber numberWithInt:self.roomSFId];
 [fieldsDict setObject:(localRoomSFId == nil) ? [NSNull null] : localRoomSFId forKey:@"RoomSFId"];
 
 id localInviteText = self.inviteText;
 [fieldsDict setObject:(localInviteText == nil) ? [NSNull null] : localInviteText forKey:@"InviteText"];
 
 id localRoomGameSetId = [NSNumber numberWithInt:self.roomGameSetId];
 [fieldsDict setObject:(localRoomGameSetId == nil) ? [NSNull null] : localRoomGameSetId forKey:@"RoomGameSetId"];
 
 id localFromUserName = self.fromUserName;
 [fieldsDict setObject:(localFromUserName == nil) ? [NSNull null] : localFromUserName forKey:@"FromUserName"];
 
 id localFromUserSFId = [NSNumber numberWithInt:self.fromUserSFId];
 [fieldsDict setObject:(localFromUserSFId == nil) ? [NSNull null] : localFromUserSFId forKey:@"FromUserSFId"];
 
 
 return fieldsDict;
}
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MNJoinRoomInvitationParams *object = [[[MNJoinRoomInvitationParams alloc]init]autorelease];
    
    object.roomGameId = ((NSNumber*)[fieldsDict objectForKey:@"RoomGameId"]).intValue;
    object.roomName = [fieldsDict objectForKey:@"RoomName"];
    object.roomSFId = ((NSNumber*)[fieldsDict objectForKey:@"RoomSFId"]).intValue;
    object.inviteText = [fieldsDict objectForKey:@"InviteText"];
    object.roomGameSetId = ((NSNumber*)[fieldsDict objectForKey:@"RoomGameSetId"]).intValue;
    object.fromUserName = [fieldsDict objectForKey:@"FromUserName"];
    object.fromUserSFId = ((NSNumber*)[fieldsDict objectForKey:@"FromUserSFId"]).intValue;
                                        
 
 return object;
}
+ (id)fromJson:(NSString*)jsonString {
 NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
 
 return [MNJoinRoomInvitationParams fromDictionary:fieldsDict];
}

@end


