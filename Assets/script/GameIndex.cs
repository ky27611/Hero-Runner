using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIndex
{

    public GameIndex()
    {

    }

    public virtual void Initialize()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void Release()
    {

    }

}

public class PlayerSelect : GameIndex
{
    public override void Initialize()
    {
        
    }

    public override void OnUpdate()
    {
    
    }

    public override void Release()
    {
    
    }

    public PlayerSelect() : base()
    {
    }
}

public class GameStart : GameIndex
{
    public override void Initialize()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void Release()
    {

    }

    public GameStart() : base()
    {
    }
}

public class NormalMode : GameIndex
{
    public override void Initialize()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void Release()
    {

    }

    public NormalMode() : base()
    {
    }
}

public class BossMode : GameIndex
{
    public override void Initialize()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void Release()
    {

    }

    public BossMode() : base()
    {
    }
}

public class StageClear : GameIndex
{
    public override void Initialize()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void Release()
    {

    }

    public StageClear() : base()
    {
    }
}

public class GameOver : GameIndex
{
    public override void Initialize()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void Release()
    {

    }

    public GameOver() : base()
    {
    }
}

public class PoseMenu : GameIndex
{
    public override void Initialize()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void Release()
    {

    }

    public PoseMenu() : base()
    {
    }
}