namespace Series.Data

type EpisodeStatus =
  | Scheduled = 0
  | Aired = 1

type SerieStatus =
  | New = 0
  | Running = 1
  | Ended = 2

type SerieId = int
type EpisodeId = int


[<CLIMutable>]
type Episode =
  { Id: EpisodeId
    Number: int
    Season : int
    Name: string
    Description: string
    Status: EpisodeStatus 
    SerieId: SerieId }

[<CLIMutable>]
type Serie =
  { Id: SerieId
    Name: string
    Description: string
    Episodes: Episode list
    Status: SerieStatus }