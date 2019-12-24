using static War3Api.Common;

namespace War3Lib.UI
{
    public static class MessageFrame
    {
        private const bool Visible = true;
        private const float AnchorX = 0f;
        private const float AnchorY = 1f;
        private const float PivotX = 0f;
        private const float PivotY = 1f;
        private const float PositionX = 100f;
        private const float PositionY = -150f;

        internal static void Refresh(framehandle frame)
        {
            if (Visible)
            {
                BlzFrameClearAllPoints(frame);
                BlzFrameSetAbsPoint(
                    frame,
                    FRAMEPOINT_BOTTOMLEFT,
                    Util.GetScreenPositionX(PositionX + (Util.ResolutionWidth * AnchorX) - (BlzFrameGetWidth(frame) * Util.Dpi2Pixels * PivotX)),
                    Util.GetScreenPositionY(PositionY + (Util.ResolutionHeight * AnchorY) - (BlzFrameGetHeight(frame) * Util.Dpi2Pixels * PivotY)));
            }
        }
    }
}