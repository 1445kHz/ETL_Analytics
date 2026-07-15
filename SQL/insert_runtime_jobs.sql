-- Populate AutoSysRuntimeJobs with consistent runtimes (10 to 45 minutes) and minor random variation
DECLARE @Jobs TABLE (
    JobName NVARCHAR(255) NOT NULL,
    BaseRunTime DECIMAL(8,2) NOT NULL
);

INSERT INTO @Jobs (JobName, BaseRunTime) VALUES
('7045kHz_AdvWorks_Sales_Processing_0', 15.50),
('7045kHz_AdvWorks_Sales_Processing_15', 18.20),
('7045kHz_AdvWorks_Sales_Processing_30', 22.00),
('7045kHz_AdvWorks_Sales_Processing_45', 24.80),
('7045kHz_AdvWorks_Currency_Rate_Updates_35', 12.00),
('7045kHz_AdvWorks_WorkOrder_Routing', 38.40),
('7045kHz_AdvWorks_Sales_Marketing_Report', 42.10);

-- Insert 5 runs for each job over the last 5 days with a small random variance
INSERT INTO [dbo].[AutoSysRuntimeJobs] (JobName, JobStatus, RunTimeInMinutes, InsertedAt)
SELECT 
    j.JobName,
    'SUCCESS' AS JobStatus,
    -- Base runtime + random variance between -1.50 and +1.50 minutes
    j.BaseRunTime + CAST(((ABS(CHECKSUM(NEWID())) % 300) - 150) / 100.0 AS DECIMAL(8,2)) AS RunTimeInMinutes,
    -- Runs scheduled once per day over the last 5 days
    DATEADD(day, -d.DayOffset, GETUTCDATE()) AS InsertedAt
FROM @Jobs j
CROSS JOIN (
    VALUES (0), (1), (2), (3), (4)
) d(DayOffset);
