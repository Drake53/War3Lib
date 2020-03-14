using System;
using System.Collections.Generic;
using System.Text;

namespace War3Lib.Event
{
    class FrameEvent
    {
    }
}

// native BlzTriggerRegisterFrameEvent                takes trigger whichTrigger, framehandle frame, frameeventtype eventId returns event
// native BlzGetTriggerFrame                          takes nothing returns framehandle
// native BlzGetTriggerFrameEvent                     takes nothing returns frameeventtype
// native BlzGetTriggerFrameValue                     takes nothing returns real
// native BlzGetTriggerFrameText                      takes nothing returns string
