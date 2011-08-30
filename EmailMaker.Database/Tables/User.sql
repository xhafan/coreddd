
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplate_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])

ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK_EmailTemplate_User]
