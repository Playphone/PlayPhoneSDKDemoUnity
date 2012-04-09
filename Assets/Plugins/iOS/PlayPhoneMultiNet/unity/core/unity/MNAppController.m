//
//  MNAppController.m
//  PlayPhone SDK for Unity
//
//  Created by Vladislav Ogol on 10.02.12.
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNDirect.h"

#import "MNAppController.h"

@implementation AppController(MNUnity)

- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url {
    NSLog(@"AppController(MNUnity) application:handleOpenURL:");
    
    return [MNDirect handleOpenURL: url];
}

@end
