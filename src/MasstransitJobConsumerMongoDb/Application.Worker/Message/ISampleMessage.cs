namespace Application.Worker.Message
{
    public interface ISampleMessage : IBaseMessage
    {
        int[] ids { get; set; }
    }

    public interface IBaseMessage
    {
        bool IsValid { get; set; }
    }
}