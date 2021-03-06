CREATE TABLE [dbo].[TB_COLLABORATOR_PHONE] (
    [CLP_ID]          INT           IDENTITY (1, 1) NOT NULL,
    [CLP_CLB_ID]      INT           NULL,
    [CLP_DESCRIPTION] VARCHAR (255) NOT NULL,
    [CLP_NUMBER]      VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_TB_COLLABORATOR_PHONE] PRIMARY KEY CLUSTERED ([CLP_ID] ASC),
    CONSTRAINT [FK_TB_COLLABORATOR_IN_TB_COLLABORATOR_PHONE] FOREIGN KEY ([CLP_CLB_ID]) REFERENCES [dbo].[TB_COLLABORATOR] ([CLB_ID])
);

