CREATE TABLE [dbo].[TB_COLLABORATOR] (
    [CLB_ID]           INT           IDENTITY (1, 1) NOT NULL,
    [CLB_NAME]         VARCHAR (255) NOT NULL,
    [CLB_MAIL]         VARCHAR (255) NOT NULL,
    [CLB_PLATE_NUMBER] VARCHAR (255) NOT NULL,
    [CLB_PASSWORD]     VARCHAR (255) NOT NULL,
    [CLB_LEADER_ID]    INT           NULL,
    CONSTRAINT [PK_TB_COLLABORATOR] PRIMARY KEY CLUSTERED ([CLB_ID] ASC)
);



