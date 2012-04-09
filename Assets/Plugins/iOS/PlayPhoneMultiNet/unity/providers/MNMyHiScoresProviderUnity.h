//
//  MNMyHiScoresProviderUnity.h
//  PlayPhone SDK for Unity
//
//  Copyright (c) 2012 PlayPhone Inc. All rights reserved.
//

#import "MNUnity.h"

EXTERN_C
const char* _MNMyHiScoresProvider_GetMyHiScore(int gameSetId);

EXTERN_C
const char* _MNMyHiScoresProvider_GetMyHiScores();

EXTERN_C
bool _MNMyHiScoresProvider_RegisterEventHandler();

EXTERN_C
bool _MNMyHiScoresProvider_UnregisterEventHandler();


