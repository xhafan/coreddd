--These values must match EmailState class - check it manually
INSERT INTO EmailState (Id, Name, CanSend) VALUES (1, 'Draft', 1)
INSERT INTO EmailState (Id, Name, CanSend) VALUES (2, 'ToBeSent', 0)
INSERT INTO EmailState (Id, Name, CanSend) VALUES (3, 'Sent', 0)