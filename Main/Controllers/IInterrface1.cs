namespace Main.Controllers
{
    public interface IInterrface1
    {
        int Get();
    }
    public class IClass1:IInterrface1
    {
        public int Get()
        {
            return 1;
        }
    }
}