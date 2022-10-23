////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: Boundary.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/02/2022 06:00 AM
//Last Modified On : 10/02/2022 5:20 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => Add comments
//Description : A struct for determine the boundary either for screen bound or other bounds
//              Used with Player Patrol Behaviour, Spawn Enemy and Scrolling Background
//              The code is from in class lab.
////////////////////////////////////////////////////////////////////////////////////////////////////////

[System.Serializable]
public struct Boundary
{
    public float min;
    public float max;
}
