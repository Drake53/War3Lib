using System;
using System.Collections.Generic;
using System.Text;

namespace War3Lib.Event
{
    class UnitEvent
    {
    }
}
/*
    constant native GetTriggerUnit takes nothing returns unit
 
    constant unitevent EVENT_UNIT_DAMAGED                               = ConvertUnitEvent(52)
        constant native GetEventDamage takes nothing returns real
        constant native GetEventDamageSource takes nothing returns unit
    constant unitevent EVENT_UNIT_DAMAGING                              = ConvertUnitEvent(314)
    constant unitevent EVENT_UNIT_DEATH                                 = ConvertUnitEvent(53)
        constant native GetDyingUnit takes nothing returns unit
        constant native GetKillingUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_DECAY                                 = ConvertUnitEvent(54)
        constant native GetDecayingUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_DETECTED                              = ConvertUnitEvent(55)
        constant native GetEventDetectingPlayer takes nothing returns player
    constant unitevent EVENT_UNIT_HIDDEN                                = ConvertUnitEvent(56)
    constant unitevent EVENT_UNIT_SELECTED                              = ConvertUnitEvent(57)
    constant unitevent EVENT_UNIT_DESELECTED                            = ConvertUnitEvent(58)
                                                                        
    constant unitevent EVENT_UNIT_STATE_LIMIT                           = ConvertUnitEvent(59)
        constant native GetEventUnitState takes nothing returns unitstate

    // Events which may have a filter for the "other unit"              
    //                                                                  
    constant unitevent EVENT_UNIT_ACQUIRED_TARGET                       = ConvertUnitEvent(60)
        constant native GetEventTargetUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_TARGET_IN_RANGE                       = ConvertUnitEvent(61)
        constant native GetEventTargetUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_ATTACKED                              = ConvertUnitEvent(62)
        constant native GetAttacker takes nothing returns unit
    constant unitevent EVENT_UNIT_RESCUED                               = ConvertUnitEvent(63)
        constant native GetRescuer  takes nothing returns unit
                                                                        
    constant unitevent EVENT_UNIT_CONSTRUCT_CANCEL                      = ConvertUnitEvent(64)
        constant native GetCancelledStructure takes nothing returns unit
        constant native GetConstructedStructure takes nothing returns unit
    constant unitevent EVENT_UNIT_CONSTRUCT_FINISH                      = ConvertUnitEvent(65)
        constant native GetCancelledStructure takes nothing returns unit
        constant native GetConstructedStructure takes nothing returns unit
                                                                        
    constant unitevent EVENT_UNIT_UPGRADE_START                         = ConvertUnitEvent(66)
    constant unitevent EVENT_UNIT_UPGRADE_CANCEL                        = ConvertUnitEvent(67)
    constant unitevent EVENT_UNIT_UPGRADE_FINISH                        = ConvertUnitEvent(68)
                                                                        
    // Events which involve the specified unit performing               
    // training of other units                                          
    //                                                                  
    constant unitevent EVENT_UNIT_TRAIN_START                           = ConvertUnitEvent(69)
        constant native GetTrainedUnitType takes nothing returns integer
    constant unitevent EVENT_UNIT_TRAIN_CANCEL                          = ConvertUnitEvent(70)
        constant native GetTrainedUnitType takes nothing returns integer
    constant unitevent EVENT_UNIT_TRAIN_FINISH                          = ConvertUnitEvent(71)
        constant native GetTrainedUnit takes nothing returns unit
                                                                        
    constant unitevent EVENT_UNIT_RESEARCH_START                        = ConvertUnitEvent(72)
    constant unitevent EVENT_UNIT_RESEARCH_CANCEL                       = ConvertUnitEvent(73)
    constant unitevent EVENT_UNIT_RESEARCH_FINISH                       = ConvertUnitEvent(74)
                                                                        
    constant unitevent EVENT_UNIT_ISSUED_ORDER                          = ConvertUnitEvent(75)
        constant native GetOrderedUnit takes nothing returns unit
        constant native GetIssuedOrderId takes nothing returns integer
    constant unitevent EVENT_UNIT_ISSUED_POINT_ORDER                    = ConvertUnitEvent(76)
        constant native GetOrderPointX takes nothing returns real
        constant native GetOrderPointY takes nothing returns real
        constant native GetOrderPointLoc takes nothing returns location
    constant unitevent EVENT_UNIT_ISSUED_TARGET_ORDER                   = ConvertUnitEvent(77)
        constant native GetOrderTarget              takes nothing returns widget
        constant native GetOrderTargetDestructable  takes nothing returns destructable
        constant native GetOrderTargetItem          takes nothing returns item
        constant native GetOrderTargetUnit          takes nothing returns unit
                                                                       
    constant unitevent EVENT_UNIT_HERO_LEVEL                            = ConvertUnitEvent(78)
        constant native GetLevelingUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_HERO_SKILL                            = ConvertUnitEvent(79)
        constant native GetLearningUnit      takes nothing returns unit
        constant native GetLearnedSkill      takes nothing returns integer
        constant native GetLearnedSkillLevel takes nothing returns integer
                                                                        
    constant unitevent EVENT_UNIT_HERO_REVIVABLE                        = ConvertUnitEvent(80)
        ? constant native GetRevivableUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_HERO_REVIVE_START                     = ConvertUnitEvent(81)
        constant native GetRevivingUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_HERO_REVIVE_CANCEL                    = ConvertUnitEvent(82)
        constant native GetRevivingUnit takes nothing returns unit
    constant unitevent EVENT_UNIT_HERO_REVIVE_FINISH                    = ConvertUnitEvent(83)
        constant native GetRevivingUnit takes nothing returns unit
                                                                        
    constant unitevent EVENT_UNIT_SUMMON                                = ConvertUnitEvent(84)
                                                                        
    constant unitevent EVENT_UNIT_DROP_ITEM                             = ConvertUnitEvent(85)
        constant native GetManipulatingUnit takes nothing returns unit
        constant native GetManipulatedItem  takes nothing returns item
    constant unitevent EVENT_UNIT_PICKUP_ITEM                           = ConvertUnitEvent(86)
        constant native GetManipulatingUnit takes nothing returns unit
        constant native GetManipulatedItem  takes nothing returns item
    constant unitevent EVENT_UNIT_USE_ITEM                              = ConvertUnitEvent(87)
        constant native GetManipulatingUnit takes nothing returns unit
        constant native GetManipulatedItem  takes nothing returns item

    constant unitevent EVENT_UNIT_LOADED                                = ConvertUnitEvent(88)

    constant widgetevent EVENT_WIDGET_DEATH                             = ConvertWidgetEvent(89)

    constant dialogevent EVENT_DIALOG_BUTTON_CLICK                      = ConvertDialogEvent(90)
        constant native GetClickedButton takes nothing returns button
        constant native GetClickedDialog    takes nothing returns dialog
    constant dialogevent EVENT_DIALOG_CLICK                             = ConvertDialogEvent(91)

    constant unitevent          EVENT_UNIT_SELL                         = ConvertUnitEvent(286)
        constant native GetSellingUnit      takes nothing returns unit
        constant native GetSoldUnit         takes nothing returns unit
        constant native GetBuyingUnit       takes nothing returns unit
    constant unitevent          EVENT_UNIT_CHANGE_OWNER                 = ConvertUnitEvent(287)
    constant unitevent          EVENT_UNIT_SELL_ITEM                    = ConvertUnitEvent(288)
    constant unitevent          EVENT_UNIT_SPELL_CHANNEL                = ConvertUnitEvent(289)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant unitevent          EVENT_UNIT_SPELL_CAST                   = ConvertUnitEvent(290)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant unitevent          EVENT_UNIT_SPELL_EFFECT                 = ConvertUnitEvent(291)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant unitevent          EVENT_UNIT_SPELL_FINISH                 = ConvertUnitEvent(292)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant unitevent          EVENT_UNIT_SPELL_ENDCAST                = ConvertUnitEvent(293)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant unitevent          EVENT_UNIT_PAWN_ITEM                    = ConvertUnitEvent(294)
*/

