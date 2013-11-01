namespace Commands
open UnityEngine


type CommandTarget = Position of Vector3 |  Target of GameObject
type Command = {
    target : CommandTarget

}
type AttackCommand = {
    target : Choice<Vector3, GameObject>
    source : GameObject
}
type MoveCommand = {
    target : Vector3
    source : GameObject
}
type HarvestCommand = {
    target : GameObject
    source : GameObject
    drop_location : GameObject
}

type CommandType = Attack of AttackCommand
                  |Move of MoveCommand
                  |Harvest of HarvestCommand



 
type Class1() = 
    member this.X = "F#"
