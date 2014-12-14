CREATE VIEW [dbo].[TopIssuesReport]
AS
    SELECT TOP 10
            IssueCode,
            COUNT(*) AS Detections,
            MAX(Message) AS SampleMessage,
            MAX(ToolCode) AS ToolCode,
            MAX(Severity) AS Severity
    FROM    dbo.Issues
    WHERE ToolCode > 0
    GROUP BY IssueCode
    ORDER BY COUNT(*) DESC
