using System;


public class InstructionsDialog : DialogBase
{
    private Action _play;

    public override MenusEnum MenuType()
    {
        return MenusEnum.StageInstructions;
    }
}
