//
//  MNScoreProgressView
//  MultiNet client
//
//  Created by Vladislav Ogol on 16.08.10.
//  Copyright 2010 PlayPhone. All rights reserved.
//
#import "MNDirect.h"
#import "MNScoreProgressProvider.h"
#import "MNScoreProgressView.h"

@interface MNScoreProgressView()
@property (nonatomic,retain) NSTimer *scoreResendTimer;
@property (nonatomic,assign) long long currentScore;

-(void) performActionForSessionStatus:(NSInteger) sessionStatus andUserStatus:(NSInteger)userStatus;
-(void) prepareView;
-(void) startScoreProgressProvider;
-(void) stopScoreProgressProvider;
-(void) setScoreCompareFunc:(MNScoreProgressProviderScoreCompareFunc) func withContext:(void*) context;
-(void) scoreResendTimerFire:(NSTimer*) timer;
-(void) scoresUpdated:(NSArray*) scoreProgressItems;
-(void) refreshScoreResendTimer;

@end


@implementation MNScoreProgressView

@synthesize currentScore;
@synthesize scoreResendTimer;
@synthesize scoreResendTimeout;

-(id) initWithFrame:(CGRect) frame {
    if (self = [super initWithFrame:frame]) {
        scoreCompareFunc         = MNScoreProgressProviderScoreCompareFuncMoreIsBetter;
        self.scoreResendTimeout  = MNScoreProgressScoreResendDefTimeout;
        
        self.backgroundColor = [UIColor clearColor];
        self.hidden          = YES;
        
        inited = NO;
    }
    
    return self;
}

- (id)initWithCoder:(NSCoder *)decoder {
    if (self = [super initWithCoder:decoder]) {
        scoreCompareFunc         = MNScoreProgressProviderScoreCompareFuncMoreIsBetter;
        self.scoreResendTimeout  = MNScoreProgressScoreResendDefTimeout;
        
        self.backgroundColor = [UIColor clearColor];
        self.hidden          = YES;
        
        inited = NO;
    }
    
    return self;
}

-(void) drawRect:(CGRect) rect {
    // Drawing code
}

-(void) dealloc {
    [self stopScoreProgressProvider];
    
    if (inited) {
        [[MNDirect scoreProgressProvider] removeDelegate:self];
        [[MNDirect getSession           ] removeDelegate:self];
        
        inited = NO;
    }
        
    [super dealloc];
}

-(void) sessionReady {
    [self checkProvider];
}

-(BOOL) checkProvider {
    if (inited) {
        return YES;
    }

    if (([MNDirect getSession           ] != nil) && 
        ([MNDirect scoreProgressProvider] != nil)) {

        [[MNDirect getSession           ] addDelegate:self];

        [[MNDirect scoreProgressProvider] addDelegate:self];
        [[MNDirect scoreProgressProvider] setScoreCompareFunc:scoreCompareFunc withContext:scoreCompareFuncContext];
        inited = YES;
    }
    else {
        inited = NO;
    }
    
    return inited;
}

-(void) willMoveToSuperview:(UIView*) newSuperview {
    if (newSuperview != nil) {
        if (![self checkProvider]) {
            return;
        }

        [self prepareView];
        
        [self performActionForSessionStatus:[MNDirect getSessionStatus] andUserStatus:[[MNDirect getSession]getRoomUserStatus]];
    }
    else {
        [self stopScoreProgressProvider];
    }
}

-(void) mnSessionStatusChangedTo:(NSUInteger) newStatus from:(NSUInteger) oldStatus {
    [self performActionForSessionStatus:newStatus andUserStatus:[[MNDirect getSession]getRoomUserStatus]];
}
-(void) mnSessionRoomUserStatusChangedTo:(NSInteger) newStatus {
    [self performActionForSessionStatus:[MNDirect getSessionStatus] andUserStatus:newStatus];
}

-(void) performActionForSessionStatus:(NSInteger) sessionStatus andUserStatus:(NSInteger)userStatus {
    if ((sessionStatus == MN_IN_GAME_PLAY) && (userStatus == MN_USER_PLAYER)) {
        [self startScoreProgressProvider];
    }
    else {
        [self stopScoreProgressProvider];
    }
}

-(void) prepareView {
    //virtual
}

-(void) startScoreProgressProvider {
    if (![self checkProvider]) {
        return;
    }
    
    if (self.hidden) {
        self.hidden = NO;
        
        [[MNDirect scoreProgressProvider] start];

        [self refreshScoreResendTimer];
    }
}

-(void) stopScoreProgressProvider {
    if (!inited) {
        return;
    }
    
    if (!self.hidden) {
        self.hidden = YES;
        [[MNDirect scoreProgressProvider] stop];
        
        if (self.scoreResendTimer != nil) {
            if ([self.scoreResendTimer isValid]) {
                [self.scoreResendTimer invalidate];
            }
            
            self.scoreResendTimer = nil;
        }
    }
}

-(void) setScoreCompareFunc:(MNScoreProgressProviderScoreCompareFunc) func withContext:(void*) context{
    if (![self checkProvider]) {
        return;
    }
    
    scoreCompareFunc = func;
    scoreCompareFuncContext = context;

    [[MNDirect scoreProgressProvider] setScoreCompareFunc:scoreCompareFunc withContext:scoreCompareFuncContext];
}
-(void) postScore:(long long) score {
    if (![self checkProvider]) {
        return;
    }
    
    self.currentScore = score;
    
    [[MNDirect scoreProgressProvider] postScore:self.currentScore];

    [self refreshScoreResendTimer];
}

-(void) refreshScoreResendTimer {
    [self.scoreResendTimer invalidate];
    self.scoreResendTimer = nil;
    
    if (self.scoreResendTimeout != 0) {
        self.scoreResendTimer = [NSTimer scheduledTimerWithTimeInterval:self.scoreResendTimeout
                                                                 target:self
                                                               selector:@selector(scoreResendTimerFire:)
                                                               userInfo:nil
                                                                repeats:YES];
    }
}

-(void) scoreResendTimerFire:(NSTimer*) timer {
    if (![self checkProvider]) {
        return;
    }
    
    [[MNDirect scoreProgressProvider] postScore:self.currentScore];
}

-(void) scoresUpdated:(NSArray*) scoreProgressItems {
    //virtual
}


@end
