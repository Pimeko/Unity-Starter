#import <Foundation/Foundation.h>



UISelectionFeedbackGenerator* selectionFeedbackGenerator;

UIImpactFeedbackGenerator* impactFeedbackGeneratorLight;
UIImpactFeedbackGenerator* impactFeedbackGeneratorMedium;
UIImpactFeedbackGenerator* impactFeedbackGeneratorHeavy;

UINotificationFeedbackGenerator* notificationFeedbackGenerator;

void _instantiateFeedbackGenerator(int id)
{
    
    switch (id)
    {
        case 0:
            selectionFeedbackGenerator = [[UISelectionFeedbackGenerator alloc] init];
            break;
        case 1:
            impactFeedbackGeneratorLight = [[UIImpactFeedbackGenerator alloc] initWithStyle:
                                            UIImpactFeedbackStyleLight];
            break;
        case 2:
            impactFeedbackGeneratorMedium =  [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium];
            break;
        case 3:
            impactFeedbackGeneratorHeavy =  [[UIImpactFeedbackGenerator alloc] initWithStyle:
                                             UIImpactFeedbackStyleHeavy];
            break;
        case 4:
        case 5:
        case 6:
            notificationFeedbackGenerator = [[UINotificationFeedbackGenerator alloc] init];
            break;
    }
}

void _prepareFeedbackGenerator (int id)
{
    switch (id)
    {
        case 0:
            [selectionFeedbackGenerator prepare];
            break;
        case 1:
            [impactFeedbackGeneratorLight prepare];
            break;
        case 2:
            [impactFeedbackGeneratorMedium prepare];
            break;
        case 3:
            [impactFeedbackGeneratorHeavy prepare];
            break;
        case 4:
        case 5:
        case 6:
            [notificationFeedbackGenerator prepare];
            break;
    }
}


void _releaseFeedbackGenerator (int id)
{
    switch (id)
    {
        case 0:
            selectionFeedbackGenerator = nil;
            break;
        case 1:
            impactFeedbackGeneratorLight = nil;
            break;
        case 2:
            impactFeedbackGeneratorMedium = nil;
            break;
        case 3:
            impactFeedbackGeneratorHeavy = nil;
            break;
        case 4:
        case 5:
        case 6:
            notificationFeedbackGenerator = nil;
            break;
    }
}



/*
 Triggers a specific feedback.
 0 = Selection change
 1 = ImpactLight
 2 = ImpactMedium
 3 = ImpactHeavy
 4 = Success
 5 = Warning
 6 = Failure
 */
void _triggerFeedbackGenerator(int id, bool advanced)
{
    switch (id)
    {
        case 0:
            if (!advanced)
                [selectionFeedbackGenerator prepare];
            [selectionFeedbackGenerator selectionChanged];
            break;
        case 1:
            if (!advanced)
                [impactFeedbackGeneratorLight prepare];
            [impactFeedbackGeneratorLight impactOccurred];
            break;
        case 2:
            if (!advanced)
                [impactFeedbackGeneratorMedium prepare];
            [impactFeedbackGeneratorMedium impactOccurred];
            break;
        case 3:
            if (!advanced)
                [impactFeedbackGeneratorHeavy prepare];
            [impactFeedbackGeneratorHeavy impactOccurred];
            break;
        case 4:
            if (!advanced)
                [notificationFeedbackGenerator prepare];
            [notificationFeedbackGenerator notificationOccurred:UINotificationFeedbackTypeSuccess];
            break;
        case 5:
            if (!advanced)
                [notificationFeedbackGenerator prepare];
            [notificationFeedbackGenerator notificationOccurred:UINotificationFeedbackTypeWarning];
            break;
        case 6:
            if (!advanced)
                [notificationFeedbackGenerator prepare];
            [notificationFeedbackGenerator notificationOccurred:UINotificationFeedbackTypeError];
            break;
    }
}




