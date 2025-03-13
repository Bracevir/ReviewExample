SELECT CreatedAt AT TIME ZONE 'UTC' AT TIME ZONE 'Russian Standard Time' AS LocalTime
FROM dbo.Reviews

SELECT UpdatedAt AT TIME ZONE 'UTC' AT TIME ZONE 'Russian Standard Time' AS LocalTime
FROM dbo.Reviews
