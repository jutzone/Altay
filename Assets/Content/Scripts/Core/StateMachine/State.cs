using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Active,
    Waiting
}

public abstract class State
{

    //! Из-за того что это монобихейвер он вызовет пустой конструктор
    //* Я уже напсал что лучше-всего избавиться от монобеха, но если сделать Стейт машину статичной, то конструктор вообще не нужен
    //* Стейты тоже должны быть статичными, так как один стейт на все приложение
    public delegate void VoidDelegate();
    public static VoidDelegate OnEnter, OnContentLoading, OnLayoutUpdate, OnContentRelease, OnExit;
    public virtual void Enter()
    {
        OnEnter?.Invoke();
    }

    public virtual void ContentLoading()
    {
        OnContentLoading?.Invoke();
    }

    public virtual void LayoutUpdate()
    {
        OnLayoutUpdate?.Invoke();
    }

    public virtual void ContentRelease()
    {
        OnContentRelease?.Invoke();
    }

    public virtual void Exit()
    {
        OnExit?.Invoke();
    }
}
