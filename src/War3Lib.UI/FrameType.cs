namespace War3Lib.UI
{
    public enum FrameType
    {
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