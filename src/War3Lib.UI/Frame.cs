using System;
using System.Collections;
using System.Collections.Generic;

using static War3Api.Common;

namespace War3Lib.UI
{
    public class Frame : IEnumerable<Frame>, IDisposable
    {
        private const string FrameTypePrefix = "UIUtils";

        private static readonly List<Frame> _frames = new List<Frame>();
        private static readonly Dictionary<framehandle, Frame> _dict = new Dictionary<framehandle, Frame>();
        private static readonly gamecache _gc = InitGameCache(Util.CacheName);

#pragma warning disable IDE0044 // Add readonly modifier
        private string _name;
        private bool _inheritScale;
        private bool _inheritOpacity;
        private bool _inheritVisibility;
        private bool _inheritEnableState;
        private bool _inheritPosition;
        private bool _inheritLevel;
        private bool _scalePosition;

        private FrameType _frameType;
        private float _localPositionX;
        private float _localPositionY;
        private float _anchorX;
        private float _anchorY;
        private float _pivotX;
        private float _pivotY;
        private float _unscaledWidth;
        private float _unscaledHeight;
        private float _valueMin;
        private float _valueMax;
        private float _fontSize;
        private string _fontType;
        private int _fontFlags;
        private int _context;
        private bool _isSimple;
        private bool _visibleSelf;
        private bool _enabledSelf;
        private int _localOpacity;

        private int _level;
        private int _opacity;
        private float _localScale;
        private float _stepSize;
        private float _width;
        private float _height;
        private float _scale;
        private float _left;
        private float _bottom;
        private float _screenPositionX;
        private float _screenPositionY;
        private float _scaledLeft;
        private float _scaledBottom;
        private float _scaledScreenPositionX;
        private float _scaledScreenPositionY;
        private Frame _parent;
        private List<Frame> _children;
        private Frame _tooltips;

        private string _mainTextureFile;
        private string _disabledTextureFile;
        private string _pushedTextureFile;
        private string _highlightTextureFile;
        private string _backgroundTextureFile;
        private string _borderTextureFile;
        private string _modelFile;

        private framehandle _frame;
        private framehandle _textFrame;
        private framehandle _modelFrame;
        private framehandle _mainTexture;
        private framehandle _disabledTexture;
        private framehandle _pushedTexture;
        private framehandle _highlightTexture;
        private framehandle _backgroundTexture;
        private framehandle _borderTexture;
#pragma warning restore IDE0044 // Add readonly modifier

        static Frame()
        {
            BlzLoadTOCFile(Util.TocFile);
        }

        public Frame(bool isSimple, string frameType, Frame parent, float x, float y, int level)
        {
            _context = GetStoredInteger(_gc, frameType, "0");
            var storedInt = GetStoredInteger(_gc, frameType, $"{_context}");
            StoreInteger(_gc, frameType, "0", storedInt == 0 ? _context + 1 : storedInt);

            _parent = parent;
            _children = new List<Frame>();
            _isSimple = isSimple;

            _frame = _isSimple ? BlzCreateSimpleFrame(frameType, DefaultFrame.Game, _context) : BlzCreateFrame(frameType, DefaultFrame.Game, 0, _context);
            _mainTexture = GetSubFrame(frameType + "Texture");
            _disabledTexture = GetSubFrame(frameType + "Disabled");
            _highlightTexture = GetSubFrame(frameType + "Highlight");
            _pushedTexture = GetSubFrame(frameType + "Pushed");
            _backgroundTexture = GetSubFrame(frameType + "Background");
            _borderTexture = GetSubFrame(frameType + "Border");
            _textFrame = GetSubFrame(frameType + "Text");
            _modelFrame = GetSubFrame(frameType + "Model");

            _inheritScale = true;
            _inheritOpacity = true;
            _inheritVisibility = true;
            _inheritEnableState = true;
            _inheritPosition = true;
            _inheritLevel = true;
            _scalePosition = true;

            var temp = Util.ReferenceDpi2Pixels;
            _unscaledWidth = BlzFrameGetWidth(_frame) * temp;
            _unscaledHeight = BlzFrameGetHeight(_frame) * temp;
            _name = $"{frameType}{_context}";
            Level = level;
            _visibleSelf = true;
            _enabledSelf = true;
            _fontType = @"Fonts\FRIZQT__.TTF";
            _fontSize = 0.013f;
            _fontFlags = 0;
            Value = 0f;
            LocalScale = 1f;
            _anchorX = 0f;
            _anchorY = 0f;
            _pivotX = 0f;
            _pivotY = 0f;
            Opacity = 255;

            _mainTextureFile = string.Empty;
            _disabledTextureFile = string.Empty;
            _pushedTextureFile = string.Empty;
            _highlightTextureFile = string.Empty;
            _backgroundTextureFile = string.Empty;
            _borderTextureFile = string.Empty;
            _modelFile = string.Empty;

            Move(x, y);
            SetMinMaxValue(0f, 1f);
            Refresh();
            _frames.Add(this);
            _dict.Add(_frame, this);

            var eventTrigger = CreateTrigger();
            for (var i = 1; i <= 16; i++)
            {
                BlzTriggerRegisterFrameEvent(eventTrigger, _frame, ConvertFrameEventType(i));
            }

            TriggerAddCondition(eventTrigger, Condition(() =>
            {
                AnyEvent?.Invoke(this, null);
                return false;
            }));
        }

