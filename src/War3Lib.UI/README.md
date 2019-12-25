## War3Lib.UI
### Based on [UIUtils](https://www.hiveworkshop.com/threads/ui-utils-v1-05.320005/) by Quilnez.

### How to install
- Import UIUtils.toc and UIUtils.fdf files. You can find them in the demo map linked above.
- Add a reference to [War3Lib.UI.Sources](https://www.nuget.org/packages/War3Lib.UI.Sources) package.

### Demo
```csharp
public static void Demo()
{
    // 1 - Hello World
    var textTemplate = new Frame(true, FrameType.SimpleText, null, -200f, -50f, 0);
    textTemplate.Name = "Template Text";
    textTemplate.Text = "|cff00ff00Hello world!|r";
    textTemplate.AnyEvent += HandleFrameEvent;
    textTemplate.SetAnchorPoint(1f, 1f);
    textTemplate.SetPivotPoint(1f, 0.5f);
    textTemplate.SetTextAlignment(TEXT_JUSTIFY_RIGHT, TEXT_JUSTIFY_TOP);
    textTemplate.Refresh();

    // 2 - Inventory
    var inventoryFrame = new Frame(false, FrameType.Texture, null, 0f, 0f, 0);
    inventoryFrame.Name = "Inventory Frame";
    inventoryFrame.Texture = @"war3mapImported\Inventory.tga";
    inventoryFrame.AnyEvent += HandleFrameEvent;
    inventoryFrame.SetAnchorPoint(1f, 0.5f);
    inventoryFrame.SetPivotPoint(1f, 0.5f);
    inventoryFrame.SetSize(293f, 438f);
    inventoryFrame.Refresh();

    // 3 - Character Bar
    var simpleCharacterBar = new Frame(true, FrameType.SimpleTexture, null, 0f, 0f, 0);
    simpleCharacterBar.Name = "Character Bar";
    simpleCharacterBar.Texture = @"war3mapImported\HP.tga";
    simpleCharacterBar.AnyEvent += HandleFrameEvent;
    simpleCharacterBar.SetAnchorPoint(0f, 1f);
    simpleCharacterBar.SetPivotPoint(0f, 1f);
    simpleCharacterBar.SetSize(400f, 127f);
    simpleCharacterBar.Refresh();

    // 4 - Footman Icon
    var footmanIcon = new Frame(false, FrameType.Button, null, 0f, 100f, 0);
    footmanIcon.Name = "Footman Icon";
    footmanIcon.Texture = @"ReplaceableTextures\CommandButtons\BTNFootman.blp";
    footmanIcon.AnyEvent += HandleFrameEvent;
    footmanIcon.SetAnchorPoint(0f, 0f);
    footmanIcon.SetPivotPoint(0f, 0f);
    footmanIcon.Refresh();

    // 5 - Hello Button
    var testSimpleButton = new Frame(true, FrameType.SimpleButton, null, 5f, 5f, 0);
    testSimpleButton.Name = "Template Simple Button";
    testSimpleButton.Text = "Hello Reforged!";
    testSimpleButton.AnyEvent += HandleFrameEvent;
    testSimpleButton.SetAnchorPoint(0f, 0f);
    testSimpleButton.SetPivotPoint(0f, 0f);
    testSimpleButton.SetSize(120f, 40f);
    testSimpleButton.Refresh();

    // 6 - HP Bar
    var healthbarFrame = new Frame(true, FrameType.Bar, footmanIcon, 0f, -5f, 0);
    healthbarFrame.Name = "Health Bar";
    healthbarFrame.Texture = $@"Replaceabletextures\Teamcolor\Teamcolor0{GetPlayerId(GetLocalPlayer())}.blp";
    healthbarFrame.AnyEvent += HandleFrameEvent;
    healthbarFrame.Value = healthbarFrame.ValueMax;
    healthbarFrame.SetAnchorPoint(0.5f, 0f);
    healthbarFrame.SetPivotPoint(0.5f, 1f);
    healthbarFrame.SetSize(footmanIcon.Width, healthbarFrame.Height);
    healthbarFrame.Refresh();

    // 7 - Horizontal Slider
    var testSliderH = new Frame(false, FrameType.SliderH, null, 0f, 100f, 0);
    testSliderH.Name = "Radar H Slider";
    testSliderH.AnyEvent += HandleFrameEvent;
    testSliderH.SetAnchorPoint(0.5f, 0f);
    testSliderH.SetPivotPoint(0.5f, 0.5f);
    testSliderH.Refresh();

    // 8 - Vertical Slider
    var testSliderV = new Frame(false, FrameType.SliderV, null, testSliderH.Width / 2 + 5f, 105f, 0);
    testSliderV.Name = "Radar V Slider";
    testSliderV.AnyEvent += HandleFrameEvent;
    testSliderV.SetAnchorPoint(0.5f, 0f);
    testSliderV.SetPivotPoint(0.5f, 0f);
    testSliderV.Refresh();

    const int SlotCount = 20;
    var slots = new bool[SlotCount];
    for (var i = 1; i <= 10; i++)
    {
        var slot = GetRandomInt(0, 20 - i);
        for (var slotIndex = 0; slotIndex < SlotCount; slotIndex++)
        {
            if (slots[slotIndex])
            {
                continue;
            }

            if (slot == 0)
            {
                var x = slotIndex % 4;
                var y = slotIndex / 4;
                var itemFrame = new Frame(false, FrameType.Button, inventoryFrame, 50f + 60f * x, -100f - 60f * y, 0);
                itemFrame.Name = $"Item #{slotIndex}";
                itemFrame.AnyEvent += HandleFrameEvent;
                itemFrame.Texture = $@"war3mapImported\item{GetRandomInt(1, 4)}.blp";
                itemFrame.HighlightTexture = @"war3mapImported\Selection.blp";
                itemFrame.SetAnchorPoint(0f, 1f);
                itemFrame.SetPivotPoint(0.5f, 0.5f);
                itemFrame.SetSize(50f, 50f);
                itemFrame.Refresh();

                slots[slotIndex] = true;
                break;
            }
            else
            {
                slot--;
            }
        }
    }
}

private static void HandleFrameEvent(object sender, EventArgs e)
{
    var name = ((Frame)sender).Name;
    var eventType = BlzGetTriggerFrameEvent();

    if (eventType == FRAMEEVENT_CONTROL_CLICK)
    {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Click |cffffcc00[{name}]|r");
    }
    else if (eventType == FRAMEEVENT_MOUSE_ENTER)
    {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Enter |cffffcc00[{name}]|r");
    }
    else if (eventType == FRAMEEVENT_MOUSE_LEAVE)
    {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Leave |cffffcc00[{name}]|r");
    }
    else if (eventType == FRAMEEVENT_MOUSE_UP)
    {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Mouse up |cffffcc00[{name}]|r");
    }
    else if (eventType == FRAMEEVENT_MOUSE_DOWN)
    {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Mouse down |cffffcc00[{name}]|r");
    }
    else if (eventType == FRAMEEVENT_MOUSE_WHEEL)
    {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Mouse wheel |cffffcc00[{name}]|r");
    }
}
```