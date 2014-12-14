CREATE TYPE [dbo].[ModuleCoverageData] AS TABLE
(
    ModuleName              VARCHAR(100)     NOT NULL,
    BlocksCoveredPercent    DECIMAL(9, 3)    NOT NULL,
    LinesCoveredPercent     DECIMAL(9, 3)    NOT NULL
)
