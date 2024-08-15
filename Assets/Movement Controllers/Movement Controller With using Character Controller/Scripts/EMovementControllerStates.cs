/// <summary>
/// The states of Movement Controller
/// </summary>
public enum EMovementControllerStates
{
    //Idle states
    IdleStand,
    IdleCrouch,
    IdleProne,
    //Walk states
    WalkStand,
    WalkStandBack,
    WalkCrouch,
    WalkCrouchBack,
    WalkProne,
    WalkProneBack,
    //Run states
    RunStand,
    RunStandBack,
    //Sub states
    //Stand to
    StandToCrouch,
    StandToProne,
    //Crouch to
    CrouchToStand,
    CrouchToProne,
    //Prone to 
    ProneToStand,
    ProneToCrouch,
    //Other
    Jump,
    Fall
}