        public Frame(bool isSimple, FrameType frameType, Frame parent, float x, float y, int level)
            : this(IsSimple(frameType, isSimple), $"{FrameTypePrefix}{frameType}", parent, x, y, level)
        {
            _frameType = frameType;
        }

        public event EventHandler AnyEvent;

        public static void RefreshAllFrames()
        {
            foreach (var frame in _frames)
            {
                if (frame.Parent is null)
                {
                    frame.Refresh();
                }
            }
        }

        public Frame Parent
        {
            get => _parent;
            set
            {
                if (value != null)
                {
                    if (_parent != value)
                    {
                        _parent._children.Remove(this);
                    }

                    value._children.Add(this);
                }

                if (!Util.PersistentChildProperties)
                {
                    if (_parent != null)
                    {
                        _localScale *= _parent._localScale;
                    }

                    _localPositionX = _screenPositionX - value._screenPositionX;
                    _localPositionY = _screenPositionY - value._screenPositionY;
                }

                _parent = value;
            }
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Text
        {
            get => BlzFrameGetText(_textFrame);
            set => BlzFrameSetText(_textFrame, value);
        }

        public int MaxLength
        {
            get => BlzFrameGetTextSizeLimit(_textFrame);
            set => BlzFrameSetTextSizeLimit(_textFrame, value);
        }

        public int TextColor
        {
            get => throw new NotSupportedException();
            set => BlzFrameSetTextColor(_textFrame, value);
        }

        public string Texture
        {
            get => _mainTextureFile;
            set
            {
                _mainTextureFile = value;
                BlzFrameSetTexture(_mainTexture, value, 0, true);
                if (string.IsNullOrEmpty(_disabledTextureFile))
                {
                    DisabledTexture = value;
                }

                if (string.IsNullOrEmpty(_pushedTextureFile))
                {
                    PushedTexture = value;
                }
            }
        }

        public string DisabledTexture
        {
            get => _disabledTextureFile;
            set
            {
                _disabledTextureFile = value;
                BlzFrameSetTexture(_disabledTexture, value, 0, true);
            }
        }

        public string HighlightTexture
        {
            get => _highlightTextureFile;
            set
            {
                _highlightTextureFile = value;
                BlzFrameSetTexture(_highlightTexture, value, 0, true);
            }
        }

        public string PushedTexture
        {
            get => _pushedTextureFile;
            set
            {
                _pushedTextureFile = value;
                BlzFrameSetTexture(_pushedTexture, value, 0, true);
            }
        }

        public string BackgroundTexture
        {
            get => _backgroundTextureFile;
            set
            {
                _backgroundTextureFile = value;
                BlzFrameSetTexture(_backgroundTexture, value, 0, true);
            }
        }

        public string BorderTexture
        {
            get => _borderTextureFile;
            set
            {
                _borderTextureFile = value;
                BlzFrameSetTexture(_borderTexture, value, 0, true);
            }
        }

        public string Model
        {
            get => _modelFile;
            set
            {
                _modelFile = value;
                BlzFrameSetTexture(_modelFrame, value, 0, true);
            }
        }

        public float Width => _width;

        public float Height => _height;

        public float UnscaledWidth
        {
            get => _unscaledWidth;
            set => SetSize(value, _unscaledHeight);
        }

        public float UnscaledHeight
        {
            get => _unscaledHeight;
            set => SetSize(_unscaledWidth, value);
        }

        public float ValueMin
        {
            get => _valueMin;
            set => SetMinMaxValue(value, _valueMax);
        }

        public float ValueMax
        {
            get => _valueMax;
            set => SetMinMaxValue(_valueMin, value);
        }

        public float LocalScale
        {
            get => _localScale;
            set
            {
                const float MinimumLocalScale = 0.0001f;
                _localScale = value < MinimumLocalScale ? MinimumLocalScale : value;
                _scale = _parent is null || !_inheritScale ? _localScale : _localScale * _parent._scale;
                _width = _unscaledWidth * _scale;
                _height = _unscaledHeight * _scale;
                SetSize(_unscaledWidth, _unscaledHeight);
                SetFont(_fontType, _fontSize, _fontFlags);
                Move();
                foreach (var node in _children)
                {
                    node.LocalScale = node._localScale;
                }
            }
        }

        public float Scale => _scale;

        public int Opacity
        {
            get => _opacity;
            set
            {
                _localOpacity = value;
                _opacity = _parent is null || !_inheritOpacity ? _localOpacity : (int)(_localOpacity * _parent._opacity / 255f);
                BlzFrameSetAlpha(_frame, _opacity);
                foreach (var node in _children)
                {
                    node.Opacity = node._localOpacity;
                }
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                BlzFrameSetLevel(_frame, TrueLevel);
                foreach (var node in _children)
                {
                    node.Level = node._level;
                }
            }
        }

        public int TrueLevel => (_parent is null || !_inheritLevel) ? _level : _level + _parent._level;

        public bool Visible
        {
            get => (_parent is null || !_inheritVisibility) ? _visibleSelf : _visibleSelf && _parent.Visible;
            set
            {
                _visibleSelf = value;
                BlzFrameSetVisible(_frame, Visible);
                foreach (var node in _children)
                {
                    node.Visible = node._visibleSelf;
                }
            }
        }

        public bool Enabled
        {
            get => (_parent is null || !_inheritEnableState) ? _enabledSelf : _enabledSelf && _parent.Enabled;
            set
            {
                _enabledSelf = value;
                BlzFrameSetEnable(_frame, Enabled);
                foreach (var node in _children)
                {
                    node.Enabled = node._enabledSelf;
                }
            }
        }

        public int VertexColor
        {
            get => throw new NotSupportedException();
            set => BlzFrameSetVertexColor(_modelFrame, value);
        }

        public float Value
        {
            get => BlzFrameGetValue(_frame);
            set => BlzFrameSetValue(_frame, value);
        }

        public float StepSize
        {
            get => _stepSize;
            set
            {
                const float MinimumStepSize = 0.0001f;
                _stepSize = value < MinimumStepSize ? MinimumStepSize : value;
                BlzFrameSetStepSize(_frame, _stepSize);
            }
        }

        public Frame Tooltips
        {
            get => _tooltips;
            set
            {
                _tooltips = value;
                BlzFrameSetTooltip(_frame, value._frame);
            }
        }

        public float Left => _left;

        public float Right => _left + _width;

        public float Bottom => _bottom;

        public float Top => _bottom + _height;

        public float ScreenPositionX => _screenPositionX;

        public float ScreenPositionY => _screenPositionY;

        // public float ScaledLeft => _scaledLeft;

        // public float ScaledBottom => _scaledBottom;

        // public float ScaledScreenPositionX => _scaledScreenPositionX;

        // public float ScaledScreenPositionY => _scaledScreenPositionY;

        public void SetAnchorPoint(float x, float y)
        {
            _anchorX = x;
            _anchorY = y;
            Move();
        }

        public void SetPivotPoint(float x, float y)
        {
            _pivotX = x;
            _pivotY = y;
            Move();
        }

        public void SetSize(float width, float height)
        {
            _unscaledWidth = width < 0 ? 0 : width;
            _unscaledHeight = height < 0 ? 0 : height;
            _width = _unscaledWidth * _scale;
            _height = _unscaledHeight * _scale;
            var scaling = Util.ScaleFactor * Util.Pixels2Dpi;
            BlzFrameSetSize(_frame, _width * scaling, _height * scaling);
            Move();
        }

        public void Move(float x, float y)
        {
            _localPositionX = x;
            _localPositionY = y;
            Move();
        }

        public void MoveEx(float x, float y)
        {
            if (_parent is null || !_inheritPosition)
            {
                Move(x, y);
            }
            else
            {
                Move(
                    (x - _parent._screenPositionX) / _parent._localScale,
                    (y - _parent._screenPositionY) / _parent._localScale);
            }
        }

        public void Relate(Frame relative, float x, float y)
        {
            x += relative._screenPositionX;
            y += relative._screenPositionY;
            if (_parent is null)
            {
                Move(x, y);
            }
            else
            {
                MoveEx(x, y);
            }
        }

        public void Click()
        {
            BlzFrameClick(_frame);
        }

        public void CageMouse(bool enable)
        {
            BlzFrameCageMouse(_frame, enable);
        }

        public void SetFocus(bool flag)
        {
            BlzFrameSetFocus(_frame, flag);
        }

        public void SetSpriteAnimate(int primaryProp, int flags)
        {
            BlzFrameSetSpriteAnimate(_frame, primaryProp, flags);
        }

        public void SetMinMaxValue(float min, float max)
        {
            _valueMin = min;
            _valueMax = max;
            BlzFrameSetMinMaxValue(_frame, min, max);
        }

        public void SetFont(string fontType, float fontSize, int flags)
        {
            _fontType = fontType;
            _fontSize = fontSize;
            _fontFlags = flags;
            if (_frameType == FrameType.SimpleText)
            {
                BlzFrameSetFont(_textFrame, _fontType, _fontSize * _scale, _fontFlags);
            }
        }

        public void SetTextAlignment(textaligntype vertical, textaligntype horizontal)
        {
            BlzFrameSetTextAlignment(_textFrame, vertical, horizontal);
        }

        public void Refresh()
        {
            Enabled = _enabledSelf;
            Opacity = _localOpacity;
            Level = _level;
            LocalScale = _localScale;
            foreach (var node in _children)
            {
                node.Refresh();
            }
        }

        private framehandle GetSubFrame(string name)
        {
            return BlzGetFrameByName(name, _context) ?? _frame;
        }

        private void Move()
        {
            if (_parent is null || !_inheritPosition)
            {
                var anchorOffsetX = Util.ResolutionWidth * _anchorX;
                var anchorOffsetY = Util.ResolutionHeight * _anchorY;
                if (!_isSimple)
                {
                    _screenPositionX = NormalizePositionX(_localPositionX + anchorOffsetX, false);
                    _scaledScreenPositionX = NormalizePositionX((_localPositionX * Util.ScaleFactor) + anchorOffsetX, true);
                }
                else
                {
                    _screenPositionX = _localPositionX + anchorOffsetX;
                    _scaledScreenPositionX = (_localPositionX * Util.ScaleFactor) + anchorOffsetX;
                }

                _screenPositionY = _localPositionY + anchorOffsetY;
                _scaledScreenPositionY = (_localPositionY * Util.ScaleFactor) + anchorOffsetY;
            }
            else
            {
                var scale = _scalePosition ? _parent._scale : 1f;
                var anchorOffsetX = _parent.Width * _anchorX;
                var anchorOffsetY = _parent.Height * _anchorY;
                _screenPositionX = _parent._left + anchorOffsetX + (_localPositionX * scale);
                _screenPositionY = _parent._bottom + anchorOffsetY + (_localPositionX * scale);
                _scaledScreenPositionX = _parent._scaledLeft + ((anchorOffsetX + (_localPositionX * scale)) * Util.ScaleFactor);
                _scaledScreenPositionY = _parent._scaledBottom + ((anchorOffsetY + (_localPositionY * scale)) * Util.ScaleFactor);
            }

            CalculateRect();
            var x = _scaledScreenPositionX - (_width * _pivotX * Util.ScaleFactor);
            var y = _scaledScreenPositionY - (_height * _pivotY * Util.ScaleFactor);
            if (!_isSimple)
            {
                x = NormalizePositionX(x, true);
            }

            BlzFrameSetAbsPoint(_frame, FRAMEPOINT_BOTTOMLEFT, Util.GetScreenPositionX(x), Util.GetScreenPositionY(y));
            foreach (var node in _children)
            {
                node.Move();
            }
        }

        private static bool IsSimple(FrameType frameType, bool isSimple)
        {
            var hasSimpleFlag = (frameType & FrameType.Simple) != 0;
            return hasSimpleFlag || (isSimple && frameType == FrameType.Bar);
        }

        private float NormalizePositionX(float x, bool scaled)
        {
            var scaleFactor = scaled ? Util.ScaleFactor : 1f;
            var min = Util.FrameBoundWidth + (_width * _pivotX * scaleFactor);
            var max = Util.ResolutionWidth - Util.FrameBoundWidth - (_width * (1f - _pivotX) * scaleFactor);

            // return x < min ? min : x > max ? max : x;
            x = x < min ? min : x;
            return x < max ? x : max;
        }

        private void CalculateRect()
        {
            var pivotOffsetX = _width * _pivotX;
            var pivotOffsetY = _height * _pivotY;

            _left = _screenPositionX - pivotOffsetX;
            _bottom = _screenPositionY - pivotOffsetY;
            _scaledLeft = _scaledScreenPositionX - (pivotOffsetX * Util.ScaleFactor);
            _scaledBottom = _scaledScreenPositionY - (pivotOffsetY * Util.ScaleFactor);
        }

        public IEnumerator<Frame> GetEnumerator()
        {
            return ((IEnumerable<Frame>)_children).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Frame>)_children).GetEnumerator();
        }

        public void Dispose()
        {
            foreach (var child in _children)
            {
                child.Dispose();
            }

            BlzDestroyFrame(_frame);
            StoreInteger(_gc, _name, I2S(_context), GetStoredInteger(_gc, _name, "0"));
            StoreInteger(_gc, _name, "0", _context);
            _frames.Remove(this);
        }
    }
}