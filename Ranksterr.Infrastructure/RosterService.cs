using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Ranksterr.Infrastructure;

public class RosterService
{
    private readonly DbConnection connection;

    public RosterService(DbConnection connection)
    {
        this.connection = connection;
    }

    public Guid CreateRoster(Guid userId, string name, string description)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreateRoster";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@UserId", userId));
        command.Parameters.Add(new SqlParameter("@Name", name));
        command.Parameters.Add(new SqlParameter("@Description", description));

        var outputId = new SqlParameter("@RosterId", SqlDbType.UniqueIdentifier)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(outputId);

        command.ExecuteNonQuery();
        connection.Close();
        return (Guid)outputId.Value;
    }

    public void UpdateRoster(Guid rosterId, Guid userId, string name, string description)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UpdateRoster";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@RosterId", rosterId));
        command.Parameters.Add(new SqlParameter("@UserId", userId));
        command.Parameters.Add(new SqlParameter("@Name", name));
        command.Parameters.Add(new SqlParameter("@Description", description));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void CreateMovie(Guid movieId, string title, string director, int releaseYear, string imageUrl)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreateMovie";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@MovieId", movieId));
        command.Parameters.Add(new SqlParameter("@Title", title));
        command.Parameters.Add(new SqlParameter("@Director", director));
        command.Parameters.Add(new SqlParameter("@ReleaseYear", releaseYear));
        command.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdateMovie(Guid movieId, string title, string director, int releaseYear, string imageUrl)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UpdateMovie";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@MovieId", movieId));
        command.Parameters.Add(new SqlParameter("@Title", title));
        command.Parameters.Add(new SqlParameter("@Director", director));
        command.Parameters.Add(new SqlParameter("@ReleaseYear", releaseYear));
        command.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void CreateSong(Guid songId, string title, string artist, string album, int releaseYear, string imageUrl)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreateSong";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@SongId", songId));
        command.Parameters.Add(new SqlParameter("@Title", title));
        command.Parameters.Add(new SqlParameter("@Artist", artist));
        command.Parameters.Add(new SqlParameter("@Album", album));
        command.Parameters.Add(new SqlParameter("@ReleaseYear", releaseYear));
        command.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdateSong(Guid songId, string title, string artist, string album, int releaseYear, string imageUrl)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UpdateSong";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@SongId", songId));
        command.Parameters.Add(new SqlParameter("@Title", title));
        command.Parameters.Add(new SqlParameter("@Artist", artist));
        command.Parameters.Add(new SqlParameter("@Album", album));
        command.Parameters.Add(new SqlParameter("@ReleaseYear", releaseYear));
        command.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void CreatePainting(Guid paintingId, string title, string artist, int yearCreated, string imageUrl)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreatePainting";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@PaintingId", paintingId));
        command.Parameters.Add(new SqlParameter("@Title", title));
        command.Parameters.Add(new SqlParameter("@Artist", artist));
        command.Parameters.Add(new SqlParameter("@YearCreated", yearCreated));
        command.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdatePainting(Guid paintingId, string title, string artist, int yearCreated, string imageUrl)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UpdatePainting";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@PaintingId", paintingId));
        command.Parameters.Add(new SqlParameter("@Title", title));
        command.Parameters.Add(new SqlParameter("@Artist", artist));
        command.Parameters.Add(new SqlParameter("@YearCreated", yearCreated));
        command.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void CreateItemToRoster(Guid rosterId, Guid itemId)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreateItemToRoster";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@RosterId", rosterId));
        command.Parameters.Add(new SqlParameter("@ItemId", itemId));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdateItemToRoster(Guid rosterItemId, Guid rosterId, Guid itemId)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UpdateItemToRoster";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@RosterItemId", rosterItemId));
        command.Parameters.Add(new SqlParameter("@RosterId", rosterId));
        command.Parameters.Add(new SqlParameter("@ItemId", itemId));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public Guid CreateGauntlet()
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreateGauntlet";
        command.CommandType = CommandType.StoredProcedure;

        var outputId = new SqlParameter("@GauntletId", SqlDbType.UniqueIdentifier)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(outputId);

        command.ExecuteNonQuery();
        connection.Close();
        return (Guid)outputId.Value;
    }

    public void CreateItemToGauntlet(Guid gauntletId, Guid itemId)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreateItemToGauntlet";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@GauntletId", gauntletId));
        command.Parameters.Add(new SqlParameter("@ItemId", itemId));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdateItemToGauntlet(Guid gauntletItemId, Guid gauntletId, Guid itemId)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UpdateItemToGauntlet";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@GauntletItemId", gauntletItemId));
        command.Parameters.Add(new SqlParameter("@GauntletId", gauntletId));
        command.Parameters.Add(new SqlParameter("@ItemId", itemId));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public Guid CreateDuel(Guid gauntletId, Guid itemId1, Guid itemId2)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CreateDuel";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@GauntletId", gauntletId));
        command.Parameters.Add(new SqlParameter("@ItemId1", itemId1));
        command.Parameters.Add(new SqlParameter("@ItemId2", itemId2));

        var outputId = new SqlParameter("@DuelId", SqlDbType.UniqueIdentifier)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(outputId);

        command.ExecuteNonQuery();
        connection.Close();
        return (Guid)outputId.Value;
    }

    public void UpdateDuel(Guid duelId, Guid gauntletId, Guid itemId1, Guid itemId2, Guid winnerId)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UpdateDuel";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@DuelId", duelId));
        command.Parameters.Add(new SqlParameter("@GauntletId", gauntletId));
        command.Parameters.Add(new SqlParameter("@ItemId1", itemId1));
        command.Parameters.Add(new SqlParameter("@ItemId2", itemId2));
        command.Parameters.Add(new SqlParameter("@WinnerId", winnerId));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void PickDuelWinner(Guid duelId, Guid winnerId)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "PickDuelWinner";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@DuelId", duelId));
        command.Parameters.Add(new SqlParameter("@WinnerId", winnerId));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void GetGauntletStats(Guid gauntletId)
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "GetGauntletStats";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@GauntletId", gauntletId));

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Duel: {reader.GetGuid(0)}, Item1: {reader.GetString(1)}, Item2: {reader.GetString(2)}, Winner: {reader.GetString(3)}");
            }
        }
        connection.Close();
    }
}