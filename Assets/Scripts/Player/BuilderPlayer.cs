

public class BuilderPlayer 
{
    public Players Builder(IPlayerBuilder builder)
    {
        builder.DefineModel();
        builder.DefineEars();
        builder.DefineColors();
        builder.DefineName();

        return builder.GetPlayer();
    } 
}
