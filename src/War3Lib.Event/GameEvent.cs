using System;
using System.Collections.Generic;
using System.Text;

namespace War3Lib.Event
{
    class GameEvent
    {
    }
}
/*
    constant gameevent EVENT_GAME_VICTORY                       = ConvertGameEvent(0)
        constant native GetWinningPlayer takes nothing returns player
    constant gameevent EVENT_GAME_END_LEVEL                     = ConvertGameEvent(1)

    constant gameevent EVENT_GAME_VARIABLE_LIMIT                = ConvertGameEvent(2)
    constant gameevent EVENT_GAME_STATE_LIMIT                   = ConvertGameEvent(3)
        constant native GetEventGameState takes nothing returns gamestate

    constant gameevent EVENT_GAME_TIMER_EXPIRED                 = ConvertGameEvent(4)

    constant gameevent EVENT_GAME_ENTER_REGION                  = ConvertGameEvent(5)
        constant native GetTriggeringRegion takes nothing returns region
        constant native GetEnteringUnit takes nothing returns unit
    constant gameevent EVENT_GAME_LEAVE_REGION                  = ConvertGameEvent(6)
        constant native GetTriggeringRegion takes nothing returns region
        constant native GetLeavingUnit takes nothing returns unit

    constant gameevent EVENT_GAME_TRACKABLE_HIT                 = ConvertGameEvent(7)
        constant native GetTriggeringTrackable takes nothing returns trackable
    constant gameevent EVENT_GAME_TRACKABLE_TRACK               = ConvertGameEvent(8)
        constant native GetTriggeringTrackable takes nothing returns trackable

    constant gameevent EVENT_GAME_SHOW_SKILL                    = ConvertGameEvent(9)
    constant gameevent EVENT_GAME_BUILD_SUBMENU                 = ConvertGameEvent(10)

    constant gameevent          EVENT_GAME_LOADED                       = ConvertGameEvent(256)
    constant gameevent          EVENT_GAME_TOURNAMENT_FINISH_SOON       = ConvertGameEvent(257)
        constant native GetTournamentFinishSoonTimeRemaining takes nothing returns real
        constant native GetTournamentFinishNowRule takes nothing returns integer
        constant native GetTournamentFinishNowPlayer takes nothing returns player
        constant native GetTournamentScore takes player whichPlayer returns integer
    constant gameevent          EVENT_GAME_TOURNAMENT_FINISH_NOW        = ConvertGameEvent(258)
    constant gameevent          EVENT_GAME_SAVE                         = ConvertGameEvent(259)
        constant native GetSaveBasicFilename takes nothing returns string
    constant gameevent          EVENT_GAME_CUSTOM_UI_FRAME              = ConvertGameEvent(310)
*/

// native TriggerRegisterDialogEvent       takes trigger whichTrigger, dialog whichDialog returns event
// native TriggerRegisterDialogButtonEvent takes trigger whichTrigger, button whichButton returns event
// native TriggerRegisterGameEvent takes trigger whichTrigger, gameevent whichGameEvent returns event
// native TriggerRegisterEnterRegion takes trigger whichTrigger, region whichRegion, boolexpr filter returns event
// native TriggerRegisterLeaveRegion takes trigger whichTrigger, region whichRegion, boolexpr filter returns event
// native TriggerRegisterTrackableHitEvent takes trigger whichTrigger, trackable t returns event
// native TriggerRegisterTrackableTrackEvent takes trigger whichTrigger, trackable t returns event
// native TriggerRegisterCommandEvent takes trigger whichTrigger, integer whichAbility, string order returns event
// native TriggerRegisterUpgradeCommandEvent takes trigger whichTrigger, integer whichUpgrade returns event

        // native TriggerRegisterGameStateEvent takes trigger whichTrigger, gamestate whichState, limitop opcode, real limitval returns event
            // constant igamestate GAME_STATE_DIVINE_INTERVENTION          = ConvertIGameState(0)
            // constant igamestate GAME_STATE_DISCONNECTED                 = ConvertIGameState(1)
            // constant fgamestate GAME_STATE_TIME_OF_DAY                  = ConvertFGameState(2)
