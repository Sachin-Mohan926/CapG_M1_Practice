// using System;
// using System.Collections.Generic;
// using System.Linq;

// public class Team : IComparable<Team>
// {
//     public string Name { get; set; }
//     public int Points { get; set; }

//     public int CompareTo(Team other)
//     {
//         if (other == null) return -1;

//         int pointsCompare = other.Points.CompareTo(this.Points); // Descending
//         if (pointsCompare != 0) return pointsCompare;

//         return string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
//     }

//     public override bool Equals(object obj)
//     {
//         return obj is Team t && string.Equals(Name, t.Name, StringComparison.OrdinalIgnoreCase);
//     }

//     public override int GetHashCode()
//     {
//         return Name?.ToLower().GetHashCode() ?? 0;
//     }
// }

// public class Match
// {
//     public Team Team1 { get; }
//     public Team Team2 { get; }

//     public int Team1Score { get; set; }
//     public int Team2Score { get; set; }

//     public bool IsPlayed { get; set; }

//     public Match(Team team1, Team team2)
//     {
//         Team1 = team1;
//         Team2 = team2;
//     }

//     public Match Clone()
//     {
//         return new Match(Team1, Team2)
//         {
//             Team1Score = Team1Score,
//             Team2Score = Team2Score,
//             IsPlayed = IsPlayed
//         };
//     }
// }


// public class Tournament
// {
//     private SortedList<int, Team> _rankings = new SortedList<int, Team>();
//     private LinkedList<Match> _schedule = new LinkedList<Match>();
//     private Stack<Match> _undoStack = new Stack<Match>();
//     private HashSet<Team> _teams = new HashSet<Team>();

//     public void AddTeam(Team team)
//     {
//         _teams.Add(team);
//         RebuildRankings();
//     }

//     public void ScheduleMatch(Match match)
//     {
//         _schedule.AddLast(match);
//         _teams.Add(match.Team1);
//         _teams.Add(match.Team2);
//         RebuildRankings();
//     }

//     public void RecordMatchResult(Match match, int team1Score, int team2Score)
//     {
//         if (match.IsPlayed)
//             throw new InvalidOperationException("Match already recorded.");

//         _undoStack.Push(match.Clone());

//         match.Team1Score = team1Score;
//         match.Team2Score = team2Score;
//         match.IsPlayed = true;

//         if (team1Score > team2Score)
//             match.Team1.Points += 3;
//         else if (team2Score > team1Score)
//             match.Team2.Points += 3;
//         else
//         {
//             match.Team1.Points += 1;
//             match.Team2.Points += 1;
//         }

//         RebuildRankings();
//     }

//     public void UndoLastMatch()
//     {
//         if (_undoStack.Count == 0)
//             throw new InvalidOperationException("No match to undo.");

//         var last = _undoStack.Pop();

//         if (!last.IsPlayed) return;

//         if (last.Team1Score > last.Team2Score)
//             last.Team1.Points -= 3;
//         else if (last.Team2Score > last.Team1Score)
//             last.Team2.Points -= 3;
//         else
//         {
//             last.Team1.Points -= 1;
//             last.Team2.Points -= 1;
//         }

//         var originalMatch = _schedule.FirstOrDefault(m =>
//             m.Team1.Equals(last.Team1) && m.Team2.Equals(last.Team2) && m.IsPlayed);

//         if (originalMatch != null)
//             originalMatch.IsPlayed = false;

//         RebuildRankings();
//     }

//     public List<Team> GetRankings()
//     {
//         return _rankings.Values.ToList();
//     }

//     public int GetTeamRanking(Team team)
//     {
//         var ordered = GetRankings();
//         int index = ordered.BinarySearch(team);
//         return index >= 0 ? index + 1 : -1;
//     }

//     private void RebuildRankings()
//     {
//         _rankings.Clear();

//         var orderedTeams = _teams.OrderBy(t => t).ToList();

//         for (int i = 0; i < orderedTeams.Count; i++)
//         {
//             _rankings[i] = orderedTeams[i];  // SortedList auto-orders by key
//         }
//     }
// }


// class Program
// {
//     static void Main()
//     {
//         Tournament tournament = new Tournament();

//         Team teamA = new Team { Name = "Team Alpha", Points = 0 };
//         Team teamB = new Team { Name = "Team Beta", Points = 0 };
//         Team teamC = new Team { Name = "Team Gamma", Points = 0 };

//         tournament.AddTeam(teamA);
//         tournament.AddTeam(teamB);
//         tournament.AddTeam(teamC);

//         Match m1 = new Match(teamA, teamB);
//         Match m2 = new Match(teamB, teamC);

//         tournament.ScheduleMatch(m1);
//         tournament.ScheduleMatch(m2);

//         tournament.RecordMatchResult(m1, 3, 1); // Team Alpha wins
//         tournament.RecordMatchResult(m2, 2, 2); // Draw

//         Console.WriteLine("=== Rankings After Matches ===");
//         foreach (var team in tournament.GetRankings())
//             Console.WriteLine($"{team.Name} - {team.Points} pts");

//         Console.WriteLine($"\nRank of Team Alpha: {tournament.GetTeamRanking(teamA)}");

//         tournament.UndoLastMatch();

//         Console.WriteLine("\n=== Rankings After Undo ===");
//         foreach (var team in tournament.GetRankings())
//             Console.WriteLine($"{team.Name} - {team.Points} pts");
//     }
// }
