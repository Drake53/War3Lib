using System;

using static War3Api.Common;

namespace War3Lib.UI
{
    public static class Util
    {
        internal const string TocFile = @"war3mapImported\UIUtils.toc";
        internal const string CacheName = "UIUtils.w3v";

        internal const bool PersistentChildProperties = true;

        private const float ResolutionCheckInterval = 0.1f;
        private const bool HideConsoleFrame = true;
        private const bool RefrainNonSimpleFrame = true;
        private const int ReferenceResolutionWidth = 1360;
        private const int ReferenceResolutionHeight = 768;

        private const bool ResourceFrameVisible = false;
        private const bool CommandButtonFrameVisible = false;

        private static int _resolutionWidth = ReferenceResolutionWidth;
        private static int _resolutionHeight = ReferenceResolutionHeight;
        private static float _scaleFactor = 1f;

        static Util()
        {
            RefreshResolution();
            if (HideConsoleFrame)
            {
                BlzEnableUIAutoPosition(false);
                BlzFrameClearAllPoints(DefaultFrame.World);
                BlzFrameClearAllPoints(DefaultFrame.Console);
                BlzFrameSetAllPoints(DefaultFrame.World, DefaultFrame.Game);
                BlzFrameSetAbsPoint(DefaultFrame.Console, FRAMEPOINT_TOPRIGHT, -999f, -999f);
                HideFrame(DefaultFrame.Console, false);
                RefreshDefaultFrames();

                if (!ResourceFrameVisible)
                {
                    HideFrame(DefaultFrame.GoldText);
                    HideFrame(DefaultFrame.LumberText);
                    HideFrame(DefaultFrame.FoodText);
                    HideFrame(DefaultFrame.ResourceBar);
                }

                if (!CommandButtonFrameVisible)
                {
                    foreach (var commandButton in DefaultFrame.GetCommandButtons())
                    {
                        HideFrame(commandButton);
                    }
                }
            }

            TimerStart(CreateTimer(), ResolutionCheckInterval, true, CheckResolution);
        }

        public static event EventHandler ResolutionChanged;

        public static int ResolutionWidth => _resolutionWidth;

        public static int ResolutionHeight => _resolutionHeight;

        public static float ScaleFactor => _scaleFactor;

        public static float Pixels2Dpi => 0.6f / _resolutionHeight;

        public static float Dpi2Pixels => _resolutionHeight / 0.6f;

        public static float ReferenceDpi2Pixels => ReferenceResolutionHeight / 0.6f;

        public static float FrameBoundWidth => (_resolutionWidth - (_resolutionHeight / 600f * 800f)) / 2f;

        public static float GetScreenPositionX(float x)
        {
            return (x - FrameBoundWidth) * Pixels2Dpi;
        }

        public static float GetScreenPositionY(float y)
        {
            return y * Pixels2Dpi;
        }

        public static void RefreshDefaultFrames()
        {
            MessageFrame.Refresh(DefaultFrame.UnitMsg);
            ChatFrame.Refresh(DefaultFrame.ChatMsg);
            TooltipsFrame.Refresh(DefaultFrame.UberTooltip);
            MinimapFrame.Refresh(DefaultFrame.Minimap);
            PortraitFrame.Refresh(DefaultFrame.Portrait);
        }

        internal static void HideFrame(framehandle frame, bool bottomLeft = true)
        {
            BlzFrameClearAllPoints(frame);
            BlzFrameSetAbsPoint(frame, bottomLeft ? FRAMEPOINT_BOTTOMLEFT : FRAMEPOINT_TOPRIGHT, -999f, -999f);
        }

        private static void RefreshResolution()
        {
            RefreshResolution(BlzGetLocalClientWidth(), BlzGetLocalClientHeight());
        }

        private static void RefreshResolution(int width, int height)
        {
            _resolutionWidth = width;
            _resolutionHeight = height;
            _scaleFactor = _resolutionHeight / (float)ReferenceResolutionHeight;

            Frame.RefreshAllFrames();
        }

        private static void CheckResolution()
        {
            var width = BlzGetLocalClientWidth();
            var height = BlzGetLocalClientHeight();
            if (width != _resolutionWidth || height != _resolutionHeight)
            {
                RefreshResolution(width, height);
                RefreshDefaultFrames();
                ResolutionChanged?.Invoke(null, null);
            }
        }
    }
}