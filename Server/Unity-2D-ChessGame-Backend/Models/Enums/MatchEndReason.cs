namespace Unity_2D_ChessGame_Backend.Models.Enums
{
    public enum MatchEndReason
    {
        Checkmate,
        Stalemate,
        Resignation,
        Timeout,
        DrawByAgreement,
        DrawByRepetition,
        DrawByInsufficientMaterial,
        DrawBy50MoveRule
    }
}
