//
//  MNWSProviderUnity.m
//  Unity-iPhone
//
//  Created by Vladislav Ogol on 26.01.12.
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import <objc/runtime.h>
#import <sys/time.h>

#import "JSON.h"

#import "MNDirect.h"
#import "MNWSProvider.h"
#import "MNWSProviderUnity.h"


#define LOADER_ID_INVALID  (-1)
#define REQUEST_ID_INVALID  (-1)

static NSMutableDictionary *MNUnityLoaderDict        = nil;
static NSMutableDictionary *MNUnityRequestLoaderDict = nil;

@interface MNWSProviderUnityRequest : NSObject
@property (retain, nonatomic) NSNumber *requestId;
@property (retain, nonatomic) NSString* name;
@property (retain, nonatomic) MNWSInfoRequest *mnWSInfoRequest;
@end

#pragma mark - MNWSProviderUnity

@interface MNWSProviderUnity : NSObject {
    long long _loaderId;
}

+ (id)shared;

- (long long)sendRequest:(NSString *)request;

- (long long)getLoderId;

@end

static MNWSProviderUnity *MNWSProviderUnityInstance = nil;

@implementation MNWSProviderUnity

+ (id)shared {
    //Begin Sync?
    if (MNWSProviderUnityInstance == nil) {
        MNWSProviderUnityInstance = [[MNWSProviderUnity alloc]init];
        
        MNUnityLoaderDict         = [[NSMutableDictionary alloc]init];
        MNUnityRequestLoaderDict  = [[NSMutableDictionary alloc]init];        
    }
    //End Sync?
    
    return MNWSProviderUnityInstance;
}

- (id)init {
    self = [super init];
    
    if (self) {
        struct timeval currentTime;
        
        gettimeofday(&currentTime,NULL);
        
        _loaderId = (long long)currentTime.tv_sec * 1000 + currentTime.tv_usec / 1000;
    }
    
    return self;
}

- (void)dealloc {
    _loaderId = LOADER_ID_INVALID;
    
    if ([MNUnityLoaderDict count] > 0) {
        DLog(@"MNUnityLoaderDict release while it not empty");
    }
    
    if ([MNUnityRequestLoaderDict count] > 0) {
        DLog(@"MNUnityRequestLoaderDict release while it not empty");
    }
    
    [MNUnityLoaderDict release];
    MNUnityLoaderDict = nil;
    
    [MNUnityRequestLoaderDict release];
    MNUnityRequestLoaderDict = nil;
    
    [super dealloc];
}

- (long long)sendRequest:(NSString *)request {
    //Begin Sync?
    long long currentLoaderId = [self getLoderId];
    
    NSArray *unityRequestsArray = [[MNUnity serializer]deserializeArray:[MNWSProviderUnityRequest class] fromJson:request];
    NSMutableArray *requestsArray = [NSMutableArray arrayWithCapacity:[unityRequestsArray count]];
    
    for (MNWSProviderUnityRequest *unityRequest in unityRequestsArray) {
        [requestsArray addObject:unityRequest.mnWSInfoRequest];
        [MNUnityRequestLoaderDict setObject:[NSNumber numberWithLongLong:currentLoaderId]
                                     forKey:unityRequest.requestId];
    }
    
    MNWSLoader *loader = [[MNDirect wsProvider] sendBatch:requestsArray];
    [MNUnityLoaderDict setObject:loader forKey:[NSNumber numberWithLongLong:currentLoaderId]];
    //End Sync?
    
    return currentLoaderId;
}

- (long long)getLoderId {
    //Begin Sync?
    _loaderId++;
    
    return _loaderId;
    //End Sync?
}

@end

#pragma mark - MNWSInfoRequestDelegate

@interface MNWSInfoRequestDelegate : NSObject <MNWSInfoRequestAnyGameDelegate,
                                               MNWSInfoRequestAnyUserDelegate,
                                               MNWSInfoRequestAnyUserGameCookiesDelegate,
                                               MNWSInfoRequestCurrentUserInfoDelegate,
                                               MNWSInfoRequestCurrGameRoomListDelegate,
                                               MNWSInfoRequestCurrGameRoomUserListDelegate,
                                               MNWSInfoRequestCurrUserBuddyListDelegate,
                                               MNWSInfoRequestCurrUserSubscriptionStatusDelegate,
                                               MNWSInfoRequestSessionSignedClientTokenDelegate,
                                               MNWSInfoRequestSystemGameNetStatsDelegate,
                                               MNWSInfoRequestLeaderboardDelegate>

@property (retain, nonatomic) NSNumber *requestId;

- (id)initWithRequestId:(NSNumber*) requestId;

@end

@implementation MNWSInfoRequestDelegate
@synthesize requestId = _requestId;