// native TriggerRegisterUnitEvent takes trigger whichTrigger, unit whichUnit, unitevent whichEvent returns event
// native TriggerRegisterDeathEvent takes trigger whichTrigger, widget whichWidget returns event
// native TriggerRegisterUnitStateEvent takes trigger whichTrigger, unit whichUnit, unitstate whichState, limitop opcode, real limitval returns event
    // constant unitstate UNIT_STATE_LIFE                          = ConvertUnitState(0)
    // constant unitstate UNIT_STATE_MAX_LIFE                      = ConvertUnitState(1)
    // constant unitstate UNIT_STATE_MANA                          = ConvertUnitState(2)
    // constant unitstate UNIT_STATE_MAX_MANA                      = ConvertUnitState(3)
// native TriggerRegisterFilterUnitEvent takes trigger whichTrigger, unit whichUnit, unitevent whichEvent, boolexpr filter returns event
// native TriggerRegisterUnitInRange takes trigger whichTrigger, unit whichUnit, real range, boolexpr filter returns event





/*
native BlzSetEventDamage                           takes real damage returns nothing
native BlzGetEventDamageTarget 	                   takes nothing returns unit
native BlzGetEventAttackType  	                   takes nothing returns attacktype
native BlzGetEventDamageType                       takes nothing returns damagetype
native BlzGetEventWeaponType  	                   takes nothing returns weapontype
native BlzSetEventAttackType                       takes attacktype attackType returns boolean
native BlzSetEventDamageType                       takes damagetype damageType returns boolean
native BlzSetEventWeaponType                       takes weapontype weaponType returns boolean
native BlzGetEventIsAttack                         takes nothing returns boolean
*/

// constant native GetTriggerDestructable takes nothing returns destructable
// constant native GetTriggerWidget takes nothing returns widget
