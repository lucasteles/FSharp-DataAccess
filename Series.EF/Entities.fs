namespace Series.Data

open System.Collections.Generic
open System.ComponentModel.DataAnnotations.Schema

type EpisodeStatus =
  | Scheduled = 0
  | Aired = 1

type EpisodeId = int
type SerieId = int

type SerieStatus =
  | New = 0
  | Running = 1
  | Ended = 2

type [<CLIMutable>] Episode =
  { Id: EpisodeId
    Number: int
    Season : int
    Name: string
    Description: string
    Status: EpisodeStatus
    SerieId: SerieId
  }


type [<CLIMutable>] Serie =
  { Id: SerieId
    Name: string
    Description: string
    Episodes: List<Episode>
    Status: SerieStatus
  }