namespace War3Lib.UI
{
    public enum FrameType
    {
        Custom = default,

        Text,
        Texture,
        Button,
        Bar,
        SliderH,
        SliderV,

        Simple = 0x10,

        SimpleText = Simple | Text,
        SimpleTexture = Simple | Texture,
        SimpleButton = Simple | Button,
    }
}