USE [TRAIN]
GO

/****** Object:  Table [dbo].[AutoSysRuntimeJobs]    Script Date: 7/12/2026 9:09:57 AM ******/
SET ANSI_NULLS ON
GO

  -- 7045kHz_AdvWorks_Sales_Processing_0 (Target: ~15m)
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_0', 'SUCCESS', 15.30, DATEADD(day, -1, GETUTCDATE()));
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_0', 'SUCCESS', 15.70, GETUTCDATE());

    -- 7045kHz_AdvWorks_Sales_Processing_15 (Target: ~18m)
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_15', 'SUCCESS', 18.05, DATEADD(day, -1, GETUTCDATE()));
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_15', 'SUCCESS', 18.40, GETUTCDATE());

    -- 7045kHz_AdvWorks_Sales_Processing_30 (Target: ~22m)
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_30', 'SUCCESS', 21.90, DATEADD(day, -1, GETUTCDATE()));
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_30', 'SUCCESS', 22.35, GETUTCDATE());

    -- 7045kHz_AdvWorks_Sales_Processing_45 (Target: ~25m)
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_45', 'SUCCESS', 24.60, DATEADD(day, -1, GETUTCDATE()));
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Processing_45', 'SUCCESS', 25.10, GETUTCDATE());

    -- 7045kHz_AdvWorks_Currency_Rate_Updates_35 (Target: ~12m)
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Currency_Rate_Updates_35', 'SUCCESS', 11.85, DATEADD(day, -1, GETUTCDATE()));
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Currency_Rate_Updates_35', 'SUCCESS', 12.20, GETUTCDATE());

    -- 7045kHz_AdvWorks_WorkOrder_Routing (Target: ~38m)
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_WorkOrder_Routing', 'SUCCESS', 38.10, DATEADD(day, -1, GETUTCDATE()));
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_WorkOrder_Routing', 'SUCCESS', 38.65, GETUTCDATE());

    -- 7045kHz_AdvWorks_Sales_Marketing_Report (Target: ~42m)
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Marketing_Report', 'SUCCESS', 41.80, DATEADD(day, -1, GETUTCDATE()));
    INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
    VALUES ('7045kHz_AdvWorks_Sales_Marketing_Report', 'SUCCESS', 42.40, GETUTCDATE());