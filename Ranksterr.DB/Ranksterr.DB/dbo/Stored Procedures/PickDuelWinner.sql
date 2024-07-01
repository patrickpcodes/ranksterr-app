CREATE PROCEDURE [dbo].[PickDuelWinner]
    @DuelId UNIQUEIDENTIFIER,
    @WinnerId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Duels
    SET WinnerId = @WinnerId
    WHERE DuelId = @DuelId;
END;