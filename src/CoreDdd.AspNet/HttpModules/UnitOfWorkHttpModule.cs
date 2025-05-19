﻿using System;
using System.Data;
using System.Web;
using CoreDdd.Domain.Events;
using CoreDdd.UnitOfWorks;

namespace CoreDdd.AspNet.HttpModules;

/// <summary>
/// Wraps a web request inside a unit of work transaction.
/// </summary>
public class UnitOfWorkHttpModule : IHttpModule
{
    private const string UnitOfWorkSessionKey = "CoreDdd_UnitOfWorkHttpModule_UnitOfWork";

    private static IUnitOfWorkFactory? _unitOfWorkFactory;
    private static IsolationLevel _isolationLevel;

    /// <summary>
    /// Initializes the instance.
    /// </summary>
    /// <param name="application">An HTTP application instance</param>
    public void Init(HttpApplication application)
    {
        application.BeginRequest += Application_BeginRequest;
        application.EndRequest += Application_EndRequest;
        application.Error += Application_Error;
    }

    /// <summary>
    /// Initializes the class. Needs to be called when starting the web application.
    /// </summary>
    /// <param name="unitOfWorkFactory">A unit of work factory to create a new unit of work for each web request</param>
    /// <param name="isolationLevel">An isolation level for the unit of work transaction</param>
    public static void Initialize(
        IUnitOfWorkFactory unitOfWorkFactory,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
    )
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _isolationLevel = isolationLevel;
    }

    private void Application_BeginRequest(object source, EventArgs e)
    {
        if (_unitOfWorkFactory == null)
        {
            throw new Exception("UnitOfWorkHttpModule was not initialized. Call UnitOfWorkHttpModule.Initialize(..) first.");
        }

        UnitOfWork = _unitOfWorkFactory.Create();
        UnitOfWork.BeginTransaction(_isolationLevel);

        DomainEvents.ResetDelayedEventsStorage();
    }

    private void Application_EndRequest(object source, EventArgs e)
    {
        if (HttpContext.Current.Server.GetLastError() != null) return;

        if (_unitOfWorkFactory == null)
        {
            throw new Exception("UnitOfWorkHttpModule was not initialized. Call UnitOfWorkHttpModule.Initialize(..) first.");
        }

        if (UnitOfWork == null)
        {
            throw new Exception("UnitOfWork is null.");
        }
            
        try
        {
            UnitOfWork.Commit();
        }
        catch
        {
            UnitOfWork.Rollback();
            throw;
        }
        finally
        {
            _unitOfWorkFactory.Release(UnitOfWork);
            UnitOfWork = null;
        }

        DomainEvents.RaiseDelayedEvents();
    }

    private void Application_Error(object source, EventArgs e)
    {
        if (UnitOfWork == null) return;
        
        if (_unitOfWorkFactory == null)
        {
            throw new Exception("UnitOfWorkHttpModule was not initialized. Call UnitOfWorkHttpModule.Initialize(..) first.");
        }

        try
        {
            UnitOfWork.Rollback();
        }
        finally
        {
            _unitOfWorkFactory.Release(UnitOfWork);
            UnitOfWork = null;
        }
    }

    private IUnitOfWork? UnitOfWork
    {
        get => (IUnitOfWork?)HttpContext.Current.Items[UnitOfWorkSessionKey];
        set => HttpContext.Current.Items[UnitOfWorkSessionKey] = value;
    }

    /// <summary>
    /// Cleans up the resources.
    /// </summary>
    public void Dispose()
    {
    }
}