using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState
{
    public SkillState()
    {
        
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void Release()
    {

    }

    public virtual void OnUpdate()
    {

    }
}

public class Knight : SkillState
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

    public Knight() : base()
    {
    }
}

public class Astronaut : SkillState
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

    public Astronaut() : base()
    {
    }
}

public class Doctor : SkillState
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

    public Doctor() : base()
    {
    }
}

public class Farmer : SkillState
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

    public Farmer() : base()
    {
    }
}


public class Zombie : SkillState
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

    public Zombie() : base()
    {
    }
}
