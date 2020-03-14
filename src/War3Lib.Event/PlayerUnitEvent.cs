using System;
using System.Collections.Generic;
using System.Text;

namespace War3Lib.Event
{
    class PlayerUnitEvent
    {
    }
}
/*
    constant playerunitevent EVENT_PLAYER_UNIT_ATTACKED                 = ConvertPlayerUnitEvent(18)
        constant native GetAttacker takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_RESCUED                  = ConvertPlayerUnitEvent(19)
        constant native GetRescuer  takes nothing returns unit

    constant playerunitevent EVENT_PLAYER_UNIT_DEATH                    = ConvertPlayerUnitEvent(20)
        constant native GetDyingUnit takes nothing returns unit
        constant native GetKillingUnit takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_DECAY                    = ConvertPlayerUnitEvent(21)
        constant native GetDecayingUnit takes nothing returns unit

    constant playerunitevent EVENT_PLAYER_UNIT_DETECTED                 = ConvertPlayerUnitEvent(22)
        constant native GetDetectedUnit takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_HIDDEN                   = ConvertPlayerUnitEvent(23)

    constant playerunitevent EVENT_PLAYER_UNIT_SELECTED                 = ConvertPlayerUnitEvent(24)
        //constant native GetSelectedUnit takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_DESELECTED               = ConvertPlayerUnitEvent(25)

    constant playerunitevent EVENT_PLAYER_UNIT_CONSTRUCT_START          = ConvertPlayerUnitEvent(26)
        constant native GetConstructingStructure takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_CONSTRUCT_CANCEL         = ConvertPlayerUnitEvent(27)
        constant native GetCancelledStructure takes nothing returns unit
        constant native GetConstructedStructure takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_CONSTRUCT_FINISH         = ConvertPlayerUnitEvent(28)
        constant native GetCancelledStructure takes nothing returns unit
        constant native GetConstructedStructure takes nothing returns unit

    constant playerunitevent EVENT_PLAYER_UNIT_UPGRADE_START            = ConvertPlayerUnitEvent(29)
    constant playerunitevent EVENT_PLAYER_UNIT_UPGRADE_CANCEL           = ConvertPlayerUnitEvent(30)
    constant playerunitevent EVENT_PLAYER_UNIT_UPGRADE_FINISH           = ConvertPlayerUnitEvent(31)

    constant playerunitevent EVENT_PLAYER_UNIT_TRAIN_START              = ConvertPlayerUnitEvent(32)
        constant native GetTrainedUnitType takes nothing returns integer
    constant playerunitevent EVENT_PLAYER_UNIT_TRAIN_CANCEL             = ConvertPlayerUnitEvent(33)
        constant native GetTrainedUnitType takes nothing returns integer
    constant playerunitevent EVENT_PLAYER_UNIT_TRAIN_FINISH             = ConvertPlayerUnitEvent(34)
        constant native GetTrainedUnit takes nothing returns unit

    constant playerunitevent EVENT_PLAYER_UNIT_RESEARCH_START           = ConvertPlayerUnitEvent(35)
        constant native GetResearchingUnit takes nothing returns unit
        constant native GetResearched takes nothing returns integer
    constant playerunitevent EVENT_PLAYER_UNIT_RESEARCH_CANCEL          = ConvertPlayerUnitEvent(36)
        constant native GetResearchingUnit takes nothing returns unit
        constant native GetResearched takes nothing returns integer
    constant playerunitevent EVENT_PLAYER_UNIT_RESEARCH_FINISH          = ConvertPlayerUnitEvent(37)
        constant native GetResearchingUnit takes nothing returns unit
        constant native GetResearched takes nothing returns integer
    constant playerunitevent EVENT_PLAYER_UNIT_ISSUED_ORDER             = ConvertPlayerUnitEvent(38)
        constant native GetOrderedUnit takes nothing returns unit
        constant native GetIssuedOrderId takes nothing returns integer
    constant playerunitevent EVENT_PLAYER_UNIT_ISSUED_POINT_ORDER       = ConvertPlayerUnitEvent(39)
        constant native GetOrderPointX takes nothing returns real
        constant native GetOrderPointY takes nothing returns real
        constant native GetOrderPointLoc takes nothing returns location
    constant playerunitevent EVENT_PLAYER_UNIT_ISSUED_TARGET_ORDER      = ConvertPlayerUnitEvent(40)
        constant native GetOrderTarget              takes nothing returns widget
        constant native GetOrderTargetDestructable  takes nothing returns destructable
        constant native GetOrderTargetItem          takes nothing returns item
        constant native GetOrderTargetUnit          takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_ISSUED_UNIT_ORDER        = ConvertPlayerUnitEvent(40)    // for compat

    constant playerunitevent EVENT_PLAYER_HERO_LEVEL                    = ConvertPlayerUnitEvent(41)
        constant native GetLevelingUnit takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_HERO_SKILL                    = ConvertPlayerUnitEvent(42)

    constant playerunitevent EVENT_PLAYER_HERO_REVIVABLE                = ConvertPlayerUnitEvent(43)
        constant native GetRevivableUnit takes nothing returns unit

    constant playerunitevent EVENT_PLAYER_HERO_REVIVE_START             = ConvertPlayerUnitEvent(44)
        constant native GetRevivingUnit takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_HERO_REVIVE_CANCEL            = ConvertPlayerUnitEvent(45)
        constant native GetRevivingUnit takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_HERO_REVIVE_FINISH            = ConvertPlayerUnitEvent(46)
        constant native GetRevivingUnit takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_SUMMON                   = ConvertPlayerUnitEvent(47)
    constant playerunitevent EVENT_PLAYER_UNIT_DROP_ITEM                = ConvertPlayerUnitEvent(48)
        constant native GetManipulatingUnit takes nothing returns unit
        constant native GetManipulatedItem  takes nothing returns item
    constant playerunitevent EVENT_PLAYER_UNIT_PICKUP_ITEM              = ConvertPlayerUnitEvent(49)
        constant native GetManipulatingUnit takes nothing returns unit
        constant native GetManipulatedItem  takes nothing returns item
    constant playerunitevent EVENT_PLAYER_UNIT_USE_ITEM                 = ConvertPlayerUnitEvent(50)
        constant native GetManipulatingUnit takes nothing returns unit
        constant native GetManipulatedItem  takes nothing returns item

    constant playerunitevent EVENT_PLAYER_UNIT_LOADED                   = ConvertPlayerUnitEvent(51)
        constant native GetTransportUnit    takes nothing returns unit
        constant native GetLoadedUnit       takes nothing returns unit
    constant playerunitevent EVENT_PLAYER_UNIT_DAMAGED                  = ConvertPlayerUnitEvent(308)
    constant playerunitevent EVENT_PLAYER_UNIT_DAMAGING                 = ConvertPlayerUnitEvent(315)

    constant playerunitevent    EVENT_PLAYER_UNIT_SELL                  = ConvertPlayerUnitEvent(269)
        constant native GetSellingUnit      takes nothing returns unit
        constant native GetSoldUnit         takes nothing returns unit
        constant native GetBuyingUnit       takes nothing returns unit
    constant playerunitevent    EVENT_PLAYER_UNIT_CHANGE_OWNER          = ConvertPlayerUnitEvent(270)
        constant native GetChangingUnit             takes nothing returns unit
        constant native GetChangingUnitPrevOwner    takes nothing returns player
    constant playerunitevent    EVENT_PLAYER_UNIT_SELL_ITEM             = ConvertPlayerUnitEvent(271)
        constant native GetSoldItem         takes nothing returns item
    constant playerunitevent    EVENT_PLAYER_UNIT_SPELL_CHANNEL         = ConvertPlayerUnitEvent(272)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant playerunitevent    EVENT_PLAYER_UNIT_SPELL_CAST            = ConvertPlayerUnitEvent(273)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant playerunitevent    EVENT_PLAYER_UNIT_SPELL_EFFECT          = ConvertPlayerUnitEvent(274)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant playerunitevent    EVENT_PLAYER_UNIT_SPELL_FINISH          = ConvertPlayerUnitEvent(275)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant playerunitevent    EVENT_PLAYER_UNIT_SPELL_ENDCAST         = ConvertPlayerUnitEvent(276)
        constant native GetSpellAbilityUnit         takes nothing returns unit
        constant native GetSpellAbilityId           takes nothing returns integer
        constant native GetSpellAbility             takes nothing returns ability
        constant native GetSpellTargetLoc           takes nothing returns location
        constant native GetSpellTargetX				takes nothing returns real
        constant native GetSpellTargetY				takes nothing returns real
        constant native GetSpellTargetDestructable  takes nothing returns destructable
        constant native GetSpellTargetItem          takes nothing returns item
        constant native GetSpellTargetUnit          takes nothing returns unit
    constant playerunitevent    EVENT_PLAYER_UNIT_PAWN_ITEM             = ConvertPlayerUnitEvent(277)




    
// EVENT_PLAYER_UNIT_SUMMONED (= EVENT_PLAYER_UNIT_SUMMON??)
constant native GetSummoningUnit    takes nothing returns unit
constant native GetSummonedUnit     takes nothing returns unit
*/