- (id)initWithRequestId:(NSNumber*) requestId {
    self = [super init];
    
    if (self) {
        _requestId = [requestId retain];
    }
    
    return  self;
}

- (void)dealloc {
    self.requestId = nil;
    
    [super dealloc];
}

- (void)clearStoredValues {
    [MNUnityLoaderDict        removeObjectForKey:[MNUnityRequestLoaderDict objectForKey:self.requestId]];
    [MNUnityRequestLoaderDict removeObjectForKey:self.requestId];
}

- (void)callUnityEventHandlerWithResult:(id)result {
    [self clearStoredValues];
    
    [MNUnity callUnityFunction:@"MNUM_MNWSInfoRequestEventHandler" withParams:self.requestId,result,nil];
}

-(void) mnWSInfoRequestAnyGameCompleted:(MNWSInfoRequestResultAnyGame*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestAnyUserCompleted:(MNWSInfoRequestResultAnyUser*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestAnyUserGameCookiesCompleted:(MNWSInfoRequestResultAnyUserGameCookies*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestCurrentUserInfoCompleted:(MNWSInfoRequestResultCurrentUserInfo*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestCurrGameRoomListCompleted:(MNWSInfoRequestResultCurrGameRoomList*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestCurrGameRoomUserListCompleted:(MNWSInfoRequestResultCurrGameRoomUserList*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestCurrUserBuddyListCompleted:(MNWSInfoRequestResultCurrUserBuddyList*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestCurrUserSubscriptionStatusCompleted:(MNWSInfoRequestResultCurrUserSubscriptionStatus*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestSessionSignedClientTokenCompleted:(MNWSInfoRequestResultSessionSignedClientToken*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestSystemGameNetStatsCompleted:(MNWSInfoRequestResultSystemGameNetStats*) result {
    [self callUnityEventHandlerWithResult:result];
}

-(void) mnWSInfoRequestLeaderboardCompleted:(MNWSInfoRequestResultLeaderboard*) result {
    [self callUnityEventHandlerWithResult:result];
}

@end

#pragma mark - MNWSProviderUnityRequest

@implementation MNWSProviderUnityRequest
@synthesize requestId = _requestId;
@synthesize name = _name;
@synthesize mnWSInfoRequest = _mnWSInfoRequest;

- (id)init {
    self = [super init];
    
    if (self) {
        _requestId = nil;
        _name = nil;
        _mnWSInfoRequest = nil;
    }
    
    return self;
}

- (void)dealloc {
    self.name = nil;
    self.mnWSInfoRequest = nil;
    self.requestId = nil;
    
    [super dealloc];
}

+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MARK;
    
    MNWSProviderUnityRequest *object = [[[MNWSProviderUnityRequest alloc]init]autorelease];
    object.requestId = [fieldsDict objectForKey:@"Id"];
    object.name = [fieldsDict objectForKey:@"Name"];
    NSDictionary *params = [fieldsDict objectForKey:@"Parameters"];

    object.mnWSInfoRequest = [NSClassFromString(object.name) fromDictionary:params withId:object.requestId];
    
    return object;
}

+ (id)fromJson:(NSString*)jsonString {
    MARK;
    NSDictionary *fieldsDict = [[[[SBJsonParser alloc]init]autorelease]objectWithString:jsonString];
    
    return [MNWSProviderUnityRequest fromDictionary:fieldsDict];
}

@end

#pragma mark - MNWSInfoRequests

@implementation MNWSInfoRequestAnyGame(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestAnyGame mnWSInfoRequestAnyGameWithGameId:((NSNumber*)[fieldsDict objectForKey:@"gameId"]).intValue
                                                        andDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestAnyUser(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestAnyUser mnWSInfoRequestAnyUserWithUserId:((NSNumber*)[fieldsDict objectForKey:@"userId"]).intValue
                                                        andDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestAnyUserGameCookies(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestAnyUserGameCookies mnWSInfoRequestAnyUserGameCookiesWithUserIdList:[fieldsDict objectForKey:@"userIdList"] 
                                                                                cookieKeyList:[fieldsDict objectForKey:@"cookieKeyList"]
                                                                                  andDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestCurrentUserInfo(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestCurrentUserInfo mnWSInfoRequestCurrentUserInfoWithDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestCurrGameRoomList(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestCurrGameRoomList mnWSInfoRequestCurrGameRoomListWithDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestCurrGameRoomUserList(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestCurrGameRoomUserList mnWSInfoRequestCurrGameRoomUserListWithRoomSFId:((NSNumber*)[fieldsDict objectForKey:@"roomSFId"]).intValue
                                                                                  andDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestCurrUserBuddyList(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestCurrUserBuddyList mnWSInfoRequestCurrUserBuddyListWithDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestCurrUserSubscriptionStatus(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestCurrUserSubscriptionStatus mnWSInfoRequestCurrUserSubscriptionStatusWithDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestSessionSignedClientToken(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestSessionSignedClientToken mnWSInfoRequestSessionSignedClientTokenWithPayload:[fieldsDict objectForKey:@"payload"]
                                                                                  andDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestSystemGameNetStats(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestSystemGameNetStats mnWSInfoRequestSystemGameNetStatsWithDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

@implementation MNWSInfoRequestLeaderboardMode(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict {
    MARK;
    MNWSInfoRequestLeaderboardMode *mode = nil;
    NSString *modeName = [fieldsDict objectForKey:@"Mode"];
    
    if ([modeName isEqualToString:@"LeaderboardModeCurrentUser"]) {
        mode = [MNWSInfoRequestLeaderboardModeCurrentUser mnWSInfoRequestLeaderboardModeCurrentUserWithScope:((NSNumber*)[fieldsDict objectForKey:@"Scope"]).intValue
                                                                                                   andPeriod:((NSNumber*)[fieldsDict objectForKey:@"Period"]).intValue];
    }
    else if ([modeName isEqualToString:@"LeaderboardModeAnyGameGlobal"]) {
        mode = [MNWSInfoRequestLeaderboardModeAnyGameGlobal mnWSInfoRequestLeaderboardModeAnyGameGlobalWithGameId:((NSNumber*)[fieldsDict objectForKey:@"GameId"]).intValue
                                                                                                        gameSetId:((NSNumber*)[fieldsDict objectForKey:@"GameSetId"]).intValue
                                                                                                        andPeriod:((NSNumber*)[fieldsDict objectForKey:@"Period"]).intValue];
    }
    else if ([modeName isEqualToString:@"LeaderboardModeAnyUserAnyGameGlobal"]) {
        mode = [MNWSInfoRequestLeaderboardModeAnyUserAnyGameGlobal mnWSInfoRequestLeaderboardModeAnyUserAnyGameGlobalWithUserId:((NSNumber*)[fieldsDict objectForKey:@"UserId"]).longLongValue
                                                                                                                         gameId:((NSNumber*)[fieldsDict objectForKey:@"GameId"]).intValue
                                                                                                                      gameSetId:((NSNumber*)[fieldsDict objectForKey:@"GameSetId"]).intValue
                                                                                                                      andPeriod:((NSNumber*)[fieldsDict objectForKey:@"Period"]).intValue];
    }
    else if ([modeName isEqualToString:@"LeaderboardModeCurrUserAnyGameLocal"]) {
        mode = [MNWSInfoRequestLeaderboardModeCurrUserAnyGameLocal mnWSInfoRequestLeaderboardModeCurrUserAnyGameLocalWithGameId:((NSNumber*)[fieldsDict objectForKey:@"GameId"]).intValue
                                                                                                                      gameSetId:((NSNumber*)[fieldsDict objectForKey:@"GameSetId"]).intValue
                                                                                                                      andPeriod:((NSNumber*)[fieldsDict objectForKey:@"Period"]).intValue];
        
    }
    else {
        return nil;
    }
    
    return mode;
}
@end

@implementation MNWSInfoRequestLeaderboard(MNUnity)
+ (id)fromDictionary:(NSDictionary*)fieldsDict withId:(NSNumber*)requestId{
    MARK;
    return [MNWSInfoRequestLeaderboard mnWSInfoRequestLeaderboardWithMode:[MNWSInfoRequestLeaderboardMode fromDictionary:[fieldsDict objectForKey:@"LeaderboardMode"]]
                                                                                  andDelegate:[[[MNWSInfoRequestDelegate alloc]initWithRequestId:requestId]autorelease]];
}
@end

#pragma mark - Extern functions

EXTERN_C
const long long _MNWSProvider_Send(const char* request) {
    DLog(@"Reseiced request: %s", request);
    
    return [[MNWSProviderUnity shared]sendRequest:[NSString stringWithUTF8String:request]];
    
    /*
     [{"id":9001, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":1}}]
     
     [
     {"id":9001, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":1}},
     {"id":9002, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":2}},
     {"id":9003, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":3}}
     ]
     */
}

EXTERN_C
const void _MNWSProvider_CancelRequest(long long loaderId) {
    DLog(@"LoaderId: %lld", loaderId);
    //Begin Sync?
    MNWSLoader *loader = [MNUnityLoaderDict objectForKey:[NSNumber numberWithLongLong:loaderId]];
    
    if (loader != nil) {
        [MNUnityLoaderDict removeObjectForKey:[NSNumber numberWithLongLong:loaderId]];
        [loader cancel];
    }    
    //End Sync?
}

