namespace FrameworkTest.TestService
{
    public interface ITestService
    {
        string SayHello(dynamic words);

        ReturnModel SayHelloCaching(int id, dynamic words);
    }
}