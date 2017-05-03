
namespace InterviewTest.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {    
            context.Database.EnsureCreated();
        }
    }
}
