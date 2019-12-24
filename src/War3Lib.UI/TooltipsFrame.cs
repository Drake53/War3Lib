using static War3Api.Common;

namespace War3Lib.UI
{
    public static class TooltipsFrame
    {
        private const bool Visible = true;
        private const float AnchorX = 1f;
        private const float AnchorY = 0f;
        private const float PivotX = 1f;
        private const float PivotY = 0f;
        private const float PositionX = -50f;
        private const float PositionY = 0f;

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